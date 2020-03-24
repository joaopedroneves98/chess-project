using board.Board;
using ChessProject.ChessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject {
    class Screen {
        /// <summary>
        /// Prints the board on the console interface
        /// </summary>
        /// <param name="board"></param>
        public static void PrintBoard(Board board) {
            for (int i = 0; i < board.Lines; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++) {
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        /// <summary>
        /// Prints the board with the possible movements for the selected piece
        /// </summary>
        /// <param name="board"></param>
        /// <param name="possibleMoves"></param>
        public static void PrintBoard(Board board, bool[,] possibleMoves) {
            ConsoleColor original = Console.BackgroundColor;
            ConsoleColor newColor = ConsoleColor.DarkGray;
            for (int i = 0; i < board.Lines; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++) {
                    if (possibleMoves[i, j]) {
                        Console.BackgroundColor = newColor;
                    }
                    else {
                        Console.BackgroundColor = original;
                    }
                    PrintPiece(board.GetPiece(i, j));
                    Console.BackgroundColor = original;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = original;
        }

        /// <summary>
        /// Prints a single Piece on the console interface
        /// </summary>
        /// <param name="piece"></param>
        public static void PrintPiece(Piece piece) {

            if (piece == null) {
                Console.Write("- ");
            }
            else {
                if (piece.Color == Color.White) {
                    Console.Write(piece);
                }
                else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        /// <summary>
        /// Reads a position from the command line
        /// </summary>
        /// <returns></returns>
        public static ChessPosition ReadChessPosition() {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }
    }
}
