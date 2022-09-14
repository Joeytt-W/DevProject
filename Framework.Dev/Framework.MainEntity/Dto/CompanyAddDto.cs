using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MainEntity.Dto
{
    public class CompanyAddDto
    {
        [Display(Name = "公司")]
        [Required(ErrorMessage ="{0}是必填项")]        
        [MaxLength(100,ErrorMessage ="{0}最大长度不能超过{1}")]
        public string Name { get; set; }

        [Display(Name = "国家")]
        [Required(ErrorMessage = "{0}是必填项")]
        [MaxLength(50, ErrorMessage = "{0}最大长度不能超过{1}")]
        public string Country { get; set; }

        [Display(Name = "行业")]
        [Required(ErrorMessage = "{0}是必填项")]
        [MaxLength(50, ErrorMessage = "{0}最大长度不能超过{1}")]
        public string Industry { get; set; }

        [Display(Name = "产品")]
        [Required(ErrorMessage = "{0}是必填项")]
        [MaxLength(100, ErrorMessage = "{0}最大长度不能超过{1}")]
        public string Product { get; set; }

        [Display(Name = "详情")]
        [Required(ErrorMessage = "{0}是必填项")]
        [MaxLength(100, ErrorMessage = "{0}最大长度不能超过{1}")]
        public string Introduction { get; set; }
    }
}
