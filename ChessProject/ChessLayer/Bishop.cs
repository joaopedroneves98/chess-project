using board.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject.chess {
    class Bishop : Piece {

        public Bishop(Board board, Color color) : base(color, board) {

        }

        public override bool[,] PossibleMovements() {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            // NO
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Line - 1, pos.Column - 1);
            }

            // NE
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Line - 1, pos.Column + 1);
            }

            // SE
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Line + 1, pos.Column + 1);
            }

            // SO
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Line + 1, pos.Column - 1);
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
            return "B";
        }
    }
}
