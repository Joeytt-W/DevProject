using Framework.MainEntity.DbEntity;
using Framework.MainEntity.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Service.IService
{
    public interface ICompanyService
    {
        Task<bool> AddCompanyAsync(Company entity);

        Task<IEnumerable<Company>> GetCompanies();

        Task<Company> QueryAsync(Guid id);

        Task<bool> UpdateAsync(Company company);

        Task<bool> DeleteAsync(Guid id);
    }
}
