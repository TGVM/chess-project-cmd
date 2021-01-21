using System;
using tabuleiro;
using System.Collections.Generic;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;



        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }

        }

        public void realizaJogada(Posicao origem, Posicao destino) {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        public void validarPosicaoOrigem(Posicao pos) {
            if (tab.peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }if (jogadorAtual != tab.peca(pos).cor) {
                throw new TabuleiroException("A peça escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis()) { 
                throw new TabuleiroException("Não existe movimento possível para a peça selecionada.");
            }
        }

        public void validarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Movimento inválido!");
            }
        }



        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }


        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas() {
            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            //tab.colocarPeca(new Cavalo(tab, Cor.Preta), new PosicaoXadrez('b', 8).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Preta), new PosicaoXadrez('c', 8).toPosicao());
            //tab.colocarPeca(new Rainha(tab, Cor.Preta), new PosicaoXadrez('d', 8).toPosicao());
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta));
            //tab.colocarPeca(new Bispo(tab, Cor.Preta), new PosicaoXadrez('f', 8).toPosicao());
            //tab.colocarPeca(new Cavalo(tab, Cor.Preta), new PosicaoXadrez('g', 8).toPosicao());
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            /*
                        tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('a', 7).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('b', 7).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('c', 7).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('d', 7).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('e', 7).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('f', 7).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('g', 7).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('h', 7).toPosicao());

                        tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('a', 2).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('b', 2).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('d', 2).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('e', 2).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('f', 2).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('g', 2).toPosicao());
                        tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('h', 2).toPosicao());
            */
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            //tab.colocarPeca(new Cavalo(tab, Cor.Branca), new PosicaoXadrez('b', 1).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Branca), new PosicaoXadrez('c', 1).toPosicao());
            //tab.colocarPeca(new Rainha(tab, Cor.Branca), new PosicaoXadrez('d', 1).toPosicao());
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca));
            //tab.colocarPeca(new Bispo(tab, Cor.Branca), new PosicaoXadrez('f', 1).toPosicao());
            //tab.colocarPeca(new Cavalo(tab, Cor.Branca), new PosicaoXadrez('g', 1).toPosicao());
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));

        }

    }
}
