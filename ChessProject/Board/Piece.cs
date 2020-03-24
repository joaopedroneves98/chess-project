using System;
using System.Collections.Generic;
using System.Text;

namespace board.Board {
    class Piece {

        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int NumberOfMovements { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Position position, Color color, Board board) {
            Position = position;
            Color = color;
            Board = board;
            NumberOfMovements = 0;
        }
    }
}
