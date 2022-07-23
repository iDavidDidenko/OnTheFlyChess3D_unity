using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{

    private readonly static int[] LEGAL_NOVES_COORDINATE = {-17, -15, -10, -6, 6, 10, 15, 17}; // possible moves in board

    public Knight(int i_piecePosition, PieceColor i_pieceColor, PiecesTpye i_pieceType) : base(i_piecePosition, i_pieceColor, i_pieceType, true) { }

    public Knight(int i_piecePosition, PieceColor i_pieceColor, PiecesTpye i_pieceType, bool i_isFirstMove) : base(i_piecePosition, i_pieceColor, i_pieceType, i_isFirstMove) { }
    public override List<Move> calcLegalMoves(Board board)
    {

       List<Move> legalMove = new List<Move>();

       foreach (int i in LEGAL_NOVES_COORDINATE)
       {
            int dataDestCoord = this.m_piecePosition + i;
            if (BoardUtils.isValidTile(dataDestCoord))
            {
                if (isFirstCol(this.m_piecePosition, i) || isSecondCol(this.m_piecePosition, i) ||
                    isSevenCol(this.m_piecePosition, i) || isEightCol(this.m_piecePosition, i))
                {
                    continue;
                }
                Tile dateDestTile = board.getTile(dataDestCoord);
                if (!dateDestTile.isTileOccupied())
                {
                    legalMove.Add(new MajorMove(board, this, dataDestCoord));
                }
                else
                {
                    Piece pieceAtDeat = dateDestTile.getPiece();
                    PieceColor pieceColor = pieceAtDeat.getPieceColor();
                    if(pieceColor != m_pieceColor)
                    {
                        legalMove.Add(new MajorAttackMove(board, this, dataDestCoord, pieceAtDeat));
                    }

                }
            }
       }
        return legalMove;
    
    }
    public override Piece movePiece(Move i_move)
    {
        return new Knight(i_move.getDestinationCoor(), i_move.getMovedPiece().getPieceColor(), PiecesTpye.Knight);
    }

    // bound first col check
    private static bool isFirstCol(int i_currentPosition, int i_dateOffset)
    {
        return BoardUtils.FIRST_COLUM[i_currentPosition] && (i_dateOffset == -17 || i_dateOffset == -10
            || i_dateOffset == 6 || i_dateOffset == 15);
    }
    private static bool isSecondCol(int i_currentPosition, int i_dateOffset)
    {
        return BoardUtils.SECOND_COLUM[i_currentPosition] && (i_dateOffset == -10 || i_dateOffset == 6);
    }   
    private static bool isSevenCol(int i_currentPosition, int i_dateOffset)
    {
        return BoardUtils.SEVEN_COLUM[i_currentPosition] && (i_dateOffset == -6 || i_dateOffset == 10);
    }
    private static bool isEightCol(int i_currentPosition, int i_dateOffset)
    {
        return BoardUtils.EIGHT_COLUM[i_currentPosition] && (i_dateOffset == -15 || i_dateOffset == -6
            || i_dateOffset == 10 || i_dateOffset == 17);
    }
}
