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
    
    protected void Start()
    {
        Cell cell = new Cell(2, 3);
        int cellx= cell.Position.x;


        RectTransform canvas = GetComponentInParent<RectTransform>();
        canvas.sizeDelta = new Vector2(cellSize*width, cellSize*height);
        
        bool[,] gridArray = new bool[width, height];
        debugTextArrays = new TextMeshProUGUI[width, height];
        if (!debugMode) return;
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                string gridPosition = x.ToString() + "," + z.ToString() + " ";

                debugTextArrays[x, z] = CreateWorldText(transform, gridPosition, GetWorldPosition(x, z));
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z +  1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
            }
        }
    }

    protected Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x,0,z) * cellSize;
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

    public bool CanBuild(Transform currentTransform)
    {
        return transform == null;
    }

    public void OnGridObjectChanged()
    {
        
    }
    
}


