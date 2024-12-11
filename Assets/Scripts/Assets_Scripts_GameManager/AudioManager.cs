using Assets.Scripts.Configs;
using System;
using UnityEngine;

namespace Assets.Scripts.GameManager
{
	internal class AudioManager : MonoBehaviour
	{
		private static AudioManager g_intance;

		public AudioSource audio_bg;

		public AudioSource audio_eff;

		private int switch_bg;

		private int switch_eff;

		public int Switch_bg
		{
			get
			{
				return this.switch_bg;
			}
			set
			{
				this.switch_bg = value;
			}
		}

		public int Switch_eff
		{
			get
			{
				return this.switch_eff;
			}
			set
			{
				this.switch_eff = value;
			}
		}

		private void Awake()
		{
			AudioManager.g_intance = this;
		}

		private void Start()
		{
			this.Init();
		}

		public static AudioManager GetInstance()
		{
			return AudioManager.g_intance;
		}

		public void Init()
		{
			this.Switch_bg = PlayerPrefs.GetInt("LocalData_Music", 1);
			this.Switch_eff = PlayerPrefs.GetInt("LocalData_Effect", 1);
			this.PlayBgMusic();
		}

		public void PlayBgMusic()
		{
			if (this.Switch_bg == 1)
			{
				this.audio_bg.Play();
				return;
			}
			this.StopBgMusic();
		}

		public void PlayBgMusic(string idx, bool isLoop = false)
		{
			if (!Configs.Configs.TSounds.ContainsKey(idx))
			{
				return;
			}
			if (this.Switch_eff == 1)
			{
				AudioClip clip = Resources.Load(Configs.Configs.TSounds[idx].Path, typeof(AudioClip)) as AudioClip;
				this.audio_bg.PlayOneShot(clip);
				this.audio_bg.loop = isLoop;
				return;
			}
			this.StopBgMusic();
		}

		public void StopBgMusic()
		{
			this.audio_bg.Stop();
		}

		public void PauseBgMusic()
		{
			this.audio_bg.Pause();
		}

		public void PlayEffect(string idx)
		{
			if (!Configs.Configs.TSounds.ContainsKey(idx))
			{
				return;
			}
			if (this.Switch_eff == 1)
			{
				AudioClip clip = Resources.Load(Configs.Configs.TSounds[idx].Path, typeof(AudioClip)) as AudioClip;
				this.audio_eff.PlayOneShot(clip);
				this.audio_eff.loop = false;
			}
		}

		public void SetMusicSwitch(int _switch)
		{
			this.Switch_bg = _switch;
			PlayerPrefs.SetInt("LocalData_Music", _switch);
		}

		public void SetEffectSwitch(int _switch)
		{
			this.Switch_eff = _switch;
			PlayerPrefs.SetInt("LocalData_Effect", _switch);
		}
	}
}
