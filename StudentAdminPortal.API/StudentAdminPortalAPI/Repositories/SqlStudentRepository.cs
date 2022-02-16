﻿using Microsoft.EntityFrameworkCore;
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
    }
}
