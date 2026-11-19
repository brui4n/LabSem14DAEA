using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab13Coronel.Models
{
    public class Student
    {
        [Key]
        public int idStudent { get; set; }

        public int Grade_idGrade { get; set; }

        [ForeignKey("Grade_idGrade")]
        public virtual Grade Grade { get; set; }

        [StringLength(45)]
        public string FirstName { get; set; }

        [StringLength(45)]
        public string LastName { get; set; }

        [StringLength(45)]
        public string Phone { get; set; }

        [StringLength(45)]
        public string Email { get; set; }

        public bool IsActive { get; set; } = true;

        // Propiedad de navegación
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
