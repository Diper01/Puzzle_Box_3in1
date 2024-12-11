using Assets.Scripts.Configs;
using Assets.Scripts.GameManager;
using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class G00303 : MonoBehaviour
{
	private sealed class __c__DisplayClass34_0
	{
		public G00303 __4__this;

		public int idx;

		internal void _PlayTips_b__0()
		{
			this.__4__this.m_img_circle.transform.localPosition = this.__4__this.m_tips_component[this.idx].localPosition;
			int num = this.idx + 1;
			this.idx = num;
			this.idx %= 3;
		}
	}

	private int gameID = 3;

	private int m_level = 1;

	private int m_next;

	private int m_lv;

	private int m_award;

	private int m_exp;

	private bool isShowAward;

	public Image m_img_circle;

	public Transform[] m_tips_component;

	public string[] m_tips_content;

	public Text m_txt_coin;

	public int Level
	{
		get
		{
			return this.m_level;
		}
		set
		{
			this.m_level = value;
		}
	}

	public int Next
	{
		get
		{
			return this.m_next;
		}
		set
		{
			this.m_next = value;
		}
	}

	public int Lv
	{
		get
		{
			return this.m_lv;
		}
		set
		{
			this.m_lv = value;
		}
	}

	public int Award
	{
		get
		{
			return this.m_award;
		}
		set
		{
			this.m_award = value;
		}
	}

	public int Exp
	{
		get
		{
			return this.m_exp;
		}
		set
		{
			this.m_exp = value;
		}
	}

	private void Start()
	{
		GM.GetInstance().SetSavedGameID(0);
		if (this.isShowAward)
		{
			GM.GetInstance().AddDiamond(this.Award, true);
			GM.GetInstance().AddExp(this.Exp);
		}
		AudioManager.GetInstance().PlayEffect("sound_eff_popup");
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
		DOTween.Kill(this.m_img_circle, false);
	}

	public void Load(int score, int next, int lv, int award = 0, int exp = 0)
	{
		this.Level = score;
		this.Next = next;
		this.Lv = lv;
		this.Award = award;
		this.Exp = exp;
		this.m_txt_coin.text = "+ " + award.ToString();
        Debug.Log("----------Level -----------");
        Debug.Log(Level);
        if (score > 15)
		{
			GM.GetInstance().SetFirstFinishGame();
		}
	}

	public void OnClickNext()
	{
		if(G00301.currentLevel >= 300)
        {
			GM.GetInstance().SaveScore(2, 0);
			GM.GetInstance().SetSavedGameID(0);
			GM.GetInstance().ResetToNewGame();
			GM.GetInstance().ResetConsumeCount();
			GlobalEventHandle.EmitDoGoHome();
			GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
			DialogManager.GetInstance().Close(null);
		}
        else
        {
			UnityEngine.Debug.Log("On next lv");
			GM.GetInstance().SetSavedGameID(this.gameID);
			M00301.GetInstance().DestroyMap();
			M00301.GetInstance().StartNewGame(Configs.TG00301[this.Next.ToString()].ID);
			GlobalEventHandle.EmitDoRefreshCheckPoint(this.Lv);
			AudioManager.GetInstance().PlayEffect("sound_eff_button");
			DialogManager.GetInstance().Close(null);
		}
	
	}

	public void OnClickAgain()
	{
		
				GM.GetInstance().AddDiamond(this.Award, true);
			
			GlobalEventHandle.EmitDoRefreshCheckPoint(this.Lv);
			this.OnClickNext();
		
	}

	public void OnClickHome()
	{
		M00301.GetInstance().DestroyMap();
		GM.GetInstance().SetSavedGameID(0);
		GM.GetInstance().ResetToNewGame();
		GM.GetInstance().ResetConsumeCount();
		GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
		GlobalEventHandle.EmitDoRefreshCheckPoint(this.Lv);
		AudioManager.GetInstance().PlayEffect("sound_eff_button");
		DialogManager.GetInstance().Close(null);
	}

	private void PlayTips()
	{
		int idx = new System.Random().Next(0, 1);
		this.m_img_circle.gameObject.SetActive(true);
		Sequence expr_35 = DOTween.Sequence();
		expr_35.Append(this.m_img_circle.DOFade(0.3f, 1f));
		expr_35.Append(this.m_img_circle.DOFade(1f, 1f));
		expr_35.Append(this.m_img_circle.DOFade(0.3f, 1f));
		expr_35.Append(this.m_img_circle.DOFade(1f, 1f));
		expr_35.SetLoops(-1);
		expr_35.SetTarget(this.m_img_circle);
		Sequence expr_BE = DOTween.Sequence();
        int idx1 = 0;
        expr_BE.AppendCallback(delegate
		{
			//int idx;
			this.m_img_circle.transform.localPosition = this.m_tips_component[idx1].localPosition;
			idx1++;
			idx = idx1;
			idx1 %= 3;
		});
		expr_BE.AppendInterval(4f);
		expr_BE.SetLoops(-1);
		expr_BE.SetTarget(this.m_img_circle);
	}

	public void IsShowAward(bool isShowAward)
	{
		this.isShowAward = isShowAward;
		base.transform.Find("panel_award").gameObject.SetActive(isShowAward);
	}
}
