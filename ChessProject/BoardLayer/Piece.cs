using System;
using System.Collections.Generic;
using System.Text;

namespace board.Board {
    abstract class Piece {

        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int NumberOfMovements { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Color color, Board board) {
            Position = null;
            Color = color;
            Board = board;
            NumberOfMovements = 0;
        }

        /// <summary>
        /// Increments the number of movements
        /// </summary>
        public void IncrementMovements() {
            NumberOfMovements++;
        }

        /// <summary>
        /// Checks if it is possible to move to a given position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool CanMoveTo(Position pos) {
            return PossibleMovements()[pos.Line, pos.Column];
        }

        /// <summary>
        /// Checks if there is any possible movement for the piece
        /// </summary>
        /// <returns></returns>
        public bool IsMovementPossible() {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < Board.Lines; i++) {
                for (int j = 0; j < Board.Columns; j++) {
                    if (mat[i, j]) {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Obtains the possible movements for a specific Piece
        /// </summary>
        /// <returns></returns>
        public abstract bool[,] PossibleMovements();
    }
}
