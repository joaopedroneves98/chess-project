using System;
using System.Collections.Generic;
using System.Text;

namespace board.Board {
    class Board {

        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int lines, int columns) {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[Lines, Columns];
        }

        public Piece GetPiece(int line, int column) {
            return Pieces[line, column];
        }

        public void PlacePiece(Piece p, Position pos) {
            Pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }
    }
}
