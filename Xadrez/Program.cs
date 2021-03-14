using System;
using tabuleiro;
using Xadrez.Entities;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.ColocarPeca(new Torre(tab, Cor.PRETO), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(tab, Cor.PRETO), new Posicao(0, 7));

                tab.ColocarPeca(new Torre(tab, Cor.BRANCO), new Posicao(7, 0));
                tab.ColocarPeca(new Torre(tab, Cor.BRANCO), new Posicao(7, 7));

                tab.ColocarPeca(new Cavalo(tab, Cor.PRETO), new Posicao(0, 1));
                tab.ColocarPeca(new Cavalo(tab, Cor.PRETO), new Posicao(0, 6));

                tab.ColocarPeca(new Cavalo(tab, Cor.BRANCO), new Posicao(7, 1));
                tab.ColocarPeca(new Cavalo(tab, Cor.BRANCO), new Posicao(7, 6));

                tab.ColocarPeca(new Bispo(tab, Cor.PRETO), new Posicao(0, 2));
                tab.ColocarPeca(new Bispo(tab, Cor.PRETO), new Posicao(0, 5));

                tab.ColocarPeca(new Bispo(tab, Cor.BRANCO), new Posicao(7, 2));
                tab.ColocarPeca(new Bispo(tab, Cor.BRANCO), new Posicao(7, 5));

                tab.ColocarPeca(new Rei(tab, Cor.PRETO), new Posicao(0, 4));
                tab.ColocarPeca(new Rainha(tab, Cor.PRETO), new Posicao(0, 3));

                tab.ColocarPeca(new Rei(tab, Cor.BRANCO), new Posicao(7, 4));
                tab.ColocarPeca(new Rainha(tab, Cor.BRANCO), new Posicao(7, 3));

                Tela.ImprimirTabuleiro(tab);

                Console.WriteLine();
            }
            catch (TabuleiroException e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
