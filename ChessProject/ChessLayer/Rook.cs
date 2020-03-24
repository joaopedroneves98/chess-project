using board.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject.chess {
    class Rook : Piece {

        public Rook(Board board, Color color) : base(color, board) {

        }

        /// <summary>
        /// Obtains the possible movements for the Rook
        /// </summary>
        /// <returns></returns>
        public override bool[,] PossibleMovements() {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            // N
            pos.SetValues(Position.Line - 1, Position.Column);
            while (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.Line = pos.Line - 1;
            }

            // S
            pos.SetValues(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.Line = pos.Line + 1;
            }

            // E
            pos.SetValues(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.Column = pos.Column + 1;
            }

            // O
            pos.SetValues(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.Column = pos.Column - 1;
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
            return "R";
        }
    }
}
