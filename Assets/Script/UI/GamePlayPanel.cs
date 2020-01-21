using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayPanel : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;

    private void Awake()
    {
        InitUIValue();
    }

    void InitUIValue()
    {
        if(GameManager.instance.gameData != null)
        {
            scoreText.text = GameManager.instance.gameData.highScore.ToString();
            timeText.text = GameManager.instance.gameData.timeTaken.ToString();
        }
    }

    public void ButtonAction()
    {
        GameManager.instance.StartGame();
        Destroy(this.gameObject);
    }
}
