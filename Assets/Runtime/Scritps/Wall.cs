using TMPro;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _wallDataText; 
    
    private BoxCollider2D _boxCollider;
    private float _wallHeight = 1f;
    private Color _wallColor = Color.white;

    public int Level { get; private set; }

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        UpdateWallDataUI();
    }

    private void UpdateWallDataUI()
    {
        _wallDataText.text = $"WALL\nLevel {Level}";
        _wallDataText.color = _wallColor;
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + _wallHeight * 0.5f);
        _wallDataText.rectTransform.position = newPos;
    }

    public void LevelUp()
    {
        Level++;

        if (Level > 0)
        {
            _wallHeight *= 2f;
            _wallColor = Color.blue;
            _boxCollider.enabled = true;
            UpdateWallDataUI();
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 xLeft = new Vector2(transform.position.x - 0.5f, transform.position.y);
        Vector2 xRight = new Vector2(transform.position.x + 0.5f, transform.position.y);
        Vector2 yRight = new Vector2(transform.position.x + 0.5f, transform.position.y + _wallHeight);
        Vector2 yLeft = new Vector2(transform.position.x - 0.5f, transform.position.y + _wallHeight);

        Gizmos.color = _wallColor;
        GizmosUtils.DrawLine(xLeft, xRight);
        GizmosUtils.DrawLine(xRight, yRight);
        GizmosUtils.DrawLine(yRight, yLeft);
        GizmosUtils.DrawLine(yLeft, xLeft);
    }
}
