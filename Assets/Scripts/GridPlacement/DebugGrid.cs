using TMPro;
using UnityEngine;

[RequireComponent(typeof(GridManager))]
public class DebugGrid : GridProperties
{
    public bool canDebug;
    public GameObject textMeshPrefab;
    private TextMeshProUGUI[,] debugTextArrays = new TextMeshProUGUI[Width, Height]; 
    
    
    void Start()
    {
        RectTransform canvas = GetComponentInParent<RectTransform>();
        canvas.sizeDelta = new Vector2(CellSize * Width, CellSize *Height);
        if(!canDebug) return;

        for (int x = 0; x < GridArray.GetLength(0); x++)
        {
            for (int z = 0; z < GridArray.GetLength(1); z++)
            {
                string gridPosition = GridArray[x,z].canBuild.ToString();
                debugTextArrays[x, z] = CreateWorldText(transform, gridPosition, GetWorldPosition(x, z));
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z +  1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
            }
        }
    }
    
    public TextMeshProUGUI CreateWorldText(Transform parent, string gridPosition, Vector3 localPosition)
    {
        GameObject textMeshPro = Instantiate(textMeshPrefab, parent);
        TextMeshProUGUI textMesh = textMeshPro.GetComponent<TextMeshProUGUI>();
        RectTransform tmpTransform = textMeshPro.GetComponent<RectTransform>();
        tmpTransform.sizeDelta = new Vector2(CellSize, CellSize);
        tmpTransform.localPosition = localPosition;
        textMesh.text = gridPosition;
        return textMesh;
    }

 
}
