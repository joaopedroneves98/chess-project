using board.Board;
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
                    if (board.GetPiece(i, j) == null) {
                        Console.Write("- ");
                    }
                    else {
                        PrintPiece(board.GetPiece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        /// <summary>
        /// Prints a single Piece on the console interface
        /// </summary>
        /// <param name="piece"></param>
        public static void PrintPiece(Piece piece) {
            if (piece.Color == Color.White) {
                Console.Write(piece);
            }
            else {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
