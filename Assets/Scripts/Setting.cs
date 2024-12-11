using Assets.Scripts.GameManager;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
	private AudioManager audioManager;

	public Sprite m_asset_switch_off;

	public Sprite m_asset_switch_on;

	public Image m_img_music_switch;

	public Image m_img_effect_switch;

	private void Start()
	{
		this.Init();
	}

	public void Init()
	{
		this.audioManager = AudioManager.GetInstance();
		this.InitUI();
	}

	public void InitUI()
	{
		this.m_img_music_switch.sprite = ((this.audioManager.Switch_bg == 1) ? this.m_asset_switch_on : this.m_asset_switch_off);
		this.m_img_effect_switch.sprite = ((this.audioManager.Switch_eff == 1) ? this.m_asset_switch_on : this.m_asset_switch_off);
	}

	public void OnClickMusicBtn()
	{
		if (this.audioManager.Switch_bg == 1)
		{
			this.audioManager.SetMusicSwitch(0);
			this.audioManager.StopBgMusic();
		}
		else
		{
			this.audioManager.SetMusicSwitch(1);
			this.audioManager.PlayBgMusic();
		}
		this.InitUI();
	}

	public void OnClickEffectBtn()
	{
		if (this.audioManager.Switch_eff == 1)
		{
			this.audioManager.SetEffectSwitch(0);
		}
		else
		{
			this.audioManager.SetEffectSwitch(1);
		}
		this.InitUI();
	}

	public void OnClickLanguage()
	{
		GameObject obj = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/setting_language") as GameObject);
		DialogManager.GetInstance().show(obj, false);
	}

	public void OnClickClose()
	{
        PauseManager.instance.Resume();
		DialogManager.GetInstance().Close(null);
		AudioManager.GetInstance().PlayEffect("sound_eff_button");
	}
}
