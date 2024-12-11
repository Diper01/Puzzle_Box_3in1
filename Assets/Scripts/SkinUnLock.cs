using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinUnLock : MonoBehaviour
{
	public Sprite[] m_skins;

	public Image m_skinIcon;

	public Text m_skinPrice;

	private int m_skinID = 1;

	private int[] prices = new int[]
	{
		0,
		100
	};

	private Action<int> OnUnlockSuccess;

	public int SkinID
	{
		get
		{
			return this.m_skinID;
		}
		set
		{
			this.m_skinID = value;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Load(int skinID)
	{
		this.SkinID = skinID;
		if (this.m_skinIcon != null)
		{
			this.m_skinIcon.sprite = this.m_skins[skinID - 1];
		}
		if (this.m_skinPrice != null)
		{
			this.m_skinPrice.text = this.prices[this.SkinID - 1].ToString();
		}
	}


	public void OnClickBuy()
	{
		if (!GM.GetInstance().isFullGEM(this.prices[this.SkinID - 1]))
		{
			ToastManager.Show("not_enough_coins", true);
			return;
		}
		GM.GetInstance().ConsumeGEM(this.prices[this.SkinID - 1]);
		GM.GetInstance().SetSkinData(this.SkinID, 0);
		//AppsflyerUtils.TrackBuySkin(this.SkinID, 2);
		Action<int> expr_63 = this.OnUnlockSuccess;
		if (expr_63 != null)
		{
			expr_63(this.SkinID);
		}
		DialogManager.GetInstance().Close(null);
	}

	public void OnClickClose()
	{
		DialogManager.GetInstance().Close(null);
	}

	public void SetOnUnlockSuccess(Action<int> callfunc)
	{
		this.OnUnlockSuccess = callfunc;
	}
}
