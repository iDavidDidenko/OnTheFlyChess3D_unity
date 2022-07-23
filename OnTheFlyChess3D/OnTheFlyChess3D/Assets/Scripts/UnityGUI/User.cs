using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class User : MonoBehaviour
{
    private Vector3 userCoordinate;
    private List<Vector2> userlegalMove;

    public User(float i_pointX, float i_pointY, float i_pointZ)
    {
        userCoordinate = new Vector3(i_pointX, i_pointY, i_pointZ); 
    }

    public void makeLegalMoves(Board i_logicBoard)
    {
        userlegalMove = new List<Vector2>();
        int x = (int) userCoordinate.x - 1;
        int z = (int) userCoordinate.z - 1;

        for (int i = x; i < x + 3; i++)
        {
            for (int j = z; j < z + 3; j++)
            {
                
                if (i >= 0 && j >= 0 && i <= 7 && j <= 7)
                {
                    int tempIndex = getConvertIndex(i, j);
                    if (i_logicBoard.gameBoard[tempIndex] is EmptyTile)
                    {
                        userlegalMove.Add(new Vector2(i, j));
                    }
                }
            }
        }
    }
    public bool isLegalMove(int i_pointX, int i_pointY)
    {
        foreach (Vector2 v in userlegalMove)
        {
            if(v.x == i_pointX && v.y == i_pointY)
            {
                return true;
            }
        }
        return false;
    }

    public int getUserCoord() { return getConvertIndex((int)userCoordinate.x, (int)userCoordinate.z); }

    public void moveUser(GameObject i_userObj, int i_pointX, int i_pointZ)
    {
        float pointY = 0.077f;
        Vector3 tempVec3 = new Vector3(i_pointX, pointY, i_pointZ);

        i_userObj.transform.position = tempVec3;
        userCoordinate = tempVec3;
    }
    // return a position from two index's
    private int getConvertIndex(int x, int y) { return (8 * x) + y; }

}
