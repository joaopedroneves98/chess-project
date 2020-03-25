using board.Board;
using ChessProject.board;
using ChessProject.chess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject.ChessLayer {
    class ChessGame {

        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; set; }
        public HashSet<Piece> Pieces { get; private set; }
        public HashSet<Piece> Captured { get; private set; }

        public ChessGame() {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
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
            if (captured != null) {
                Captured.Add(captured);
            }
        }

        /// <summary>
        /// Obtains the captured pieces with the given color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public HashSet<Piece> CapturedPieces(Color color) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (var item in Captured) {
                if (item.Color == color) {
                    aux.Add(item);
                }
            }
            return aux;
        }

        /// <summary>
        /// Obtains the pieces that are still in play
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public HashSet<Piece> PiecesInPlay(Color color) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (var item in Pieces) {
                if (item.Color == color) {
                    aux.Add(item);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        public void ValidateDestination(Position origin, Position destination) {
            if (!Board.GetPiece(origin).CanMoveTo(destination)) {
                throw new BoardException("Invalid destination!");
            }
        }

        /// <summary>
        /// Validation for the original position selected
        /// </summary>
        /// <param name="pos"></param>
        public void ValidateOriginPosition(Position pos) {
            if (Board.GetPiece(pos) == null) {
                throw new BoardException("No piece in the selected position!");
            }
            if (CurrentPlayer != Board.GetPiece(pos).Color) {
                throw new BoardException("The selected piece isn't yours!");
            }
            if (!Board.GetPiece(pos).IsMovementPossible()) {
                throw new BoardException("No possible movements for the selected piece!");
            }
        }

        /// <summary>
        /// Moves the piece to the desired destination
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        public void MakeMove(Position origin, Position destination) {
            ExecuteMovement(origin, destination);
            Turn++;
            changePlayer();
        }

        /// <summary>
        /// Changes the current player
        /// </summary>
        private void changePlayer() {
            CurrentPlayer = CurrentPlayer == Color.White ? CurrentPlayer = Color.Black : CurrentPlayer = Color.White;
        }

        /// <summary>
        /// Places the initial pieces on the board
        /// </summary>
        private void placePieces() {
            PlaceNewPiece('a', 1, new Rook(Board, Color.White));
            PlaceNewPiece('b', 1, new Horse(Board, Color.White));
            PlaceNewPiece('c', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('d', 1, new Queen(Board, Color.White));
            PlaceNewPiece('e', 1, new King(Board, Color.White));
            PlaceNewPiece('f', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('g', 1, new Horse(Board, Color.White));
            PlaceNewPiece('h', 1, new Rook(Board, Color.White));
            PlaceNewPiece('a', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('b', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('c', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('d', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('e', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('f', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('g', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('h', 2, new Pawn(Board, Color.White));

            PlaceNewPiece('a', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('b', 8, new Horse(Board, Color.Black));
            PlaceNewPiece('c', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('d', 8, new Queen(Board, Color.Black));
            PlaceNewPiece('e', 8, new King(Board, Color.Black));
            PlaceNewPiece('f', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('g', 8, new Horse(Board, Color.Black));
            PlaceNewPiece('h', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('a', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('b', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('c', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('d', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('e', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('f', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('g', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('h', 7, new Pawn(Board, Color.Black));
        }

        public void PlaceNewPiece(char column, int line, Piece piece) {
            Board.PlacePiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }
    }
}
