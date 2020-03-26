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
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }

        public ChessGame() {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            placePieces();
        }

        /// <summary>
        /// Moves a piece from the origin to the destination
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        public Piece ExecuteMovement(Position origin, Position destination) {
            Piece p = Board.RemovePiece(origin);
            p.IncrementMovements();
            Piece captured = Board.RemovePiece(destination);
            Board.PlacePiece(p, destination);
            if (captured != null) {
                Captured.Add(captured);
            }

            // SPECIAL PLAY SMALL CASTLING
            if (p is King && destination.Column == origin.Column + 2) {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDest = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementMovements();
                Board.PlacePiece(rook, rookDest);
            }

            // SPECIAL PLAY BIG CASTLING
            if (p is King && destination.Column == origin.Column - 2) {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDest = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementMovements();
                Board.PlacePiece(rook, rookDest);
            }

            // SPECIAL PLAY EN PASSANT
            if (p is Pawn) {
                if (origin.Column != destination.Column && captured == null) {
                    Position pawnPos;
                    if (p.Color == Color.White) {
                        pawnPos = new Position(destination.Line + 1, destination.Column);
                    }
                    else {
                        pawnPos = new Position(destination.Line - 1, destination.Column);
                    }
                    captured = Board.RemovePiece(pawnPos);
                    Captured.Add(captured);
                }
            }

            return captured;
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

        /// <summary>
        /// Verifies if the king is in Check
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool IsInCheck(Color color) {
            Piece r = King(color);
            if (r == null) {
                throw new BoardException("There is no King for the color " + color + " on the board!");
            }
            foreach (var x in PiecesInPlay(Opponent(color))) {
                bool[,] mat = x.PossibleMovements();
                if (mat[r.Position.Line, r.Position.Column]) { // If the king's position is a possible movement, he is in Check
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Obtains the king for a certain color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private Piece King(Color color) {
            foreach (var x in PiecesInPlay(color)) {
                if (x is King) {
                    return x;
                }
            }
            return null;
        }

        /// <summary>
        /// Obtains the color of the opponent
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private Color Opponent(Color color) {
            return color == Color.White ? Color.Black : Color.White;
        }

        /// <summary>
        /// Validates if the piece can move to a certain position
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        public void ValidateDestination(Position origin, Position destination) {
            Board.ValidatePosition(destination);
            if (!Board.GetPiece(origin).PossibleMovement(destination)) {
                throw new BoardException("Invalid destination!");
            }
        }

        /// <summary>
        /// Validation for the original position selected
        /// </summary>
        /// <param name="pos"></param>
        public void ValidateOriginPosition(Position pos) {
            Board.ValidatePosition(pos);
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
            Piece capturedPiece = ExecuteMovement(origin, destination);

            if (IsInCheck(CurrentPlayer)) {
                UndoMovement(origin, destination, capturedPiece);
                throw new BoardException("You can't put yourself in Check!");
            }

            if (IsInCheck(Opponent(CurrentPlayer))) {
                Check = true;
            }
            else {
                Check = false;
            }

            if (TestCheckmate(Opponent(CurrentPlayer))) {
                Finished = true;
            }
            else {
                Turn++;
                changePlayer();
            }

            //Turn++;
            //changePlayer();

            Piece p = Board.GetPiece(destination);

            // SPECIAL PLAY EN PASSANT
            if (p is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2)) {
                VulnerableEnPassant = p;
            }
            else {
                VulnerableEnPassant = null;
            }
        }

        /// <summary>
        /// Verifies if a player is in checkmate
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool TestCheckmate(Color color) {
            if (!IsInCheck(color)) {
                return false;
            }
            foreach (var x in PiecesInPlay(color)) {
                bool[,] mat = x.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++) {
                    for (int j = 0; j < Board.Columns; j++) {
                        if (mat[i,j]) {
                            Position origin = x.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = ExecuteMovement(origin, destination);
                            bool checkTest = IsInCheck(color);
                            UndoMovement(origin, destination, capturedPiece);
                            if (!checkTest) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Undoes the movement 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="capturedPiece"></param>
        public void UndoMovement(Position origin, Position destination, Piece capturedPiece) {
            Piece p = Board.RemovePiece(destination);
            p.DecrementMovements();
            if (capturedPiece != null) {
                Board.PlacePiece(capturedPiece, destination);
                Captured.Remove(capturedPiece);
            }
            Board.PlacePiece(p, origin);

            // SPECIAL PLAY SMALL CASTLING
            if (p is King && destination.Column == origin.Column + 2) {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDest = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookDest);
                rook.IncrementMovements();
                Board.PlacePiece(rook, rookOrigin);
            }

            // SPECIAL PLAY BIG CASTLING
            if (p is King && destination.Column == origin.Column - 2) {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDest = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookDest);
                rook.IncrementMovements();
                Board.PlacePiece(rook, rookOrigin);
            }

            // SPECIAL PLAY EN PASSANT
            if (p is Pawn) {
                if (origin.Column != destination.Column && capturedPiece == VulnerableEnPassant) {
                    Piece pawn = Board.RemovePiece(destination);
                    Position pawnPos;
                    if (p.Color == Color.White) {
                        pawnPos = new Position(3, destination.Column);
                    }
                    else {
                        pawnPos = new Position(4, destination.Column);
                    }
                    Board.PlacePiece(pawn, pawnPos);
                }
            }
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
            PlaceNewPiece('e', 1, new King(Board, Color.White, this));
            PlaceNewPiece('f', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('g', 1, new Horse(Board, Color.White));
            PlaceNewPiece('h', 1, new Rook(Board, Color.White));
            PlaceNewPiece('a', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('b', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('c', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('d', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('e', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('f', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('g', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('h', 2, new Pawn(Board, Color.White, this));

            PlaceNewPiece('a', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('b', 8, new Horse(Board, Color.Black));
            PlaceNewPiece('c', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('d', 8, new Queen(Board, Color.Black));
            PlaceNewPiece('e', 8, new King(Board, Color.Black, this));
            PlaceNewPiece('f', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('g', 8, new Horse(Board, Color.Black));
            PlaceNewPiece('h', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }

        public void PlaceNewPiece(char column, int line, Piece piece) {
            Board.PlacePiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }
    }
}
