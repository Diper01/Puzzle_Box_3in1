using Assets.Scripts.GameManager;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubscriptionDialog : MonoBehaviour
{
	public Text m_txt_awards;

	public Text m_txt_day;

	public Button m_button;

	public GameObject m_img_circle;

	[SerializeField]
	public List<string> m_languages = new List<string>();

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Load(int day, int value)
	{
		this.m_txt_awards.text = "+" + value;
		this.m_txt_day.text = Localisation.GetString(this.m_languages[day - 1]);
	}

	public void OnClickButton()
	{
		GlobalEventHandle.EmitGetDiamondHandle(0, true);
		DialogManager.GetInstance().Close(null);
	}

	private void PlayAni()
	{
		this.m_img_circle.transform.DOBlendableLocalRotateBy(new Vector3(0f, 0f, 180f), 2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
	}
}
