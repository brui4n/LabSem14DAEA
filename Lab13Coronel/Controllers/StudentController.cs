using Lab13Coronel.DTO.Response;
using Lab13Coronel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab13Coronel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<StudentResponseV1> GetStudents(
            [FromQuery] string? firstName,
            [FromQuery] string? lastName,
            [FromQuery] string? email)
        {
            // 1. Consulta base filtrando solo los activos (eliminación lógica)
            var query = _context.Students.Where(s => s.IsActive);

            // 2. Filtros dinámicos si se envía algún parámetro de búsqueda
            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(s => s.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(s => s.LastName.Contains(lastName));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(s => s.Email.Contains(email));
            }

            // 3. Ordenar de forma descendente por apellido y proyectar al DTO
            var students = query
                .OrderByDescending(s => s.LastName)
                .Select(s => new StudentResponseV1
                {
                    idStudent = s.idStudent,
                    Grade_idGrade = s.Grade_idGrade,
                    firstName = s.FirstName,
                    lastName = s.LastName,
                    phone = s.Phone,
                    email = s.Email
                })
                .ToList();

            return students;
        }

        // Endpoint 2 (Nuevo requerimiento)
        [HttpGet("by-grade")]
        public List<StudentGradeResponseV1> GetStudentsByNameAndGrade(
            [FromQuery] string? firstName,
            [FromQuery] string? gradeName)
        {
            // 1. Partimos de las matrículas activas cuyos estudiantes, grados y cursos estén activos
            var query = _context.Enrollments
                .Include(e => e.Student)
                    .ThenInclude(s => s.Grade)
                .Include(e => e.Course)
                .Where(e => e.IsActive && e.Student.IsActive && e.Student.Grade.IsActive && e.Course.IsActive);

            // 2. Filtro por nombre del estudiante
            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(e => e.Student.FirstName.Contains(firstName));
            }

            // 3. Filtro por nombre del grado
            if (!string.IsNullOrEmpty(gradeName))
            {
                query = query.Where(e => e.Student.Grade.Name.Contains(gradeName));
            }

            // 4. Ordenar de forma descendente por nombre del curso y proyectar los datos
            var result = query
                .OrderByDescending(e => e.Course.Name)
                .Select(e => new StudentGradeResponseV1
                {
                    idStudent = e.Student.idStudent,
                    firstName = e.Student.FirstName,
                    lastName = e.Student.LastName,
                    phone = e.Student.Phone,
                    email = e.Student.Email,
                    courseName = e.Course.Name,
                    grade = new GradeResponseV1
                    {
                        idGrade = e.Student.Grade.idGrade,
                        name = e.Student.Grade.Name,
                        description = e.Student.Grade.Description
                    }
                })
                .ToList();

            return result;
        }
    }
}
