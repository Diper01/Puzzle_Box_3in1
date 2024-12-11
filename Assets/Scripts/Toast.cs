using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
	private sealed class __c__DisplayClass3_0
	{
		public Action callback;

		public Toast __4__this;

		internal void _FadeOut_b__0()
		{
			this.callback();
			UnityEngine.Object.Destroy(this.__4__this.gameObject);
		}
	}

	public Image image;

	public Text text;

	public void InitToast(string str, Action callback)
	{
		this.text.text = str;
		//this.text.font = Language.GetFont();
		int length = str.Length;
		if (length > 28)
		{
			int arg_2C_0 = length / 28;
		}
		this.FadeOut(callback);
	}

	public void FadeOut(Action callback)
	{
		this.image.DOFade(0f, 2f).SetDelay(0.5f).OnComplete(delegate
		{
			callback();
			UnityEngine.Object.Destroy(this.gameObject);
		});
		this.text.DOFade(0f, 2f).SetDelay(0.5f);
	}

	public void Move(float speed, int targetPos)
	{
		base.transform.DOLocalMoveY((float)targetPos * (this.image.rectTransform.sizeDelta.y + 10f), speed, false);
	}
}
