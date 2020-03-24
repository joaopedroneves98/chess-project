using board.Board;
using ChessProject.chess;

namespace ChessProject {
    class Program {
        static void Main(string[] args) {
            Board b = new Board(8, 8);

            b.PlacePiece(new Tower(b, Color.Black), new Position(0, 0));

            Screen.PrintBoard(b);
        }
    }
}
