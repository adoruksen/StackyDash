using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DuringGame : MonoBehaviour
{
    #region Singleton Pattern
    public static DuringGame instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public void RetryButtonHandleEvent()
    {
        LevelManager.gameState = GameState.Finish;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
