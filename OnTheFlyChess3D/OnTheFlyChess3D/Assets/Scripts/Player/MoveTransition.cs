using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class keep all the informaion we need when a piece change his tile - position
public class MoveTransition 
{
    private Board m_transitionBoard;
    private Move m_move;
    private bool m_moveStatus;
        
    public MoveTransition(Board i_board, Move i_move, bool i_moveStatus)
    {
        m_transitionBoard = i_board;
        m_move = i_move;
        m_moveStatus = i_moveStatus;
    }
    public bool getMoveStatus()
    {
        return m_moveStatus;
    }
    public Board getTransitionBoard() { return m_transitionBoard; } 
}
