using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject.board {
    class BoardException : Exception{
    
        public BoardException(string message) : base(message) { }
    }
}
