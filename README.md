# sekure-sdk-dotnet
Sekure-sdk-dotnet es una biblioteca que facilita la implementación del desarrollo de los productos de [Sekure](https://www.sekure.com.co/) en proyectos dentro del marco del ecosistema de .net 

Actualmente tiene más de 3.000 descargas y constantemente se actualiza.
![image](https://user-images.githubusercontent.com/49702007/169101083-381a028d-4fd0-4d8d-b13c-d166dff28e32.png)

# Instalar el nuget
Si estas en visual studio puedes hacerlo a traves de la consola del **Package Manager** o el **Manage NuGet Packages for soluction**, en este caso vamos a ver como se instala con la consola del **Package Manager**

### Package Manager
La herramienta de package manager la encuentra en la barra de menú - Tools - NuGet package manager - Package manager console.
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
>  - Pre-producción:  https://api.sekure.com.co/ospreprod/
>  - Producción:      https://api.sekure.com.co/os/
  
### KeySubscriptionInsuranceOS: 
Es la suscripción que debe de tener el cliente para utilizar el sdk de sekure, sin una suscripción no es posible utilizar los servicios de sekure

# Consumir del NuGet
En el archivo .cs que vayas a consumir los servicios del SDK debes de crear el constructor y realizar la inyeccion de dependencias
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
El descubrimiento es la configuración del producto y se identifica cuales son los input parameter que se requiere para cotizar, confirmar y emitir
````
    public async Task<IActionResult> Discovery(int productId)
    {
        Product productDiscovery = await _sekure.GetProductById(productId);
        return Ok(productDiscovery);
    }
````

- productId: Es el id del producto a cotizar

La variable productDiscovery que es tipo **Product** recibe un estructura similar al .json que se muestra a continuación
````
{
    "productDetail": {
        "productId": "{productId}",
        "productName": "{productName}",
        "policyTypeName": "{policyTypeName}",
        "insuranceCompanyName": "{insuranceCompanyName}"
    },
    "policyHolder": [
        {
            "firstName": "{firstName}",
            "secondName": "{secondName}",
            "lastName": "{lastName}",
            "secondLastName": "{secondLastName}",
            "birthdate": "{birthdate}",
            "gender": "{gender}",
            "address": "{address}",
            "identificationType": "{identificationType}",
            "identificationNumber": "{identificationNumber}",
            "maritalStatus": "{maritalStatus}",
            "email": "{email}",
            "phoneNumber": "{phoneNumber}"
        },
        {
            "firstName": "{firstName}",
            "address": "{address}",
            "identificationType": "{identificationType}",
            "identificationNumber": "{identificationNumber}",
            "email": "{email}",
            "phoneNumber": "{phoneNumber}"
        }
    ],
    "quote": [],
    "confirm": [
        {
            "name": "Policy Holder",
            "inputParameterId": 480,
            "inputParameterType": "Object",
            "inputParameterValue": null,
            "inputParameterDescription": "Información requerida para el tomador",
            "inputParameterRequired": "true",
            "showApi": true,
            "inputParameterSchemaList": [
                {
                    "propertyId": "499",
                    "propertyName": "Tipo de documento",
                    "propertyValue": "",
                    "propertyDescription": "Se debe de enviar el número que corresponda según el listado de opciones.",
                    "propertyTypeDescription": "List",
                    "propertyTypeId": 3,
                    "propertyTypeListValue": "1 = Cédula ciudadanía, 2 = Cédula extranjería, 3 = NIT, 4 = Tarjeta de identidad, 5 = Pasaporte, 6 = Tarjeta del seguro social extranjeros, 7 = Sociedad extranjera sin Nit en Colombia, 8 = Fideicomiso",
                    "propertyRequired": "true",
                    "isAssistanceType": "false"
                },
                {
                    "propertyId": "500",
                    "propertyName": "Número de documento",
                    "propertyValue": "",
                    "propertyDescription": "Se debe de poner el número del documento seleccionado",
                    "propertyTypeDescription": "Text",
                    "propertyTypeId": 1,
                    "propertyTypeListValue": "",
                    "propertyRequired": "true",
                    "isAssistanceType": "false"
                }
            ]
        },
        {
            "name": "idplan",
            "inputParameterId": 481,
            "inputParameterType": "Numeric",
            "inputParameterValue": null,
            "inputParameterDescription": "Este hace referencia al Id del producto",
            "inputParameterRequired": "true",
            "showApi": true,
            "inputParameterSchemaList": []
        }
    ],
    "toEmit": [
        {
            "name": "Email",
            "inputParameterId": 483,
            "inputParameterType": "Text",
            "inputParameterValue": null,
            "inputParameterDescription": "Email del tomador",
            "inputParameterRequired": "false",
            "showApi": true,
            "inputParameterSchemaList": []
        }
    ],
    "askSekure": []
}
````
 
 ### Cotización
 > En este ejemplo la respuesta de la variable productDiscovery del método Discovery (estructura .json) observamos que para **quote** no se requiere envierle ningún input parameter.
 > 
 >````
 >....
 >"quote": [],
 >....
 >````


Para la cotización se utiliza el método `await _sekure.Quote(executableProduct)` que recibe como parametro el modelo del SDK **ExecutableProduct**.

````
    public async Task<IActionResult> Quotation(Product productDiscovery)
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

Respuesta de la variable **quote**
````
{
    "marketingTracking": null,
    "sessionId": "7bccca61-e5bd-40a9-212c-08da3943ffbe",
    "productDetail": {
        "productId": {productId},
        "productName": "{productName}",
        "policyTypeName": "{policyTypeName}",
        "insuranceCompanyName": "{insuranceCompanyName}"
    },
    "policyHolder": {
        "firstName": "without getting",
        "secondName": null,
        "lastName": "without getting",
        "secondLastName": null,
        "birthdate": "0001-01-01T00:00:00",
        "gender": "O",
        "address": null,
        "identificationType": "1",
        "identificationNumber": "without getting",
        "maritalStatus": null,
        "email": "without getting",
        "phoneNumber": "without getting"
    },
    "quotes": [
        {
            "sessionId": "7bccca61-e5bd-40a9-212c-08da3943ffbe",
            "planId": "138",
            "planNumber": null,
            "planName": null,
            "insuredValue": null,
            "coverages": [
                {
                    "nameResult": "Hurto calificado de los dineros retirados de los Cajeros Electrónicos (de cualquier red), Corresponsales Bancarios, Caja Humana (fleteo).",
                    "valueResult": "$700.000",
                    "descriptionResult": null,
                    "deductibleResult": null,
                    "isAssistanceResult": null
                },
                {
                    "nameResult": "Falsificación o clonación de la tarjeta de Crédito y/o débito.",
                    "valueResult": "$700.000",
                    "descriptionResult": null,
                    "deductibleResult": null,
                    "isAssistanceResult": null
                },
                {
                    "nameResult": "Protección por uso indebido de la tarjeta de Crédito y/o débito y/o chequera, como consecuencia de robo, hurto y/o pérdida.",
                    "valueResult": "$700.000",
                    "descriptionResult": null,
                    "deductibleResult": null,
                    "isAssistanceResult": null
                },
                {
                    "nameResult": "Dańos Accidentales de compras efectuadas con las tarjetas de crédito y/o débito y/o chequera.",
                    "valueResult": "$700.000",
                    "descriptionResult": null,
                    "deductibleResult": null,
                    "isAssistanceResult": null
                },
                {
                    "nameResult": "Utilización forzada de la tarjeta y/o Chequeras (Paseo millonario).",
                    "valueResult": "$700.000",
                    "descriptionResult": null,
                    "deductibleResult": null,
                    "isAssistanceResult": null
                },
                {
                    "nameResult": "Cobertura a Bolso o billetera.",
                    "valueResult": "$700.000",
                    "descriptionResult": null,
                    "deductibleResult": null,
                    "isAssistanceResult": null
                },
                {
                    "nameResult": "Hurto calificado de documentos: (Licencia de conducción, cédula, pasaporte, tarjeta de propiedad del vehículo, libreta militar, Carnet EPS y/o caja de compensación, carnet estudiantil, carnet laboral).",
                    "valueResult": "$700.000",
                    "descriptionResult": null,
                    "deductibleResult": null,
                    "isAssistanceResult": null
                }
            ],
            "deductible": null,
            "startDate": "19 mayo, 2022",
            "termTime": "19 mayo, 2023",
            "premiumAmount": "52236",
            "premiumPaymentInterval": "0",
            "beneficiaries": [],
            "gracePeriodsList": [],
            "additionalInfo": null,
            "additionalInsured": null,
            "policyNumber": null
        }
    ],
    "paymentGatewaySkr": false
}
````

### Confirmar
>
> **Parametros de confirmar**
> 
> En este ejemplo la respuesta de la variable productDiscovery del método Discovery (estructura .json) y observamos que para **confirm** requiere envierle los siguientes input parameter.
> 
>````
>....
>"confirm": [
>    {
>        "name": "Policy Holder",
>        "inputParameterId": 480,
>        "inputParameterType": "Object",
>        "inputParameterValue": null,
>        "inputParameterDescription": "Información requerida para el tomador",
>        "inputParameterRequired": "true",
>        "showApi": true,
>        "inputParameterSchemaList": [
>            {
>                "propertyId": "499",
>                "propertyName": "Tipo de documento",
>                "propertyValue": "",
>                "propertyDescription": "Se debe de enviar el número que corresponda según el listado de opciones.",
>                "propertyTypeDescription": "List",
>                "propertyTypeId": 3,
>                "propertyTypeListValue": "1 = Cédula ciudadanía, 2 = Cédula extranjería, 3 = NIT, 4 = Tarjeta de identidad, 5 = Pasaporte, 6 = Tarjeta del seguro social extranjeros, 7 = Sociedad extranjera sin Nit en Colombia, 8 = Fideicomiso",
>                "propertyRequired": "true",
>                "isAssistanceType": "false"
>            },
>            {
>                "propertyId": "500",
>                "propertyName": "Número de documento",
>                "propertyValue": "",
>                "propertyDescription": "Se debe de poner el número del documento seleccionado",
>                "propertyTypeDescription": "Text",
>                "propertyTypeId": 1,
>                "propertyTypeListValue": "",
>                "propertyRequired": "true",
>                "isAssistanceType": "false"
>            }
>        ]
>    },
>    {
>        "name": "idplan",
>        "inputParameterId": 481,
>        "inputParameterType": "Numeric",
>        "inputParameterValue": null,
>        "inputParameterDescription": "Este hace referencia al Id del producto",
>        "inputParameterRequired": "true",
>        "showApi": true,
>        "inputParameterSchemaList": []
>    }
>   ],
>....
>````
>
> **Ejemplo de la parametrización**:
> 
> A continuación se crea un método básico de ejemplo para asignarle el valor a los input parameter 
> 
> ````
> private void Parameterization(List<InputParameter> parameters)
>{
>    parameters.ForEach(parameter => 
>    {
>        if(parameter.Name == "idplan")
>        {
>            parameter.InputParameterValue = "2022";
>        }
>        else if(parameter.Name == "Policy Holder")
>        {
>            if(parameter.InputParameterSchemaList.Any())
>            {
>                parameter.InputParameterSchemaList.ForEach(parameterSchema => 
>                {
>                    if(parameterSchema.PropertyName == "Tipo de documento")
>                    {
>                        parameterSchema.PropertyValue = "1";
>                    }
>                    else if(parameterSchema.PropertyName == "Número de documento")
>                    {
>                        parameterSchema.PropertyValue = "10000000000";
>                    }
>                    else if(parameterSchema.PropertyName == "Email")
>                    {
>                        parameterSchema.PropertyValue = "test@sekure.com.co";
>                    }
>                });
>            }
>        }
>    }); 
>}
> ````


Método para **confirmar**
Para la confirmar se utiliza el método `await _sekure.Confirm(executableProduct, sessionId)` que recibe como parametro el modelo del SDK **ExecutableProduct** y el sessionId del producto seleccionado.
````
    public async Task<IActionResult> Confirm(Product productDiscovery, Guid sessionId)
    {
        Parameterization(productDiscovery.Confirm);
        ExecutableProduct executableProduct = new ExecutableProduct()
        {
            ProductDetail = productDiscovery.ProductDetail,
            Parameters = productDiscovery.Confirm
        };

        Policy confirm = await _sekure.Confirm(executableProduct, sessionId);
        return Ok(confirm);
    }
````
- sessionId: Es el id del producto seleccionado, este sessionId lo obtiene en el response de cotizar
  
Respuesta de la variable **confirm**
````
{
    "marketingTracking": null,
    "sessionId": "7bccca61-e5bd-40a9-212c-08da3943ffbe",
    "productDetail": {
        "productId": 138,
        "productName": "Asistencia Bolso Protegido",
        "policyTypeName": "Asistencias",
        "insuranceCompanyName": "Andi Asistencia"
    },
    "policyHolder": {
        "firstName": "PrimerNombre",
        "secondName": "SegundoNombre",
        "lastName": "PrimerApellido",
        "secondLastName": "SegundoApellido",
        "birthdate": "2022-02-18T00:00:00Z",
        "gender": "m",
        "address": "Carrera 100 # 134 -10",
        "identificationType": "1",
        "identificationNumber": "1078985631",
        "maritalStatus": "1",
        "email": "prueba@email.com",
        "phoneNumber": "3154789856"
    },
    "confirmedQuote": {
        "planId": "138",
        "planNumber": null,
        "planName": null,
        "insuredValue": null,
        "coverages": [
            {
                "nameResult": "Hurto calificado de los dineros retirados de los Cajeros Electrónicos (de cualquier red), Corresponsales Bancarios, Caja Humana (fleteo).",
                "valueResult": "$700.000",
                "descriptionResult": null,
                "deductibleResult": null,
                "isAssistanceResult": null
            },
            {
                "nameResult": "Falsificación o clonación de la tarjeta de Crédito y/o débito.",
                "valueResult": "$700.000",
                "descriptionResult": null,
                "deductibleResult": null,
                "isAssistanceResult": null
            },
            {
                "nameResult": "Protección por uso indebido de la tarjeta de Crédito y/o débito y/o chequera, como consecuencia de robo, hurto y/o pérdida.",
                "valueResult": "$700.000",
                "descriptionResult": null,
                "deductibleResult": null,
                "isAssistanceResult": null
            },
            {
                "nameResult": "Dańos Accidentales de compras efectuadas con las tarjetas de crédito y/o débito y/o chequera.",
                "valueResult": "$700.000",
                "descriptionResult": null,
                "deductibleResult": null,
                "isAssistanceResult": null
            },
            {
                "nameResult": "Utilización forzada de la tarjeta y/o Chequeras (Paseo millonario).",
                "valueResult": "$700.000",
                "descriptionResult": null,
                "deductibleResult": null,
                "isAssistanceResult": null
            },
            {
                "nameResult": "Cobertura a Bolso o billetera.",
                "valueResult": "$700.000",
                "descriptionResult": null,
                "deductibleResult": null,
                "isAssistanceResult": null
            },
            {
                "nameResult": "Hurto calificado de documentos: (Licencia de conducción, cédula, pasaporte, tarjeta de propiedad del vehículo, libreta militar, Carnet EPS y/o caja de compensación, carnet estudiantil, carnet laboral).",
                "valueResult": "$700.000",
                "descriptionResult": null,
                "deductibleResult": null,
                "isAssistanceResult": null
            }
        ],
        "deductible": null,
        "startDate": "19 mayo, 2022",
        "termTime": "19 mayo, 2023",
        "premiumAmount": "52236",
        "premiumPaymentInterval": "0",
        "beneficiaries": [],
        "gracePeriodsList": [],
        "additionalInfo": [],
        "additionalInsured": null,
        "policyNumber": null
    },
    "status": "Created"
}
````

### Emitir

> **Parametros del emitir**
 > En este ejemplo la respuesta de la variable productDiscovery del método Discovery (estructura .json) observamos que para **toEmit** requiere envierle los siguientes input parameter.
>````
>....
>    "toEmit": [
>        {
>            "name": "Email",
>            "inputParameterId": 483,
>            "inputParameterType": "Text",
>            "inputParameterValue": null,
>            "inputParameterDescription": "Email del tomador",
>            "inputParameterRequired": "false",
>            "showApi": true,
>            "inputParameterSchemaList": []
>        }
>    ],
>....
>````

Método para **emitir**
 
Para la emitir se utiliza el método `await _sekure.Emit(executableProduct, sessionId)` que recibe como parametro el modelo del SDK **ExecutableProduct** y el sessionId del producto seleccionado.
````
    public async Task<IActionResult> ToEmit(Product productDiscovery, Guid sessionId)
    {
        Parameterization(productDiscovery.ToEmit);
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

Respuesta de la variable **emit**
````
Proceso de emision realizado correctamente. Negociación completada exitosamente.
````

#### **NOTA**: 
>
>Este SDK cuenta con más servicios como Pay y AskSekure que tienen el mismo comportamiento que deben de utilizarsen de acuerdo a la configuración del producto
