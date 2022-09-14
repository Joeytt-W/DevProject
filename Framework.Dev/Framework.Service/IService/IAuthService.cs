using Framework.MainEntity.DbEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Service.IService
{
    public interface IAuthService
    {
        Task<Employee> QueryAsync(string employeeNo);
    }
}
