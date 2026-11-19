using System.ComponentModel.DataAnnotations;

namespace Lab13Coronel.Models
{
    public class Course
    {
        [Key]
        public int idCourse { get; set; }

        [StringLength(45)]
        public string Name { get; set; }

        public int Credit { get; set; }

        public bool IsActive { get; set; } = true;

        // Propiedad de navegación
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
