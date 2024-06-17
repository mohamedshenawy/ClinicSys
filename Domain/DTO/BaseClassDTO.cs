using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class BaseClassDTO
    {
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
