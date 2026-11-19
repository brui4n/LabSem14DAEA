using System.ComponentModel.DataAnnotations;

namespace Lab13Coronel.Models
{
    public class Grade
    {
        [Key]
        public int idGrade { get; set; }

        [StringLength(45)]
        public string Name { get; set; }

        [StringLength(45)]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        // Propiedad de navegación
        public virtual ICollection<Student> Students { get; set; }
    }
}
