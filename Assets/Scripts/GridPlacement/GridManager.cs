using UnityEngine;

public class GridManager : GridProperties
{
    [SerializeField] private GameObject housePrefab;
    
    public void Start()
    {

        for (int x = 0; x < GridArray.GetLength(0); x++)
        {
            for (int z = 0; z < GridArray.GetLength(1); z++)
            {
                GridArray[x, z] = new Cell();
            }
        }
    }

    private void Update()
    {   
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = MouseToScreen.GetWorldPosition();
            GetGridPosition(worldPosition, out int x, out int z);
            Instantiate(housePrefab, GetWorldPosition(x, z), Quaternion.identity);
        }
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("Hello");
        Vector3 worldPosition = MouseToScreen.GetWorldPosition();
        Instantiate(housePrefab, worldPosition, Quaternion.identity);
    }
    
}
