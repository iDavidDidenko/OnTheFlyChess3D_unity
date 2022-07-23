using System.Collections;
using System.Collections.Generic;


public enum MoveStatus
{
    DONE = 0,
    ILLEGAL_MOVE = 0,

}

public class iMoveStatus 
{
   public static bool isDone() { return true; }

   public static bool ILLEGAL_MOVE() { return false; }

   public static bool LEVAES_PLAYER_IN_CHECK() { return false; }
}
public class MyTimer
{
    public static void startTimer()
    {
        Timer.startTimer = true;
    }
    // return true if time over
    public static bool getTimer()
    {
        bool temp = Timer.reset;
        Timer.reset = false;
        return temp;
    }
}

