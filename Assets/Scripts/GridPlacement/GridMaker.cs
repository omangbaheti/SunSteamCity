using System;
using TMPro;
using UnityEngine;

public class GridMaker<CellProperty>: MonoBehaviour
{
    [SerializeField] private bool debugMode;    
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int cellSize;
    [SerializeField] private GameObject textMeshPrefab;
    TextMeshProUGUI[,] debugTextArrays; 
    
    protected void Start()
    {
        RectTransform canvas = GetComponentInParent<RectTransform>();
        canvas.sizeDelta = new Vector2(cellSize*width, cellSize*height);
        
        CellProperty[,] gridArray = new CellProperty[width, height];
        debugTextArrays = new TextMeshProUGUI[width, height];
        if (debugMode)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    string gridPosition = x.ToString() + "," + y.ToString() + " ";

                    debugTextArrays[x, y] = CreateWorldText(transform, gridPosition, GetWorldPosition(x, y));
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y +  1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
        }
    }

    protected Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x,0,y) * cellSize;
    }

    protected void GetGridPosition(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        z = Mathf.FloorToInt(worldPosition.z / cellSize);
    }
    
    protected TextMeshProUGUI CreateWorldText(Transform parent, string gridPosition, Vector3 localPosition)
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


