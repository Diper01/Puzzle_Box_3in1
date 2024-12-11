using Assets.Scripts.GameManager;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	public Text m_lb_title;

	public Text m_lb_score;

	public Text m_lb_score_value;

	public Button m_btn_home;

	public Button m_btn_refresh;

	public Button m_btn_continue;

	public Action OnClickHomeHandle;

	public Action OnClickRefreshHandle;

	public Action OnClickContinueHandle;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetScore(int score)
	{
		this.m_lb_score_value.text = string.Format((score < 1000) ? "{0}" : "{0:0,00}", score);
	}

	public void SetTitle(string id)
	{
		this.m_lb_score.text = Localisation.GetString(id);
	}

	public void OnClickHome()
	{
        PauseManager.instance.Resume();
        OnClickHomeHandle();
        Debug.Log("OnClickHome from :  " + gameObject);
        DialogManager.GetInstance().Close(null);
	}

	public void OnClickRefresh()
	{
        OnClickRefreshHandle();
		DialogManager.GetInstance().Close(null);
	}

	public void OnClickContinue()
	{
        PauseManager.instance.Resume();
        //OnClickContinueHandle();
        DialogManager.GetInstance().Close(null);
	}
}
