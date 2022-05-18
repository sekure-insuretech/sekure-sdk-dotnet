# sekure-sdk-dotnet
Sekure-sdk-dotnet es una biblioteca que facilita la implementación del desarrollo de los productos de [Sekure](https://www.sekure.com.co/) en proyectos dentro del marco del ecosistema de .net 

Actualmente tiene más de 3.000 descargas y constantemente se actualiza.
![image](https://user-images.githubusercontent.com/49702007/169101083-381a028d-4fd0-4d8d-b13c-d166dff28e32.png)

# Instalar el nuget
Si estas en visual studio puedes hacerlo a traves de la consola del **Package Manager** o el **Manage NuGet Packages for soluction**, en este caso vamos a ver como se instala con la consola del **Package Manager**

### Package Manager
La herramienta de package manager la encuentra en la barra de menú - Tools - NuGet packate manager - packate manager console.
Selecciona el proyecto donde desea instalar el sdk de sekure y escribes la siguiente linea de código
`Install-Package Sekure -Version {Número_de_versión}` donde **Número_de_versión** es el número de la versión que desea instalar
 
### Ejemplo
![image](https://user-images.githubusercontent.com/49702007/169109738-acde3fbb-89e9-412c-a127-675c352c8cf7.png)

# Configuración del NuGet
Una vez instalado en el archivo startup.cs debes de registrar el servicio del SDK.

````
  services.AddSingleton<IInsuranceOS, InsuranceOS>( _ => 
  new InsuranceOS("{UrlInsuranceOS}", "{KeySubscriptionInsuranceOS}", new System.Net.Http.HttpClient()));
````
### UrlInsuranceOS: 
Es la url de los siguientes ambientes
>  - stage:           https://api.sekure.com.co/osstage/
>  - Pre-producción:  https://api.sekure.com.co/ospreprod/
>  - Producción:      https://api.sekure.com.co/os/
  
### KeySubscriptionInsuranceOS: 
Es la suscripción que debe de tener el cliente para utilizar el sdk de sekure, sin una suscripción no es posible utilizar los servicios de sekure

# Consumir del NuGet
En el archivo .cs que vayas a consumirlo debes de crearle su constructo y realizar la inyeccion de dependencias
````
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IInsuranceOS _sekure;

        public ProductController(IInsuranceOS sekure)
        {
            _sekure = sekure;
        }
    }
````
### Descubrimiento del producto
Con el descubrimiento del producto se identifica cuales son los input parameter que se requiere para cotizar, confirmar y emitir
````
    public async Task<IActionResult> Discovery(int productId)
    {
        Product productDiscovery = await _sekure.GetProductById(productId);
        return Ok(productDiscovery);
    }
````

- productId: Es el id del producto a cotizar
 
 ### Cotización
Para la cotización se realiza una instancia del modelo del SDK ExecutableProduct y se empieza a llenar los input parameter de acuerdo al descubrimiento

````
    public async Task<IActionResult> Discovery(Product productDiscovery)
    {
        ExecutableProduct executableProduct = new ExecutableProduct()
        {
            ProductDetail = productDiscovery.ProductDetail,
            Parameters = productDiscovery.Quote
        };

        QuotedProduct quote = await _sekure.Quote(executableProduct);
        return Ok(quote);
    }
````

### Confirmar
Para confirmar se realiza nuevamente la instancia del modelo del SDK ExecutableProduct y se empieza a llenar los input parameter de acuerdo al descubrimiento, también necesitará la sessiónId de la cotización seleccionada.

````
    public async Task<IActionResult> Confirm(Product productDiscovery, Guid sessionId)
    {
        ExecutableProduct executableProduct = new ExecutableProduct()
        {
            ProductDetail = productDiscovery.ProductDetail,
            Parameters = productDiscovery.Confirm
        };

        Policy quote = await _sekure.Confirm(executableProduct, sessionId);
        return Ok(quote);
    }
````
- sessionId: Es el id del producto seleccionado, este sessionId lo obtiene en el response de cotizar
  
### Emitir
Para la emisión es similar a los dos métodos anteriores, se debe de pasar el executableProduct y sessionId de la cotización seleccionada

````
    public async Task<IActionResult> ToEmit(Product productDiscovery, Guid sessionId)
    {
        ExecutableProduct executableProduct = new ExecutableProduct()
        {
            ProductDetail = productDiscovery.ProductDetail,
            Parameters = productDiscovery.Confirm
        };

        string emit = await _sekure.Emit(executableProduct, sessionId);
        return Ok(emit);
    }
````
- sessionId: Es el id del producto seleccionado, este sessionId lo obtiene en el response de cotizar

#### **NOTA**: 
>Los input parameter de deben de llenar en el campo de InputParameterValue cuando no son de tipo object. Cuando son de tipo object se debe de llenar los ParameterSchema en su propiedad PropertyValue 
>
>Este SDK cuenta con más servicios como Pay y AskSekure que tienen el mismo comportamiento que deben de utilizarsen de acuerdo a la configuración del producto
