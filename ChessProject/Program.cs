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

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    bool[,] possibleMoves = game.Board.GetPiece(origin).PossibleMovements(); // Gets the possible movements for the selected piece

                    Console.Clear();
                    Screen.PrintBoard(game.Board, possibleMoves); // Prints the board with the possible moves

                    Console.WriteLine();
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
