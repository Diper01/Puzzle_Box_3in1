using Assets.Scripts.GameManager;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class G00305 : MonoBehaviour
{
	public static int STAR_WIDTH = 35;

	public GameObject Img_Star;

	public Image Img_bg;

	public Image Progress;

	public Text Desc;

	public Text Label;

	[SerializeField]
	public List<Color> colors = new List<Color>();

	private int m_id;

	private int m_fullNum;

	private bool m_isActivity = true;

	public int Id
	{
		get
		{
			return this.m_id;
		}
		set
		{
			this.m_id = value;
		}
	}

	public int FullNum
	{
		get
		{
			return this.m_fullNum;
		}
		set
		{
			this.m_fullNum = value;
		}
	}

	public bool IsActivity
	{
		get
		{
			return this.m_isActivity;
		}
		set
		{
			this.m_isActivity = value;
		}
	}

	public void Init(int id = 0, int lv = 0, int num = 0, int maxnum = 0, string desc = "")
	{
		this.Id = id;
		this.SetMaxNum(maxnum);
		this.SetImgStar(lv);
		this.SetLabel(num);
		this.SetDesc(desc);
		this.SetProgress((float)num / (float)maxnum);
	}

	public void OnClick()
	{
		//if (this.IsActivity)
		//{
		//	M003.GetInstance().DoClickLvHandler(this);
		//}
		//else
		//{
		//	ToastManager.Show("level_not_open", true);
		//}
		M003.GetInstance().DoClickLvHandler(this);
		AudioManager.GetInstance().PlayEffect("sound_eff_button");
	}

	public void SetIsUnLock(bool isUnlock)
	{
		this.IsActivity = isUnlock;
		this.Img_bg.color = this.colors[isUnlock ? 0 : 1];
	}

	public void SetMaxNum(int max)
	{
		this.FullNum = max;
	}

	public void SetImgStar(int star)
	{
		if (star < 0 || star > 5)
		{
			return;
		}
		for (int i = 1; i <= 5; i++)
		{
			Transform transform = this.Img_Star.transform.Find("img_star_" + i.ToString());
			transform.gameObject.SetActive(i <= star);
			if (i <= star)
			{
				transform.GetComponent<RectTransform>().localPosition = new Vector3((float)((star - 1) * -(float)(G00305.STAR_WIDTH / 2) + (i - 1) * G00305.STAR_WIDTH), 0f, 0f);
			}
		}
	}

	public void SetProgress(float perc)
	{
		this.Progress.fillAmount = perc;
	}

	public void SetDesc(string lang)
	{
		this.Desc.text = Localisation.GetString(lang);
	}

	public void SetLabel(int num)
	{
		this.Label.text = string.Format("{0}/{1}", num, this.FullNum);
	}
}
