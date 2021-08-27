using System.Collections.Generic;
using UnityEngine;

public class BuildingAssetTemplate : ScriptableObject
{
    [SerializeField] private List<CoreBuildingTypes> validInputs;
    [SerializeField] private List<CoreBuildingTypes> validOutputs;

    public List<CoreBuildingTypes> ValidInputs => validInputs;
    public List<CoreBuildingTypes> ValidOutputs => validOutputs;
}
