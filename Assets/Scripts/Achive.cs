using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Achive : MonoBehaviour
{
	private sealed class __c__DisplayClass8_0
	{
		public AchiveItem obj;

		internal void _EntranceAni_b__2()
		{
			this.obj.gameObject.SetActive(true);
		}
	}

	[Serializable]
	private sealed class __c
	{
		public static readonly Achive.__c __9 = new Achive.__c();

		public static TweenCallback __9__8_0;

		public static TweenCallback __9__8_1;

		internal void _EntranceAni_b__8_0()
		{
		}

		internal void _EntranceAni_b__8_1()
		{
		}
	}

	[SerializeField]
	public List<AchiveItem> m_items;


	private void Update()
	{
		Utils.BackListener(base.gameObject, delegate
		{
			this.OnClickReturn();
		});
	}



	public void OnClickReturn()
	{
		GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
	}


}
