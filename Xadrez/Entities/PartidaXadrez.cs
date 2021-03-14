using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez.Entities
{
    class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }

        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }

        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaXadrez()
        {
            tab = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.BRANCO;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }

        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if(tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }

            if(jogadorAtual != tab.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }

            if (!tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida" +
                    "!");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.Peca(origem).PodeMoverPara(destino)) 
            {
                throw new TabuleiroException("Posicação de destino inválida!");
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            turno++;
            MudaJogador();
        }

        private void MudaJogador()
        {
            if (jogadorAtual == Cor.BRANCO)
            {
                jogadorAtual = Cor.PRETO;
            }
            else
            {
                jogadorAtual = Cor.BRANCO;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca peca in capturadas)
            {
                if(peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }
            return aux;
        }

        public HashSet<Peca> PrecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca peca in capturadas)
            {
                if (peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 8, new Torre(tab, Cor.PRETO));
            ColocarNovaPeca('b', 8, new Cavalo(tab, Cor.PRETO));
            ColocarNovaPeca('c', 8, new Bispo(tab, Cor.PRETO));
            ColocarNovaPeca('d', 8, new Rainha(tab, Cor.PRETO));
            ColocarNovaPeca('e', 8, new Rei(tab, Cor.PRETO));
            ColocarNovaPeca('f', 8, new Bispo(tab, Cor.PRETO));
            ColocarNovaPeca('g', 8, new Cavalo(tab, Cor.PRETO));
            ColocarNovaPeca('h', 8, new Torre(tab, Cor.PRETO));

            ColocarNovaPeca('a', 7, new Peao(tab, Cor.PRETO));
            ColocarNovaPeca('b', 7, new Peao(tab, Cor.PRETO));
            ColocarNovaPeca('c', 7, new Peao(tab, Cor.PRETO));
            ColocarNovaPeca('d', 7, new Peao(tab, Cor.PRETO));
            ColocarNovaPeca('e', 7, new Peao(tab, Cor.PRETO));
            ColocarNovaPeca('f', 7, new Peao(tab, Cor.PRETO));
            ColocarNovaPeca('g', 7, new Peao(tab, Cor.PRETO));
            ColocarNovaPeca('h', 7, new Peao(tab, Cor.PRETO));

            ColocarNovaPeca('a', 1, new Torre(tab, Cor.BRANCO));
            ColocarNovaPeca('b', 1, new Cavalo(tab, Cor.BRANCO));
            ColocarNovaPeca('c', 1, new Bispo(tab, Cor.BRANCO));
            ColocarNovaPeca('d', 1, new Rainha(tab, Cor.BRANCO));
            ColocarNovaPeca('e', 1, new Rei(tab, Cor.BRANCO));
            ColocarNovaPeca('f', 1, new Bispo(tab, Cor.BRANCO));
            ColocarNovaPeca('g', 1, new Cavalo(tab, Cor.BRANCO));
            ColocarNovaPeca('h', 1, new Torre(tab, Cor.BRANCO));

            ColocarNovaPeca('a', 2, new Peao(tab, Cor.BRANCO));
            ColocarNovaPeca('b', 2, new Peao(tab, Cor.BRANCO));
            ColocarNovaPeca('c', 2, new Peao(tab, Cor.BRANCO));
            ColocarNovaPeca('d', 2, new Peao(tab, Cor.BRANCO));
            ColocarNovaPeca('e', 2, new Peao(tab, Cor.BRANCO));
            ColocarNovaPeca('f', 2, new Peao(tab, Cor.BRANCO));
            ColocarNovaPeca('g', 2, new Peao(tab, Cor.BRANCO));
            ColocarNovaPeca('h', 2, new Peao(tab, Cor.BRANCO));
        }
    }
}
