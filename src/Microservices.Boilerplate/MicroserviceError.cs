namespace Microservices.Boilerplate
{
    /// <summary>
    /// A DTO for a Microservice Exception
    /// </summary>
    public class MicroserviceError
    {
        /// <summary>
        /// Gets or sets the exception message
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that contains the error message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> that defines the HTTP status code.
        /// </value>
        public int StatusCode { get; set; }
    }
}
