using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _towerDataText;

    private Color _towerColor = Color.yellow;
    
    public int Level { get; private set; }

    private void Awake()
    {
        UpdateWallDataUI();
    }

    public void LevelUp()
    {
        Level++;

        if (Level > 0)
        {
            UpdateWallDataUI();
        }
    }

    private void UpdateWallDataUI()
    {
        _towerDataText.text = $"TOWER\nLevel {Level}";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _towerColor;

        if (Level != 0)
        {
            DrawTowerLevel1();
        }
        else
        {
            DrawTowerLevel0();
        }
    }

    private void DrawTowerLevel0()
    {
        Vector2 xLeft = new Vector2(transform.position.x - 0.5f, transform.position.y);
        Vector2 xRight = new Vector2(transform.position.x + 0.5f, transform.position.y);
        Vector2 yRight = new Vector2(transform.position.x + 0.5f, transform.position.y + 1f);
        Vector2 yLeft = new Vector2(transform.position.x - 0.5f, transform.position.y + 1f);

        GizmosUtils.DrawLine(xLeft, xRight);
        GizmosUtils.DrawLine(xRight, yRight);
        GizmosUtils.DrawLine(yRight, yLeft);
        GizmosUtils.DrawLine(yLeft, xLeft);
    }

    private void DrawTowerLevel1()
    {
        Vector2 p1 = new Vector2(transform.position.x - 0.75f, transform.position.y);
        Vector2 p2 = new Vector2(transform.position.x - 0.5f, transform.position.y + 2f);

        Vector2 p3 = new Vector2(transform.position.x + 0.75f, transform.position.y);
        Vector2 p4 = new Vector2(transform.position.x + 0.5f, transform.position.y + 2f);

        Vector2 p5 = new Vector2(transform.position.x - 0.375f, transform.position.y + 2.375f);
        Vector2 p6 = new Vector2(transform.position.x + 0.375f, transform.position.y + 2.375f);

        GizmosUtils.DrawLine(p1, p2);
        GizmosUtils.DrawLine(p3, p4);

        GizmosUtils.DrawLine(p4, p2);
        GizmosUtils.DrawLine(p2, p5);
        GizmosUtils.DrawLine(p4, p6);
        GizmosUtils.DrawLine(p5, p6);
    }
}
