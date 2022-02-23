using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="LevelAsset",menuName ="ScriptableObjects/Level/LevelAsset",order =2)]
public class LevelAsset : ScriptableObject
{
    public GameObject[] levels;
}
