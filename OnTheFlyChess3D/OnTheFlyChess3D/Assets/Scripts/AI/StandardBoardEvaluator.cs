using System.Collections.Generic;

public class StandardBoardEvaluator 
{
    public static int CHECK_BONUS = 45;
    public static int CHECK_MATE_BONUS = 10000;
    public static int DEPTH_BONUS = 100;
    public static int CASTLE_BONUS = 25;
    public static int ATTACK_MULTIPLIER = 2;
    public int evaluate(Board i_board, int i_depth)
    {
        return scorePlayer(i_board, i_board.whitePlayer(), i_depth) - 
               scorePlayer(i_board, i_board.blackPlayer(), i_depth);

    }
    private int scorePlayer(Board i_board, Player i_player, int i_depth)
    {
        return KingSafety(i_player) + pieceValue(i_player) + mobility(i_player) + check(i_player) + checkMake(i_player, i_depth) + castled(i_player) + attacks(i_player);
    }
    private static int attacks(Player i_player)
    {
        int attackScore = 0;

        for(int i = 0; i < i_player.getLegalMoves().Count; i++)
        {
            List<Move> moves = i_player.getLegalMoves()[i];

            foreach(Move move in moves)
            {
                if (move.isAttack())
                {
                    Piece movedPiece = move.getMovedPiece();
                    Piece attackedPiece = move.getAttackedPiece();
                    if (movedPiece.getPieceValue() <= attackedPiece.getPieceValue())
                    {
                        attackScore++;
                    }
                }
            }
        }
        return attackScore * ATTACK_MULTIPLIER;
    }
    private static int castled(Player i_player)
    {
        return i_player.isCastled() ? CASTLE_BONUS : 0;
    }


    private static int checkMake(Player i_player, int i_depth)
    {
        return i_player.getOpponent().isInCheckMake() ? CHECK_MATE_BONUS * depthBonus(i_depth) : 0;
    }
    private static int depthBonus(int i_depth)
    {
        return i_depth == 0 ? 1 : DEPTH_BONUS * i_depth;
    }

    private static int check(Player i_player)
    {
        return i_player.getOpponent().isInCheck() ? CHECK_BONUS : 0;
    }

    private static int mobility(Player i_player)
    {
        return i_player.getLegalMoves().Count;
    }

    private static int pieceValue(Player i_player)
    {
        int pieceValueScore = 0;

        foreach(Piece piece in i_player.getActivePieces())
        {
            pieceValueScore += piece.getPieceValue();
        }

        return pieceValueScore;
    }
    private static int KingSafety(Player i_plyer)
    {
        KingSaftyAnalyzer analyzer = new KingSaftyAnalyzer();
        
        KingDistance kingDis = analyzer.calculateKingTropism(i_plyer);

        return ((kingDis.getEnemyPiece().getPieceValue() / 100) * kingDis.getDistance());


    }
}
