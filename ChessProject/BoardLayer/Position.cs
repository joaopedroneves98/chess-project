using System;
using System.Collections.Generic;
using System.Text;

namespace board.Board {
    class Position {
        public int Line { get; set; }
        public int Column { get; set; }

        public Position(int line, int column) {
            Line = line;
            Column = column;
        }

        /// <summary>
        /// Changes the position's values
        /// </summary>
        /// <param name="line"></param>
        /// <param name="column"></param>
        public void SetValues(int line, int column) {
            Line = line;
            Column = column;
        }

        public override string ToString() {
            return Line
                + ", "
                + Column;
        }
    }
}
