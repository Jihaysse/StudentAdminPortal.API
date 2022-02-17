﻿using Microsoft.AspNetCore.Mvc;
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
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
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

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            var studentDataModel = mapper.Map<DataModels.Student>(request);
            await studentRepository.AddStudent(studentDataModel);

            var studentDomainModel = mapper.Map<Student>(studentDataModel);
            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = studentDataModel.Id }, studentDomainModel);
        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {
            if (await studentRepository.Exists(studentId))
            {
                var studentDataModel = mapper.Map<DataModels.Student>(request);

                // Update details
                var updatedStudent = await studentRepository.UpdateStudent(studentId, studentDataModel);
                if (updatedStudent != null)
                {
                    var studentDomainModel = mapper.Map<Student>(updatedStudent);
                    return Ok(studentDomainModel);
                }
            }
            return NotFound();
            
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await studentRepository.Exists(studentId))
            {
                // Delete the student
                var student = await studentRepository.DeleteStudent(studentId);
                var studentDomainModel = mapper.Map<Student>(student);
                return Ok(studentDomainModel);
            }

            return NotFound();
        }
    }
}
