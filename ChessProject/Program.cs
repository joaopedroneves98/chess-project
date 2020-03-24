using board.Board;
using ChessProject.board;
using ChessProject.chess;
using ChessProject.ChessLayer;
using System;

namespace ChessProject {
    class Program {
        static void Main(string[] args) {
            try {
                ChessGame game = new ChessGame();

                while (!game.Finished) {
                    Console.Clear();
                    Screen.PrintBoard(game.Board);

                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();
                    Console.Write("Destination: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();

                    game.ExecuteMovement(origin, destination);
                }

            }
            catch (BoardException e) {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
