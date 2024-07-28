using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace Domain.Entities
{
    public class Clinic : BaseClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<Doctor> Doctors => _doctors;
        
        private Clinic(string name)
        {
            Name = name;
            CreationDate = DateTime.Now;
            IsDeleted = false;
        }
        private readonly List<Doctor> _doctors = new();


        public Clinic Create(string name)
        {
            var instance = new Clinic(name);
            if (!IsValid(instance))
                throw new InvalidOperationException("this name is not correct!");

            return instance;
        }

        private bool IsValid(Clinic model)
        {
            int notValidCounter = 0;
            if (Equals(model.Name, string.Empty))
            {
                notValidCounter++;
            }

            if (model.Name.Length <= 0 || model.Name.Length > 50)
            {
                notValidCounter++;
            }

            return notValidCounter <= 0;
        }

    }
}