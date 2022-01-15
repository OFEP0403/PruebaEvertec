using System;
namespace test.data_access.evertec.Models
{
    /// <summary>
    /// Response generico hacia el front
    /// </summary>
    /// <typeparam name="T">Tipo de objeto que se quiere como resultado</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Objeto generico para el resultado
        /// </summary>
        public T result { get; set; }

        /// <summary>
        /// Codigo de la transacción o error
        /// </summary>
        public int statusCode { get; set; }

        /// <summary>
        /// Descripcion de la transacción o error
        /// </summary>
        public string statusDesc { get; set; }
    }
}