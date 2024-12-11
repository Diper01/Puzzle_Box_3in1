using System;

namespace Assets.Scripts.Utils
{
	public struct Opera
	{
		public int x;

		public int y;

		public bool isOpen;

		public bool isClose;

		public Opera(int px, int py, bool pIsOpen, bool pIsClose)
		{
			this.x = px;
			this.y = py;
			this.isOpen = pIsOpen;
			this.isClose = pIsClose;
		}
	}
}
