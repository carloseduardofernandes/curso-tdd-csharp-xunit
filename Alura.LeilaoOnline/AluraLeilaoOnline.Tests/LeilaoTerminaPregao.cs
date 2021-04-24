using Alura.LeilaoOnline.Core;
using Xunit;

namespace AluraLeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
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
            var leilao = new Leilao("Van Gogh");
            leilao.IniciaPregao();

            foreach (var lance in lances)
            {
                leilao.RecebeLance(null, lance);//como não utizamos a pessoa no teste não passado
            }

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - valor esperado
            var valorObtido = leilao.Ganhador.Valor;//como não utizamos a pessoa no teste não passado

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
