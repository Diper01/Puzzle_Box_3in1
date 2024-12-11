using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DG.Tweening
{
	public static class DOTweenModuleSprite
	{
		private sealed class __c__DisplayClass0_0
		{
			public SpriteRenderer target;

			internal Color _DOColor_b__0()
			{
				return this.target.color;
			}

			internal void _DOColor_b__1(Color x)
			{
				this.target.color = x;
			}
		}

		private sealed class __c__DisplayClass1_0
		{
			public SpriteRenderer target;

			internal Color _DOFade_b__0()
			{
				return this.target.color;
			}

			internal void _DOFade_b__1(Color x)
			{
				this.target.color = x;
			}
		}

		private sealed class __c__DisplayClass3_0
		{
			public Color to;

			public SpriteRenderer target;

			internal Color _DOBlendableColor_b__0()
			{
				return this.to;
			}

			internal void _DOBlendableColor_b__1(Color x)
			{
				Color b = x - this.to;
				this.to = x;
				this.target.color += b;
			}
		}

		public static Tweener DOColor(this SpriteRenderer target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFade(this SpriteRenderer target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Sequence DOGradientColor(this SpriteRenderer target, Gradient gradient, float duration)
		{
			Sequence sequence = DOTween.Sequence();
			GradientColorKey[] colorKeys = gradient.colorKeys;
			int num = colorKeys.Length;
			for (int i = 0; i < num; i++)
			{
				GradientColorKey gradientColorKey = colorKeys[i];
				if (i == 0 && gradientColorKey.time <= 0f)
				{
					target.color = gradientColorKey.color;
				}
				else
				{
					float duration2 = (i == num - 1) ? (duration - sequence.Duration(false)) : (duration * ((i == 0) ? gradientColorKey.time : (gradientColorKey.time - colorKeys[i - 1].time)));
					sequence.Append(target.DOColor(gradientColorKey.color, duration2).SetEase(Ease.Linear));
				}
			}
			return sequence;
		}

		public static Tweener DOBlendableColor(this SpriteRenderer target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate(Color x)
			{
				Color b = x - to;
				to = x;
				target.color += b;
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget(target);
		}
	}
}
