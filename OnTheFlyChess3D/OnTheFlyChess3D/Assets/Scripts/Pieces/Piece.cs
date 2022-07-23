using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece
{
    protected  int m_piecePosition;

    protected  PieceColor m_pieceColor;
    protected  PiecesTpye m_pieceType;

    public bool isFirstMove1;

    private int cachedHashCode;

    protected MajorMove m_majorMove;
    protected AttackMove m_attackMove;

    public Piece(int i_piecePosition, PieceColor i_pieceColor, PiecesTpye i_pieceType, bool i_isFirstMove)
    {
        this.m_piecePosition = i_piecePosition;
        this.m_pieceColor = i_pieceColor;
        this.m_pieceType = i_pieceType;
        // TODO:
        this.isFirstMove1 = i_isFirstMove;
        cachedHashCode = computeHashCode();
    }
    public int getPieceValue() { return (int)this.m_pieceType; }
    public bool isFirstMove() { return this.isFirstMove1; }
    public int getPiecePosition() { return m_piecePosition; }
    public override int GetHashCode() { return cachedHashCode; }
    public bool isKing() { return m_pieceType == PiecesTpye.King; }
    public PieceColor getPieceColor() { return m_pieceColor; }
    public PiecesTpye getPieceType() { return m_pieceType; }
    public override bool Equals(object other)
    {
        if(this == other)
        {
            return true;
        }
        if(!(other is Piece))
        {
            return false;
        }
        Piece otherPiece = (Piece)other;
        return m_piecePosition == otherPiece.getPiecePosition() && m_pieceType == otherPiece.getPieceType()
               && m_pieceColor == otherPiece.getPieceColor() && isFirstMove1 == otherPiece.isFirstMove();
    }
    private int computeHashCode()
    {
        int result = m_pieceType.GetHashCode();
        result = 31 * result + m_piecePosition;
        result = 31 * result + m_pieceColor.GetHashCode();
        result = 31 * result + (isFirstMove1 ? 1 : 0);
        return result;
    } 
    public abstract List<Move> calcLegalMoves(Board board);
    public abstract Piece movePiece(Move i_move);

}
