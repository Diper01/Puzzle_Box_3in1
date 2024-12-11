using Assets.Scripts.Configs;
using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class G003 : MonoBehaviour
{
	public GameObject ScrollView;

	public GameObject ScrollViewLv;

	public GameObject gamebox;

	public GameObject gamebox_LV;

	public RectTransform m_video;

	public Text m_videoTimer;

	private M003 m_model;

	private List<G00305> m_lvs = new List<G00305>();

	private List<G00300> m_checkPoints = new List<G00300>();

	public M003 Model
	{
		get
		{
			return this.m_model;
		}
		set
		{
			this.m_model = value;
		}
	}

	public List<G00305> Lvs
	{
		get
		{
			return this.m_lvs;
		}
		set
		{
			this.m_lvs = value;
		}
	}

	public List<G00300> CheckPoints
	{
		get
		{
			return this.m_checkPoints;
		}
		set
		{
			this.m_checkPoints = value;
		}
	}

	private void Start()
	{
		this.Model = M003.GetInstance();
		this.InitUI();
		this.InitEvent();
	}

	private void Update()
	{
		Utils.BackListener(base.gameObject, delegate
		{
			this.OnClickReturn();
		});
	}

	public void InitUI()
	{
		this.InitLv();
		this.UpdateCheckPoint(301);
		this.ScrollView.SetActive(false);
		this.ScrollViewLv.SetActive(true);
	}

	public void InitLv()
	{
		if (this.Model.GetLvCount() > 0)
		{
			this.gamebox_LV.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (float)(this.Model.GetLvBoxHeight() + 240));
			foreach (KeyValuePair<string, TG003> current in this.Model.Config_G003)
			{
				GameObject expr_6C = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/G00305") as GameObject);
				G00305 component = expr_6C.GetComponent<G00305>();
				TG003 value = current.Value;
				component.Init(value.ID, value.Star, 0, value.Count, value.Lang);
				expr_6C.SetActive(true);
				expr_6C.transform.SetParent(this.gamebox_LV.transform, false);
				this.Lvs.Add(component);
			}
		}
		this.UpdateLv();
	}

	public void UpdateLv()
	{
		foreach (G00305 current in this.Lvs)
		{
			int num = this.Model.Lv_score[current.Id.ToString()];
			current.SetIsUnLock(num != -1);
			current.SetLabel(0);
			current.SetProgress(0f);
			List<string> list = this.Model.GetcheckPointByLv(current.Id);
			int num2 = 0;
			foreach (string arg_8B_0 in list)
			{
				num2++;
				if (arg_8B_0 == num.ToString())
				{
					current.SetLabel(num2);
					current.SetProgress((float)num2 / (float)list.Count);
					break;
				}
			}
		}
	}

	public void UpdateCheckPoint(int lv = 301)
	{
		this.Model.LoadScore();
		using (List<G00300>.Enumerator enumerator = this.CheckPoints.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				enumerator.Current.gameObject.SetActive(false);
			}
		}
		List<string> list = this.Model.GetcheckPointByLv(lv);
		if (list.Count > 0)
		{
			this.gamebox.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (float)(this.Model.GetGameBoxHeightEx(lv) + 280));
			int num = 0;
			foreach (string current in list)
			{
				TG00301 tG = this.Model.Config_G00301[current];
				if (this.CheckPoints.Count - 1 < num)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/G00300") as GameObject);
					gameObject.SetActive(true);
					gameObject.transform.SetParent(this.gamebox.transform, false);
					this.CheckPoints.Add(gameObject.GetComponent<G00300>());
				}
				this.CheckPoints[num].gameObject.SetActive(true);
				this.CheckPoints[num].Init(num + 1, num, current);
				this.CheckPoints[num].SetCheckpointStatus(tG.ID <= this.Model.Lv_score[lv.ToString()] + 1);
				num++;
			}
			if (this.Model.Lv_score[lv.ToString()] == 0)
			{
				this.CheckPoints[0].SetCheckpointStatus(true);
			}
		}
		this.UpdateLv();
	}

	public void InitEvent()
	{
		M003 expr_06 = this.Model;
		expr_06.DoClickLvHandler = (Action<G00305>)Delegate.Combine(expr_06.DoClickLvHandler, new Action<G00305>(this.DoClickLv));
		M003 expr_2D = this.Model;
		expr_2D.DoClickCheckPointHandler = (Action<G00300>)Delegate.Combine(expr_2D.DoClickCheckPointHandler, new Action<G00300>(this.DoClickCheckPoint));
		GlobalEventHandle.DoRefreshCheckPoint += new Action<int>(this.UpdateCheckPoint);
	}

	public void DoClickLv(G00305 btn)
	{
		this.ScrollView.SetActive(true);
		this.ScrollViewLv.SetActive(false);
		this.UpdateCheckPoint(btn.Id);
	}

	public void DoClickCheckPoint(G00300 btn)
	{
		this.Model.StartNewGame(btn.Key);
		base.gameObject.SetActive(false);
	}



	public void OnClickReturn()
	{
		if (this.ScrollViewLv.activeInHierarchy)
		{
			GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
		}
		if (this.ScrollView.activeInHierarchy)
		{
			this.ScrollView.SetActive(false);
			this.ScrollViewLv.SetActive(true);
		}
	}

	
}
