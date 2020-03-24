using board.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject.chess {
    class King : Piece{

        public King(Board board, Color color) : base(color, board) {

        }

        public override string ToString() {
            return "K";
        }
    }
}
