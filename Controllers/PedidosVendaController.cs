using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Context;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosVendaController : ControllerBase
    {
        private  VendasContext context;
        private VendedorController vendedor;
        public PedidosVendaController(VendasContext context){
            this.context = context;
        }

        [HttpGet]
        public IActionResult ObterTodosPedidos(){
            var pedidos = context.Pedidos.ToList();

            return Ok(pedidos);
        }

        [HttpGet("BuscarPorVendedor")]
        public IActionResult VendasPorVendedor(int id){
            var vendas = context.Pedidos.Where(x => x.IdVendedor == id);
            
            return Ok(vendas);
        }

        [HttpPost("GerarVenda")]
        public IActionResult GerarVenda(PedidoVenda venda){

            vendedor = new VendedorController(context);
            var verificarVendedor = vendedor;
            verificarVendedor.ObterPorId(venda.IdVendedor);

            if(verificarVendedor.context == null){
                return Unauthorized("Vendedor não econtrado");
            }
            venda.DataHoraVenda = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            venda.StatusVenda = "Aguardando Pagamento";
            context.Add(venda);
            context.SaveChanges();
            return CreatedAtAction(nameof(ObterVenda), new {id = venda.Id},venda);
        }

        [HttpGet("BuscarPorID")]
        public IActionResult ObterVenda(int id)
        {
            var venda = context.Pedidos.Find(id);
            if(venda == null){
                return NotFound();
            }
            return Ok(venda);
        }

        [HttpPut("PagamentoAutorizado{id}")]
        public IActionResult PagamentoAprovado(int id){
            var pedido = context.Pedidos.Find(id);
            
                if(pedido == null){
                    return NotFound("Pedido não encontrado!");
                }
                else if(pedido.StatusVenda != "Aguardando Pagamento"){
                    return Unauthorized("Não é possivel alterar esse pedido");
                }
                pedido.StatusVenda = "Pagamento Autorizado";
                context.Update(pedido);
                context.SaveChanges();
                return Ok("Pedido alterado com sucesso");
            
        }

        [HttpPut("EncaminhadoTransportadora{id}")]
        public IActionResult EncaminhadoTransportadora(int id){
            var pedido = context.Pedidos.Find(id);
            
                if(pedido == null){
                    return NotFound("Pedido não encontrado!");
                }
                else if(pedido.StatusVenda == "Pagamento Autorizado"){
                    pedido.StatusVenda = "Ecaminhado para a transportadora";
                    context.Update(pedido);
                    context.SaveChanges();

                    return Ok($"Status do Pedido = {pedido.StatusVenda}");
                }

                return Unauthorized("Não é possivel embarcar este pedido");
            
        }

        [HttpPut("DeclaraEntrega{id}")]
        public IActionResult DeclararEntrega(int id){
            var pedido = context.Pedidos.Find(id);
            
                if(pedido == null){
                    return NotFound("Pedido não encontrado!");
                }
                else if(pedido.StatusVenda == "Encaminhado para a transportadora"){
                    pedido.StatusVenda = $"Entregue no dia {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} ";
                    context.Update(pedido);
                    context.SaveChanges();

                    return Ok($"Status do Pedido = {pedido.StatusVenda}");
                }

                return Unauthorized("Não é possivel entregar este pedido este pedido");
            
        }

        [HttpPut("CancelarPedido{id}")]
        public IActionResult Cancelar(int id){
            var pedido = context.Pedidos.Find(id);
           var teste = ObterVenda(id);
           if(teste != null){
            return Ok("Passou no teste");
           }
                if(pedido == null){
                    return NotFound("Pedido não encontrado!");
                }
                else if(pedido.StatusVenda != "Encaminhado para a transportadora" || 
                pedido.StatusVenda != "Pagamento Autorizado"  ){
                    pedido.StatusVenda = "Cancelado";
                    context.Update(pedido);
                    context.SaveChanges();

                    return Ok($"Status do Pedido = {pedido.StatusVenda}");
                }

                return Unauthorized("Não é possivel cancelar esse pedido");
            
        }


        [HttpDelete("{id}")]
        public IActionResult DeletarPedido(int id){
            var pedido = context.Pedidos.Find(id);
            if(pedido == null){
                return NotFound("Pedido não econtrado!");
            }
            context.Remove(id);
            context.SaveChanges();
            return Ok("Pedido deletado com sucesso!");
        }
        [HttpDelete("DeleteTudo")]
        public IActionResult DeleteTudo(){
            var delete = context.Pedidos.ToList();
            foreach(var id in context.Pedidos.ToList()){
                context.Remove(id);
            }
  
            context.SaveChanges();
            return Ok();
        }
    }
}