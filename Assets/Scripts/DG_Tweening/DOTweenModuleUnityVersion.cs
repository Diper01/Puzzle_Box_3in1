using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DG.Tweening
{
	public static class DOTweenModuleUnityVersion
	{
		private sealed class __c__DisplayClass8_0
		{
			public Material target;

			public int propertyID;

			internal Vector2 _DOOffset_b__0()
			{
				return this.target.GetTextureOffset(this.propertyID);
			}

			internal void _DOOffset_b__1(Vector2 x)
			{
				this.target.SetTextureOffset(this.propertyID, x);
			}
		}

		private sealed class __c__DisplayClass9_0
		{
			public Material target;

			public int propertyID;

			internal Vector2 _DOTiling_b__0()
			{
				return this.target.GetTextureScale(this.propertyID);
			}

			internal void _DOTiling_b__1(Vector2 x)
			{
				this.target.SetTextureScale(this.propertyID, x);
			}
		}

		public static Sequence DOGradientColor(this Material target, Gradient gradient, float duration)
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

		public static Sequence DOGradientColor(this Material target, Gradient gradient, string property, float duration)
		{
			Sequence sequence = DOTween.Sequence();
			GradientColorKey[] colorKeys = gradient.colorKeys;
			int num = colorKeys.Length;
			for (int i = 0; i < num; i++)
			{
				GradientColorKey gradientColorKey = colorKeys[i];
				if (i == 0 && gradientColorKey.time <= 0f)
				{
					target.SetColor(property, gradientColorKey.color);
				}
				else
				{
					float duration2 = (i == num - 1) ? (duration - sequence.Duration(false)) : (duration * ((i == 0) ? gradientColorKey.time : (gradientColorKey.time - colorKeys[i - 1].time)));
					sequence.Append(target.DOColor(gradientColorKey.color, property, duration2).SetEase(Ease.Linear));
				}
			}
			return sequence;
		}

		public static CustomYieldInstruction WaitForCompletion(this Tween t, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new DOTweenCYInstruction.WaitForCompletion(t);
		}

		public static CustomYieldInstruction WaitForRewind(this Tween t, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new DOTweenCYInstruction.WaitForRewind(t);
		}

		public static CustomYieldInstruction WaitForKill(this Tween t, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new DOTweenCYInstruction.WaitForKill(t);
		}

		public static CustomYieldInstruction WaitForElapsedLoops(this Tween t, int elapsedLoops, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new DOTweenCYInstruction.WaitForElapsedLoops(t, elapsedLoops);
		}

		public static CustomYieldInstruction WaitForPosition(this Tween t, float position, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new DOTweenCYInstruction.WaitForPosition(t, position);
		}

		public static CustomYieldInstruction WaitForStart(this Tween t, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new DOTweenCYInstruction.WaitForStart(t);
		}

		public static Tweener DOOffset(this Material target, Vector2 endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			return DOTween.To(() => target.GetTextureOffset(propertyID), delegate(Vector2 x)
			{
				target.SetTextureOffset(propertyID, x);
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOTiling(this Material target, Vector2 endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			return DOTween.To(() => target.GetTextureScale(propertyID), delegate(Vector2 x)
			{
				target.SetTextureScale(propertyID, x);
			}, endValue, duration).SetTarget(target);
		}
	}
}
