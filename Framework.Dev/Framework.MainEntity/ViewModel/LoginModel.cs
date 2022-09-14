using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MainEntity.ViewModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "{0}是必填项")]
        [Display(Name ="工号")]
        public string EmployeeNo { get; set; }
        [Required(ErrorMessage = "{0}是必填项")]
        [Display(Name = "姓名")]
        public string Name { get; set; }
    }
}
