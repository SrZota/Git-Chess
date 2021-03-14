﻿using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }

        public int Colunas { get; set; }

        public Peca[,] pecas;

        public Tabuleiro()
        {

        }

        public Tabuleiro(int linhas, int colunas)
        {
            this.Linhas = linhas;
            this.Colunas = Colunas;
            pecas = new Peca
                [linhas, colunas];
        }
    }
}