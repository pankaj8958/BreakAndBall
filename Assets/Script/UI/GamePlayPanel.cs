using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayPanel : MonoBehaviour
{
    public Text titleText;
    public Text buttonText;

    public Text highScoreText;
    public Text highScoreTimeText;

    public GameObject currentScorePanel;
    public Text currentScoreText;
    public Text currentTimeText;

    private void Awake()
    {
        InitUIValue();
    }

    void InitUIValue()
    {
        if(GameManager.instance.gameSavedData != null)
        {
            highScoreText.text = GameManager.instance.gameSavedData.score.ToString();
            highScoreTimeText.text = GameManager.instance.gameSavedData.timeTaken.ToString();
        }

        if(GameManager.instance.currentLevelData != null)
        {
            if(GameManager.instance.currentLevelData.score > 0)
            {
                currentScorePanel.SetActive(true);
                currentScoreText.text = "current Score: " + GameManager.instance.currentLevelData.score.ToString();
                currentTimeText.text = "Current Time: " + GameManager.instance.currentLevelData.timeTaken.ToString();
            }
        }
    }

    public void TextBasedOnScreen (bool isLevelLooseScreen = false)
    {
        if(!isLevelLooseScreen)
        {
            titleText.text = "Level Start";
            buttonText.text = "Play";
        } else
        {
            titleText.text = "Level Lost";
            buttonText.text = "Retry";
        }
    }

    public void ButtonAction()
    {
        GameManager.instance.StartGame();
        GameManager.instance.SwitchScene(Constants.SCENE_NAME.GamePlayScene);
        Destroy(this.gameObject);
    }
}
