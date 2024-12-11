using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
	private sealed class __c__DisplayClass10_0
	{
		public int id;

		public Skin __4__this;

		internal void _OnClickSkin_b__0(int skinID)
		{
			GM.GetInstance().SetLocalSkinID(this.id);
			this.__4__this.InitUI();
		}
	}

	public GameObject[] m_items;

	public Sprite[] m_sprites;

	private void Start()
	{
	}

	private void Update()
	{
		Utils.BackListener(base.gameObject, delegate
		{
			this.OnClickReturn();
		});
	}

	private void OnEnable()
	{
		this.InitUI();
	}

	private void OnDestroy()
	{
	}


	public void OnClickReturn()
	{
		GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
	}

	public void OnClickSkin(int id = 0)
	{
		List<int> skinData = GM.GetInstance().GetSkinData();
		if (id > skinData.Count)
		{
			return;
		}
		if (skinData[id - 1] == 1)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/skinBuy") as GameObject);
			gameObject.GetComponent<SkinUnLock>().Load(id);
			gameObject.GetComponent<SkinUnLock>().SetOnUnlockSuccess(delegate(int skinID)
			{
				GM.GetInstance().SetLocalSkinID(id);
				this.InitUI();
			});
			DialogManager.GetInstance().show(gameObject, false);
			return;
		}
		GM.GetInstance().SetLocalSkinID(id);
		this.InitUI();
	}

	private void InitUI()
	{
		if (this.m_items == null || this.m_sprites == null)
		{
			return;
		}
		List<int> skinData = GM.GetInstance().GetSkinData();
		int num = 0;
		while (num < this.m_items.Length && num < skinData.Count)
		{
			int num2 = skinData[num];
			GameObject gameObject = this.m_items[num];
			switch (num2)
			{
			case 0:
				if (GM.GetInstance().SkinID == num + 1)
				{
					gameObject.transform.Find("img_status").GetComponent<Image>().sprite = this.m_sprites[1];
				}
				else
				{
					gameObject.transform.Find("img_status").GetComponent<Image>().sprite = this.m_sprites[0];
				}
				break;
			case 1:
				gameObject.transform.Find("img_status").GetComponent<Image>().sprite = this.m_sprites[2];
				break;
			case 2:
				if (GM.GetInstance().SkinID == num + 1)
				{
					gameObject.transform.Find("img_status").GetComponent<Image>().sprite = this.m_sprites[1];
				}
				else
				{
					gameObject.transform.Find("img_status").GetComponent<Image>().sprite = this.m_sprites[0];
				}
				break;
			}
			num++;
		}
	}

	
}
