using Microsoft.AspNetCore.Mvc;
using StudentAdminPortalAPI.Repositories;
using StudentAdminPortalAPI.DomainModels;
using AutoMapper;

namespace StudentAdminPortalAPI.Controllers
{
    [ApiController]
    public class StudentsController : ControllerBase
    {
        readonly IStudentRepository studentRepository;
        readonly IMapper mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            // Fetch all students
            var students = await studentRepository.GetStudentsAsync();

            // Convert data model to domain model
            List<Student> domainModelStudents = mapper.Map<List<Student>>(students);
            return Ok(domainModelStudents);
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            // Fetch student details
            var student = await studentRepository.GetStudentAsync(studentId);

            if (student == null)
                return NotFound();
            
            // Convert data model to domain model
            Student domainModelStudent = mapper.Map<Student>(student);
            return Ok(domainModelStudent);
        }
    }
}
