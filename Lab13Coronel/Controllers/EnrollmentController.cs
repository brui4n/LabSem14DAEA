using Lab13Coronel.DTO.Response;
using Lab13Coronel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab13Coronel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<EnrollmentResponseV1> GetEnrollmentsByCourse([FromQuery] string? courseName)
        {
            // 1. Consulta base incluyendo estudiante y curso activos
            var query = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => e.IsActive && e.Student.IsActive && e.Course.IsActive);

            // 2. Filtro por nombre del curso si se envía el parámetro
            if (!string.IsNullOrEmpty(courseName))
            {
                query = query.Where(e => e.Course.Name.Contains(courseName));
            }

            // 3. Ordenar por nombre de curso y luego por apellido del estudiante
            var enrollments = query
                .OrderBy(e => e.Course.Name)
                .ThenBy(e => e.Student.LastName)
                .Select(e => new EnrollmentResponseV1
                {
                    idEnrollment = e.idEnrollment,
                    date = e.Date,
                    student = new StudentResponseV1
                    {
                        idStudent = e.Student.idStudent,
                        Grade_idGrade = e.Student.Grade_idGrade,
                        firstName = e.Student.FirstName,
                        lastName = e.Student.LastName,
                        phone = e.Student.Phone,
                        email = e.Student.Email
                    },
                    course = new CourseResponseV1
                    {
                        idCourse = e.Course.idCourse,
                        name = e.Course.Name,
                        credit = e.Course.Credit
                    }
                })
                .ToList();

            return enrollments;
        }



        [HttpGet("by-grade")]
        public List<EnrollmentResponseV1> GetEnrollmentsByGrade([FromQuery] string? gradeName)
        {
            // 1. Consulta base incluyendo estudiante, su grado y el curso activos
            var query = _context.Enrollments
                .Include(e => e.Student)
                    .ThenInclude(s => s.Grade)
                .Include(e => e.Course)
                .Where(e => e.IsActive && e.Student.IsActive && e.Student.Grade.IsActive && e.Course.IsActive);

            // 2. Filtro por nombre del grado si se envía el parámetro
            if (!string.IsNullOrEmpty(gradeName))
            {
                query = query.Where(e => e.Student.Grade.Name.Contains(gradeName));
            }

            // 3. Ordenar por nombre de curso y luego por apellido del estudiante
            var enrollments = query
                .OrderBy(e => e.Course.Name)
                .ThenBy(e => e.Student.LastName)
                .Select(e => new EnrollmentResponseV1
                {
                    idEnrollment = e.idEnrollment,
                    date = e.Date,
                    student = new StudentResponseV1
                    {
                        idStudent = e.Student.idStudent,
                        Grade_idGrade = e.Student.Grade_idGrade,
                        firstName = e.Student.FirstName,
                        lastName = e.Student.LastName,
                        phone = e.Student.Phone,
                        email = e.Student.Email
                    },
                    course = new CourseResponseV1
                    {
                        idCourse = e.Course.idCourse,
                        name = e.Course.Name,
                        credit = e.Course.Credit
                    }
                })
                .ToList();

            return enrollments;
        }
    }
}
