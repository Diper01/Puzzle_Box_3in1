using System;

namespace Assets.Scripts.GameManager
{
	public struct sDropData
	{
		public int srcIdx;

		public int dstIdx;

		public sDropData(int src, int dst)
		{
			this.srcIdx = src;
			this.dstIdx = dst;
		}
	}
}
