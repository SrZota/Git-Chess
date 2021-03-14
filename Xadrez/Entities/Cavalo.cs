using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez.Entities
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "C";
        }
    }
}
