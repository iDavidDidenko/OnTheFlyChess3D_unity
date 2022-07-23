using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move
{
    protected readonly Board board;
    protected readonly Piece movedPiece;
    protected readonly int destinationCoordinate;
    protected readonly bool isFirstMove;

    public static Move NULL_MOVE = new NullMove();
    public Move(Board i_board, Piece i_movePiece, int i_destinationCoordinate)
    {
        board = i_board;
        movedPiece = i_movePiece;
        destinationCoordinate = i_destinationCoordinate;
        //isFirstMove = movedPiece == null ? false : movedPiece.isFirstMove();
        isFirstMove = movedPiece.isFirstMove();
    }
    public Move(Board i_board, int i_destinationCoordinate)
    {
        board = i_board;
        movedPiece = null;
        destinationCoordinate = i_destinationCoordinate;
        isFirstMove = false;
    }




    public virtual bool isAttack() { return false; }
    public virtual bool isCastlingMove() { return false; }
    public virtual Piece getAttackedPiece() { return null; }
    public int getDestinationCoor() { return destinationCoordinate; }
    public Piece getMovedPiece() { return movedPiece; }
    public virtual int getCurrentCoord() { return getMovedPiece().getPiecePosition(); }

    public Board getBoard() { return board; }   

    // creating new board 
    public virtual Board execute()
    {
        Builder builder = new Builder();
        foreach (Piece piece in board.currentPlayer().getActivePieces())
        {
            if (!movedPiece.Equals(piece))
            {
                builder.setPiece(piece);
            }
        }


        foreach (Piece piece in board.currentPlayer().getOpponent().getActivePieces())
        {
            if (!movedPiece.Equals(piece))
            {
                builder.setPiece(piece);
            }
            
        }
        builder.setPiece(movedPiece.movePiece(this));
        builder.setMoveMaker(board.currentPlayer().getOpponent().getPlayerColor());
        return builder.build();
    }

    public override int GetHashCode()
    {
        int prime = 31;
        int result = 1;
        result = prime * result + destinationCoordinate;
        result = prime * result + movedPiece.GetHashCode();
        result = prime * result + movedPiece.getPiecePosition();
        return result; 
    }
    public override bool Equals(object other)
    {
       if(this == other) { return true; }

       if(!(other is Move)) { return false; }

       Move otherMove = (Move) other;

       return  getCurrentCoord() ==  otherMove.getCurrentCoord() &&
               getDestinationCoor() == otherMove.getDestinationCoor() &&
               getMovedPiece().Equals(otherMove.getMovedPiece());

    }


    public static class MoveFactory
    {
        public static Move createMove(Board i_board, int i_currentCoord, int i_destinationCoord) 
        {


                foreach (List<Move> move in i_board.currentPlayer().getLegalMoves())
                {
                    foreach(Move m in move)
                    {
                   // Debug.Log("inside createMove -> m.currentCoord : " + m.getCurrentCoord() + " m.destCoord : " + m.getDestinationCoor());

                            if (m.getCurrentCoord() == i_currentCoord && m.getDestinationCoor() == i_destinationCoord)
                            {
                                return m;
                            }
                    }
                }
            return null;
        }
    }
}

public class MajorAttackMove : AttackMove
{
    public MajorAttackMove(Board i_board, Piece i_piece, int i_destCoord, Piece i_pieceAttacked) :
                            base(i_board, i_piece, i_destCoord, i_pieceAttacked) { }
    public override bool Equals(object other)
    {
        return this == other || other is MajorAttackMove && base.Equals(other);
    }

}

// regular move 
public class MajorMove : Move
{
    public MajorMove(Board i_board, Piece i_movePiece, int i_destinationCoordinate) : base(i_board, i_movePiece, i_destinationCoordinate) { }

    public override bool Equals(object other)
    {
        return this == other || other is MajorMove && base.Equals(other);
    }

   /* public override string ToString()
    {
        return movedPiece.getPieceType().ToString() + BoardUtils.getPositionAtCoordinate(this.destinationCoordinate);
    }*/

}
// attaking move
public class AttackMove : Move
{
    Piece attackedPiece;
    public AttackMove(Board i_board, Piece i_movePiece, int i_destinationCoordinate, Piece i_attackedPiece) : base(i_board, i_movePiece, i_destinationCoordinate)
    {
        attackedPiece = i_attackedPiece;
    }
  
