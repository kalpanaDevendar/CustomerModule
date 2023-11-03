using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CustomerModule.Models
{
    [DataContract]
    public class ResultDTO
    {
       
        [DataMember(EmitDefaultValue = false)]
        public Guid CustomerId { get; set; }
        
        [Required(ErrorMessage = "Customer Name is required.")]
        public string? CustomerName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "EmailId Name is required.")]
        public string? EmailId { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string? Address { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Guid OrderId { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public DateTime? OrderDate { get; set; }
       
       
    }
}
