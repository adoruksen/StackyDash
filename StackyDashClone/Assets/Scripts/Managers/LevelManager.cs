using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static GameState gameState;
    public LevelAsset levelAsset;

    private void Start()
    {
        SceneLoadLayer.instance.SceneLoadAnimation();
        CreateLevel();
        
    }
    void CreateLevel()
    {
        if (GameManager.Level <= levelAsset.levels.Length)
        {
            Instantiate(levelAsset.levels[GameManager.Level - 1]);
        }
        else
        {
            Instantiate(levelAsset.levels[Random.Range(0, levelAsset.levels.Length)]);
        }
    }



}
