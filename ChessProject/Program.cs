using board.Board;
using ChessProject.board;
using ChessProject.chess;

namespace ChessProject {
    class Program {
        static void Main(string[] args) {
            try {
                Board b = new Board(8, 8);

                b.PlacePiece(new Rook(b, Color.Black), new Position(0, 0));
                b.PlacePiece(new Rook(b, Color.Black), new Position(1, 3));
                b.PlacePiece(new King(b, Color.Black), new Position(0, 2));

                Screen.PrintBoard(b);
            }
            catch (BoardException e) {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
