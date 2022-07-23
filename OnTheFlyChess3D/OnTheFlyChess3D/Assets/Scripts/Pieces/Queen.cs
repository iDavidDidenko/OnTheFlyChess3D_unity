using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    private static int[] LEGAL_NOVES_COORDINATE = { -9, -8, -7, -1, 1, 7, 8, 9 };
    public Queen(int i_piecePosition, PieceColor i_pieceColor, PiecesTpye i_pieceType) : base(i_piecePosition, i_pieceColor, i_pieceType, true) { }

    public Queen(int i_piecePosition, PieceColor i_pieceColor, PiecesTpye i_pieceType, bool i_isFirstMove) : base(i_piecePosition, i_pieceColor, i_pieceType, i_isFirstMove) { }

    public override List<Move> calcLegalMoves(Board board)
    {
        List<Move> legalMove = new List<Move>();

        foreach (int i in LEGAL_NOVES_COORDINATE)
        {
            int dateDestCoord = this.m_piecePosition;

            while (BoardUtils.isValidTile(dateDestCoord))
            {
                if (isFirstColum(dateDestCoord, i) || isEightColum(dateDestCoord, i))
                {
                    break;
                }

                dateDestCoord += i;
                if (BoardUtils.isValidTile(dateDestCoord))
                {
                    Tile dateDestTile = board.getTile(dateDestCoord);
                    if (!dateDestTile.isTileOccupied())
                    {
                        legalMove.Add(new MajorMove(board, this, dateDestCoord));
                    }
                    else
                    {
                        Piece pieceAtDeat = dateDestTile.getPiece();
                        PieceColor pieceColor = pieceAtDeat.getPieceColor();
                        if (pieceColor != m_pieceColor)
                        {
                            legalMove.Add(new MajorAttackMove(board, this, dateDestCoord, pieceAtDeat));
                        }
                        break;

                    }
                }
            }


        }

        return legalMove;

    }
    public override Piece movePiece(Move i_move)
    {
        return new Queen(i_move.getDestinationCoor(), i_move.getMovedPiece().getPieceColor(), PiecesTpye.Queen);
    }

    private static bool isFirstColum(int i_currentPosition, int i_dataOffset)
    {
        return BoardUtils.FIRST_COLUM[i_currentPosition] && (i_dataOffset == -1 || i_dataOffset == -9 || i_dataOffset == 7);
    }
    private static bool isEightColum(int i_currentPosition, int i_dataOffset)
    {
        return BoardUtils.EIGHT_COLUM[i_currentPosition] && (i_dataOffset == -7 || i_dataOffset == 1 || i_dataOffset == 9);
    }



}
