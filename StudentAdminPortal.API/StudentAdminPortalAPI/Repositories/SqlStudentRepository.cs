using Microsoft.EntityFrameworkCore;
using StudentAdminPortalAPI.DataModels;

namespace StudentAdminPortalAPI.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        readonly StudentAdminContext context;

        public SqlStudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Students
                .Include(nameof(Gender))
                .Include(nameof(Address))
                .ToListAsync();
        }

        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await context.Students
                .Include(nameof(Gender))
                .Include(nameof(Address))
                .FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await context.Genders.ToListAsync();
        }

        public async Task<bool> Exists(Guid studentId)
        {
            return await context.Students.AnyAsync(x => x.Id == studentId);
        }

        public async Task<Student> UpdateStudent(Guid studentId, Student request)
        {
            Student existingStudent = await GetStudentAsync(studentId);
            if (existingStudent == null)
                return null;
            
            existingStudent.FirstName = request.FirstName;
            existingStudent.LastName = request.LastName;
            existingStudent.GenderID = request.GenderID;
            existingStudent.DateOfBirth = request.DateOfBirth;
            existingStudent.Email = request.Email;
            existingStudent.Mobile = request.Mobile;
            existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
            existingStudent.Address.PostalAddress = request.Address.PostalAddress;

            await context.SaveChangesAsync();
            return existingStudent;
            
        }

        public async Task<Student> DeleteStudent(Guid studentId)
        {
            Student student = await GetStudentAsync(studentId);

            if (student == null) return null;
            
            context.Students.Remove(student);
            await context.SaveChangesAsync();
            return student;            
        }

        public async Task<Student> AddStudent(Student request)
        {
            var newStudent = await context.Students.AddAsync(request);
            await context.SaveChangesAsync();
            return newStudent.Entity;
        }

        public async Task<bool> UpdateProfileImage(Guid studentId, string profileImageURL)
        {
            var student = await GetStudentAsync(studentId);

            if (student != null)
            {
                student.ProfileImageURL = profileImageURL;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
