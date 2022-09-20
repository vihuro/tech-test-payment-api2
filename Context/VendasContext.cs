using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Context
{
    public class VendasContext : DbContext
    {
        public VendasContext(DbContextOptions<VendasContext> options) : base(options){

        }
        public DbSet<PedidoVenda> Pedidos{get; set;}

        public DbSet<VendedorModel> Vendedor{get;set;}
        
    }
}