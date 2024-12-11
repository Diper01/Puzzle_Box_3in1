using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DG.Tweening
{
	public static class DOTweenModulePhysics
	{
		private sealed class __c__DisplayClass0_0
		{
			public Rigidbody target;

			internal Vector3 _DOMove_b__0()
			{
				return this.target.position;
			}
		}

		private sealed class __c__DisplayClass1_0
		{
			public Rigidbody target;

			internal Vector3 _DOMoveX_b__0()
			{
				return this.target.position;
			}
		}

		private sealed class __c__DisplayClass2_0
		{
			public Rigidbody target;

			internal Vector3 _DOMoveY_b__0()
			{
				return this.target.position;
			}
		}

		private sealed class __c__DisplayClass3_0
		{
			public Rigidbody target;

			internal Vector3 _DOMoveZ_b__0()
			{
				return this.target.position;
			}
		}

		private sealed class __c__DisplayClass4_0
		{
			public Rigidbody target;

			internal Quaternion _DORotate_b__0()
			{
				return this.target.rotation;
			}
		}

		private sealed class __c__DisplayClass5_0
		{
			public Rigidbody target;

			internal Quaternion _DOLookAt_b__0()
			{
				return this.target.rotation;
			}
		}

		private sealed class __c__DisplayClass6_0
		{
			public Rigidbody target;

			public float startPosY;

			public bool offsetYSet;

			public float offsetY;

			public Sequence s;

			public Vector3 endValue;

			public Tween yTween;

			internal Vector3 _DOJump_b__0()
			{
				return this.target.position;
			}

			internal void _DOJump_b__1()
			{
				this.startPosY = this.target.position.y;
			}

			internal Vector3 _DOJump_b__2()
			{
				return this.target.position;
			}

			internal Vector3 _DOJump_b__3()
			{
				return this.target.position;
			}

			internal void _DOJump_b__4()
			{
				if (!this.offsetYSet)
				{
					this.offsetYSet = true;
					this.offsetY = (this.s.isRelative ? this.endValue.y : (this.endValue.y - this.startPosY));
				}
				Vector3 position = this.target.position;
				position.y += DOVirtual.EasedValue(0f, this.offsetY, this.yTween.ElapsedPercentage(true), Ease.OutQuad);
				this.target.MovePosition(position);
			}
		}

		private sealed class __c__DisplayClass7_0
		{
			public Rigidbody target;

			internal Vector3 _DOPath_b__0()
			{
				return this.target.position;
			}
		}

		private sealed class __c__DisplayClass8_0
		{
			public Transform trans;

			public Rigidbody target;

			internal Vector3 _DOLocalPath_b__0()
			{
				return this.trans.localPosition;
			}

			internal void _DOLocalPath_b__1(Vector3 x)
			{
				this.target.MovePosition((this.trans.parent == null) ? x : this.trans.parent.TransformPoint(x));
			}
		}

		private sealed class __c__DisplayClass9_0
		{
			public Rigidbody target;

			internal Vector3 _DOPath_b__0()
			{
				return this.target.position;
			}
		}

		private sealed class __c__DisplayClass10_0
		{
			public Transform trans;

			public Rigidbody target;

			internal Vector3 _DOLocalPath_b__0()
			{
				return this.trans.localPosition;
			}

			internal void _DOLocalPath_b__1(Vector3 x)
			{
				this.target.MovePosition((this.trans.parent == null) ? x : this.trans.parent.TransformPoint(x));
			}
		}

		public static Tweener DOMove(this Rigidbody target, Vector3 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOMoveX(this Rigidbody target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(endValue, 0f, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget(target);
		}

		public static Tweener DOMoveY(this Rigidbody target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(0f, endValue, 0f), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
		}

		public static Tweener DOMoveZ(this Rigidbody target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(0f, 0f, endValue), duration).SetOptions(AxisConstraint.Z, snapping).SetTarget(target);
		}

		public static Tweener DORotate(this Rigidbody target, Vector3 endValue, float duration, RotateMode mode = RotateMode.Fast)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> expr_31 = DOTween.To(() => target.rotation, new DOSetter<Quaternion>(target.MoveRotation), endValue, duration);
			expr_31.SetTarget(target);
			expr_31.plugOptions.rotateMode = mode;
			return expr_31;
		}

		public static Tweener DOLookAt(this Rigidbody target, Vector3 towards, float duration, AxisConstraint axisConstraint = AxisConstraint.None, Vector3? up = null)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> expr_42 = DOTween.To(() => target.rotation, new DOSetter<Quaternion>(target.MoveRotation), towards, duration).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetLookAt);
			expr_42.plugOptions.axisConstraint = axisConstraint;
			expr_42.plugOptions.up = ((!up.HasValue) ? Vector3.up : up.Value);
			return expr_42;
		}

		public static Sequence DOJump(this Rigidbody target, Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = 0f;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			Tween yTween = DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(0f, jumpPower, 0f), duration / (float)(numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative<Tweener>().SetLoops(numJumps * 2, LoopType.Yoyo).OnStart(delegate
			{
				startPosY = target.position.y;
			});
			s.Append(DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(endValue.x, 0f, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)).Join(DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(0f, 0f, endValue.z), duration).SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear)).Join(yTween).SetTarget(target).SetEase(DOTween.defaultEaseType);
			yTween.OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = (s.isRelative ? endValue.y : (endValue.y - startPosY));
				}
				Vector3 position = target.position;
				position.y += DOVirtual.EasedValue(0f, offsetY, yTween.ElapsedPercentage(true), Ease.OutQuad);
				target.MovePosition(position);
			});
			return s;
		}

		public static TweenerCore<Vector3, Path, PathOptions> DOPath(this Rigidbody target, Vector3[] path, float duration, PathType pathType = PathType.Linear, PathMode pathMode = PathMode.Full3D, int resolution = 10, Color? gizmoColor = null)
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			TweenerCore<Vector3, Path, PathOptions> expr_59 = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.position, new DOSetter<Vector3>(target.MovePosition), new Path(pathType, path, resolution, gizmoColor), duration).SetTarget(target).SetUpdate(UpdateType.Fixed);
			expr_59.plugOptions.isRigidbody = true;
			expr_59.plugOptions.mode = pathMode;
			return expr_59;
		}

		public static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(this Rigidbody target, Vector3[] path, float duration, PathType pathType = PathType.Linear, PathMode pathMode = PathMode.Full3D, int resolution = 10, Color? gizmoColor = null)
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			Transform trans = target.transform;
			TweenerCore<Vector3, Path, PathOptions> expr_65 = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => trans.localPosition, delegate(Vector3 x)
			{
				target.MovePosition((trans.parent == null) ? x : trans.parent.TransformPoint(x));
			}, new Path(pathType, path, resolution, gizmoColor), duration).SetTarget(target).SetUpdate(UpdateType.Fixed);
			expr_65.plugOptions.isRigidbody = true;
			expr_65.plugOptions.mode = pathMode;
			expr_65.plugOptions.useLocalPosition = true;
			return expr_65;
		}

		internal static TweenerCore<Vector3, Path, PathOptions> DOPath(this Rigidbody target, Path path, float duration, PathMode pathMode = PathMode.Full3D)
		{
			TweenerCore<Vector3, Path, PathOptions> expr_41 = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.position, new DOSetter<Vector3>(target.MovePosition), path, duration).SetTarget(target);
			expr_41.plugOptions.isRigidbody = true;
			expr_41.plugOptions.mode = pathMode;
			return expr_41;
		}

		internal static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(this Rigidbody target, Path path, float duration, PathMode pathMode = PathMode.Full3D)
		{
			Transform trans = target.transform;
			TweenerCore<Vector3, Path, PathOptions> expr_4D = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => trans.localPosition, delegate(Vector3 x)
			{
				target.MovePosition((trans.parent == null) ? x : trans.parent.TransformPoint(x));
			}, path, duration).SetTarget(target);
			expr_4D.plugOptions.isRigidbody = true;
			expr_4D.plugOptions.mode = pathMode;
			expr_4D.plugOptions.useLocalPosition = true;
			return expr_4D;
		}
	}
}
