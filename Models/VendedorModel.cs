namespace tech_test_payment_api.Models
{
    public class VendedorModel
    {
        private int id;
        private string nome;
        private string eMail;
        private string cpf;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string EMail { get => eMail; set => eMail = value; }
        public string Cpf { get => cpf; set => cpf = value; }
    }
}