using System;

namespace Assets.Scripts.GameManager
{
	public struct sGridRMD
	{
		public int gameRow;

		public int gameCol;

		public int gridRow;

		public int gridCol;

		public sGridRMD(int gameRow, int gameCol, int gridRow, int gridCol)
		{
			this.gameRow = gameRow;
			this.gameCol = gameCol;
			this.gridRow = gridRow;
			this.gridCol = gridCol;
		}
	}
}
