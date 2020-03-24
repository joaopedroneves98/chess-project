using ChessProject.board;
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

        /// <summary>
        /// Obtains the piece for the given Line and Column
        /// </summary>
        /// <param name="line"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public Piece GetPiece(int line, int column) {
            return Pieces[line, column];
        }

        /// <summary>
        /// Obtains the piece for the given Position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Piece GetPiece(Position pos) {
            return Pieces[pos.Line, pos.Column];
        }

        /// <summary>
        /// Places a given Piece on the given Position
        /// </summary>
        /// <param name="p"></param>
        /// <param name="pos"></param>
        public void PlacePiece(Piece p, Position pos) {
            if (PieceExists(pos)) {
                throw new BoardException("There already is a piece in that position!");
            }
            Pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        /// <summary>
        /// Checks if a given position is valid for the current board
        /// </summary>
        /// <param name="pos">Position</param>
        /// <returns></returns>
        public bool ValidPosition(Position pos) {
            if (pos.Line < 0 || pos.Line >= Lines || pos.Column < 0 || pos.Column >= Columns) {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// Checks if a piece already exists in the given position
        /// </summary>
        /// <returns></returns>
        public bool PieceExists(Position pos) {
            ValidatePosition(pos);
            return GetPiece(pos) != null;
        }

        /// <summary>
        /// Validates the given Position
        /// </summary>
        /// <param name="pos"></param>
        public void ValidatePosition(Position pos) {
            if (!ValidPosition(pos)) {
                throw new BoardException("Invalid Position!");
            }
        }

        /// <summary>
        /// Removes a piece from the board
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Piece RemovePiece(Position pos) {
            if (GetPiece(pos) == null) {
                return null;
            }

            Piece aux = GetPiece(pos);
            aux.Position = null;
            Pieces[pos.Line, pos.Column] = null;
            return aux;
        }
    }
}
