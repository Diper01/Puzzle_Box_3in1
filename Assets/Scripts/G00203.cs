using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class G00203 : MonoBehaviour
{
	[Serializable]
	private sealed class __c
	{
		public static readonly G00203.__c __9 = new G00203.__c();

		public static Action __9__16_0;

		internal void _OnClickShare_b__16_0()
		{
			if (GM.GetInstance().isFirstShare())
			{
				GM.GetInstance().ResetFirstShare(1);
				GM.GetInstance().AddDiamond(5, true);
			}
//			AppsflyerUtils.TrackShare();
		}
	}

	private sealed class __c__DisplayClass17_0
	{
		public G00203 __4__this;

		public int idx;

		internal void _PlayTips_b__0()
		{
			this.__4__this.m_img_circle.transform.localPosition = this.__4__this.m_tips_component[this.idx].localPosition;
			int num = this.idx + 1;
			this.idx = num;
			this.idx %= 3;
		}
	}

	public Text m_txt_score_value;

	public Text m_txt_record_value;

	public Button m_btn_again;

	public Button m_btn_main;

	private int gameID = 2;

	public Image m_img_circle;


	public Transform[] m_tips_component;

	public string[] m_tips_content;

	private void Start()
	{
		GM.GetInstance().SetSavedGameID(0);
		GM.GetInstance().SaveScore(this.gameID, 0);
		M002.GetInstance().FinishCount = 0;
		AudioManager.GetInstance().PlayEffect("sound_eff_victory");
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
		DOTween.Kill(this.m_img_circle, false);
	}

	public void Load(int score, int maxScore)
	{
		this.m_txt_score_value.text = string.Format((score < 1000) ? "{0}" : "{0:0,00}", score);
		this.m_txt_record_value.text = string.Format((maxScore < 1000) ? "{0}" : "{0:0,00}", maxScore);
	}

	public void OnClickAgain()
	{
		GM.GetInstance().SetSavedGameID(this.gameID);
		M002.GetInstance().StartNewGame();
		DialogManager.GetInstance().Close(null);
	}

	public void OnClickAds()
	{
		
			GM.GetInstance().SetSavedGameID(this.gameID);
			DialogManager.GetInstance().Close(null);
			Action expr_25 = M002.GetInstance().DoVedioRefresh;
			if (expr_25 == null)
			{
				return;
			}
			expr_25();
	}

	public void OnClickHome()
	{
		M002.GetInstance().Score = 0;
		GM.GetInstance().SaveScore(this.gameID, 0);
		GM.GetInstance().SetSavedGameID(0);
		GM.GetInstance().ResetToNewGame();
		GM.GetInstance().ResetConsumeCount();
		DialogManager.GetInstance().Close(null);
		GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
	}

	public void OnClickShare()
	{
		Action arg_20_0;
		if ((arg_20_0 = G00203.__c.__9__16_0) == null)
		{
			arg_20_0 = (G00203.__c.__9__16_0 = new Action(G00203.__c.__9._OnClickShare_b__16_0));
		}
		FacebookUtil.Share(arg_20_0, null);
	}

	private void PlayTips()
	{
		int idx = 0;
		this.m_img_circle.gameObject.SetActive(true);
		Sequence expr_2A = DOTween.Sequence();
		expr_2A.Append(this.m_img_circle.DOFade(0.3f, 1f));
		expr_2A.Append(this.m_img_circle.DOFade(1f, 1f));
		expr_2A.Append(this.m_img_circle.DOFade(0.3f, 1f));
		expr_2A.Append(this.m_img_circle.DOFade(1f, 1f));
		expr_2A.SetLoops(-1);
		expr_2A.SetTarget(this.m_img_circle);
		Sequence expr_B3 = DOTween.Sequence();
        int idx1 = 0;
        expr_B3.AppendCallback(delegate
		{
			//int idx;
			this.m_img_circle.transform.localPosition = this.m_tips_component[idx1].localPosition;
			idx1++;
			idx = idx1;
			idx1 %= 3;
		});
		expr_B3.AppendInterval(4f);
		expr_B3.SetLoops(-1);
		expr_B3.SetTarget(this.m_img_circle);
	}
}
