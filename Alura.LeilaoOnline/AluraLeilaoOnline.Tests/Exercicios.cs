using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AluraLeilaoOnline.Tests
{
    public class Exercicios
    {
        /// <summary>
        /// Como deveríamos modificar o seguinte teste para parametrizar seus dados de entrada com as anotações 
        /// [Theory] e [InlineData]?
        /// </summary>
        [Theory]
        [InlineData (2, 5, 7)]
        public void RetornaSomaDeDoisNumeros(Int32 num1, Int32 num2, Int32 resultadoEsperado)
        {
            //Arrange
            //var num1 = 2;
            //var num2 = 5;
            //var resultadoEsperado = 7;
            //Act
            var resultadoObtido = num1 + num2;
            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtido);
        }

        /// <summary>
        /// Como eu deveria alterar o código corrigido do exercício anterior para acrescentar os três seguintes cenários:
        /// -1 + -1 = -2
        /// 0 + 3 = 3
        /// Int32.MaxValue + 1 = Int32.MaxValue
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <param name="resultadoEsperado"></param>
        [Theory]
        [InlineData(-1, -1, -2)]
        [InlineData(0, 3, 3)]
        [InlineData(Int32.MaxValue, 0, Int32.MaxValue)]
        public void RetornaSomaDeDoisNumerosVariosCenarios(Int32 num1, Int32 num2, Int32 resultadoEsperado)
        {
            //Arrange

            //Act
            var resultadoObtido = num1 + num2;

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtido);
        }
    }
}