    public override bool isAttack()
    {
        return true;
    }
    public override Piece getAttackedPiece()
    {
        return attackedPiece;
    }
    public override int GetHashCode()
    {
        return attackedPiece.GetHashCode() + base.GetHashCode();
    }
    public override bool Equals(object other)
    {
        if(this == other) { return true; }

        if(!(other is AttackMove)) { return false; }
        AttackMove otherAttackMove = (AttackMove)other;

        return base.Equals(otherAttackMove) && getAttackedPiece().Equals(otherAttackMove.getAttackedPiece());

    }

}
public class PawnMove : Move
{
    public PawnMove(Board i_board, Piece i_movePiece, int i_destinationCoordinate) : base(i_board, i_movePiece, i_destinationCoordinate) { }

    public override bool Equals(object other)
    {
        return this == other || other is PawnMove && base.Equals(other);
    }

}
public class PawnAttackMove : AttackMove
{
    public PawnAttackMove(Board i_board, Piece i_movePiece, int i_destinationCoordinate, Piece i_attackedPiece) : base(i_board, i_movePiece, i_destinationCoordinate, i_attackedPiece) { }

    public override bool Equals(object other)
    {
        return this == other || other is PawnAttackMove && base.Equals(other);
    }
  


}

public class PawnEnPassantAttackMove : PawnAttackMove
{
    public PawnEnPassantAttackMove(Board i_board, Piece i_movePiece, int i_destinationCoordinate, Piece i_attackedPiece) : base(i_board, i_movePiece, i_destinationCoordinate, i_attackedPiece) { }

    public override bool Equals(object other)
    {
        return this == other || other is PawnEnPassantAttackMove && base.Equals(other);
    }
    public override Board execute()
    {
        Builder builder = new Builder();
        foreach(Piece piece in this.board.currentPlayer().getActivePieces())
        {
            if (!movedPiece.Equals(piece))
            {
                builder.setPiece(piece);
            }
        }
        foreach(Piece piece in this.board.currentPlayer().getOpponent().getActivePieces())
        {
            if (!piece.Equals(this.getAttackedPiece()))
            {
                builder.setPiece(piece);
            }
        }
        builder.setPiece(this.movedPiece.movePiece(this));
        builder.setMoveMaker(this.board.currentPlayer().getOpponent().getPlayerColor());
        return builder.build();
    }
}
public class PawnPromotion : Move
{
    public Move m_decoratedMove;
    public Pawn promotedPawn;
    public PawnPromotion(Move i_decoratedMove) : base(i_decoratedMove.getBoard(), i_decoratedMove.getMovedPiece(), i_decoratedMove.getDestinationCoor())
    {
        m_decoratedMove = i_decoratedMove;
        promotedPawn = (Pawn) i_decoratedMove.getMovedPiece();
    }

    public override Board execute()
    {
        Board pawnMoveBoard = this.m_decoratedMove.execute();
        Builder builder = new Builder();

        foreach (Piece piece in pawnMoveBoard.currentPlayer().getActivePieces())
        {
            if (!promotedPawn.Equals(piece))
            {
                builder.setPiece(piece);
            }
        }
        foreach (Piece piece in pawnMoveBoard.currentPlayer().getOpponent().getActivePieces())
        {
           builder.setPiece(piece);
        }
        builder.setPiece(this.promotedPawn.getPromotinPiece().movePiece(this));
        builder.setMoveMaker(pawnMoveBoard.currentPlayer().getPlayerColor());
        return builder.build(); 

    }
    public override bool isAttack()
    {
        return m_decoratedMove.isAttack();
    }
    public override Piece getAttackedPiece()
    {
        return m_decoratedMove.getAttackedPiece();
    }
    public override int GetHashCode()
    {
        return m_decoratedMove.GetHashCode() + (31 * promotedPawn.GetHashCode());   
    }
    public override bool Equals(object other)
    {
        return this == other || other is PawnPromotion && base.Equals(other);
    }
}


