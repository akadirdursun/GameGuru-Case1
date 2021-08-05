using UnityEngine;

public class GridTile : MonoBehaviour
{
    private Vector2 gridPos;
    private bool isCrossed;

    public Vector2 GridPos { get => gridPos; set => gridPos = value; }
    public bool IsCrossed { get => isCrossed; set => isCrossed = value; }

    private void OnMouseDown()
    {
        if (isCrossed)
            return;

        isCrossed = true;
        GameManager.Instance.SpawnX(this);
    }
}