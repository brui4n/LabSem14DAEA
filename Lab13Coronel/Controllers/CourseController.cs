using Lab13Coronel.DTO.Request;
using Lab13Coronel.DTO.Response;
using Lab13Coronel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Lab13Coronel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTAR TODOS (Solo activos)
        [HttpGet]
        public List<CourseResponseV1> GetCourses()
        {
            var courses = _context.Courses
                .Where(c => c.IsActive)
                .Select(c => new CourseResponseV1
                {
                    idCourse = c.idCourse,
                    name = c.Name,
                    credit = c.Credit
                })
                .ToList();

            return courses;
        }

        // BUSCAR POR ID (Solo si está activo)
        [HttpGet("{id}")]
        public ActionResult<CourseResponseV1> GetCourseById(int id)
        {
            var course = _context.Courses
                .Where(c => c.idCourse == id && c.IsActive)
                .Select(c => new CourseResponseV1
                {
                    idCourse = c.idCourse,
                    name = c.Name,
                    credit = c.Credit
                })
                .FirstOrDefault();

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // INSERTAR
        [HttpPost]
        public CourseResponseV1 InsertCourse(CourseRequestV1 request)
        {
            var course = new Course
            {
                Name = request.name,
                Credit = request.credit,
                IsActive = true
            };

            _context.Courses.Add(course);
            _context.SaveChanges();

            return new CourseResponseV1
            {
                idCourse = course.idCourse,
                name = course.Name,
                credit = course.Credit
            };
        }

        // ACTUALIZAR
        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, CourseRequestV1 request)
        {
            var course = _context.Courses.FirstOrDefault(c => c.idCourse == id && c.IsActive);

            if (course == null)
            {
                return NotFound();
            }

            course.Name = request.name;
            course.Credit = request.credit;

            _context.SaveChanges();

            return Ok();
        }

        // ELIMINACIÓN LÓGICA
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.idCourse == id && c.IsActive);

            if (course == null)
            {
                return NotFound();
            }

            course.IsActive = false;
            _context.SaveChanges();

            return Ok();
        }
    }
}
