using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DG.Tweening
{
	public static class DOTweenModulePhysics2D
	{
		private sealed class __c__DisplayClass0_0
		{
			public Rigidbody2D target;

			internal Vector2 _DOMove_b__0()
			{
				return this.target.position;
			}
		}

		private sealed class __c__DisplayClass1_0
		{
			public Rigidbody2D target;

			internal Vector2 _DOMoveX_b__0()
			{
				return this.target.position;
			}
		}

		private sealed class __c__DisplayClass2_0
		{
			public Rigidbody2D target;

			internal Vector2 _DOMoveY_b__0()
			{
				return this.target.position;
			}
		}

		private sealed class __c__DisplayClass3_0
		{
			public Rigidbody2D target;

			internal float _DORotate_b__0()
			{
				return this.target.rotation;
			}
		}

		private sealed class __c__DisplayClass4_0
		{
			public Rigidbody2D target;

			public float startPosY;

			public bool offsetYSet;

			public float offsetY;

			public Sequence s;

			public Vector2 endValue;

			public Tween yTween;

			internal Vector2 _DOJump_b__0()
			{
				return this.target.position;
			}

			internal void _DOJump_b__1(Vector2 x)
			{
				this.target.position = x;
			}

			internal void _DOJump_b__2()
			{
				this.startPosY = this.target.position.y;
			}

			internal Vector2 _DOJump_b__3()
			{
				return this.target.position;
			}

			internal void _DOJump_b__4(Vector2 x)
			{
				this.target.position = x;
			}

			internal void _DOJump_b__5()
			{
				if (!this.offsetYSet)
				{
					this.offsetYSet = true;
					this.offsetY = (this.s.isRelative ? this.endValue.y : (this.endValue.y - this.startPosY));
				}
				Vector3 v = this.target.position;
				v.y += DOVirtual.EasedValue(0f, this.offsetY, this.yTween.ElapsedPercentage(true), Ease.OutQuad);
				this.target.MovePosition(v);
			}
		}

		public static Tweener DOMove(this Rigidbody2D target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOMoveX(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), new Vector2(endValue, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget(target);
		}

		public static Tweener DOMoveY(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), new Vector2(0f, endValue), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
		}

		public static Tweener DORotate(this Rigidbody2D target, float endValue, float duration)
		{
			return DOTween.To(() => target.rotation, new DOSetter<float>(target.MoveRotation), endValue, duration).SetTarget(target);
		}

		public static Sequence DOJump(this Rigidbody2D target, Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = 0f;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			Tween yTween = DOTween.To(() => target.position, delegate(Vector2 x)
			{
				target.position = x;
			}, new Vector2(0f, jumpPower), duration / (float)(numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative<Tweener>().SetLoops(numJumps * 2, LoopType.Yoyo).OnStart(delegate
			{
				startPosY = target.position.y;
			});
			s.Append(DOTween.To(() => target.position, delegate(Vector2 x)
			{
				target.position = x;
			}, new Vector2(endValue.x, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)).Join(yTween).SetTarget(target).SetEase(DOTween.defaultEaseType);
			yTween.OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = (s.isRelative ? endValue.y : (endValue.y - startPosY));
				}
				Vector3 v = target.position;
				v.y += DOVirtual.EasedValue(0f, offsetY, yTween.ElapsedPercentage(true), Ease.OutQuad);
				target.MovePosition(v);
			});
			return s;
		}
	}
}
