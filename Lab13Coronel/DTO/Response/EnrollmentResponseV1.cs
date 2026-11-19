namespace Lab13Coronel.DTO.Response
{
    public class EnrollmentResponseV1
    {
        public int idEnrollment { get; set; }
        public DateTime date { get; set; }
        public StudentResponseV1 student { get; set; }
        public CourseResponseV1 course { get; set; }
    }
}
