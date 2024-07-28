using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public abstract class BaseClass
    {
        protected DateTime CreationDate { get; set; }
        protected DateTime? UpdatedDate { get; set; }
        protected bool IsDeleted { get; set; } = false;
    }
}