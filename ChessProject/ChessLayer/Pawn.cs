using board.Board;
using ChessProject.ChessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject.chess {
    class Pawn : Piece {

        public ChessGame Game { get; private set; }
        public Pawn(Board board, Color color, ChessGame game) : base(color, board) {
            Game = game;
        }

        public override bool[,] PossibleMovements() {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            if (Color == Color.White) {
                pos.SetValues(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos) && NumberOfMovements == 0) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && EnemyExists(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && EnemyExists(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                // SPECIAL PLAY EN PASSANT
                if (Position.Line == 3) {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && EnemyExists(left) && Board.GetPiece(left) == Game.VulnerableEnPassant) {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && EnemyExists(right) && Board.GetPiece(right) == Game.VulnerableEnPassant) {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else {
                pos.SetValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos) && NumberOfMovements == 0) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && EnemyExists(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && EnemyExists(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                // SPECIAL PLAY EN PASSANT
                if (Position.Line == 4) {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && EnemyExists(left) && Board.GetPiece(left) == Game.VulnerableEnPassant) {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && EnemyExists(right) && Board.GetPiece(right) == Game.VulnerableEnPassant) {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }

        /// <summary>
        /// Evaluates if there is an enemy in the given position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool EnemyExists(Position pos) {
            Piece p = Board.GetPiece(pos);
            return p != null && p.Color != Color;
        }

        /// <summary>
        /// Checks if the position is free
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool IsFree(Position pos) {
            return Board.GetPiece(pos) == null;
        }

        public override string ToString() {
            return "P";
        }
    }
}
