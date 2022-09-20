using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Context;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController : ControllerBase
    {
 
         public  VendasContext context;

        public VendedorController(VendasContext context){
            this.context = context;
        }

       [HttpGet("RetornaLista")]  
       public void RetornarLista(int id){
        var context = this.context.Vendedor.ToList();
       }
        public bool exists = false;
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id){
            
            var context = this.context.Vendedor.Find(id);
            if(context == null){
                this.context = null;
                return NotFound("Vendedor não econtrado!");
            }
            exists = true;
            return Ok(context);
        }

        [HttpGet]
        public IActionResult ObertTodosVendedores(){
            var context = this.context.Vendedor.ToList();
            
            return Ok(context);
        }

        [HttpPost]
        public IActionResult CadastrarVendedor(VendedorModel vendedor){

            this.context.Add(vendedor);
            this.context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new {id = vendedor.Id}, vendedor);
        }
        [HttpDelete("{id}")]
        public IActionResult DeletarVendedor(int id){
            var vendedor = this.context.Vendedor.Find(id);
            if(vendedor == null){
                return NotFound("Vendedor não econtrado!");
            }
            this.context.Remove(vendedor);
            this.context.SaveChanges();
            return Ok($"Vendedor {vendedor.Nome} deletado com sucesso");
        }
        
    }
}