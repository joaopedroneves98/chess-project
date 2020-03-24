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
        /// Obtains the possible movements for a specific Piece
        /// </summary>
        /// <returns></returns>
        public abstract bool[,] PossibleMovements();
    }
}
