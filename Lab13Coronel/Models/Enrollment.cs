using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab13Coronel.Models
{
    public class Enrollment
    {
        [Key]
        public int idEnrollment { get; set; }

        public int Student_idStudent { get; set; }

        [ForeignKey("Student_idStudent")]
        public virtual Student Student { get; set; }

        public int Course_idCourse { get; set; }

        [ForeignKey("Course_idCourse")]
        public virtual Course Course { get; set; }

        public DateTime Date { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
