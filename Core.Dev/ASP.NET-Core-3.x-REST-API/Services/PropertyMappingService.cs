using System;
using System.Collections.Generic;
using System.Linq;
using Routine.Api.Entities;
using Routine.Api.Models;

namespace Routine.Api.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        #region 建立Dto到Entity的可用于排序字段的映射关系
        private readonly Dictionary<string, PropertyMappingValue> _companyPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"Id", new PropertyMappingValue(new List<string>{"Id"}) },
                {"CompanyName", new PropertyMappingValue(new List<string>{"Name"}) },
                {"Country", new PropertyMappingValue(new List<string>{"Country"}) },
                {"Industry", new PropertyMappingValue(new List<string>{ "Industry"})},
                {"Product", new PropertyMappingValue(new List<string>{"Product"})},
                {"Introduction", new PropertyMappingValue(new List<string>{"Introduction"})}
            };

        private readonly Dictionary<string, PropertyMappingValue> _employeePropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"Id", new PropertyMappingValue(new List<string>{"Id"}) },
                {"CompanyId", new PropertyMappingValue(new List<string>{"CompanyId"}) },
                {"EmployeeNo", new PropertyMappingValue(new List<string>{"EmployeeNo"}) },
                {"Name", new PropertyMappingValue(new List<string>{"FirstName", "LastName"})},
                {"GenderDisplay", new PropertyMappingValue(new List<string>{"Gender"})},
                {"Age", new PropertyMappingValue(new List<string>{"DateOfBirth"}, true)}
            };
        #endregion

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<EmployeeDto, Employee>(_employeePropertyMapping));
            _propertyMappings.Add(new PropertyMapping<CompanyDto, Company>(_companyPropertyMapping));
        }

        /// <summary>
        /// 获取映射关系，通过类型确认
        /// </summary>
        /// <typeparam name="TSource">Dto</typeparam>
        /// <typeparam name="TDestination">Entity</typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            var propertyMappings = matchingMapping.ToList();
            if (propertyMappings.Count == 1)
            {
                return propertyMappings.First().MappingDictionary;
            }

            throw new Exception($"无法找到唯一的映射关系：{typeof(TSource)}, {typeof(TDestination)}");
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldAfterSplit = fields.Split(",");
            foreach (var field in fieldAfterSplit)
            {
                var trimmedField = field.Trim();
                var indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.Ordinal);
                var propertyName = indexOfFirstSpace == -1 ? trimmedField 
                    : trimmedField.Remove(indexOfFirstSpace);

                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
