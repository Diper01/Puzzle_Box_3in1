using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class G00102 : MonoBehaviour
{
    [Serializable]
    private sealed class __c
    {
        public static readonly G00102.__c __9 = new G00102.__c();

        public static Action __9__17_0;

        internal void _OnClickShare_b__17_0()
        {
            if (GM.GetInstance().isFirstShare())
            {
                GM.GetInstance().ResetFirstShare(1);
                GM.GetInstance().AddDiamond(5, true);
            }
            //			AppsflyerUtils.TrackShare();
        }
    }

    private sealed class __c__DisplayClass18_0
    {
        public G00102 __4__this;

        public int idx;

        internal void _PlayTips_b__0()
        {
            this.__4__this.m_img_circle.transform.localPosition = this.__4__this.m_tips_component[this.idx].localPosition;
            int num = this.idx + 1;
            this.idx = num;
            this.idx %= 1;
        }
    }

    public Text m_txt_score_value;

    public Text m_txt_record_value;

    public Text m_txt_needGEM;

    public Image m_img_circle;

    private double m_needGEM;

    private int gameID = 1;

    public Transform[] m_tips_component;

    public string[] m_tips_content;

    private void Start()
    {
        this.m_needGEM = 100.0 * Math.Pow(2.0, (double)GM.GetInstance().ConsumeCount);
        this.m_txt_needGEM.text = this.m_needGEM.ToString();
        GM.GetInstance().SetSavedGameID(0);
        GM.GetInstance().SaveScore(this.gameID, 0);
        M001.GetInstance().FinishCount = 0;
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

    public void OnClickDiamond()
    {
        if (!GM.GetInstance().isFullGEM(Convert.ToInt32(this.m_needGEM)))
        {
            ToastManager.Show("not_enough_coins", true);
            return;
        }
        GM.GetInstance().SetSavedGameID(this.gameID);
        GM.GetInstance().ConsumeGEM(Convert.ToInt32(this.m_needGEM));
        GM.GetInstance().AddConsumeCount();
        M001.GetInstance().FillLife(false);
        M001.GetInstance().DoFillLife();
        DialogManager.GetInstance().Close(null);
    }

    public void OnClickAds()
    {

        GM.GetInstance().SetSavedGameID(this.gameID);
        M001.GetInstance().FillLife(false);
        M001.GetInstance().DoFillLife();
        DialogManager.GetInstance().Close(null);

    }

    public void OnClickHome()
    {
        M001.GetInstance().Score = 0;
        GM.GetInstance().SaveScore(this.gameID, 0);
        GM.GetInstance().SetSavedGameID(0);
        GM.GetInstance().ResetToNewGame();
        GM.GetInstance().ResetConsumeCount();
        DialogManager.GetInstance().Close(null);
        GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
    }

    public void OnClickAgain()
    {
        GM.GetInstance().SaveScore(this.gameID, 0);
        GM.GetInstance().SetSavedGameID(0);
        GM.GetInstance().ResetToNewGame();
        GM.GetInstance().ResetConsumeCount();
        M001.GetInstance().Score = 0;
        M001.GetInstance().StartNewGame();
        DialogManager.GetInstance().Close(null);
    }

    public void OnClickShare()
    {
        Action arg_20_0;
        if ((arg_20_0 = G00102.__c.__9__17_0) == null)
        {
            arg_20_0 = (G00102.__c.__9__17_0 = new Action(G00102.__c.__9._OnClickShare_b__17_0));
        }
        FacebookUtil.Share(arg_20_0, null);
    }

    private void PlayTips()
    {
        int idx = new System.Random().Next(0, 1);
        Sequence expr_24 = DOTween.Sequence();
        expr_24.Append(this.m_img_circle.DOFade(0.3f, 1f));
        expr_24.Append(this.m_img_circle.DOFade(1f, 1f));
        expr_24.Append(this.m_img_circle.DOFade(0.3f, 1f));
        expr_24.Append(this.m_img_circle.DOFade(1f, 1f));
        expr_24.SetLoops(-1);
        expr_24.SetTarget(this.m_img_circle);
        Sequence expr_AD = DOTween.Sequence();
        int idx1 = 0;
        expr_AD.AppendCallback(delegate
        {
            //int idx;
            this.m_img_circle.transform.localPosition = this.m_tips_component[idx1].localPosition;
            idx1++;
            idx = idx1;
            idx1 %= 1;
        });
        expr_AD.AppendInterval(4f);
        expr_AD.SetLoops(-1);
        expr_AD.SetTarget(this.m_img_circle);
    }
}
