using board.Board;
using ChessProject.chess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject.ChessLayer {
    class ChessGame {

        public Board Board { get; private set; }
        private int turn;
        private Color currentPlayer;
        public bool Finished { get; set; }

        public ChessGame() {
            Board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            Finished = false;
            placePieces();
        }

        /// <summary>
        /// Moves a piece from the origin to the destination
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        public void ExecuteMovement(Position origin, Position destination) {
            Piece p = Board.RemovePiece(origin);
            p.IncrementMovements();
            Piece captured = Board.RemovePiece(destination);
            Board.PlacePiece(p, destination);
        }

        /// <summary>
        /// Places the initial pieces on the board
        /// </summary>
        private void placePieces() {
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.PlacePiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.PlacePiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}