public class PawnJump : Move
{
    public PawnJump(Board i_board, Piece i_movePiece, int i_destinationCoordinate) : base(i_board, i_movePiece, i_destinationCoordinate) { }

    public override Board execute()
    {
        Builder bulder = new Builder();

        foreach(Piece piece in board.currentPlayer().getActivePieces())
        {
            if (!movedPiece.Equals(piece))
            {
                bulder.setPiece(piece);
            }
        }

        foreach(Piece piece in board.currentPlayer().getOpponent().getActivePieces())
        {
            bulder.setPiece(piece);
        }

        Pawn movedPawn = (Pawn)movedPiece.movePiece(this);
        bulder.setPiece(movedPawn);
        bulder.setEnPassantPawn(movedPawn);
        bulder.setMoveMaker(board.currentPlayer().getOpponent().getPlayerColor());
        return bulder.build();
    }
}
 public class CastleMove : Move
{
    public Rook castleRook;
    public int castleRookStart;
    public int castleRookDestination;
    public CastleMove(Board i_board, Piece i_movePiece, int i_destinationCoordinate, Rook i_castleRook, int i_castleRookStart, int i_castleRookDestiantion) :
        base(i_board, i_movePiece, i_destinationCoordinate)
    {
        castleRook = i_castleRook;
        castleRookStart = i_castleRookStart;
        castleRookDestination = i_destinationCoordinate;
    }
    public Rook getCastleRook() { return castleRook; }
    public  override bool isCastlingMove() { return true; }

    public override Board execute()
    {
        Builder builer = new Builder();

        foreach (Piece piece in board.currentPlayer().getActivePieces())
        {
            if (!movedPiece.Equals(piece) && !castleRook.Equals(piece))
            {
                builer.setPiece(piece);
            }
        }

        foreach (Piece piece in board.currentPlayer().getOpponent().getActivePieces())
        {
            builer.setPiece(piece);
        }
        builer.setPiece(movedPiece.movePiece(this));
        builer.setPiece(new Rook(castleRookDestination, castleRook.getPieceColor(), PiecesTpye.Rook));
        builer.setMoveMaker(board.currentPlayer().getOpponent().getPlayerColor());

        return builer.build();
    }

    public override int GetHashCode()
    {
        int prime = 31;
        int result = base.GetHashCode();
        result = prime * result + this.castleRook.GetHashCode();
        result = prime * result + this.castleRookDestination;
        return result;

    }
    public override bool Equals(object other)
    {
       if(this == other)
        {
            return true;
        }
           
       if(!(other is CastleMove))
        {
            return false;
        }
       CastleMove otherCastleMove = (CastleMove)other;
        return base.Equals(otherCastleMove)  && this.castleRook.Equals(otherCastleMove.getCastleRook());
    }
}
public class kingSideCastlMove : CastleMove
{
    public kingSideCastlMove(Board i_board, Piece i_movePiece, int i_destinationCoordinate, Rook i_castleRook, int i_castleRookStart, int i_castleRookDestiantion) : base(i_board, i_movePiece, i_destinationCoordinate, i_castleRook, i_castleRookStart, i_castleRookDestiantion) { }

    public override bool Equals(object other)
    {
        return this == other || other is kingSideCastlMove && base.Equals(other);
    }


    public override string ToString()
    {
        return "0-0";
    }
}
public class QueenSideCastlMove : CastleMove
{
    public QueenSideCastlMove(Board i_board, Piece i_movePiece, int i_destinationCoordinate, Rook i_castleRook, int i_castleRookStart, int i_castleRookDestiantion) : base(i_board, i_movePiece, i_destinationCoordinate, i_castleRook, i_castleRookStart, i_castleRookDestiantion) { }
    public override string ToString()
    {
        return "0-0-0";
    }
    public override bool Equals(object other)
    {
        return this == other || other is QueenSideCastlMove && base.Equals(other);
    }
}

public class NullMove : Move
{
    public NullMove() : base(null, 65) { }

    public new Board execute()
    {
        return null;
    }
    public override int getCurrentCoord()
    {
        return -1;
    }


}

