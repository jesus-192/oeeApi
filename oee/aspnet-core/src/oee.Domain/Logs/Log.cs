using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace oee.Logs
{
    public class Log : BasicAggregateRoot<int>
    {
        public string mensaje { get; set; }
    }
}
