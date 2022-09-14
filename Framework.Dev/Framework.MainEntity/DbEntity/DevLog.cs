using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MainEntity.DbEntity
{
    public class DevLog
    {
        public int Id { get; set; }
        public string CreateTime { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Account { get; set; }
        public string RealName { get; set; }
        public string Operation { get; set; }
        public string IP { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
    }
}
