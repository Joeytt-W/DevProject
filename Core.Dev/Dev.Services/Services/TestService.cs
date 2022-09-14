using Dev.Entity.DbEntities;
using Dev.Entity.ViewModel;
using Dev.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Services.Services
{
    public class TestService : ITestService
    {
        public async Task<IEnumerable<DbStu>> GetTestStudentInfo()
        {
            List<DbStu> students = new List<DbStu>();
            for(int i = 1; i < 11; i++)
            {
                students.Add(new DbStu
                {
                    Id = i,
                    DbName = "学生" + i
                });
            }

            return await Task.Run<IEnumerable<DbStu>>(
                () => students.FindAll(c => c.DbName.StartsWith('学')));
        }
    }
}
