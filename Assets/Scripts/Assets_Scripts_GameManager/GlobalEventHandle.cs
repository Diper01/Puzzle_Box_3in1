using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.GameManager
{
	internal class GlobalEventHandle
	{

		public static Action<int> OnRefreshTaskHandle;

		public static Action<int> OnRefreshAchiveHandle;

		public static Action<string[]> OnRefreshMaxScoreHandle;



		public static Action DoTransiformSkin;

		public static event Action<int, bool> GetDiamondHandle;

		public static event Action<int> ConsumeDiamondHandle;

		public static event Action<bool> AddExpHandle;

		public static event Action DoClickBottom;

		public static event Action DoGoHome;

		public static event Action<bool, List<GameObject>> DoUseProps;

		public static event Action<string, bool> AdsHandle;

		public static event Action<string, int> OnClickPageButtonHandle;

		public static event Action<int> DoRefreshCheckPoint;

		public static void EmitGetDiamondHandle(int number, bool isPlayAni)
		{
			Action<int, bool> expr_05 = GlobalEventHandle.GetDiamondHandle;
			if (expr_05 == null)
			{
				return;
			}
			expr_05(number, isPlayAni);
		}

		public static void EmitConsumeDiamondHandle(int number)
		{
			Action<int> expr_05 = GlobalEventHandle.ConsumeDiamondHandle;
			if (expr_05 == null)
			{
				return;
			}
			expr_05(number);
		}

		public static void EmitAddExpHandle(bool isLevelUp)
		{
			Action<bool> expr_05 = GlobalEventHandle.AddExpHandle;
			if (expr_05 == null)
			{
				return;
			}
			expr_05(isLevelUp);
		}

		public static void EmitDoClickBottom()
		{
			Action expr_05 = GlobalEventHandle.DoClickBottom;
			if (expr_05 == null)
			{
				return;
			}
			expr_05();
		}

		public static void EmitDoGoHome()
		{
			Action expr_05 = GlobalEventHandle.DoGoHome;
			if (expr_05 == null)
			{
				return;
			}
			expr_05();
		}

		public static void EmitDoUseProps(bool isDel, List<GameObject> objs)
		{
			Action<bool, List<GameObject>> expr_05 = GlobalEventHandle.DoUseProps;
			if (expr_05 == null)
			{
				return;
			}
			expr_05(isDel, objs);
		}

		public static void EmitAdsHandle(string timer, bool isEnd)
		{
			Action<string, bool> expr_05 = GlobalEventHandle.AdsHandle;
			if (expr_05 == null)
			{
				return;
			}
			expr_05(timer, isEnd);
		}

		public static void EmitClickPageButtonHandle(string name, int value = 0)
		{
			Action<string, int> expr_05 = GlobalEventHandle.OnClickPageButtonHandle;
			if (expr_05 == null)
			{
				return;
			}
			expr_05(name, value);
		}

		public static void EmitRefreshTaskHandle(int id)
		{
			Action<int> expr_05 = GlobalEventHandle.OnRefreshTaskHandle;
			if (expr_05 == null)
			{
				return;
			}
			expr_05(id);
		}

		public static void EmitRefreshAchiveHandle(int id)
		{
			Action<int> expr_05 = GlobalEventHandle.OnRefreshAchiveHandle;
			if (expr_05 == null)
			{
				return;
			}
			expr_05(id);
		}

		public static void EmitRefreshMaxScoreHandle(string[] array)
		{
			Action<string[]> expr_05 = GlobalEventHandle.OnRefreshMaxScoreHandle;
			if (expr_05 == null)
			{
				return;
			}
			expr_05(array);
		}

		public static void EmitDoRefreshCheckPoint(int lv)
		{
			Action<int> expr_05 = GlobalEventHandle.DoRefreshCheckPoint;
			if (expr_05 == null)
			{
				return;
			}
			expr_05(lv);
		}
	}
}
