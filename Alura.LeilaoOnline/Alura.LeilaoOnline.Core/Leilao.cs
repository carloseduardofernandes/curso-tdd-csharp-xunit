using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoEmAndamento,
        LeilaoCriado,
        LeilaoFinalizado
    }

    public class Leilao
    {
        private EstadoLeilao Estado { get; set; }
        private Interessada _ultimoCliente { get; set; }
        private IList<Lance> _lances;
        private IModalidadeAvaliacao _avaliador { get; }
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }

        public Leilao(string peca, IModalidadeAvaliacao avaliador)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoCriado;
            _avaliador = avaliador;
        }

        private bool LanceAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento) && 
                (cliente != _ultimoCliente);
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if(LanceAceito(cliente, valor))
            {
                _ultimoCliente = cliente;
                _lances.Add(new Lance(cliente, valor));
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new InvalidOperationException($"Não é possível terminar o pregão sem ele ter sido começado." +
                    $" Para isso, Utilize o método IniciaPregao()");
            }

            Ganhador = _avaliador.Avalia(this);
            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }
}
