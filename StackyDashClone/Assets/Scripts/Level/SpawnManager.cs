using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    static GameObject grounds;
    static GameObject bridges;
    static GameObject obstacles;
    static GameObject dashes;
    static GameObject finishParts;
    static GameObject portalParts;

    static Vector3 lastDashPos;
    static float firstDash;
    static int dashCount;
    static int bridgeCount;
    static Vector3 lastBridgePos = Vector3.zero;


    public static void Spawn(GridSpecs gridSpecs, int curPart , int lastPart)
    {
        lastDashPos = Vector3.zero;
        grounds = new GameObject("GroundParts" + curPart);
        bridges = new GameObject("Bridges" + curPart);
        obstacles = new GameObject("Obstacles" + curPart);
        dashes = new GameObject("Dashes" + curPart);
        portalParts = new GameObject("PortalParts" + curPart);
       
        if (curPart != 1)
        {
            firstDash = FirstDashPosition(gridSpecs);
            lastBridgePos = FindLastBridge(curPart);
        }
        else
        {
            firstDash = 0;
            lastBridgePos = Vector3.zero;
        }

        for (int row = 0; row < gridSpecs.boardSize.x; row++)
        {
            for (int column = 0; column < gridSpecs.boardSize.y; column++)
            {
                Vector3 pos = new Vector3((row - firstDash + lastBridgePos.x), 0, (gridSpecs.boardSize.y - column) + lastBridgePos.z);
                Instantiator("Ground", pos, grounds.transform, column,gridSpecs);
            }
        }

        for (int row = 0; row < gridSpecs.boardSize.x; row++)
        {
            for (int column = 0; column < gridSpecs.boardSize.y; column++)
            {
                if (gridSpecs.objType[row,column] == InteractableTypes.Obstacle)
                {
                    Vector3 pos = new Vector3(row - firstDash + lastBridgePos.x, 2.1f, (gridSpecs.boardSize.y - column) + lastBridgePos.z);
                    Instantiator("Obstacle", pos, obstacles.transform,column,gridSpecs);
                }
                else if (gridSpecs.objType[row, column] == InteractableTypes.Dash)
                {
                    Vector3 pos = new Vector3(row - firstDash + lastBridgePos.x, 2.1f, (gridSpecs.boardSize.y - column) + lastBridgePos.z);
                    Instantiator("Dash", pos, dashes.transform,column,gridSpecs);
                }
                else if (gridSpecs.objType[row, column] == InteractableTypes.Portal)
                {
                    Vector3 pos = new Vector3(row - firstDash + lastBridgePos.x, 2.1f, (gridSpecs.boardSize.y - column) + lastBridgePos.z);
                    Instantiator("Portal", pos, portalParts.transform, column, gridSpecs);
                }
                else if (gridSpecs.objType[row,column]==InteractableTypes.UpLeft)
                {
                    Vector3 pos = new Vector3(row - firstDash + lastBridgePos.x, 2.1f, (gridSpecs.boardSize.y - column) + lastBridgePos.z);
                    Instantiator("UpLeft", pos, dashes.transform, column, gridSpecs);
                }
                else if (gridSpecs.objType[row, column] == InteractableTypes.UpRight)
                {
                    Vector3 pos = new Vector3(row - firstDash + lastBridgePos.x, 2.1f, (gridSpecs.boardSize.y - column) + lastBridgePos.z);
                    Instantiator("UpRight", pos, dashes.transform, column, gridSpecs);
                }
                else if (gridSpecs.objType[row, column] == InteractableTypes.DownLeft)
                {
                    Vector3 pos = new Vector3(row - firstDash + lastBridgePos.x, 2.1f, (gridSpecs.boardSize.y - column) + lastBridgePos.z);
                    Instantiator("DownLeft", pos, dashes.transform, column, gridSpecs);
                }
                else if (gridSpecs.objType[row, column] == InteractableTypes.DownRight)
                {
                    Vector3 pos = new Vector3(row - firstDash + lastBridgePos.x, 2.1f, (gridSpecs.boardSize.y - column) + lastBridgePos.z);
                    Instantiator("DownRight", pos, dashes.transform, column, gridSpecs);
                }
            }
        }

        if (curPart == lastPart)
        {
            SpawnFinishPart(gridSpecs);
        }
        else
        {
            Vector3 startPos = lastDashPos;
            for (int i = 0; i < gridSpecs.bridgeLenght; i++)
            {
                Vector3 pos = new Vector3(startPos.x, 2.1f, (startPos.z) + i + 1);
                Instantiator("Bridge", pos, bridges.transform,0,gridSpecs);
            }
        }
    }


    static float FirstDashPosition(GridSpecs gridSpecs)
    {
        for (int i = 0; i < gridSpecs.boardSize.x; i++)
        {
            if (gridSpecs.objType[i, gridSpecs.boardSize.y - 1] == InteractableTypes.Dash)
                return i;
        }
        return -1;

    }

    static Vector3 FindLastBridge(int index)
    {
        GameObject lastBridge = GameObject.Find("Bridges" + (index - 1));
        return lastBridge.transform.GetChild(lastBridge.transform.childCount - 1).transform.localPosition;

    }
    static void Instantiator(string objName,Vector3 insPos,Transform parent,int columnValue,GridSpecs gridSpecs)
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/" + objName,typeof(GameObject)) as GameObject,parent);
        obj.transform.position = insPos;
        if (objName=="Dash")
        {
            dashCount++;
            if (columnValue==0)
            {
                lastDashPos = obj.transform.position;
            }
        }
        if (objName == "Bridge")
        {
            bridgeCount++;
            gridSpecs.startPos = obj.transform.position;
        }
    }

    public static void SetPlayerPos(int row,int column, GridSpecs gridSpecs)
    {
        GameObject player =  GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(row, 2.1f, gridSpecs.boardSize.y - column);
    }

    static void SpawnFinishPart(GridSpecs gridSpecs)
    {
        finishParts = new GameObject("FinishParts");
        for (int i = 0; i < gridSpecs.boardSize.x; i++)
        {
            for (int j = 0; j < gridSpecs.boardSize.y; j++)
            {
                Vector3 pos = new Vector3((i - (gridSpecs.boardSize.x / 2) + lastDashPos.x), .2f, (gridSpecs.boardSize.y - j) + lastDashPos.z);
                Instantiator("Ground", pos, finishParts.transform, 0, gridSpecs);
                if (i == gridSpecs.boardSize.x-1 && j==0)
                {
                    Vector3 lastFinishPos = new Vector3(lastDashPos.x,2.15f, (gridSpecs.boardSize.y - j) + lastDashPos.z);
                    SpawnMultipliers(lastFinishPos,gridSpecs);
                }
            }
        }
    }

    static void SpawnMultipliers(Vector3 firstMultPos,GridSpecs gridSpecs)
    {
        int offset = 1;
        float multLength = (dashCount-bridgeCount)/11;
        for (int i = 0; i < multLength; i++)
        {
            Instantiator("FinishMultiplier", new Vector3(firstMultPos.x +1 ,2.15f,firstMultPos.z+offset),finishParts.transform,0,gridSpecs);
            if (i == multLength-1)
            {
                Vector3 lastMultPos = new Vector3(firstMultPos.x, 1.8f, (firstMultPos.z +offset+ 9));
                Instantiator("Finish", lastMultPos, finishParts.transform, 0, gridSpecs);
            }
            offset += 7;
        }
    }
}
