using UnityEngine;

public class GridManager : GridMaker
{
    [SerializeField] private GameObject housePrefab;

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
