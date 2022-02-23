using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Level/GridSpecsForEachLevel", order = 1)]
public class GridSpecsForLevel : ScriptableObject
{
    public List<GridSpecs> gridSpecifications;
}
