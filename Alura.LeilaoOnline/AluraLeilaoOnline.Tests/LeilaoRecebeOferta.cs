using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AluraLeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(new double[] { 100, 1200, 1400, 1300 }, 4)]
        [InlineData(new Double[] {800, 900 }, 2)]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(double[] ofertas, int qtdEsperada)
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            leilao.IniciaPregao();
            var fulano = new Interessada("Fulano", leilao);

            foreach (var lance in ofertas)
            {
                leilao.RecebeLance(null, lance);
            }

            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert - valor esperado
            var qtdObtida = leilao.Lances.Count();

            Assert.Equal(qtdEsperada, qtdObtida);
        }
    }
}
