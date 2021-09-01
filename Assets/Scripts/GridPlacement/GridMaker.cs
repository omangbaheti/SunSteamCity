using TMPro;
using UnityEngine;

public class GridMaker: GridProperties
{
    [SerializeField] private GameObject textMeshPrefab;

    public void Start()
    {

        Cell[,] gridArray = new Cell[Width, Height];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                gridArray[x, z] = new Cell();
            }
        }
    }

    
    
    

    public bool CanBuild(Transform currentTransform)
    {
        return transform == null;
    }

    public void OnGridObjectChanged()
    {
        
    }
    
}


