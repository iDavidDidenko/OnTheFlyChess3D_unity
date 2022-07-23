using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningMove
{
  

    private int getRandomMove()
    {
        return Random.Range(0, 5);
    }

    public Move firstKnowingOpening(Board i_gameBoard, byte i_index)
    {
     
        if(i_index == 0)
        {
            return Move.MoveFactory.createMove(i_gameBoard, 52, 36);
        }
        if(i_index == 1)
        {
             return Move.MoveFactory.createMove(i_gameBoard, 12, 28);
        }
        if(i_index == 2)
        {
            return Move.MoveFactory.createMove(i_gameBoard, 62, 45);
        }

   
      
        return null;

    }

   

}
