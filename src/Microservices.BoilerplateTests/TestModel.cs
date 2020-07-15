using System.ComponentModel.DataAnnotations;

namespace Microservices.BoilerplateTests
{
    public class TestModel
    {
        public string OptionalField { get; set; }

        [Required]
        public string RequiredField { get; set; }
    }
}
