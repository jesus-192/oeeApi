using oee.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace oee.Logs
{
    public class LogAppService : ApplicationService, ILogsAppService
    {
        private readonly IRepository<Log, int> _logRepository;
        public LogAppService(IRepository<Log, int> logRepository)
        {
            _logRepository= logRepository;
        }

        public async Task<LogsDto> CreateAsync(LogsDto logs)
        {
            var todoItem = await _logRepository.InsertAsync(
         new Log { mensaje = logs.Mensaje }
     );
            return new LogsDto { Mensaje = todoItem.mensaje };
        }
    }
}
