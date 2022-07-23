using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    // gameObjects init from unity
    public List<GameObject> ListObjWhitePieces;
    public List<GameObject> ListObjBlackPieces;
    public GameObject objTile;
    public GameObject userObj;

    // control unity Piece by code
    private Dictionary<short, GameObject> piecesListHash = new Dictionary<short, GameObject>(32);

    // user details
    private User user = new User(3f, 0.077f, 5f);
    // user lock of first check legal move
    private bool firstCheck = true;

    // logic Board 
    private Board logicBoard = Board.createStandardBoard();

    // opening moves for both 
    private OpeningMove botMovesObj = new OpeningMove();
    private byte botStepMove = 0;

    // Bot 
    MiniMax strategy = new MiniMax(2);

    // Queue for turn's
    private Queue<PiecesTpye> turnQueue = new Queue<PiecesTpye>();

    public int pointX = -1; // Coordinate on the board 
    public int pointZ = -1; // Coordinate on the board

    // object of end game 
    private EndGameState endGameObjState = new EndGameState();

    // data base stream
    private GameDataBase gameDataBase = new GameDataBase();
    private List<string> savedGame;
    private int indexReadGame = 0;

    void Start()
    {
        initPieceList();
        initializePlayerTurn();
        initGameObjTiles();


        // init write stream
        //gameDataBase.initStreamWriter()
        gameDataBase.initStreamRead();
        savedGame = gameDataBase.Read();
    }
    void Update()
    {
        liveGameTurnEngine();

        //createSaveingGame();
        //playKnowingGame();
    }

    // read a saved game to play
    private void playKnowingGame()
    {
        if(indexReadGame < savedGame.Count)
        {
            

            string strLine = savedGame[indexReadGame];

            int diff = strLine.Length - 1;

            string stePoint = ",";
            Debug.LogError("Lengthhhhhhhhhhhhhhhhhhh "+strLine.Length);
            int indexPoint = strLine.IndexOf(stePoint);

            string strSource = strLine.Substring(0, indexPoint);
            Debug.Log("str: " + strLine + " point: " + indexPoint + " s: " + strSource + " d: ");

            string strDest = strLine.Substring(indexPoint + 1, diff - 1);

          

            int source = int.Parse(strSource);

            int dest = int.Parse(strDest);

            if (piecesListHash.ContainsKey((short)dest))
            {
                myDestroyGameObj((byte)dest);
            }


            transGamePiece((byte)source, (byte)dest);


        }
        else
        {
            Debug.LogError("game end! ");
            Debug.Break();
        }
    }

    // create new game by write to file
    private void createSaveingGame()
    {
        Move aiMove;
        MoveTransition transition;
        byte source;
        byte destination;
        
        if (botStepMove < 5)
        {
            aiMove = botMovesObj.firstKnowingOpening(logicBoard, botStepMove);
            botStepMove++;
        }
        else
        {
            aiMove = strategy.execute(logicBoard);

        }
        
        transition = logicBoard.currentPlayer().makeMove(aiMove);

        if (transition.getMoveStatus())
        {
            source = (byte)aiMove.getCurrentCoord();
            destination = (byte)aiMove.getDestinationCoor();

            logicBoard = transition.getTransitionBoard();

            string writeStr = string.Format("{0},{1}", source, destination);
            gameDataBase.Write(writeStr);


            if (logicBoard.m_currentPlayer.isInCheckMake())
            {
                gameDataBase.closeWrite();
                Debug.LogError("finish create game");
                Debug.Break();
                
            }
        }

    }
    // game Processing
    private void liveGameTurnEngine()
    {
        if(turnQueue.Peek() == PiecesTpye.White)
        {
            bothMove();
            turnQueue.Dequeue();
        }
        else if(turnQueue.Peek() == PiecesTpye.User)
        {
            UpdateMouseCoordinates();
        }
        else if(turnQueue.Peek() == PiecesTpye.Black)
        {
            bothMove();
            turnQueue.Dequeue();
        }


        if (endGameObjState.getIsGameEnd()) //game end 
        {
          
            Debug.LogError("******** game end! ********");
            // TODO here
            if (endGameObjState.getIsUserLose())
            {
                Debug.Break();
            }

            if (endGameObjState.getIsBotLose())
            {
                Debug.Break(); 

            }


        }

        if(turnQueue.Count == 0)
        {
            initializePlayerTurn();
        }
    }
    // Both make move
    private void bothMove()
    {
        Move aiMove;
        Tile destTileForDestroy;
        MoveTransition transition;
        byte source;
        byte destination;
        // ToDO:
        if(botStepMove > 5)
        {
            aiMove = botMovesObj.firstKnowingOpening(logicBoard, botStepMove);
            botStepMove++;
        }
        else
        {
            aiMove = strategy.execute(logicBoard);
          
        }
        Debug.Log("test for kind of piece: position!! " + aiMove.getMovedPiece().getPiecePosition() + " !! piece value!! "
          + aiMove.getMovedPiece().getPieceType());

        transition = logicBoard.currentPlayer().makeMove(aiMove);

        if (transition.getMoveStatus())
        {
            source = (byte)aiMove.getCurrentCoord();
            destination = (byte)aiMove.getDestinationCoor();
            destTileForDestroy = logicBoard.gameBoard[destination];

            logicBoard = transition.getTransitionBoard();
          
            if (destTileForDestroy.isTileOccupied())
            {
                myDestroyGameObj(destination);
            }
            
            if(user.getUserCoord() == destination)
            {
                // user lose
                //endGameObjState.setIsUserLose();
                //endGameObjState.setIsGameEnd();
            }

            if (logicBoard.m_currentPlayer.isInCheckMake())
            {
                // user win - bot lose
                endGameObjState.setIsBotLose();
                endGameObjState.setIsGameEnd();
            }

            transGamePiece(source, destination);

        }
        else
        {
            Debug.LogError("transition failed!");
        }

        Debug.LogWarning("test pass: " + testBoard());
    }
    // define X and Y positoin by Mouse
    private void UpdateMouseCoordinates()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Tile", "User")))
        {
           
            Vector3 tempCoord = hit.collider.transform.position;
            pointX = (int)tempCoord.x;
            pointZ = (int)tempCoord.z;
            if (pointX >= 0 && pointZ >= 0 && pointX < 8 && pointZ < 8)
            {
                if (Input.GetMouseButtonDown(0)) // clicked in left click mouse code - 0
                {
                    if (firstCheck)
                    {
                        user.makeLegalMoves(logicBoard);
                        firstCheck = false;
                    }

                    if (user.isLegalMove(pointX, pointZ))
                    {
                        user.moveUser(userObj, pointX, pointZ);
                        turnQueue.Dequeue();
                        firstCheck = true;
                    }

                }
            }
        }
        else
        {
            pointX = -1;
            pointZ = -1;
        }
        
    }
    // queue for turn step
    private void initializePlayerTurn()
    {
        turnQueue.Enqueue(PiecesTpye.White);
        turnQueue.Enqueue(PiecesTpye.User);
        turnQueue.Enqueue(PiecesTpye.Black);
        turnQueue.Enqueue(PiecesTpye.User);
    }
    // Destroy gameObj piece
    private void myDestroyGameObj(byte i_destintion)
    {
        GameObject removeGameObj = piecesListHash[i_destintion];

        if (piecesListHash[i_destintion].gameObject.layer == LayerMask.NameToLayer("White"))
        {
            ListObjWhitePieces.Remove(removeGameObj);
        } 
        else if(piecesListHash[i_destintion].gameObject.layer == LayerMask.NameToLayer("Black"))
        {
            ListObjBlackPieces.Remove(removeGameObj); 
        }
        piecesListHash.Remove(i_destintion);
        Destroy(removeGameObj);
    }
    // transform gameObj position
    private void transGamePiece(byte i_source, byte i_destintion) 
    {
        int[] transPointsXY = getConvertPoints(i_destintion);
        float transPointY = getYpointByTypePiece(i_source);
        piecesListHash[i_source].transform.position = new Vector3(transPointsXY[0], transPointY, transPointsXY[1]);
        changeKeyValueOfPieceDictionary(i_source, i_destintion);
        Debug.Log("change: " + "X: " + transPointsXY[0] + " Z: " + transPointsXY[1] + " source: " + i_source + " destination : " + i_destintion +
            " ListHash size: " + piecesListHash.Count + " configBoard size: " + logicBoard.getBuilderConfig().boardConfig.Count);
    }
    // conver point from logic board to points X Y 
    private int[] getConvertPoints(byte i_index) { return BoardCombin.getPointVirtual(i_index); }

    private float getYpointByTypePiece(byte i_source)
    {
        GameObject pieceByTag = piecesListHash[i_source];

        if(pieceByTag.tag == "Pawn")
        {
            return 0.1f;
        }
        if(pieceByTag.tag == "Rook" || pieceByTag.tag == "King")
        {
            return 0.5f;
        }
        if(pieceByTag.tag == "Knight")
        {
            return 0.544f;
        }
        if(pieceByTag.tag == "Queen")
        {
            return 0.69f;
        }
        if(pieceByTag.tag == "Bishop")
        {
            return 0.46f;
        }

        throw null;
    }
    // change the key (key is the current index where Piece in on the Tile) value of the Dictionary after making move
    private void changeKeyValueOfPieceDictionary(short i_source, short i_destintion)
    {
        GameObject piece = piecesListHash[i_source];
        piecesListHash.Remove(i_source);
        piecesListHash.Add(i_destintion, piece);
    }
    // init the Dictionary<short, GameObject> PiecesList to get EASY access to UI piece and control them
    private void initPieceList()
    {
        for (short i = 0, j = 48; i < 16; i++, j++)
        {
            GameObject whiteObjPiece = ListObjWhitePieces[i];
            GameObject blackObjPiece = ListObjBlackPieces[i];
            piecesListHash.Add(i, whiteObjPiece);
            piecesListHash.Add(j, blackObjPiece);
        }
    }
    // init tiles to connect between  
    private void initGameObjTiles()
    {
        GameObject initTile;
        Vector3 setTilePosition = Vector3.zero;
        float pointY = -0.364f;
        for (byte i = 0; i < 8; i++)
        {
            for(byte j = 0; j < 8; j++ )
            {
                initTile = Instantiate(objTile);
                initTile.transform.position = new Vector3(i, pointY, j);
                initTile.transform.localScale = new Vector3(1,1f,1);
            }
        }
    }

    private bool testBoard()
    {
       int size = piecesListHash.Count;
        Builder b = logicBoard.getBuilderConfig(); 
       

       return true;
    }
}
