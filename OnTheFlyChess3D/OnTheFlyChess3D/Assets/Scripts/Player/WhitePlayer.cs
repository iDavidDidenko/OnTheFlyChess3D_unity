using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePlayer : Player
{

    public WhitePlayer(Board i_board, List<List<Move>> i_whiteStandMoves, List<List<Move>> i_blackStandMoves) : base(i_board, i_whiteStandMoves, i_blackStandMoves) { }

    public override List<Piece> getActivePieces()
    {
        return m_board.getWhitePieces();
    }

    public override PieceColor getPlayerColor()
    {
        return PieceColor.WHITE;
    }
    public override Player getOpponent()
    {
        return m_board.blackPlayer(); 
    }
    public override List<Move> calculateKingCastles(List<Move> i_playerLegals, List<Move> i_opponentLegals)
    {
       

        List <List<Move>> temp_opponentLegals = new List<List<Move>>(1);

        if (m_playerKing == null)
            return new List<Move>();


        temp_opponentLegals.Add(i_opponentLegals);



       List<Move> kingCastles = new List<Move>();

       if(m_playerKing.isFirstMove() && !isInCheck())
        {
            if (!m_board.getTile(61).isTileOccupied() && !m_board.getTile(62).isTileOccupied())
            {
                Tile rookTile = m_board.getTile(63);
                if (rookTile.isTileOccupied() && rookTile.getPiece().isFirstMove())
                {
                    if(Player.calcualteAttacksOnTile(61, temp_opponentLegals).Count == 0 &&
                       Player.calcualteAttacksOnTile(62, temp_opponentLegals).Count == 0 &&
                       rookTile.getPiece().getPieceType() == PiecesTpye.Rook)
                    {
                        kingCastles.Add(new kingSideCastlMove(m_board, m_playerKing, 62, (Rook)rookTile.getPiece(), rookTile.getTileCoordinate(), 61));
                    }
                }

            }

            if(!m_board.getTile(59).isTileOccupied() &&
               !m_board.getTile(58).isTileOccupied() &&
               !m_board.getTile(57).isTileOccupied())
            {
                Tile rookTile = m_board.getTile(56);
                if(rookTile.isTileOccupied() && rookTile.getPiece().isFirstMove() &&
                    Player.calcualteAttacksOnTile(58, temp_opponentLegals).Count == 0 &&
                    Player.calcualteAttacksOnTile(59, temp_opponentLegals).Count == 0 &&
                    rookTile.getPiece().getPieceType() == PiecesTpye.King)
                {
                    kingCastles.Add(new QueenSideCastlMove(m_board, m_playerKing, 58, (Rook)rookTile.getPiece(),rookTile.getTileCoordinate(), 59));
                }


            }

        }

        return kingCastles;


    }
    

}
