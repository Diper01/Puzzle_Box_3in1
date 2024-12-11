using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

namespace DG.Tweening
{
	public static class DOTweenModuleAudio
	{
		private sealed class __c__DisplayClass0_0
		{
			public AudioSource target;

			internal float _DOFade_b__0()
			{
				return this.target.volume;
			}

			internal void _DOFade_b__1(float x)
			{
				this.target.volume = x;
			}
		}

		private sealed class __c__DisplayClass1_0
		{
			public AudioSource target;

			internal float _DOPitch_b__0()
			{
				return this.target.pitch;
			}

			internal void _DOPitch_b__1(float x)
			{
				this.target.pitch = x;
			}
		}

		private sealed class __c__DisplayClass2_0
		{
			public AudioMixer target;

			public string floatName;

			internal float _DOSetFloat_b__0()
			{
				float result;
				this.target.GetFloat(this.floatName, out result);
				return result;
			}

			internal void _DOSetFloat_b__1(float x)
			{
				this.target.SetFloat(this.floatName, x);
			}
		}

		public static Tweener DOFade(this AudioSource target, float endValue, float duration)
		{
			if (endValue < 0f)
			{
				endValue = 0f;
			}
			else if (endValue > 1f)
			{
				endValue = 1f;
			}
			return DOTween.To(() => target.volume, delegate(float x)
			{
				target.volume = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOPitch(this AudioSource target, float endValue, float duration)
		{
			return DOTween.To(() => target.pitch, delegate(float x)
			{
				target.pitch = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOSetFloat(this AudioMixer target, string floatName, float endValue, float duration)
		{
			return DOTween.To(delegate
			{
				float result;
				target.GetFloat(floatName, out result);
				return result;
			}, delegate(float x)
			{
				target.SetFloat(floatName, x);
			}, endValue, duration).SetTarget(target);
		}

		public static int DOComplete(this AudioMixer target, bool withCallbacks = false)
		{
			return DOTween.Complete(target, withCallbacks);
		}

		public static int DOKill(this AudioMixer target, bool complete = false)
		{
			return DOTween.Kill(target, complete);
		}

		public static int DOFlip(this AudioMixer target)
		{
			return DOTween.Flip(target);
		}

		public static int DOGoto(this AudioMixer target, float to, bool andPlay = false)
		{
			return DOTween.Goto(target, to, andPlay);
		}

		public static int DOPause(this AudioMixer target)
		{
			return DOTween.Pause(target);
		}

		public static int DOPlay(this AudioMixer target)
		{
			return DOTween.Play(target);
		}

		public static int DOPlayBackwards(this AudioMixer target)
		{
			return DOTween.PlayBackwards(target);
		}

		public static int DOPlayForward(this AudioMixer target)
		{
			return DOTween.PlayForward(target);
		}

		public static int DORestart(this AudioMixer target)
		{
			return DOTween.Restart(target, true, -1f);
		}

		public static int DORewind(this AudioMixer target)
		{
			return DOTween.Rewind(target, true);
		}

		public static int DOSmoothRewind(this AudioMixer target)
		{
			return DOTween.SmoothRewind(target);
		}

		public static int DOTogglePause(this AudioMixer target)
		{
			return DOTween.TogglePause(target);
		}
	}
}
