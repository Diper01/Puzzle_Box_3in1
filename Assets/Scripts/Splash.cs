using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
	private sealed class _LoadAsyncScene_d__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int __1__state;

		private object __2__current;

		public Splash __4__this;

		object IEnumerator<object>.Current
		{
			get
			{
				return this.__2__current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this.__2__current;
			}
		}

		public _LoadAsyncScene_d__7(int __1__state)
		{
			this.__1__state = __1__state;
		}

		void IDisposable.Dispose()
		{
		}

		bool IEnumerator.MoveNext()
		{
			int num = this.__1__state;
			Splash splash = this.__4__this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.__1__state = -1;
			}
			else
			{
				this.__1__state = -1;
				splash.m_async = SceneManager.LoadSceneAsync("MainScene");
				splash.m_async.allowSceneActivation = false;
			}
			if (splash.m_async.isDone)
			{
				return false;
			}
			if (splash.m_isComplateAni)
			{
				splash.m_async.allowSceneActivation = true;
			}
			this.__2__current = null;
			this.__1__state = 1;
			return true;
		}

		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	public Image m_logo;

	public Image m_logo2;

	private bool m_isComplateAni;

	private AsyncOperation m_async;

	private void Awake()
	{
		this.m_logo.gameObject.SetActive(false);
		this.m_logo.color = new Color(1f, 1f, 1f, 0f);
		this.m_logo2.gameObject.SetActive(false);
		this.m_logo2.color = new Color(1f, 1f, 1f, 0f);
	}

	private void Start()
	{
		this.m_logo.gameObject.SetActive(true);
		Sequence expr_16 = DOTween.Sequence();
		expr_16.Append(this.m_logo.DOFade(1f, 1f));
		expr_16.AppendCallback(delegate
		{
			this.m_isComplateAni = true;
		});
		this.m_logo2.gameObject.SetActive(true);
		this.m_logo2.DOFade(1f, 1f);
		base.StartCoroutine(this.LoadAsyncScene());
	}

	private void Update()
	{
	}

	//[IteratorStateMachine(typeof(Splash._003CLoadAsyncScene_003Ed__7))]
	private IEnumerator LoadAsyncScene()
	{
		while (true)
		{
			int num = 0;
			if (num != 0)
			{
				if (num != 1)
				{
					break;
				}
			}
			else
			{
				this.m_async = SceneManager.LoadSceneAsync("MainScene");
				this.m_async.allowSceneActivation = false;
			}
			if (this.m_async.isDone)
			{
				goto Block_4;
			}
			if (this.m_isComplateAni)
			{
				this.m_async.allowSceneActivation = true;
			}
			yield return null;
		}
		yield break;
		Block_4:
		yield break;
	}
}
