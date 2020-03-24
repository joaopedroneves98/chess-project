using board.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject.chess {
    class Pawn : Piece {

        public Pawn(Board board, Color color) : base(color, board) {

        }

        public override bool[,] PossibleMovements() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return "P";
        }
    }
}
