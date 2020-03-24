using board.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject.ChessLayer {
    class ChessPosition {

        public char Column { get; set; }
        public int Line { get; set; }

        public ChessPosition(char column, int line) {
            Column = column;
            Line = line;
        }

        public override string ToString() {
            return "" + Column + Line;
        }

        /// <summary>
        /// Converts a ChessPosition to a Position
        /// </summary>
        /// <returns></returns>
        public Position ToPosition() {
            return new Position(8 - Line, Column - 'a');
        }
    }
}
