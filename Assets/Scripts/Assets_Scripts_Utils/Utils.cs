using Assets.Scripts.GameManager;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.Utils
{
	internal class Utils
	{

		public static void CallAndroidObjectFunc<T>(string packageName, string className, string objectName, string methodName, out T output, params object[] args)
		{
			AndroidJavaObject @static = new AndroidJavaClass(string.Format("{0}.{1}", packageName, className)).GetStatic<AndroidJavaObject>(objectName);
			output = @static.Call<T>(methodName, args);
		}

		public static void CallAndroidMethodFunc<T>(string packageName, string className, string objectName, string methodName, out T output, params object[] args)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaClass(string.Format("{0}.{1}", packageName, className)).CallStatic<AndroidJavaObject>(objectName, Array.Empty<object>());
			output = androidJavaObject.Call<T>(methodName, args);
		}

		public static void CallAndroiSDKdFunc<T>(string className, string methodName, out T output, params object[] args)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaClass(string.Format("{0}.{1}", "xdo.sdk.unitylibrary", className)).CallStatic<AndroidJavaObject>("getInstance", Array.Empty<object>());
			output = androidJavaObject.Call<T>(methodName, args);
		}



		public static void ShowLoginRewards()
		{
			if (GM.GetInstance().IsNewUser())
			{
				return;
			}
			bool flag = false;
			int[] signInData = LoginData.GetInstance().GetSignInData();
			for (int i = 0; i < signInData.Length; i++)
			{
				if (signInData[i] == 1)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return;
			}
			GameObject obj = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/activity") as GameObject);
			DialogManager.GetInstance().show(obj, true);
		}

		public static void ShowConfirmOrCancel(Action confirmCallFunc, Action cancelConfirm, string text = "do_you_want_to_quit_the_game_now?", bool isID = true)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/confirm") as GameObject);
			gameObject.GetComponent<Confirm>().SetText(text, true);
			gameObject.GetComponent<Confirm>().SetCallFunc(confirmCallFunc, cancelConfirm);
			DialogManager.GetInstance().show(gameObject, false);
		}

		public static void ShowConfirm(Action confirmCallFunc, string text, bool isID = true)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/confirm") as GameObject);
			gameObject.GetComponent<Confirm>().SetType(Confirm.ConfirmType.OK);
			gameObject.GetComponent<Confirm>().SetText(text, true);
			gameObject.GetComponent<Confirm>().SetCallFunc(confirmCallFunc, null);
			DialogManager.GetInstance().show(gameObject, false);
		}

		public static Pause ShowPause(int score, Action homeCallFunc, Action refreshCallFunc, Action continueCallFunc)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/pause") as GameObject);
			gameObject.GetComponent<Pause>().OnClickHomeHandle = homeCallFunc;
			gameObject.GetComponent<Pause>().OnClickRefreshHandle = refreshCallFunc;
			gameObject.GetComponent<Pause>().OnClickContinueHandle = continueCallFunc;
			gameObject.GetComponent<Pause>().SetScore(score);
			DialogManager.GetInstance().show(gameObject, false);
			return gameObject.GetComponent<Pause>();
		}

		public static void AndroidBack(Action action)
		{
			if (DialogManager.GetInstance().IsShow())
			{
				if (!DialogManager.GetInstance().isIgnoreBack())
				{
					DialogManager.GetInstance().Close(null);
                    PauseManager.instance.Resume();
					return;
				}
			}
			else if (action != null)
			{
				action();
			}
		}

		public static void BackListener(GameObject activty, Action action)
		{
			if (!activty.activeSelf)
			{
				return;
			}
			if (Input.GetKeyUp(KeyCode.Escape))
			{
				AndroidBack(action);
			}
		}

		public static void ExitGame()
		{
			Application.Quit();
		}
	}
}
