using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public abstract class BaseClass
    {
        public DateTime CreationDate { get; protected set; }
        public DateTime? UpdatedDate { get; protected set; }
        public bool IsDeleted { get; protected set; } = false;
    }
}