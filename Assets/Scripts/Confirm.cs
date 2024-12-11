using Assets.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Confirm : MonoBehaviour
{
	public enum ConfirmType
	{
		OK,
		OkAndCancel,
		VEDIO,
		VEDIO2
	}

    [SerializeField] Text titleText;

	private Action m_onClickOkHandle;

	private Action m_onClickCancelHandle;

	public Text m_txt_content;

	public Text m_txt_value;

	public GameObject m_btn_ok;

	public GameObject m_btn_cancel;

	public GameObject m_btn_confirm;


	private Confirm.ConfirmType m_type = Confirm.ConfirmType.OkAndCancel;

	private void Start()
	{
		switch (this.m_type)
		{
		case Confirm.ConfirmType.OK:
			this.m_btn_ok.SetActive(false);
			this.m_btn_cancel.SetActive(false);
			this.m_btn_confirm.SetActive(true);
			return;
		case Confirm.ConfirmType.OkAndCancel:
			this.m_btn_ok.SetActive(true);
			this.m_btn_cancel.SetActive(true);
			this.m_btn_confirm.SetActive(false);
			return;
		case Confirm.ConfirmType.VEDIO:
			this.m_btn_ok.SetActive(false);
			this.m_btn_cancel.SetActive(true);
			this.m_btn_confirm.SetActive(false);
			return;
		case Confirm.ConfirmType.VEDIO2:
			this.m_btn_ok.SetActive(true);
			this.m_btn_cancel.SetActive(true);
			this.m_btn_confirm.SetActive(false);
			return;
		default:
			return;
		}
	}

	private void Update()
	{
	}

	public void SetType(Confirm.ConfirmType type)
	{
		this.m_type = type;
	}

	public void SetText(string txt, bool isID = true)
	{
		if (isID)
		{
			this.m_txt_content.text = Localisation.GetString(txt);
            if(txt == "do_you_want_to_quit_the_game?")
            {
                SetTitle();
            }
		}
		else
		{
			this.m_txt_content.text = txt;
		}
        
		//this.m_txt_content.font = Language.GetFont();
	}
    private void SetTitle()
    {
        titleText.text = Localisation.GetString("Exit");
    }
	public void SetValue(int value)
	{
		this.m_txt_value.text = value.ToString();
	}

	public void OnClickOk()
	{
		DialogManager.GetInstance().Close(null);
		Action expr_11 = this.m_onClickOkHandle;
		if (expr_11 == null)
		{
			return;
		}
		expr_11();
	}

	public void OnClickCancel()
	{
		DialogManager.GetInstance().Close(null);
		Action expr_11 = this.m_onClickCancelHandle;
		if (expr_11 == null)
		{
			return;
		}
		expr_11();
	}

	public void SetCallFunc(Action okCallFunc = null, Action cancelCallFunc = null)
	{
		this.m_onClickOkHandle = okCallFunc;
		this.m_onClickCancelHandle = cancelCallFunc;
	}
}
