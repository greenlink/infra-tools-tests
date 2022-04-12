namespace AutofacTest
{
    public class TestePresenter
    {
        private readonly TesteService _testeService;
        public TestePresenter(TesteService testeService)
        {
            _testeService = testeService;
        }

        public string RetornarNomeAtual() => _testeService.RetornarNomeAtual();
    }
}