using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: adding finish mate game state to "MoveStatus" to UI as a alarm
// TODO: adding alpha beta
// TODO: for smart AI need to add for each piece his value on a current Tile


public class MiniMax 
{
    private StandardBoardEvaluator boardEvaluator;
    private int searchDepth;
  
    public MiniMax(int i_searchDepth)
    {
        boardEvaluator = new StandardBoardEvaluator();
        this.searchDepth = i_searchDepth;
    }

    public Move execute(Board i_board)
    {
        Move bestMove = null;

        int highestSeenvalue = int.MinValue;
        int lowestSeenValue = int.MaxValue;
        int currentValue;


        int numMoves = i_board.currentPlayer().getLegalMoves().Count;

        for (int i = 0; i < numMoves; i++)
        {
            List<Move> m = i_board.currentPlayer().getLegalMoves()[i];


            foreach (Move move in m)
            {
                MoveTransition moveTran = i_board.currentPlayer().makeMove(move);
                if (moveTran.getMoveStatus())
                {
                    currentValue = i_board.currentPlayer().getPlayerColor() == PieceColor.WHITE ? min(moveTran.getTransitionBoard(), searchDepth - 1, highestSeenvalue, lowestSeenValue) :
                                                                                                  max(moveTran.getTransitionBoard(), searchDepth - 1, highestSeenvalue, lowestSeenValue);
                    if(i_board.currentPlayer().getPlayerColor() == PieceColor.WHITE && currentValue > highestSeenvalue)
                    {
                        highestSeenvalue = currentValue;
                        bestMove = move;
                    }
                    else if (i_board.currentPlayer().getPlayerColor() == PieceColor.BLACK && currentValue < lowestSeenValue)
                    {
                        lowestSeenValue = currentValue;
                        bestMove = move;
                    }

                }
            }

        }
        return bestMove;
    }
    private int calculateAttackBonus(Player i_player, Move i_move)
    {
        int attackBonus = i_move.isAttack() ? 1000 : 0;

        return attackBonus * (i_player.getPlayerColor() == PieceColor.WHITE ? 1 : -1);
    }


    public int min(Board i_board, int i_depth, int i_highest, int i_lowest)
    {
        if(i_depth == 0 || isEndGameSenario(i_board))
        {
            return boardEvaluator.evaluate(i_board, i_depth);
        }
        int currentValue = i_lowest;
        
        for(int i = 0; i < i_board.currentPlayer().getLegalMoves().Count; i++)
        {
            List<Move> m = i_board.currentPlayer().getLegalMoves()[i];

            foreach(Move move in m)
            {
                MoveTransition moveTran = i_board.currentPlayer().makeMove(move);
                if (moveTran.getMoveStatus())
                {
                    currentValue = Mathf.Min(currentValue, max(moveTran.getTransitionBoard(), i_depth - 1, i_highest, currentValue));
                    if(currentValue <= i_highest)
                    {
                        return i_highest;
                    }

                }
            }

        }
        return currentValue;

    }
    public static bool isEndGameSenario(Board i_board)
    {
        return i_board.currentPlayer().isInCheckMake() || i_board.currentPlayer().isInStaleMate();
    }


    public int max(Board i_board, int i_depth, int i_highest, int i_lowest)
    {
        if (i_depth == 0 || isEndGameSenario(i_board))
        {
            return boardEvaluator.evaluate(i_board, i_depth);
        }
        int currentValue = i_highest;

        for (int i = 0; i < i_board.currentPlayer().getLegalMoves().Count; i++)
        {
            List<Move> m = i_board.currentPlayer().getLegalMoves()[i];

            foreach (Move move in m)
            {
                MoveTransition moveTran = i_board.currentPlayer().makeMove(move);
                if (moveTran.getMoveStatus())
                {
                    currentValue = Mathf.Max(currentValue, min(moveTran.getTransitionBoard(), i_depth - 1, currentValue, i_lowest));
                    if (currentValue >= i_lowest)
                    {
                        return i_lowest;
                    }

                }
            }

        }
        return currentValue;
    }
}
