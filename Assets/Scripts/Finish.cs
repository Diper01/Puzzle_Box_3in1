using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    private int m_gameID = 1;

    private int m_number = 1;

    private int m_type = 1;

    public G00101 m_g00101;

    public G00201 m_g00201;

    public Image m_progress;

    public GameObject m_btn_continue;

    public GameObject m_btn_coins;

    public GameObject m_btn_no;

    public int GameID
    {
        get
        {
            return this.m_gameID;
        }
        set
        {
            this.m_gameID = value;
        }
    }

    public int Number
    {
        get
        {
            return this.m_number;
        }
        set
        {
            this.m_number = value;
        }
    }

    private void Start()
    {

        this.m_type = 2;

        this.m_btn_coins.gameObject.SetActive(false);
        this.m_btn_continue.gameObject.SetActive(false);
        this.m_progress.DOFillAmount(0f, 10f).SetEase(Ease.Linear).OnComplete(delegate
        {
            this.OnClickAgain();
        });
        Sequence sequence = DOTween.Sequence();
        int type = this.m_type;
        if (type != 1)
        {
            if (type == 2)
            {
                sequence.Append(this.m_btn_coins.transform.DOScale(1.1f, 1f));
                sequence.Append(this.m_btn_coins.transform.DOScale(1f, 1f));
                sequence.SetLoops(-1);
                sequence.SetTarget(this.m_btn_coins);
                this.m_btn_coins.gameObject.SetActive(true);
            }
        }
        else
        {
            sequence.Append(this.m_btn_continue.transform.DOScale(1.1f, 1f));
            sequence.Append(this.m_btn_continue.transform.DOScale(1f, 1f));
            sequence.SetLoops(-1);
            sequence.SetTarget(this.m_btn_continue);
            this.m_btn_continue.gameObject.SetActive(true);
        }
        sequence = DOTween.Sequence();
        sequence.Append(this.m_btn_no.transform.DOScale(0f, 0f));
        sequence.AppendInterval(2f);
        sequence.Append(this.m_btn_no.transform.DOScale(1f, 0.2f));
        sequence.SetTarget(this.m_btn_no);
        GM.GetInstance().SetFirstFinishGame();
    }

    private void Update()
    {
    }

    private void OnDestroy()
    {
        DOTween.Kill(this.m_btn_no, false);
        DOTween.Kill(this.m_btn_continue, false);
        DOTween.Kill(this.m_btn_coins, false);
        this.m_progress.DOKill(false);
    }

    private void OnApplicationFocus(bool isFocus)
    {
        if (isFocus)
        {
            UnityEngine.Debug.Log("unit ads true");
            return;
        }
        UnityEngine.Debug.Log("unit ads false");
    }

    public void Load(int gameId, int number)
    {
        GM.GetInstance().SetSavedGameID(0);
        GM.GetInstance().SaveScore(this.GameID, 0);
        this.GameID = gameId;
        this.Number = number;
        if (gameId == 1)
        {
            this.m_g00101.gameObject.SetActive(true);
            this.m_g00201.gameObject.SetActive(false);
            this.m_g00101.setNum(number);
            return;
        }
        if (gameId != 2)
        {
            return;
        }
        this.m_g00101.gameObject.SetActive(false);
        this.m_g00201.gameObject.SetActive(true);
        this.m_g00201.setNum(number);
    }

    public void OnClickAds()
    {
        int gameID = this.GameID;
        if (gameID == 1)
        {

            GM.GetInstance().SetSavedGameID(this.GameID);
            M001.GetInstance().FillLife(false);
            M001.GetInstance().DoFillLife();
            DialogManager.GetInstance().Close(null);

            this.ShowFinish();

            return;
        }
        if (gameID != 2)
        {
            return;
        }

        GM.GetInstance().SetSavedGameID(this.GameID);
        DialogManager.GetInstance().Close(null);
        Action expr_25 = M002.GetInstance().DoVedioRefresh;
        if (expr_25 == null)
        {
            return;
        }
        expr_25();

        this.ShowFinish();

    }

    public void OnClickCoin()
    {
        if (!GM.GetInstance().isFullGEM(20))
        {
            ToastManager.Show("not_enough_coins", true);
            return;
        }
        int gameID = this.GameID;
        GM.GetInstance().ConsumeGEM(20);
        if (gameID == 1)
        {
            GM.GetInstance().SetSavedGameID(this.GameID);
            M001.GetInstance().FillLife(false);
            M001.GetInstance().DoFillLife();
            DialogManager.GetInstance().Close(null);
            return;
        }
        if (gameID != 2)
        {
            return;
        }
        GM.GetInstance().SetSavedGameID(this.GameID);
        DialogManager.GetInstance().Close(null);
        Action expr_85 = M002.GetInstance().DoVedioRefresh;
        if (expr_85 == null)
        {
            return;
        }
        expr_85();
    }

    public void OnClickAgain()
    {
        if (GM.GetInstance().IsFirstFinishGame())
        {
            this.ShowFinish();
            return;
        }

        this.ShowFinish();

    }

    private void ShowFinish()
    {
        this.m_progress.DOKill(false);

        DialogManager.GetInstance().Close(delegate
        {
            int gameID = this.GameID;
            if (gameID == 1)
            {
                GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/G00102") as GameObject);
                gameObject.GetComponent<G00102>().Load(M001.GetInstance().Score, M001.GetInstance().MaxScore);
                DialogManager.GetInstance().show(gameObject, true);
                return;
            }
            if (gameID != 2)
            {
                return;
            }
            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/G00203") as GameObject);
            gameObject2.GetComponent<G00203>().Load(M002.GetInstance().Score, M002.GetInstance().MaxScore);
            DialogManager.GetInstance().show(gameObject2, true);
        });
    }
}
