using Npgsql;
using oee.Clientes;
using oee.Inventario;
using oee.Inventarios;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
           
            try
            {
                var pk = new PrivateKeyFile("C:\\Users\\joasa\\Downloads\\soporteaws.pem");
                var keyFiles = new[] { pk };
                var methods = new List<AuthenticationMethod>();
                methods.Add(new PrivateKeyAuthenticationMethod("ubuntu", keyFiles));

                var con = new ConnectionInfo("3.89.201.178", 22, "ubuntu", methods.ToArray());
                using (var client = new SshClient(con))
                {
                    client.Connect();
                    ForwardedPortLocal port = new ForwardedPortLocal("127.0.0.1", "127.0.0.1", 60941);
                    client.AddForwardedPort(port);
                    port.Start();

                    string connString =
                        $"Server=localhost;Database=Odoo;Port=60941;" +
                         "User Id=adminjr;Password=!JRadmin!@;" +
                         "Timeout = 60;CommandTimeout = 60";

                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM account_move", conn);
                        Int64 count = (Int64)command.ExecuteScalar();
                        conn.Close();
                    }
                }

                //var items = await _repository.GetListAsync();
                //return items
                //    .Select(item => new ProductoDto
                //    {
                //        Id = item.Id,
                //        Nombre = item.Nombre
                //    }).ToList();
            }
            catch(Exception ex)
            {

            }
            return null;
        }
    }
}
