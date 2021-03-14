using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez.Entities
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override bool[,] MovimentosPossiveis()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
