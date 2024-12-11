using Assets.Scripts.Configs;
using Assets.Scripts.GameManager;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Activity : MonoBehaviour
{
	private sealed class __c__DisplayClass11_0
	{
		public Activity __4__this;

		public int id;

		internal void _OnClickVedio_b__0()
		{
			this.__4__this.OnClickButton(this.id);
			DialogManager.GetInstance().Close(null);
		}
	}

	[Serializable]
	private sealed class __c
	{
		public static readonly Activity.__c __9 = new Activity.__c();

		public static Action __9__11_1;

		internal void _OnClickVedio_b__11_1()
		{
			DialogManager.GetInstance().Close(null);
		}
	}

	public GameObject m_prefab_dialog;

	[SerializeField]
	public List<GameObject> m_items = new List<GameObject>();

	public RectTransform m_video;

	public Text m_videoTimer;

	[SerializeField]
	public List<Color> m_colors = new List<Color>();

	[SerializeField]
	public List<Color> m_colors2 = new List<Color>();

	private void Start()
	{
		this.InitUI();
	}

	private void Update()
	{
	}

	private void OnEnable()
	{
	}

	public void OnClickButton(int id)
	{
		if (!Configs.TActivitys.ContainsKey(id.ToString()))
		{
			return;
		}
		LoginData.GetInstance().SetSignInData(id, 2);
		TActivity tActivity = Configs.TActivitys[id.ToString()];
		GM.GetInstance().AddDiamond(tActivity.Item, true);
		this.InitUI();
	}

	public void OnClickNo()
	{
		int[] arg_0E_0 = LoginData.GetInstance().GetSignInData();
		int num = 0;
		bool flag = false;
		int[] array = arg_0E_0;
		for (int i = 0; i < array.Length; i++)
		{
			int arg_1B_0 = array[i];
			num++;
			if (arg_1B_0 == 1)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			return;
		}
		this.OnClickButton(num);
		DialogManager.GetInstance().Close(null);
	}

	public void OnClickReturn()
	{
		GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
	}

	private void InitUI()
	{
		int serialLoginCount = LoginData.GetInstance().GetSerialLoginCount();
		int[] signInData = LoginData.GetInstance().GetSignInData();
		for (int i = 0; i < signInData.Length; i++)
		{
			this.ShowLogin(this.m_items[i], serialLoginCount, signInData[i], i + 1);
		}
	}

	private void ShowLogin(GameObject obj, int count, int status, int id)
	{
		int skinID = GM.GetInstance().SkinID;
		List<Color> list;
		if (skinID != 1)
		{
			if (skinID != 2)
			{
				list = this.m_colors;
			}
			else
			{
				list = this.m_colors2;
			}
		}
		else
		{
			list = this.m_colors;
		}
		TActivity tActivity = Configs.TActivitys[id.ToString()];
		Component arg_9B_0 = obj.transform.Find("img_icon/img_bg/txt_value");
		Transform transform = obj.transform.Find("txt_desc01");
		Transform transform2 = obj.transform.Find("img_item_bg");
		Transform transform3 = obj.transform.Find("img_finish");
		Transform transform4 = obj.transform.Find("button/txt");
		arg_9B_0.GetComponent<Text>().text = string.Format("{0}", tActivity.Item.ToString());
		transform.GetComponent<Text>().text = Localisation.GetString(tActivity.Desc);
		switch (status)
		{
		case 0:
			transform3.gameObject.SetActive(false);
			transform2.GetComponent<Image>().color = list[1];
			transform4.GetComponent<Text>().text = Localisation.GetString("claim");
			return;
		case 1:
			transform3.gameObject.SetActive(false);
			transform2.GetComponent<Image>().color = list[1];
			transform4.GetComponent<Text>().text = Localisation.GetString("claim");
			return;
		case 2:
			transform3.gameObject.SetActive(true);
			transform2.GetComponent<Image>().color = list[2];
			transform4.GetComponent<Text>().text = Localisation.GetString("claimed");
			return;
		default:
			return;
		}
	}

	private void ShowAwards(int day)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_prefab_dialog);
		gameObject.GetComponent<ActivityDialog>().Load(day);
		DialogManager.GetInstance().show(gameObject, false);
	}

}
