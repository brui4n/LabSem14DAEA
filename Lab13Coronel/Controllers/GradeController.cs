using Lab13Coronel.DTO.Request;
using Lab13Coronel.DTO.Response;
using Lab13Coronel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab13Coronel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GradeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTAR TODOS (Solo activos)
        [HttpGet]
        public List<GradeResponseV1> GetGrades()
        {
            var grades = _context.Grades
                .Where(g => g.IsActive)
                .Select(g => new GradeResponseV1
                {
                    idGrade = g.idGrade,
                    name = g.Name,
                    description = g.Description
                })
                .ToList();

            return grades;
        }

        // BUSCAR POR ID (Solo si está activo)
        [HttpGet("{id}")]
        public ActionResult<GradeResponseV1> GetGradeById(int id)
        {
            var grade = _context.Grades
                .Where(g => g.idGrade == id && g.IsActive)
                .Select(g => new GradeResponseV1
                {
                    idGrade = g.idGrade,
                    name = g.Name,
                    description = g.Description
                })
                .FirstOrDefault();

            if (grade == null)
            {
                return NotFound();
            }

            return grade;
        }

        // INSERTAR
        [HttpPost]
        public GradeResponseV1 InsertGrade(GradeRequestV1 request)
        {
            var grade = new Grade
            {
                Name = request.name,
                Description = request.description,
                IsActive = true
            };

            _context.Grades.Add(grade);
            _context.SaveChanges();

            return new GradeResponseV1
            {
                idGrade = grade.idGrade,
                name = grade.Name,
                description = grade.Description
            };
        }

        // ACTUALIZAR
        [HttpPut("{id}")]
        public IActionResult UpdateGrade(int id, GradeRequestV1 request)
        {
            var grade = _context.Grades.FirstOrDefault(g => g.idGrade == id && g.IsActive);

            if (grade == null)
            {
                return NotFound();
            }

            grade.Name = request.name;
            grade.Description = request.description;

            _context.SaveChanges();

            return Ok();
        }

        // ELIMINACIÓN LÓGICA
        [HttpDelete("{id}")]
        public IActionResult DeleteGrade(int id)
        {
            var grade = _context.Grades.FirstOrDefault(g => g.idGrade == id && g.IsActive);

            if (grade == null)
            {
                return NotFound();
            }

            grade.IsActive = false;
            _context.SaveChanges();

            return Ok();
        }
    }
}
