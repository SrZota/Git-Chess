using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez.Entities
{
    class Rainha : Peca
    {
        public Rainha(Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "D";
        }
    }
}
