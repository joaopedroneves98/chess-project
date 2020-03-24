using board.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject.chess {
    class Queen : Piece {

        public Queen(Board board, Color color) : base(color, board) {

        }

        public override string ToString() {
            return "Q";
        }
    }
}
