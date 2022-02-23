using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneLoadLayer : MonoBehaviour
{
    GameObject img_sceneLoad;

    #region Singleton Pattern
    public static SceneLoadLayer instance;
    private void Awake()
    {
        instance = this;
        img_sceneLoad = transform.GetChild(0).gameObject;

    }
    #endregion

    void Start()
    {
    }

    public void SceneLoadAnimation(bool isStart = true)
    {
        if (isStart)
        {
            img_sceneLoad.transform.localScale = new Vector2(75, 75);
            img_sceneLoad.transform.DOScale(0, 1);
        }
        else
        {
            img_sceneLoad.transform.DOScale(75, 1);
        }
    }

    void Update()
    {
        
    }
}
