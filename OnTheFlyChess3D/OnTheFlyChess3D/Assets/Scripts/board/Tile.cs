using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile
{
    protected readonly int tileCoordinate;

    private static readonly Dictionary<int, EmptyTile> EMPTY_TILES = createAllPossibleEmptyTiles();

    public Tile(int i_tileCoordinate)
    {
        tileCoordinate = i_tileCoordinate;
    }
    public abstract bool isTileOccupied();

    public abstract Piece getPiece();

    public int getTileCoordinate() { return tileCoordinate; }

    private static  Dictionary<int, EmptyTile> createAllPossibleEmptyTiles()
    {
        Dictionary<int, EmptyTile> emptyTileMap = new Dictionary<int, EmptyTile>(64);
        
        for(int i = 0; i < 64; i++)
        {   
            emptyTileMap.Add(i, new EmptyTile(i));
        }
        return emptyTileMap;
    }

    public static Tile createTile(int i_tileCoordinate, Piece i_piece)
    { 
        if(i_piece != null)
        {
            return new OccupiedTile(i_tileCoordinate, i_piece);
        }
        return new EmptyTile(i_tileCoordinate);

    }





}
public class EmptyTile : Tile
{
    public EmptyTile(int i_coordinate) : base(i_coordinate) { }

    public override bool isTileOccupied() { return false; }

    public override Piece getPiece() { return null; }

}
public class OccupiedTile : Tile
{
    private Piece pieceOnTile;

    public OccupiedTile(int i_tileCoordinate, Piece i_pieceOnTile) : base(i_tileCoordinate)
    {
        pieceOnTile = i_pieceOnTile;
    }

    public override bool isTileOccupied() { return true; }

    public override Piece getPiece() { return pieceOnTile; }

}
