using TMPro;
using UnityEngine;

public class GridMaker: MonoBehaviour
{
    [SerializeField] private bool debugMode;    
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int cellSize;
    [SerializeField] private GameObject textMeshPrefab;
    TextMeshProUGUI[,] debugTextArrays; 
    
    private void Start()
    {
        RectTransform canvas = GetComponentInParent<RectTransform>();
        canvas.sizeDelta = new Vector2(cellSize*width, cellSize*height);
        
        int[,] gridArray = new int[width, height];
        debugTextArrays = new TextMeshProUGUI[width, height];
        if (debugMode)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    string gridPosition = x.ToString() + "," + y.ToString() + " ";

                    debugTextArrays[x, y] = CreateWorldText(transform, gridPosition, GetWorldPosition(x, y));
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white);
                }
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    private void GetGridPosition(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
    }
    
    public TextMeshProUGUI CreateWorldText(Transform parent, string gridPosition, Vector3 localPosition)
    {
        GameObject textMeshPro = Instantiate(textMeshPrefab, parent);
        TextMeshProUGUI textMesh = textMeshPro.GetComponent<TextMeshProUGUI>();
        RectTransform tmpTransform = textMeshPro.GetComponent<RectTransform>();
        tmpTransform.sizeDelta = new Vector2(cellSize, cellSize);
        tmpTransform.localPosition = localPosition;
        textMesh.text = gridPosition;

        return textMesh;
    }
}


