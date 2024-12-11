using Assets.Scripts.GameManager;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinComponent : MonoBehaviour
{
	public bool m_isFont;

	public bool m_isColor;

	public bool m_isSprite;

	public Color[] m_colors;

	public Sprite[] m_sprites;

	private void Start()
	{
		this.Transiform();
		GlobalEventHandle.DoTransiformSkin = (Action)Delegate.Combine(GlobalEventHandle.DoTransiformSkin, new Action(this.Transiform));
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
		GlobalEventHandle.DoTransiformSkin = (Action)Delegate.Remove(GlobalEventHandle.DoTransiformSkin, new Action(this.Transiform));
	}

	private void Transiform()
	{
		if (this.m_isFont)
		{
			if (this.m_colors == null || this.m_colors.Length == 0)
			{
				return;
			}
			Text component = base.GetComponent<Text>();
			if (component != null)
			{
				component.color = this.m_colors[GM.GetInstance().SkinID - 1];
			}
		}
		if (this.m_isColor)
		{
			if (this.m_colors == null || this.m_colors.Length == 0)
			{
				return;
			}
			Image component2 = base.GetComponent<Image>();
			if (component2 != null)
			{
				component2.color = this.m_colors[GM.GetInstance().SkinID - 1];
			}
		}
		if (this.m_isSprite)
		{
			if (this.m_sprites == null || this.m_sprites.Length == 0)
			{
				return;
			}
			Image component3 = base.GetComponent<Image>();
			if (component3 != null)
			{
				component3.sprite = this.m_sprites[GM.GetInstance().SkinID - 1];
			}
		}
	}
}
