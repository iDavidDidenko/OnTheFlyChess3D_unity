using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPlayer : Player
{
    public BlackPlayer(Board i_board, List<List<Move>> i_whiteStandMoves, List<List<Move>> i_blackStandMoves) : base(i_board, i_whiteStandMoves, i_blackStandMoves) { }

    public override List<Piece> getActivePieces()
    {
        return m_board.getBlackPieces();
    }

    public override PieceColor getPlayerColor()
    {
        return PieceColor.BLACK;
    }
    public override Player getOpponent()
    {
        return m_board.whitePlayer();
    }
    public override List<Move> calculateKingCastles(List<Move> i_playerLegals, List<Move> i_opponentLegals)
    {
       
        List<List<Move>> temp_opponentLegals = new List<List<Move>>(1);

        if (m_playerKing == null)
            return new List<Move>();



        temp_opponentLegals[0] = i_opponentLegals;




        List<Move> kingCastles = new List<Move>();
        Debug.Log(" + " + m_playerKing.isFirstMove());
        if (m_playerKing.isFirstMove() && !isInCheck())
        {
            if (!m_board.getTile(5).isTileOccupied() && !m_board.getTile(6).isTileOccupied())
            {
                Tile rookTile = m_board.getTile(7);
                if (rookTile.isTileOccupied() && rookTile.getPiece().isFirstMove())
                {
                    if (Player.calcualteAttacksOnTile(5, temp_opponentLegals).Count == 0 &&
                       Player.calcualteAttacksOnTile(6, temp_opponentLegals).Count == 0 &&
                       rookTile.getPiece().getPieceType() == PiecesTpye.Rook)
                    {
                        kingCastles.Add(new kingSideCastlMove(m_board, m_playerKing, 6, (Rook)rookTile.getPiece(), rookTile.getTileCoordinate(), 5));

                    }
                }

            }

            if (!m_board.getTile(1).isTileOccupied() &&
               !m_board.getTile(2).isTileOccupied() &&
               !m_board.getTile(3).isTileOccupied())
            {
                Tile rookTile = m_board.getTile(0);
                if (rookTile.isTileOccupied() && rookTile.getPiece().isFirstMove() &&
                    Player.calcualteAttacksOnTile(2, temp_opponentLegals).Count == 0 &&
                    Player.calcualteAttacksOnTile(3, temp_opponentLegals).Count == 0 &&
                    rookTile.getPiece().getPieceType() == PiecesTpye.King)
                {
                    kingCastles.Add(new QueenSideCastlMove(m_board, m_playerKing, 2, (Rook)rookTile.getPiece(), rookTile.getTileCoordinate(), 3));
                }


            }

        }

        return kingCastles;

    }
}
