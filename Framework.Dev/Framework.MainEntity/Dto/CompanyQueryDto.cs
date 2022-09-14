using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MainEntity.Dto
{
    public class CompanyQueryDto
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "公司")]
        public string Name { get; set; }

        [Display(Name = "国家")]
        public string Country { get; set; }

        [Display(Name = "行业")]
        public string Industry { get; set; }

        [Display(Name = "产品")]
        public string Product { get; set; }

        [Display(Name = "详情")]
        public string Introduction { get; set; }
    }
}
