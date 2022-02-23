using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Consts
{
    public const int maxSize = 20; //gridlerin 20x20 den fazla olma �ans� yok
}

[System.Serializable]
public class GridSpecs
{

    public Vector2Int boardSize;

    [System.NonSerialized] public Vector3 startPos;

    public int bridgeLenght;

    public int scoreLenght;

    public InteractableTypes[,] objType = new InteractableTypes[Consts.maxSize, Consts.maxSize]; //objelerin typelar�na g�re spawn edilmesini kolayla�t�racak board

}




