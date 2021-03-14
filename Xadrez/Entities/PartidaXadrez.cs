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

        public bool xeque { get; private set; }

        public Peca vulnevarelEnPassant { get; private set; }

        public PartidaXadrez()
        {
            tab = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.BRANCO;
            terminada = false;
            xeque = false;
            vulnevarelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            //Jogada Roque Pequeno
            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = tab.RetirarPeca(origemTorre);
                torre.IncrementarQuantidadeMovimentos();
                tab.ColocarPeca(torre, destinoTorre);
            }

            //Jogada Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = tab.RetirarPeca(origemTorre);
                torre.IncrementarQuantidadeMovimentos();
                tab.ColocarPeca(torre, destinoTorre);
            }

            //Jogada En Passant
            if(p is Peao)
            {
                if(origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if(p.Cor == Cor.BRANCO)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = tab.RetirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.DecrementarQuantidadeMovimentos();
            if(pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.ColocarPeca(p, origem);

            //Jogada Roque Pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = tab.RetirarPeca(destinoTorre);
                torre.DecrementarQuantidadeMovimentos();
                tab.ColocarPeca(torre, origemTorre);
            }

            //Jogada Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = tab.RetirarPeca(destinoTorre);
                torre.DecrementarQuantidadeMovimentos();
                tab.ColocarPeca(torre, origemTorre);
            }

            //Jogada En Passant
            if(p is Peao)
            {
                if(origem.Coluna != destino.Coluna && pecaCapturada == vulnevarelEnPassant)
                {
                    Peca peao = tab.RetirarPeca(destino);
                    Posicao posP;
                    if(p.Cor == Cor.BRANCO)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    tab.ColocarPeca(peao, posP);
                }
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
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstarEmXeque(jogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em Xeque!");
            }

            Peca p = tab.Peca(destino);

            if (EstarEmXeque(Adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (TesteXequeMate(Adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                MudaJogador();
            }

            //Jogada En Passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                vulnevarelEnPassant = p;
            }
            else
            {
                vulnevarelEnPassant = null;
            }

            // #jogadaespecial promocao
            if (p is Peao)
            {
                if ((p.Cor == Cor.BRANCO && destino.Linha == 0) || (p.Cor == Cor.PRETO && destino.Linha == 7))
                {
                    p = tab.RetirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Rainha(tab, p.Cor);
                    tab.ColocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }
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

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca peca in pecas)
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

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.BRANCO)
            {
                return Cor.PRETO;
            }
            else
            {
                return Cor.BRANCO;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstarEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstarEmXeque(cor))
            {
                return false;
            }

            foreach (Peca peca in PecasEmJogo(cor)) 
            {
                bool[,] mat = peca.MovimentosPossiveis();
                for(int i = 0; i < tab.Linhas; i++)
                {
                    for(int j = 0; j < tab.Colunas; j++)
                    {
                        if(mat[i, j])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstarEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }

                        }
                    }
                }
            }
            return true;
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 8, new Torre(tab, Cor.PRETO));
            ColocarNovaPeca('c', 8, new Bispo(tab, Cor.PRETO));
            ColocarNovaPeca('b', 8, new Cavalo(tab, Cor.PRETO));
            ColocarNovaPeca('d', 8, new Rainha(tab, Cor.PRETO));
            ColocarNovaPeca('e', 8, new Rei(tab, Cor.PRETO, this));
            ColocarNovaPeca('f', 8, new Bispo(tab, Cor.PRETO));
            ColocarNovaPeca('g', 8, new Cavalo(tab, Cor.PRETO));
            ColocarNovaPeca('h', 8, new Torre(tab, Cor.PRETO));

            ColocarNovaPeca('a', 7, new Peao(tab, Cor.PRETO, this));
            ColocarNovaPeca('b', 7, new Peao(tab, Cor.PRETO, this));
            ColocarNovaPeca('c', 7, new Peao(tab, Cor.PRETO, this));
            ColocarNovaPeca('d', 7, new Peao(tab, Cor.PRETO, this));
            ColocarNovaPeca('e', 7, new Peao(tab, Cor.PRETO, this));
            ColocarNovaPeca('f', 7, new Peao(tab, Cor.PRETO, this));
            ColocarNovaPeca('g', 7, new Peao(tab, Cor.PRETO, this));
            ColocarNovaPeca('h', 7, new Peao(tab, Cor.PRETO, this));

            ColocarNovaPeca('a', 1, new Torre(tab, Cor.BRANCO));
            ColocarNovaPeca('b', 1, new Cavalo(tab, Cor.BRANCO));
            ColocarNovaPeca('c', 1, new Bispo(tab, Cor.BRANCO));
            ColocarNovaPeca('d', 1, new Rainha(tab, Cor.BRANCO));
            ColocarNovaPeca('e', 1, new Rei(tab, Cor.BRANCO, this));
            ColocarNovaPeca('f', 1, new Bispo(tab, Cor.BRANCO));
            ColocarNovaPeca('g', 1, new Cavalo(tab, Cor.BRANCO));
            ColocarNovaPeca('h', 1, new Torre(tab, Cor.BRANCO));

            ColocarNovaPeca('a', 2, new Peao(tab, Cor.BRANCO, this));
            ColocarNovaPeca('b', 2, new Peao(tab, Cor.BRANCO, this));
            ColocarNovaPeca('c', 2, new Peao(tab, Cor.BRANCO, this));
            ColocarNovaPeca('d', 2, new Peao(tab, Cor.BRANCO, this));
            ColocarNovaPeca('e', 2, new Peao(tab, Cor.BRANCO, this));
            ColocarNovaPeca('f', 2, new Peao(tab, Cor.BRANCO, this));
            ColocarNovaPeca('g', 2, new Peao(tab, Cor.BRANCO, this));
            ColocarNovaPeca('h', 2, new Peao(tab, Cor.BRANCO, this));
        }
    }
}
