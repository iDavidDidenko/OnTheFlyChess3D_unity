using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceColor
{
    WHITE = -1,
    BLACK = 1,


   
}
public static class WhichColor
{
    public static Piece m_piece;

    public static bool isBlack(Piece i_piece)
    {
        m_piece = i_piece;
        PieceColor color = m_piece.getPieceColor();

        if(color == PieceColor.BLACK)
        {
            return true;
        }
        return false;
    }
    public static bool isWhite(Piece i_piece)
    {
        m_piece = i_piece;
        PieceColor color = m_piece.getPieceColor();

        if (color == PieceColor.WHITE)
        {
            return true;
        }
        return false;
    }

    public static Player choosePlayer(WhitePlayer i_whitePlayer, BlackPlayer i_blackPlayer, PieceColor i_playerColor)
    {
        return i_playerColor == PieceColor.WHITE ? (Player) i_whitePlayer : (Player) i_blackPlayer;
    }
    

}
public static class isPawnPro
{
    public static bool isPawnPromotion(PieceColor i_color, int i_position)
    {
        if(i_color == PieceColor.BLACK)
        {
          //  Debug.Log("im inside isPawnPro");
            return BoardUtils.FIRST_ROW[i_position];  
        }
       // Debug.Log("im inside isPawnPro 2222");
        return BoardUtils.EIGTH_ROW[i_position]; 
    }
}