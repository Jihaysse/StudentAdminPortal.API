using Microsoft.AspNetCore.Mvc;
using StudentAdminPortalAPI.Repositories;
using StudentAdminPortalAPI.DomainModels;
using AutoMapper;

namespace StudentAdminPortalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await studentRepository.GetStudentsAsync();

            List<Student> domainModelStudents = mapper.Map<List<Student>>(students);
            return Ok(domainModelStudents);
        }
    }
}
