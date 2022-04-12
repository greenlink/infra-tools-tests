namespace AutofacTest
{
    public class TesteService
    {
        private readonly TesteModel _testeModel;
        
        public TesteService(TesteModel testeModel)
        {
            _testeModel = testeModel;
        }

        public string RetornarNomeAtual() => _testeModel.NomeAtual;
    }
}