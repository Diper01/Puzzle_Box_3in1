using System;

namespace Assets.Scripts.Utils
{
	public class Node
	{
		public int x;

		public int y;

		public float g;

		public float h;

		public float f;

		public bool isWalk;

		public bool isOpened;

		public bool isClosed;

		public Node parent;

		public Node(int x, int y, bool isWalk)
		{
			this.x = x;
			this.y = y;
			this.isWalk = isWalk;
			this.g = (this.h = (this.f = 0f));
			this.isOpened = false;
			this.isClosed = false;
			this.parent = null;
		}
	}
}
