using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }

    private void InitialSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        InitialSingleton();
    }
    #endregion

    [SerializeField] private GameObject crossMarkPrefab;
    [Header("UI")]
    [SerializeField] private Text matchCountText;

    private Dictionary<Vector2, CrossMark> xMarks = new Dictionary<Vector2, CrossMark>();

    public void SpawnX(GridTile crossedTile)
    {
        GameObject x = Instantiate(crossMarkPrefab);
        x.transform.position = crossedTile.transform.position;
        x.transform.localScale = crossedTile.transform.localScale;
        CrossMark newX = new CrossMark(x, crossedTile);
        xMarks.Add(newX.posOnGrid, newX);
        SearchMatches(newX);
    }

    private void SearchMatches(CrossMark curX)
    {
        List<CrossMark> match = new List<CrossMark>();
        match.Add(curX);
        for (int i = 0; i < match.Count; i++)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x != 0 && y != 0)
                        continue;

                    Vector2 n = match[i].posOnGrid + new Vector2(x, y);
                    if (!xMarks.ContainsKey(n) || match.Contains(xMarks[n]))
                        continue;

                    match.Add(xMarks[n]);
                }
            }
        }
        if (match.Count >= 3)
            RemoveTheMatch(match);
    }

    private void RemoveTheMatch(List<CrossMark> match)
    {
        matchCountText.text = "Match Count: " + match.Count;
        for (int i = match.Count - 1; i >= 0; i--)
        {
            Destroy(match[i].xObject);
            match[i].gridTile.IsCrossed = false;
            xMarks.Remove(match[i].posOnGrid);
        }
    }
    
    public void ResetTheGame()
    {
        foreach (CrossMark x in xMarks.Values)
        {
            Destroy(x.xObject);
        }
        xMarks.Clear();
    }
}