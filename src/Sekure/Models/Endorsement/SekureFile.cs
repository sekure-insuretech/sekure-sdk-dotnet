using System;

namespace Sekure.Models.Endorsement
{
    /// <summary>Archivo devuelto por InsuranceOS (PDF, Excel o documento).</summary>
    public class SekureFile
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }

    /// <summary>Error HTTP de InsuranceOS con su código y el cuerpo de la respuesta.</summary>
    public class SekureApiException : Exception
    {
        public int StatusCode { get; }
        public string ResponseBody { get; }

        public SekureApiException(int statusCode, string responseBody)
            : base($"statusCode: {statusCode}, messageException: {responseBody}")
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
        }
    }
}
