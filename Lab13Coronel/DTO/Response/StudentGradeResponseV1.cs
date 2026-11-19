namespace Lab13Coronel.DTO.Response
{
    public class StudentGradeResponseV1
    {
        public int idStudent { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string courseName { get; set; }
        public GradeResponseV1 grade { get; set; }
    }
}
