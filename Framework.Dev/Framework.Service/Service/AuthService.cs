using Framework.MainEntity.DbEntity;
using Framework.Service.IRepository;
using Framework.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Service.Service
{
    public class AuthService: IAuthService
    {
        private readonly IBaseRepository<Employee> _baseRepository;

        public AuthService(IBaseRepository<Employee> baseRepository)
        {
            _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
        }

        public async Task<Employee> QueryAsync(string employeeNo)
        {
            return await _baseRepository.QueryAsync(e => e.EmployeeNo == employeeNo);
        }
    }
}
