using board.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject.chess {
    class King : Piece{

        public King(Board board, Color color) : base(color, board) {

        }

        /// <summary>
        /// Obtains the possible movements for the King
        /// </summary>
        /// <returns></returns>
        public override bool[,] PossibleMovements() {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            // N
            pos.SetValues(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            // NE
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            // E
            pos.SetValues(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            // SE
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            // S
            pos.SetValues(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            // SO
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            // O
            pos.SetValues(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            // NO
            pos.SetValues(Position.Line - 1, Position.Column - 1);
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
            return "K";
        }
    }
}
