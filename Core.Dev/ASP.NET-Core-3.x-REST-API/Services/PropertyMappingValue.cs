using System;
using System.Collections.Generic;

namespace Routine.Api.Services
{
    public class PropertyMappingValue
    {
        public IEnumerable<string> DestinationProperties { get; set; }

        public bool Revert { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinationProperties">如果传入排序的字段对应数据库实体的多个字段，那么数据库实体的多个字段存放在DestinationProperties,即如果传入的是按Name排序  Name 映射到数据库字段是 FirstName + LastName,那么FirstName,LastName保存到DestinationProperties</param>
        /// <param name="revert">是否要反转排序方式,如传入字段是年龄，二而数据库保存的字段是出生日期，传入时要求按年龄升序，那么实际上是要按出生日期降序 Revert=true</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PropertyMappingValue(IEnumerable<string> destinationProperties, bool revert = false)
        {
            DestinationProperties = destinationProperties 
                                    ?? throw new ArgumentNullException(nameof(destinationProperties));
            Revert = revert;
        }
    }
}
