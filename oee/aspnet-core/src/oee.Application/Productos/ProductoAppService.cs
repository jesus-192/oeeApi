using Npgsql;
using oee.Clientes;
using oee.Inventario;
using oee.Inventarios;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace oee.Productos
{
    public class ProductoAppService : ApplicationService, IProductoAppService
    {
        private readonly IRepository<Producto, int> _repository;

        public ProductoAppService(IRepository<Producto, int> repository)
        {
            _repository = repository;
        }
        public async Task<ProductoDto> CreateAsync(ProductoDto producto)
        {
            var todoItem = await _repository.InsertAsync(
                new Producto { Nombre = producto.Nombre }
            );

            return new ProductoDto
            {
                Id = todoItem.Id,
                Nombre = todoItem.Nombre
            };
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<ProductoDto>> GetListAsync()
        {
            ConnectionInfo connectionInfo = null; // TODO: Initialize to an appropriate value
            SshClient client = new SshClient("localhost", 5432, "adminjr", "!JRadmin!@");
            var port = new ForwardedPortLocal("localhost", "3.89.201.178", 22);
            client.AddForwardedPort(port);
            port.Start();

            string pass = @"MIIEogIBAAKCAQEAg1jcSOFlPwwnbu7QWN2A9yDMF9ZZOkgr92r0huidblB9Q8WT\r\nOe9b9U9BTCAS4F+yfvovtroGRx9EKPKwK5cjWAXmr/NLqAMQoF8EUobNkczffDay\r\nuHrwq7K+umEtTB8KCqwlS0KOyNy10TcY7oeuqAxY9kuhnURS0FmNbePvZq4DUo4B\r\nRTjQpymuBj/5Cx8EFQYiTm/WiillDG1pmEH9uSyrK5FD87OiArVga0JDgapCLvtM\r\no5gDwPxoklx0H8AXyFhU88OzS0wnoUk5bO+KYsjHK4w0ggxYGdmjOJZQsgr4Gu/P\r\n0pUJphzkXLWViz3YEal+UOus5t7dsoZSSy3GZQIDAQABAoIBAA49Vvxruy8/oRLM\r\nvzezI+Um4BmBeoDwDBwxszrhAxhPj5UGWXTBd2W75uda3tEGbvKHKx8TQdT6Fcie\r\nruy64PENCrpulPgtBONuCajsxmKVJHFx+KQ5Z9glc7n/CQsknlET4qMrHxG1o9x/\r\nratGeG/tjsN85ZOIMbY2zzdnq6+k6qwKmpAh3p8TDYdi3Gvrt3CeC0+ja0moPJU3\r\nt784ChAXhEFF4N+/+bXwIe0UWBd+pY9AmX6uFaKFkkn/GgTnO5MvQQXP5hcLNIXR\r\nEHubmmCPfmiVxvP+dE8BIJLYne89rtBUt/68lGcBJOeZmMLih/dZwm7zOINy8EDr\r\nTm9glw0CgYEA8FYh2Qf3Di4xfy4OMJJUfBSkeWDs/oSajlUr2TIVfKp4aQco79/j\r\ntlSmU3ObYYQ4m5031cTR05cA25YV54/xNvSIvq3WPNxU4l84u5Ag+9vrPnk343Ol\r\nbrdKV+VoDyhnDoRzPhWnCS9x4p4JwMPakDIyH+5rePzvUv/S/5NruI8CgYEAi+hO\r\nqX3qt7zBZOlCVAcSi03nFcnsF8s0duSk8FDZ6Tfd3JQZRW0aMysgvys9DbzHHTkV\r\n6Yv0Qql9H/iojX+Vybdstefs9jOKSuDLsp+owsQM86tw1XE4YoErnJya3HnWIzyk\r\naGTipaT774W5Zt33/GRuMEaNoCmof9ucie27Q8sCgYBaHSGR29+xjHZ4pk8hvSw6\r\nXg8Rh9s0z4mYBq1rQdE2rkB5uPqLfppGGlVHAxjmpooHJxrxZ1v5hywGKa9K5Pjf\r\nzy6auFmU9lRJkMSX50HGzb3V7eObwBfufIE+lcC/tjl2Awmm5L19vzFndDRZ99Wy\r\nrW+H84COm+AsccmulJsWVwKBgHusrqSbf3pObDLVE+INonHsOoODTKW35rfW5Irf\r\nHdJQpVnQvQonKMrkq/zxrvXO08DcbqdWJWUsSEST3gO/LdzzvTCMRWdaJjyMvOQ4\r\nyMjq33wjKAo2HJ7PCotV0HtnZEkf2UHDNKsyzdZtQnhRbV76NIGOTLGxQZdMvfMb\r\naLfDAoGAJ8xVqR6L4RQuRTfEGItc8g0gl53FhQK2/z5ifSLDNvYiF1jsr8zxXXNV\r\n0dMTaUI8+Mys/E/XeU6ACIFPybzMP8H5HQ20S9RGwwCBVkgnxkkExbaFsr4Fhts/\r\n5Wk1bHKRZBB/Aj/0Zb3OsImqJy9a8AFphFQyQgllkN9Um/u1hqE=";

            string connString =
                $"Server={port.BoundHost};Database=dbname;Port={port.BoundPort};" +
                 $"User Id=ubuntu;Password={pass};";

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                Console.WriteLine("ENTRO A LA CONEXION");

            }
            return new List<ProductoDto>();
            //var items = await _repository.GetListAsync();
            //return items
            //    .Select(item => new ProductoDto
            //    {
            //        Id = item.Id,
            //        Nombre = item.Nombre
            //    }).ToList();
        }
    }
}
