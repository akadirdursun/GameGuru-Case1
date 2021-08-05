using UnityEngine;

[System.Serializable]
public struct CrossMark
{
    public GameObject xObject { get; private set; }
    public GridTile gridTile { get; private set; }
    public Vector2 posOnGrid { get; private set; }

    public CrossMark(GameObject obj, GridTile tile)
    {
        xObject = obj;
        gridTile = tile;
        posOnGrid = tile.GridPos;
    }
}
