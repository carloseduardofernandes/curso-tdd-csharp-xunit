using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AluraLeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Theory]
        [InlineData(new double[] { 100, 1200, 1400, 1300 }, 4)]
        [InlineData(new double[] { 800, 900 }, 2)]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(double[] ofertas, int qtdEsperada)
        {
            //Arranje - cenário
            IModalidadeAvaliacao modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];

                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert - valor esperado
            var qtdObtida = leilao.Lances.Count();

            Assert.Equal(qtdEsperada, qtdObtida);
        }

        

        [Theory]
        [InlineData(new double[] { 5000000000, 7000000000, 9000000000 }, 5000000000)]
        public void IgnoraLanceDadoLanceDoMesmoClienteSeguido(double [] lances, double valorLanceEsperado)
        {
            //Arrange = cenario
            IModalidadeAvaliacao modalidade = new MaiorValor();
            var leilao = new Leilao("Mona Lisa", modalidade);
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();

            //Act = When
            foreach (var lance in lances)
            {
                leilao.RecebeLance(fulano, lance);
            }

            leilao.TerminaPregao();

            var valorLanceObtido = leilao.Ganhador.Valor;

            //Assert = Then
            Assert.Equal(valorLanceEsperado, valorLanceObtido);
        }
    }
}
