using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    private static int[] LEGAL_NOVES_COORDINATE = { -9, -8, -7, -1, 1, 7, 8, 9 };

    public King(int i_piecePosition, PieceColor i_pieceColor, PiecesTpye i_pieceType) : base(i_piecePosition, i_pieceColor, i_pieceType, true) { }

    public King(int i_piecePosition, PieceColor i_pieceColor, PiecesTpye i_pieceType, bool i_isFirstMove) : base(i_piecePosition, i_pieceColor, i_pieceType, i_isFirstMove) { }
    public override List<Move> calcLegalMoves(Board board)
    {

        List<Move> legalmoves = new List<Move>();
        int dataOffset;

        for (int i = 0; i < LEGAL_NOVES_COORDINATE.Length; i++)
        {
            dataOffset = LEGAL_NOVES_COORDINATE[i];
            int destinatoinCoor = m_piecePosition + dataOffset;

            if (isFirstCol(m_piecePosition, dataOffset) || isEightCol(m_piecePosition, dataOffset))
            {
                continue; 
            }


            if (BoardUtils.isValidTile(destinatoinCoor))
            {
              

                Tile destinationTile = board.getTile(destinatoinCoor);
                if (!destinationTile.isTileOccupied()) // if tile is empty 
                {
                    legalmoves.Add(new MajorMove(board, this, destinatoinCoor));
                }
                else
                {
                    Piece pieceAtDestination = destinationTile.getPiece();
                    PieceColor pColor = pieceAtDestination.getPieceColor();

                    if (this.m_pieceColor != pColor) // if tile is no empty and  enemy piece - attaking move
                    {
                        legalmoves.Add(new MajorAttackMove(board, this, destinatoinCoor, pieceAtDestination));



                    }
                }

            }

        }
        return legalmoves;
    }
    public override Piece movePiece(Move i_move)
    {
        return new King(i_move.getDestinationCoor(), i_move.getMovedPiece().getPieceColor(), PiecesTpye.King);
    }
    // bound first col check
    private static bool isFirstCol(int i_currentPosition, int i_dateOffset)
    {
        return BoardUtils.FIRST_COLUM[i_currentPosition] && (i_dateOffset == -9 || i_dateOffset == -1
            || i_dateOffset == 7);
    }
    private static bool isEightCol(int i_currentPosition, int i_dateOffset)
    {
        return BoardUtils.EIGHT_COLUM[i_currentPosition] && (i_dateOffset == -7 || i_dateOffset == 1 || i_dateOffset == 9);
    }
}
