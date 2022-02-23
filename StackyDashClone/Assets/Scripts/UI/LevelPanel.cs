using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelPanel : MonoBehaviour
{
    #region Singleton Pattern
    public static LevelPanel instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [System.NonSerialized] public GameObject scoreTxt;
    Text levelTxt;
    

    private void Start()
    {
        SetValues();
        LevelTextSetter();
    }

    void SetValues()
    {
        scoreTxt = transform.GetChild(1).gameObject;
        levelTxt = transform.GetChild(0).GetComponent<Text>();
    }

    void LevelTextSetter()
    {
        levelTxt.text = "LEVEL " + GameManager.Level;
    }

    public string ScoreCalculator(int dashCount)
    {
        return scoreTxt.GetComponent<Text>().text = dashCount.ToString();
    }
}
