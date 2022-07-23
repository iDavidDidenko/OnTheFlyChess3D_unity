using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGameObjs : MonoBehaviour
{
    // init the Dictionary<short, GameObject> PiecesList to get EASY access to UI piece and control them
    public static Dictionary<short, GameObject> initPieceList(List<GameObject> i_ListObjWhitePieces, List<GameObject> i_ListObjBlackPieces)
    {
        Dictionary<short, GameObject> piecesListHash = new Dictionary<short, GameObject>(32);

        for (short i = 0, j = 48; i < 16; i++, j++)
        {
            GameObject whiteObjPiece = i_ListObjWhitePieces[i];
            GameObject blackObjPiece = i_ListObjBlackPieces[i];
            piecesListHash.Add(i, whiteObjPiece);
            piecesListHash.Add(j, blackObjPiece);
        }
        return piecesListHash;
    }

    // init tiles to connect between  
    public static void initGameObjTiles(GameObject i_ObjTile)
    {
        GameObject initTile;
        Vector3 setTilePosition = Vector3.zero;
        float pointY = -0.364f;
        for (byte i = 0; i < 8; i++)
        {
            for (byte j = 0; j < 8; j++)
            {
                initTile = Instantiate(i_ObjTile);
                initTile.transform.position = new Vector3(i, pointY, j);
                initTile.transform.localScale = new Vector3(1, 1f, 1);
            }
        }
    }
}
