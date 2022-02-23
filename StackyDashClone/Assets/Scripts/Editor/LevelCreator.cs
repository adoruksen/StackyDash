using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;




public class LevelCreator : EditorWindow
{
    public GridSpecsForLevel levelParts;
    //int viewIndex = 1;
    Vector2 scrollPos;

    [MenuItem("Window/My Editors/Level Creator")]
    static void CreateLevelCreator()
    {
        GetWindow(typeof(LevelCreator));
    }


    private void OnGUI()
    {
        #region Header
        GUILayout.BeginHorizontal();
        GUILayout.Label("Level Designer Tool", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();
        #endregion

        
        levelParts = (GridSpecsForLevel)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Level/ScriptableObjects/Level.asset", typeof(GridSpecsForLevel));
        if (levelParts !=null)
        {
            #region Create Level Specs Button
            if (GUILayout.Button("Create Level Specifications"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets/Scripts/Level/ScriptableObjects/Level.asset", typeof(GridSpecsForLevel)) as GridSpecsForLevel;
            }
            #endregion

            if (levelParts.gridSpecifications.Count > 0)
            {
                scrollPos = GUILayout.BeginScrollView(scrollPos);

                for (int section = 1; section <= levelParts.gridSpecifications.Count; section++)
                {
                    GUIStyle boardStyle = new GUIStyle("box");
                    boardStyle.padding = new RectOffset(10, 10, 10, 10);
                    boardStyle.margin.left = 32;

                    GUIStyle obstacleStyle = new GUIStyle("popup");
                    GUIStyle dashStyle = new GUIStyle("popup");
                    GUIStyle playerPosStyle = new GUIStyle("popup");
                    GUIStyle portalStyle = new GUIStyle("popup");
                    GUIStyle shifterStyle = new GUIStyle("popup");


                    #region Board Part
                    GUILayout.BeginHorizontal(boardStyle);
                    GUILayout.BeginVertical();

                    GUILayout.Label("Level Part " + section, EditorStyles.boldLabel);
                    levelParts.gridSpecifications[section - 1].boardSize = (Vector2Int)EditorGUILayout.Vector2IntField("Board Size", levelParts.gridSpecifications[section - 1].boardSize, GUILayout.ExpandWidth(false));
                    #region Bridge Part

                    if (section != levelParts.gridSpecifications.Count)
                    {
                        GUILayout.Space(30);
                        GUILayout.Label("Bridge Specs", EditorStyles.boldLabel);
                        GUILayout.BeginVertical();
                        levelParts.gridSpecifications[section - 1].bridgeLenght = (int)EditorGUILayout.IntField("Bridge Lenght", levelParts.gridSpecifications[section - 1].bridgeLenght, GUILayout.ExpandWidth(false));
                        GUILayout.EndVertical();
                    }
                    #endregion


                    #region Build Part Button
                    GUILayout.Space(30);
                    GUILayout.Label("Level Part : " + section + " Functions", EditorStyles.boldLabel);
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Build Part : " + section))
                    {
                        SpawnManager.Spawn(levelParts.gridSpecifications[section - 1], section, levelParts.gridSpecifications.Count);
                    }
                    GUILayout.EndHorizontal();
                    #endregion

                    GUILayout.EndVertical();

                    for (int row = 0; row < levelParts.gridSpecifications[section - 1].boardSize.x; row++)
                    {
                        GUILayout.BeginVertical();
                        for (int column = 0; column < levelParts.gridSpecifications[section - 1].boardSize.y; column++)
                        {
                            EditorGUILayout.BeginHorizontal();

                            if (levelParts.gridSpecifications[section - 1].objType[row, column] == InteractableTypes.Dash)
                            {
                                levelParts.gridSpecifications[section - 1].objType[row, column] =
                                    (InteractableTypes)EditorGUILayout.EnumPopup(levelParts.gridSpecifications[section - 1].objType[row, column], dashStyle);
                            }

                            else if (levelParts.gridSpecifications[section - 1].objType[row, column] == InteractableTypes.Player)
                            {
                                levelParts.gridSpecifications[section - 1].objType[row, column] =
                                    (InteractableTypes)EditorGUILayout.EnumPopup(levelParts.gridSpecifications[section - 1].objType[row, column], playerPosStyle);
                                SpawnManager.SetPlayerPos(row, column, levelParts.gridSpecifications[section - 1]);
                            }

                            else if (levelParts.gridSpecifications[section - 1].objType[row, column] == InteractableTypes.Obstacle)
                            {
                                levelParts.gridSpecifications[section - 1].objType[row, column] =
                                    (InteractableTypes)EditorGUILayout.EnumPopup(levelParts.gridSpecifications[section - 1].objType[row, column], obstacleStyle);
                            }
                            else if (levelParts.gridSpecifications[section - 1].objType[row, column] == InteractableTypes.Portal)
                            {
                                levelParts.gridSpecifications[section - 1].objType[row, column] =
                                    (InteractableTypes)EditorGUILayout.EnumPopup(levelParts.gridSpecifications[section - 1].objType[row, column], portalStyle);
                            }

                            else
                            {
                                levelParts.gridSpecifications[section - 1].objType[row, column] =
                                    (InteractableTypes)EditorGUILayout.EnumPopup(levelParts.gridSpecifications[section - 1].objType[row, column], shifterStyle);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        GUILayout.EndVertical();
                    }
                    EditorGUILayout.EndHorizontal();
                    #endregion
                }

            }
        }
        GUIUtility.ExitGUI();

    }
}
