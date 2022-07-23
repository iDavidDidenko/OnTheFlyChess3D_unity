using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSaftyAnalyzer
{



    public KingDistance calculateKingTropism(Player player)
    {
        int playerKingSquare = player.getPlayerKing().getPiecePosition();
        List<List<Move>> enemyMoves = player.getOpponent().getLegalMoves();

        Piece closestPiece = null;
        int closestDistance = int.MaxValue;

        for(int i = 0; i < enemyMoves.Count; i++)
        {

            List<Move> moves = enemyMoves[i];


            foreach(Move move in moves)
            {
                int currentDistance = calculateChebyshevDistance(playerKingSquare, move.getDestinationCoor());
                if (currentDistance < closestDistance)
                {
                    closestDistance = currentDistance;
                    closestPiece = move.getMovedPiece();
                }
            }
        }
        return new KingDistance(closestPiece, closestDistance);
    }



    private int calculateChebyshevDistance(int i_kingTileId, int i_enemyAttackTileId)
    {
        int squareOneRank = getRank(i_kingTileId);
        int squareTwoRank = getRank(i_enemyAttackTileId);

        int squareOneFile = getFile(i_kingTileId);
        int squareTwoFile = getFile(i_enemyAttackTileId);

        int rankDistance = Mathf.Abs(squareTwoRank - squareOneRank);
        int fileDistance = Mathf.Abs(squareTwoFile - squareOneFile);

        return Mathf.Max(rankDistance, fileDistance);
    }

    private int getFile(int i_coord)
    {
        if (BoardUtils.FIRST_COLUM[i_coord])
        {
            return 1;
        }
        else if (BoardUtils.SECOND_COLUM[i_coord])
        {
            return 2;
        }
        else if (BoardUtils.THIRD_COLUM[i_coord])
        {
            return 3;
        }
        else if (BoardUtils.FOURTH_COLUM[i_coord])
        {
            return 4;
        }
        else if (BoardUtils.FIRST_COLUM[i_coord])
        {
            return 5;
        }
        else if (BoardUtils.SIXTH_COLUM[i_coord])
        {
            return 6;
        }
        else if (BoardUtils.SEVEN_COLUM[i_coord])
        {
            return 7;
        }
        else if (BoardUtils.EIGHT_COLUM[i_coord])
        {
            return 8;
        }
        return -1;
    }

    public int getRank(int i_coord)
    {
        if (BoardUtils.FIRST_ROW[i_coord])
        {
            return 1;
        }
        else if (BoardUtils.SECOND_ROW[i_coord])
        {
            return 2;
        }
        else if (BoardUtils.THIRD_ROW[i_coord])
        {
            return 3;
        }
        else if (BoardUtils.FOURTH_ROW[i_coord])
        {
            return 4;
        }
        else if (BoardUtils.FIVE_ROW[i_coord])
        {
            return 5;
        }
        else if (BoardUtils.SIXTH_ROW[i_coord])
        {
            return 6;
        }
        else if (BoardUtils.SEVEN_ROW[i_coord])
        {
            return 7;
        }
        else if (BoardUtils.EIGTH_ROW[i_coord])
        {
            return 8;
        }

        return -1;
    }

}

public class KingDistance
{
    public Piece enemyPiece;
    public int distance;

    public KingDistance(Piece i_enemyDistance, int i_distance)
    {
        this.enemyPiece = i_enemyDistance;
        this.distance = i_distance;
    }
    public Piece getEnemyPiece() { return enemyPiece; }

    public int getDistance() { return distance; }   

    public int tropismScore() { return (this.enemyPiece.getPieceValue() / 10) * this.distance; }
}