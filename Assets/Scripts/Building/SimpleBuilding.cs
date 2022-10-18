using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleBuilding : MonoBehaviour
{
    public bool isVertical;
    public Vector2Int placedPosition;
    [SerializeField] private SimpleBuildingType simpleBuildingType;
    [SerializeField] private int price = 10;
    [SerializeField] private int productionRate;
    [SerializeField] private int width;
    [SerializeField] private int length;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject coinAndText;
    
    
    private Material[] allMaterials;
    private List<Color> allColors = new List<Color>();
    
    public SimpleBuildingType SimpleBuildingType => simpleBuildingType;
    public int Price => price;
    public int ProductionRate => productionRate;
    public int Width => width;
    public int Length => length;
    public GameObject Model => model;
    

    public static Dictionary<SimpleBuildingType, Vector2Int> buildingDimensions = new Dictionary<SimpleBuildingType, Vector2Int>()
    {
        {SimpleBuildingType.Farm, new Vector2Int(1,1)},
        {SimpleBuildingType.Lumber, new Vector2Int(1,1)},
        {SimpleBuildingType.Mine, new Vector2Int(1,1)}
    };
    private void Start()
    {
        allMaterials = GetComponentInChildren<Renderer>().materials;
        foreach (Material mat in allMaterials)
            allColors.Add(mat.color);
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Debug.Log("hello");
    //         ShowCoinViz();
    //     }
    // }

    private void OnEnable()
    {
        EconomyManager.MoneyUpdated += ShowCoinViz;
    }

    private void OnDisable()
    {
        EconomyManager.MoneyUpdated -= ShowCoinViz;
    }

    private void ShowCoinViz()
    {
        GameObject cnt=Instantiate(coinAndText, transform.position + 0.1f * Vector3.up, Quaternion.identity);
        cnt.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(productionRate.ToString());
        cnt.GetComponent<Rigidbody>().velocity=0.1f*Vector3.up;
        StartCoroutine(DestroyCoin(cnt));
    }

    private IEnumerator DestroyCoin(GameObject cnt)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(cnt);
    }

    public List<Vector2Int> FillGridPositions(Vector2Int offset, SimpleBuilding simpleBuilding)
    {
        List<Vector2Int> occupiedGridPositions = new List<Vector2Int>();

        for (int x = 0; x < simpleBuilding.Width; x++)
        {
            for (int y = 0; y < simpleBuilding.Length; y++)
            {
                // if (x < 0 || x >= GridProperties.Width || y < 0 || y >= GridProperties.Height) continue;
                occupiedGridPositions.Add(offset + new Vector2Int(x, y));
                Debug.Log($"Filling:{offset + new Vector2Int(x, y)}");
            }
        }

        return occupiedGridPositions;
    }
    
    public void FlashColor(Color tempColor)
    {
        foreach(Material mat in allMaterials)
        {
            mat.color = tempColor;
        }
        Invoke("ResetColor", 0.5f);
    }

    private void ResetColor()
    {
        int i = 0; 
        foreach(Material mat in allMaterials)
        {
            mat.color = allColors[i++];
        }
    }
}

public enum SimpleBuildingType
{
    NULL=-1,
    Farm=0,
    Mine=1,
    Lumber=2,
    Empty=3
}