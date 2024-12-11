using System;
using UnityEngine;

namespace DG.Tweening
{
	public static class DOTweenCYInstruction
	{
		public class WaitForCompletion : CustomYieldInstruction
		{
			private readonly Tween t;

			public override bool keepWaiting
			{
				get
				{
					return this.t.active && !this.t.IsComplete();
				}
			}

			public WaitForCompletion(Tween tween)
			{
				this.t = tween;
			}
		}

		public class WaitForRewind : CustomYieldInstruction
		{
			private readonly Tween t;

			public override bool keepWaiting
			{
				get
				{
					return this.t.active && (!this.t.playedOnce || this.t.position * (float)(this.t.CompletedLoops() + 1) > 0f);
				}
			}

			public WaitForRewind(Tween tween)
			{
				this.t = tween;
			}
		}

		public class WaitForKill : CustomYieldInstruction
		{
			private readonly Tween t;

			public override bool keepWaiting
			{
				get
				{
					return this.t.active;
				}
			}

			public WaitForKill(Tween tween)
			{
				this.t = tween;
			}
		}

		public class WaitForElapsedLoops : CustomYieldInstruction
		{
			private readonly Tween t;

			private readonly int elapsedLoops;

			public override bool keepWaiting
			{
				get
				{
					return this.t.active && this.t.CompletedLoops() < this.elapsedLoops;
				}
			}

			public WaitForElapsedLoops(Tween tween, int elapsedLoops)
			{
				this.t = tween;
				this.elapsedLoops = elapsedLoops;
			}
		}

		public class WaitForPosition : CustomYieldInstruction
		{
			private readonly Tween t;

			private readonly float position;

			public override bool keepWaiting
			{
				get
				{
					return this.t.active && this.t.position * (float)(this.t.CompletedLoops() + 1) < this.position;
				}
			}

			public WaitForPosition(Tween tween, float position)
			{
				this.t = tween;
				this.position = position;
			}
		}

		public class WaitForStart : CustomYieldInstruction
		{
			private readonly Tween t;

			public override bool keepWaiting
			{
				get
				{
					return this.t.active && !this.t.playedOnce;
				}
			}

			public WaitForStart(Tween tween)
			{
				this.t = tween;
			}
		}
	}
}
