
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tech_test_payment_api.Models
{
    public class PedidoVenda
    {
        private int id{get; set;}
        private DateTime dataHoraVenda;
        private string statusVenda;
        private int idVendedor;

        public int Id { get => id; set => id = value; }
        public DateTime DataHoraVenda { get => dataHoraVenda; set => dataHoraVenda = value; }
        public string StatusVenda { get => statusVenda; set => statusVenda = value; }
        public int IdVendedor { get => idVendedor; set => idVendedor = value; }
    }
}