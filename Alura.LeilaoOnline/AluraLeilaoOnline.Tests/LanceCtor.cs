using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AluraLeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoLanceValorNegativo()
        {
            IModalidadeAvaliacao modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);

            //Arranje
            var valorNegativo = -100;

            //Assert
            Assert.Throws<ArgumentException>(
                //Act
                 () => new Lance(fulano, valorNegativo)
            );
        }
    }
}
