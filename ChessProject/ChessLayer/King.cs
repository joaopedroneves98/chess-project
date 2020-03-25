using board.Board;
using ChessProject.ChessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject.chess {
    class King : Piece{

        public ChessGame Game { get; private set; }
        public King(Board board, Color color, ChessGame game) : base(color, board) {
            Game = game;
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

            // SPECIAL PLAY CASTLING (ROQUE)
            if (NumberOfMovements == 0 && !Game.Check) {
                // SMALL CASTLING
                Position posRook = new Position(Position.Line, Position.Column + 3);
                if (testRookForCastling(posRook)) {
                    // Check if position next to king and rook is available
                    Position p1 = new Position(Position.Line, Position.Column + 1); // Position next to the king
                    Position p2 = new Position(Position.Line, Position.Column + 2); // Position next to the rook
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null) {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }
            }


            return mat;
        }

        /// <summary>
        /// Check if the rook is suitable for Castling
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool testRookForCastling(Position pos) {
            Piece p = Board.GetPiece(pos);
            return p != null && p is Rook && p.Color == Color && p.NumberOfMovements == 0;
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
