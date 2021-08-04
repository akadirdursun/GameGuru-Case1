using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int n;
    [SerializeField] private GameObject gridTilePrefab;

    private List<GameObject> gridTileList = new List<GameObject>();
    private Camera cam;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        cam = Camera.main;
        spriteRenderer = gridTilePrefab.GetComponent<SpriteRenderer>();
        BuildTheGrid();
    }

    public void BuildTheGrid()
    {
        ClearOldGrid();
        ScaleGridTiles();
        Vector2 offset = spriteRenderer.bounds.size;
        Vector2 topLeftCorner = cam.ScreenToWorldPoint(new Vector2(0, Screen.height));
        topLeftCorner += new Vector2(offset.x / 2, -(offset.y / 2));
        for (int x = 0; x < n; x++)
        {
            for (int y = 0; y < n; y++)
            {
                GameObject tile = Instantiate(gridTilePrefab, transform);
                tile.transform.position = topLeftCorner + new Vector2(x * offset.x, -(y * offset.y));
                gridTileList.Add(tile);
            }
        }
    }

    private void ClearOldGrid()
    {
        for (int i = gridTileList.Count - 1; i >= 0; i--)
        {
            Destroy(gridTileList[i]);
            gridTileList.RemoveAt(i);
        }
    }

    private void ScaleGridTiles()
    {
        float w = cam.ViewportToWorldPoint(Vector2.one).x * 2;
        gridTilePrefab.transform.localScale = Vector2.one * (w / n);
    }

    private void OnDisable()
    {
        gridTilePrefab.transform.localScale = Vector2.one;
    }

    //private void garbage()
    //{
    //    ClearOldGrid();
    //    ScaleGridTiles();
    //    Vector2 spawnPos = cam.ScreenToWorldPoint(new Vector2(0, Screen.height));
    //    Vector2 offset = spriteRenderer.bounds.size;
    //    Debug.Log(spriteRenderer.bounds.size);
    //    Debug.Log(offset.x);
    //    for (int i = 0; i < n; i++)
    //    {
    //        for (int j = 0; j < n; j++)
    //        {
    //            GameObject tile = Instantiate(gridTilePrefab, transform);
    //            tile.transform.position = new Vector2((i * offset.x) - (offset.x / 2), spawnPos.y - (j * offset.y) - ((offset.y / 2)));
    //            gridTileList.Add(tile);
    //        }
    //    }
    //}
}
