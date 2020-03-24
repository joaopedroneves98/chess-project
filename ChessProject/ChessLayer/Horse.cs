using board.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject.chess {
    class Horse : Piece {

        public Horse(Board board, Color color) : base(color, board) {

        }

        public override bool[,] PossibleMovements() {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            pos.SetValues(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line + 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }

        /// <summary>
        /// Evaluates if the piece can move to a certain position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool canMove(Position pos) {
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != Color;
        }

        public override string ToString() {
            return "H";
        }
    }
}
