using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player 
{
    protected Board m_board;
    protected King m_playerKing;
    protected List<List<Move>> m_legalMoves;
    private bool m_isInCheck;
    public Player(Board i_Board, List<List<Move>> i_legalMoves, List<List<Move>> i_opponentMoves)
    {
        m_board = i_Board;
        m_legalMoves = combine(i_legalMoves, i_opponentMoves);
        m_playerKing = establishKing();
        m_isInCheck = (Player.calcualteAttacksOnTile(m_playerKing.getPiecePosition(), i_opponentMoves)).Count == 0 ? false : true;
    }
    public List<List<Move>> getLegalMoves() { return m_legalMoves; }
    public bool isInStaleMate() { return !m_isInCheck && !hasEscapeMoves(); }
    public bool isInCheckMake() { return m_isInCheck && !hasEscapeMoves(); }
    public King getPlayerKing() { return m_playerKing; }
    public bool isInCheck() { return m_isInCheck; }
    public bool isCastled() { return false; }
    
    public  List<List<Move>> combine(List<List<Move>> i_legalMoves, List<List<Move>> i_opponentMoves)
    {
        List<Move> tempLegalMove = new List<Move>();
        List<Move> tempOpponentMove = new List<Move>();
        
        foreach(List<Move> move in i_legalMoves)
        {
            foreach(Move move2 in move)
            {
                tempLegalMove.Add(move2);
            }
        }
        foreach (List<Move> move in i_opponentMoves)
        {
            foreach (Move move2 in move)
            {
                tempOpponentMove.Add(move2);
            }
        }
        List<Move> combineAfterCalaKingCaslte = calculateKingCastles(tempLegalMove, tempOpponentMove);
        i_legalMoves.Add(combineAfterCalaKingCaslte);
        return i_legalMoves;
    } 

    public static List<Move> calcualteAttacksOnTile(int i_piecePosition, List<List<Move>> i_opponentMoves)
    {
       List<Move> attackingMoves = new List<Move>();
        for(int i = 0 ; i < i_opponentMoves.Count; i++)
        {
            List<Move> temp = i_opponentMoves[i];
            foreach (Move move2 in temp)
            {
                if (i_piecePosition == move2.getDestinationCoor())
                {
                    attackingMoves.Add(move2);
                }
            }
        }
            
        
        return attackingMoves;
    }
    private King establishKing()
    {
        foreach(Piece piece in getActivePieces())
        {
            if (piece.isKing())
            {
                return (King) piece; 
            }
        }
        return null; 
    }
    public bool isMoveLegal(Move i_move)
    {
        for(int i = 0; i < getActivePieces().Count; i++)
        {
            List<Move> move = m_legalMoves[i];
            foreach (Move move2 in move)
            {
                

                if (move2.getCurrentCoord() == i_move.getCurrentCoord() && move2.getDestinationCoor() == i_move.getDestinationCoor())
                {
                    return true; 
                }
            }
        }
        return false;
    }
    protected bool hasEscapeMoves()
    {
        for(int i = 0; i < m_legalMoves.Count; i++)
        {   
            List<Move> temp = m_legalMoves[i];

            foreach (Move move2 in temp)
            {
                MoveTransition transition = makeMove(move2);
                if (transition.getMoveStatus())
                {
                    return true;
                }
            }
        }
            
        
        return false;
    }
    public MoveTransition makeMove(Move i_move)
    {
        if (!isMoveLegal(i_move))
        {
            
            return new MoveTransition(m_board, i_move, iMoveStatus.ILLEGAL_MOVE());
        }
       
        Board transitionBoard = i_move.execute();

        List<Move> kingAttacks = Player.calcualteAttacksOnTile(transitionBoard.currentPlayer().getOpponent().getPlayerKing().getPiecePosition(),
            transitionBoard.currentPlayer().getLegalMoves());

        if(!(kingAttacks.Count == 0))
        {
            return new MoveTransition(m_board, i_move, iMoveStatus.LEVAES_PLAYER_IN_CHECK());
        }
        

        return new MoveTransition(transitionBoard, i_move, iMoveStatus.isDone());
    }
    public abstract List<Piece> getActivePieces();
    public abstract PieceColor getPlayerColor();
    public abstract Player getOpponent();

    public abstract List<Move> calculateKingCastles(List<Move> i_playerLegals, List<Move> i_opponentLegals);

}
