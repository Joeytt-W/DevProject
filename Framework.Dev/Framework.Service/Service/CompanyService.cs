using AutoMapper;
using Framework.MainEntity.DbEntity;
using Framework.MainEntity.Dto;
using Framework.Service.IRepository;
using Framework.Service.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Service.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IBaseRepository<Company> _baseRepository;

        public CompanyService(IBaseRepository<Company> baseRepository)
        {
            _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
        }

        public async Task<bool> AddCompanyAsync(Company entity)
        {
            return await _baseRepository.AddAsync(entity,true);
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task<Company> QueryAsync(Guid id)
        {
            return await _baseRepository.QueryAsync(t => t.Id == id);
        }

        public async Task<bool> UpdateAsync(Company company)
        {
            return await _baseRepository.UpdateAsync(company, true);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await _baseRepository.QueryAsync(t => t.Id == id);
            if(model == null)
                return false;
            return await _baseRepository.DeleteAsync(model, true);
        }
    }
}
