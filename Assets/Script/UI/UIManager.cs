using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
	public Text scoreText;
	public Text timerText;
	public Transform canvasTrans;
	public GameObject hudPanel;

	[HideInInspector]
	public static UIManager instance;

	private GameObject mainGameplayPanel;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		} else
		{
			Destroy(this.gameObject);
			return;
		}
		InitUIValue();
	}

	public void CreateGamePlayScreen ()
	{
		if(mainGameplayPanel != null)
		{
			GameObject panelObject = Instantiate(mainGameplayPanel, canvasTrans);
		}
	}

	private void InitUIValue ()
	{
		hudPanel.SetActive(false);
		mainGameplayPanel = Resources.Load(Constants.screenGamePlayPanel_UI) as GameObject;
	}

}
