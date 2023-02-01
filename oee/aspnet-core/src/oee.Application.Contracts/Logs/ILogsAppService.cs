using oee.Inventario;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace oee.Logs
{
    public interface ILogsAppService
    {
        Task<LogsDto> CreateAsync(LogsDto logs);
    }
}
