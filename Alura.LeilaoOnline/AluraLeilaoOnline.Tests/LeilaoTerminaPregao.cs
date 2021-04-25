using Alura.LeilaoOnline.Core;
using Xunit;

namespace AluraLeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1250, 1200, new double[] { 800, 1150, 1400, 1250})]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(double valorEsperado, double valorDestino,
            double[] ofertas)
        {
            //Arranje
            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];

                if ((i % 2) == 0)
                    leilao.RecebeLance(fulano, valor);
                else
                    leilao.RecebeLance(maria, valor);
            }

            //Act
            leilao.TerminaPregao();

            //Assert
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arranje - cenário
            IModalidadeAvaliacao modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            //Assert - valor esperado
            var excecaoObtida = Assert.Throws<System.InvalidOperationException>(
                //Act - método sob teste
                () => leilao.TerminaPregao()
            );

            var msgEsperada = "Não é possível terminar o pregão sem ele ter sido começado. Para isso, Utilize o método IniciaPregao()";
            Assert.Equal(msgEsperada, excecaoObtida.Message);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arranje - cenário
            IModalidadeAvaliacao modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            leilao.IniciaPregao();

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - valor esperado
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(new double[] { 800, 900, 1000, 990 }, 1000)]//Cenario 1 - leilao varios lances
        [InlineData(new double[] { 800 }, 800)]//Cenario 2 - leilao com 1 lance
        [InlineData(new double[] { 800, 900, 990, 1000 }, 1000)]//Cenario 3 - leilao com varios lances, ordenados por valor
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double [] lances, double valorEsperado)
        {
            //Arranje - cenário
            IModalidadeAvaliacao modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            for (int i = 0; i < lances.Length; i++)
            {
                var valor = lances[i];

                if ((i % 2) == 0)
                    leilao.RecebeLance(fulano, valor);
                else
                    leilao.RecebeLance(maria, valor);
            }

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - valor esperado
            var valorObtido = leilao.Ganhador.Valor;//como não utizamos a pessoa no teste não passado

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
