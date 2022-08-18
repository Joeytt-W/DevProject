using Dev.Entity.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Services.IServices
{
    public interface ITestService
    {
        Task<IEnumerable<DbStu>> GetTestStudentInfo();
    }
}
