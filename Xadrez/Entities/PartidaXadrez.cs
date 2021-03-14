﻿using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez.Entities
{
    class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        private int turno;
        private Cor jogadorAtual;
        public bool terminada { get; private set; }

        public PartidaXadrez()
        {
            tab = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.BRANCO;
            terminada = false;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
        }

        private void ColocarPecas()
        {
            tab.ColocarPeca(new Torre(tab, Cor.PRETO), new PosicaoXadrez('a', 8).ToPosicao());
            tab.ColocarPeca(new Cavalo(tab, Cor.PRETO), new PosicaoXadrez('b', 8).ToPosicao());
            tab.ColocarPeca(new Bispo(tab, Cor.PRETO), new PosicaoXadrez('c', 8).ToPosicao());
            tab.ColocarPeca(new Rainha(tab, Cor.PRETO), new PosicaoXadrez('d', 8).ToPosicao());
            tab.ColocarPeca(new Rei(tab, Cor.PRETO), new PosicaoXadrez('e', 8).ToPosicao());
            tab.ColocarPeca(new Bispo(tab, Cor.PRETO), new PosicaoXadrez('f', 8).ToPosicao());
            tab.ColocarPeca(new Cavalo(tab, Cor.PRETO), new PosicaoXadrez('g', 8).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.PRETO), new PosicaoXadrez('h', 8).ToPosicao());

            tab.ColocarPeca(new Peao(tab, Cor.PRETO), new PosicaoXadrez('a', 7).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.PRETO), new PosicaoXadrez('b', 7).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.PRETO), new PosicaoXadrez('c', 7).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.PRETO), new PosicaoXadrez('d', 7).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.PRETO), new PosicaoXadrez('e', 7).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.PRETO), new PosicaoXadrez('f', 7).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.PRETO), new PosicaoXadrez('g', 7).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.PRETO), new PosicaoXadrez('h', 7).ToPosicao());

            tab.ColocarPeca(new Torre(tab, Cor.BRANCO), new PosicaoXadrez('a', 1).ToPosicao());
            tab.ColocarPeca(new Cavalo(tab, Cor.BRANCO), new PosicaoXadrez('b', 1).ToPosicao());
            tab.ColocarPeca(new Bispo(tab, Cor.BRANCO), new PosicaoXadrez('c', 1).ToPosicao());
            tab.ColocarPeca(new Rainha(tab, Cor.BRANCO), new PosicaoXadrez('d', 1).ToPosicao());
            tab.ColocarPeca(new Rei(tab, Cor.BRANCO), new PosicaoXadrez('e', 1).ToPosicao());
            tab.ColocarPeca(new Bispo(tab, Cor.BRANCO), new PosicaoXadrez('f', 1).ToPosicao());
            tab.ColocarPeca(new Cavalo(tab, Cor.BRANCO), new PosicaoXadrez('g', 1).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.BRANCO), new PosicaoXadrez('h', 1).ToPosicao());

            tab.ColocarPeca(new Peao(tab, Cor.BRANCO), new PosicaoXadrez('a', 2).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.BRANCO), new PosicaoXadrez('b', 2).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.BRANCO), new PosicaoXadrez('c', 2).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.BRANCO), new PosicaoXadrez('d', 2).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.BRANCO), new PosicaoXadrez('e', 2).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.BRANCO), new PosicaoXadrez('f', 2).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.BRANCO), new PosicaoXadrez('g', 2).ToPosicao());
            tab.ColocarPeca(new Peao(tab, Cor.BRANCO), new PosicaoXadrez('h', 2).ToPosicao());
        }
    }
}