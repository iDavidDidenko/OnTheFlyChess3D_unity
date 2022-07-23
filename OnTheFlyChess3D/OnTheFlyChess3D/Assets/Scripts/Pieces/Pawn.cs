using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    private static int[] LEGAL_NOVES_COORDINATE = {8, 16, 7, 9};

    public Pawn(int i_piecePosition, PieceColor i_pieceColor, PiecesTpye i_pieceType) : base(i_piecePosition, i_pieceColor, i_pieceType, true) { }

    public Pawn(int i_piecePosition, PieceColor i_pieceColor, PiecesTpye i_pieceType, bool i_isFirstMove) : base(i_piecePosition, i_pieceColor, i_pieceType, i_isFirstMove) { }
    public override List<Move> calcLegalMoves(Board board)
    {
        List<Move> LegalMove = new List<Move>();
       

        foreach(int dataOffset in LEGAL_NOVES_COORDINATE)
        {
            int DestinationCoor = m_piecePosition + (((int) getPieceColor()) * dataOffset);

            if (!BoardUtils.isValidTile(DestinationCoor))
            {
                continue;
            }
            if(dataOffset == 8 && !board.getTile(DestinationCoor).isTileOccupied())
            {

                if (isPawnPro.isPawnPromotion(this.getPieceColor(),DestinationCoor))
                {
                    Debug.Log("S: " + getPiecePosition() + " D: " + DestinationCoor);
                    LegalMove.Add(new PawnPromotion(new PawnMove(board, this, DestinationCoor)));

                }
                else
                {
                    //Debug.Log("ELSE ------- S: " + getPiecePosition() + " D: " + DestinationCoor);
                    LegalMove.Add(new PawnMove(board, this, DestinationCoor));
                }



            }
            else if(dataOffset == 16 && isFirstMove() && 
                ((BoardUtils.SECOND_ROW[m_piecePosition] && WhichColor.isBlack(this) || 
                (BoardUtils.SEVEN_ROW[m_piecePosition] && WhichColor.isWhite(this)))))
            {
                int behindDestinatoinCoord = this.m_piecePosition + (((int) getPieceColor()) * 8);
                if (!board.getTile(behindDestinatoinCoord).isTileOccupied() && 
                    !board.getTile(DestinationCoor).isTileOccupied())
                {
                    LegalMove.Add(new PawnJump(board, this, DestinationCoor));
                }
            }
            else if(dataOffset == 7 &&
                !((BoardUtils.EIGHT_COLUM[m_piecePosition] && WhichColor.isWhite(this)) || 
                (BoardUtils.FIRST_COLUM[m_piecePosition] && WhichColor.isBlack(this))))
            {
                if (board.getTile(DestinationCoor).isTileOccupied())
                {
                    Piece pieceOn = board.getTile(DestinationCoor).getPiece();
                    if (m_pieceColor != pieceOn.getPieceColor())
                    {
                        if (isPawnPro.isPawnPromotion(this.getPieceColor(), DestinationCoor))
                        {
                            Debug.Log("im inside the second test area");
                            LegalMove.Add(new PawnPromotion(new PawnAttackMove(board, this, DestinationCoor, pieceOn)));
                        }
                        else
                        {
                            LegalMove.Add(new PawnAttackMove(board, this, DestinationCoor, pieceOn));
                        }
                    }
                } 
                else if (board.getEnPassantPawn() != null) 
                {
                    int add = 0;

                    if(this.m_pieceColor == PieceColor.WHITE)
                    {
                        add = 1;
                    }
                    else
                    {
                        add = -1;
                    }

                    if(board.getEnPassantPawn().getPiecePosition() == (this.m_piecePosition + add ))
                    {
                        Piece pieceOnCandidate = board.getEnPassantPawn();
                        if(m_pieceColor != pieceOnCandidate.getPieceColor())
                        {
                            LegalMove.Add(new PawnEnPassantAttackMove(board, this, DestinationCoor, pieceOnCandidate));
                        }
                    }
                }
            }
            else if (dataOffset == 9 &&
               !((BoardUtils.FIRST_COLUM[m_piecePosition] && WhichColor.isWhite(this)) ||
                (BoardUtils.EIGHT_COLUM[m_piecePosition] && WhichColor.isBlack(this))))
            {
                if (board.getTile(DestinationCoor).isTileOccupied())
                {
                    Piece pieceOn = board.getTile(DestinationCoor).getPiece();
                    if (m_pieceColor != pieceOn.getPieceColor())
                    {
                        if (isPawnPro.isPawnPromotion(this.getPieceColor(), DestinationCoor))
                        {
                            Debug.Log("im inside the third test");
                            LegalMove.Add(new PawnPromotion(new PawnAttackMove(board, this, DestinationCoor, pieceOn)));

                        }
                        else
                        {
                            LegalMove.Add(new PawnAttackMove(board, this, DestinationCoor, pieceOn));

                        }
                    }
                }
                else if (board.getEnPassantPawn() != null)
                {
                    int add = 0;

                    if (this.m_pieceColor == PieceColor.WHITE)
                    {
                        add = 1;
                    }
                    else
                    {
                        add = -1;
                    }

                    if (board.getEnPassantPawn().getPiecePosition() == (this.m_piecePosition - add))
                    {
                        Piece pieceOnCandidate = board.getEnPassantPawn();
                        if (m_pieceColor != pieceOnCandidate.getPieceColor())
                        {
                            LegalMove.Add(new PawnEnPassantAttackMove(board, this, DestinationCoor, pieceOnCandidate));
                        }
                    }
                }
            }
        }

        return LegalMove;

    }

    public override Piece movePiece(Move i_move)
    {
        return new Pawn(i_move.getDestinationCoor(), i_move.getMovedPiece().getPieceColor(), PiecesTpye.Pawn);
    }

    public Piece getPromotinPiece()
    {
        return new Queen(this.m_piecePosition, this.m_pieceColor, this.m_pieceType , false);
    }
}
