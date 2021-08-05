using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int n;
    [SerializeField] private GameObject gridTilePrefab;
    [Header("UI")]
    [SerializeField] private Text nInputTextField;

    private List<GridTile> gridTileList = new List<GridTile>();
    private Camera cam;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        cam = Camera.main;
        spriteRenderer = gridTilePrefab.GetComponent<SpriteRenderer>();
        BuildTheGrid();
    }

    public void RebuildTheGrid()
    {
        n = int.Parse(nInputTextField.text);
        BuildTheGrid();
    }
    private void BuildTheGrid()
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
                GridTile gridTile = Instantiate(gridTilePrefab, transform).GetComponent<GridTile>();
                gridTile.transform.position = topLeftCorner + new Vector2(x * offset.x, -(y * offset.y));
                gridTile.GridPos = new Vector2(x, y);
                gridTileList.Add(gridTile);
            }
        }
    }

    private void ClearOldGrid()
    {
        GameManager.Instance.ResetTheGame();
        for (int i = gridTileList.Count - 1; i >= 0; i--)
        {
            Destroy(gridTileList[i].gameObject);
            gridTileList.RemoveAt(i);
        }
    }

    private void ScaleGridTiles()
    {
        float w = cam.ViewportToWorldPoint(Vector2.one).x * 2;
        gridTilePrefab.transform.localScale = Vector2.one * (w / n);
    }
}
