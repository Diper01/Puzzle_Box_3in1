using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
	private sealed class __c__DisplayClass10_0
	{
		public TaskItem obj;

		internal void _EntranceAni_b__2()
		{
			this.obj.gameObject.SetActive(true);
		}
	}

	[Serializable]
	private sealed class __c
	{
		public static readonly Task.__c __9 = new Task.__c();

		public static TweenCallback __9__10_0;

		public static TweenCallback __9__10_1;

		internal void _EntranceAni_b__10_0()
		{
		}

		internal void _EntranceAni_b__10_1()
		{
		}
	}

	[SerializeField]
	public List<TaskItem> items = new List<TaskItem>();


	private void Start()
	{
		GlobalTimer.GetInstance().RefreshHandle += new Action(this.Refresh);
	}

	private void Update()
	{
		Utils.BackListener(base.gameObject, delegate
		{
			this.OnClickReturn();
		});
	}

	private void OnEnable()
	{
		this.EntranceAni();
	}

	private void OnDestroy()
	{
		GlobalTimer.GetInstance().RefreshHandle -= new Action(this.Refresh);
	}


	public void OnClickReturn()
	{
		GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
	}

	private void Refresh()
	{
		using (List<TaskItem>.Enumerator enumerator = this.items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				enumerator.Current.BindDataToUI();
			}
		}
	}

	private void EntranceAni()
	{
		Sequence sequence = DOTween.Sequence();
		for (int i = 0; i < this.items.Count; i++)
		{
			TaskItem obj = this.items[i];
			Vector3 localPosition = obj.transform.localPosition;
			float y = localPosition.y;
			localPosition.y = -568f;
			obj.transform.localPosition = localPosition;
			obj.gameObject.SetActive(false);
			Tweener t = obj.transform.DOLocalMoveY(y, 0.2f, false).SetDelay((float)i * 0.05f).OnStart(delegate
			{
				obj.gameObject.SetActive(true);
			});
			sequence.Insert(0f, t);
		}
		Sequence arg_11A_0 = sequence;
		TweenCallback arg_11A_1;
		if ((arg_11A_1 = Task.__c.__9__10_0) == null)
		{
			arg_11A_1 = (Task.__c.__9__10_0 = new TweenCallback(Task.__c.__9._EntranceAni_b__10_0));
		}
		arg_11A_0.OnStart(arg_11A_1);
		Sequence arg_140_0 = sequence;
		TweenCallback arg_140_1;
		if ((arg_140_1 = Task.__c.__9__10_1) == null)
		{
			arg_140_1 = (Task.__c.__9__10_1 = new TweenCallback(Task.__c.__9._EntranceAni_b__10_1));
		}
		arg_140_0.OnComplete(arg_140_1);
	}
}
