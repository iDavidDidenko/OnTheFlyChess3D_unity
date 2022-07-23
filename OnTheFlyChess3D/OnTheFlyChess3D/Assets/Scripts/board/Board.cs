using System.Collections.Generic;
using UnityEngine;


public class Board
{
    /// <summary>
    /// game board 
    /// </summary>
    public  List<Tile> gameBoard;

    /// <summary>
    /// list of current pieces.
    /// save the active pieces on the board
    /// </summary>
    public List<Piece> whitePieces;
    public List<Piece> blackPieces;

    /// <summary>
    /// Players
    /// </summary>
    public WhitePlayer m_whitePlayer;
    public BlackPlayer m_blackPlayer;

    public Player m_currentPlayer;


    public List<List<Move>> whiteStandardLegalMoves;
    public List<List<Move>> blackStandardLegalMoves;

    public Pawn enPassantPawn;

    private Builder m_builder;

    public Board(Builder i_builder)
    {
        m_builder = i_builder;
        gameBoard = createGameBoard(i_builder);
        whitePieces = calculateActivePieces(gameBoard, PieceColor.WHITE);
        blackPieces = calculateActivePieces(gameBoard, PieceColor.BLACK);

        enPassantPawn = i_builder.enPassantPawn;

        whiteStandardLegalMoves = calculateLegalMoves(whitePieces);
        blackStandardLegalMoves = calculateLegalMoves(blackPieces);

        m_whitePlayer = new WhitePlayer(this, whiteStandardLegalMoves, blackStandardLegalMoves);
        m_blackPlayer = new BlackPlayer(this, blackStandardLegalMoves, whiteStandardLegalMoves);

        m_currentPlayer = WhichColor.choosePlayer(m_whitePlayer, m_blackPlayer, i_builder.nextMoveMaker);
    }
    public Tile getTile(int i_tileCoordinate) { return gameBoard[i_tileCoordinate]; }
    public Player whitePlayer() { return m_whitePlayer; }
    public Player blackPlayer() { return m_blackPlayer; }
    public List<Piece> getBlackPieces() { return blackPieces; }
    public List<Piece> getWhitePieces() { return whitePieces; }
    public Player currentPlayer() { return m_currentPlayer; }
    public Pawn getEnPassantPawn() { return enPassantPawn; }
    public Builder getBuilderConfig() { return m_builder; }
    public List<List<Move>> calculateLegalMoves(List<Piece> i_pieces)
    {
        List<List<Move>> legalMove = new List<List<Move>>();
        foreach (Piece piece in i_pieces)
        {
            legalMove.Add(piece.calcLegalMoves(this));
        }
        return legalMove;
    }
    // calcualte actually current Pieces and return list. input -> game state (board)
    public static List<Piece> calculateActivePieces(List<Tile> i_gameBoard, PieceColor i_pieceColor)
    {
        List<Piece> activePiece = new List<Piece>(); 

        foreach(Tile tile in i_gameBoard)
        {
            if (tile.isTileOccupied())
            {
                Piece piece = tile.getPiece(); 

                if(piece.getPieceColor() == i_pieceColor)
                {
                    activePiece.Add(piece);
                }
            }
        }
        return activePiece;
    }
    public  List<Tile> createGameBoard(Builder i_builder)
    {
        List<Tile> tiles = new List<Tile>(64);


        for(int i = 0; i < 64; i++)
        {
            tiles.Add(new EmptyTile(i));
        }
        foreach(KeyValuePair<int, Piece> a in i_builder.boardConfig)
        {
            tiles[a.Value.getPiecePosition()] = new OccupiedTile(a.Value.getPiecePosition(), a.Value); 
        }

        return tiles;
    }
    // initial position of the chess board. using Builder Class
    public static Board createStandardBoard()
    {
        Builder builder = new Builder();

        // black
        builder.setPiece(new Rook(0, PieceColor.BLACK, PiecesTpye.Rook));
        builder.setPiece(new Knight(1, PieceColor.BLACK, PiecesTpye.Knight));
        builder.setPiece(new Bishop(2, PieceColor.BLACK, PiecesTpye.Bishop));
        builder.setPiece(new Queen(3, PieceColor.BLACK, PiecesTpye.Queen));
        builder.setPiece(new King(4, PieceColor.BLACK, PiecesTpye.King));
        builder.setPiece(new Bishop(5, PieceColor.BLACK, PiecesTpye.Bishop));
        builder.setPiece(new Knight(6, PieceColor.BLACK, PiecesTpye.Knight));
        builder.setPiece(new Rook(7, PieceColor.BLACK, PiecesTpye.Rook));
        builder.setPiece(new Pawn(8, PieceColor.BLACK, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(9, PieceColor.BLACK, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(10, PieceColor.BLACK, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(11, PieceColor.BLACK, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(12, PieceColor.BLACK, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(13, PieceColor.BLACK, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(14, PieceColor.BLACK, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(15, PieceColor.BLACK, PiecesTpye.Pawn));

        // white
        builder.setPiece(new Pawn(48, PieceColor.WHITE, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(49, PieceColor.WHITE, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(50, PieceColor.WHITE, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(51, PieceColor.WHITE, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(52, PieceColor.WHITE, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(53, PieceColor.WHITE, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(54, PieceColor.WHITE, PiecesTpye.Pawn));
        builder.setPiece(new Pawn(55, PieceColor.WHITE, PiecesTpye.Pawn));
        builder.setPiece(new Rook(56, PieceColor.WHITE, PiecesTpye.Rook));
        builder.setPiece(new Knight(57, PieceColor.WHITE, PiecesTpye.Knight));
        builder.setPiece(new Bishop(58, PieceColor.WHITE, PiecesTpye.Bishop));
        builder.setPiece(new Queen(59, PieceColor.WHITE, PiecesTpye.Queen));
        builder.setPiece(new King(60, PieceColor.WHITE, PiecesTpye.King));
        builder.setPiece(new Bishop(61, PieceColor.WHITE, PiecesTpye.Bishop));
        builder.setPiece(new Knight(62, PieceColor.WHITE, PiecesTpye.Knight));
        builder.setPiece(new Rook(63, PieceColor.WHITE, PiecesTpye.Rook));
        builder.setMoveMaker(PieceColor.WHITE);
        return builder.build();
    }
}
// this class help us to build instance of the board (?????) + build the chess board
public class Builder
{
    public Dictionary<int, Piece> boardConfig;
    public PieceColor nextMoveMaker;
    public Pawn enPassantPawn;
    public Builder()
    {
        boardConfig = new Dictionary<int, Piece>();
    }
    public Board build() { return new Board(this); }
    public Builder setPiece(Piece i_piece)
    {
        if (boardConfig.ContainsKey(i_piece.getPiecePosition()))
        {
            boardConfig.Remove(i_piece.getPiecePosition());
        }

        boardConfig.Add(i_piece.getPiecePosition(), i_piece);
        return this;
    }
    public Builder setMoveMaker(PieceColor i_pieceColor)
    {
        nextMoveMaker = i_pieceColor;
        return this;
    }
    public void setEnPassantPawn(Pawn i_enPassantPawn)
    {
        enPassantPawn = i_enPassantPawn;
    }


}