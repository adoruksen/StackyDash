using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreStartPanel : MonoBehaviour
{
    private void Start()
    {
        LevelManager.gameState = GameState.BeforeStart;
    }

    public void GameStarterTap()
    {
        LevelManager.gameState = GameState.Normal;
        gameObject.SetActive(false);
        DuringGame.instance.gameObject.SetActive(true);
        LevelPanel.instance.transform.GetChild(0).gameObject.SetActive(true);
        LevelPanel.instance.transform.GetChild(1).gameObject.SetActive(true);
    }
}
