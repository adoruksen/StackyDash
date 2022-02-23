using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompletePanel : MonoBehaviour
{
    #region Singleton Pattern
    public static CompletePanel instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    Text coinText;
    GameObject status;
    GameObject score;
    GameObject nextLevelButton;
    GameObject coin;
    void Start()
    {
        SetValues();
    }
    void SetValues()
    {
        status = transform.GetChild(0).gameObject;
        score = transform.GetChild(1).gameObject;
        nextLevelButton = transform.GetChild(2).gameObject;
        coin = transform.GetChild(3).gameObject;
        coinText = transform.GetChild(3).GetChild(1).GetComponent<Text>();
    }

    
    public void NextButtonFunc()
    {
        GameManager.Level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneLoadLayer.instance.SceneLoadAnimation(false);
    }
    public void UpdateCoin(int amount)
    {
        int before = GameManager.Coin;
        GameManager.Coin += amount;
        DOTween.To(() => before, x => before = x, GameManager.Coin, 1).OnUpdate(() => coinText.text = before + string.Empty);
    }

    public string SetFinalScoreText(int dashCount)
    {
        return score.GetComponent<Text>().text = "Score: " + dashCount.ToString();
    }
    public void Activator(bool condition = true)
    {
        if (condition)
        {
            status.GetComponent<Text>().text = "Level " + GameManager.Level + " Completed!";
            StartCoroutine(PanelOpenDelay());
        }
        IEnumerator PanelOpenDelay()
        {
            yield return new WaitForSeconds(1f);

            coin.SetActive(true);
            status.SetActive(true);
            nextLevelButton.SetActive(true);
            score.SetActive(true);
        }
    }

    


}
