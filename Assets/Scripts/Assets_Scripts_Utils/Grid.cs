using System;
using System.Collections.Generic;

namespace Assets.Scripts.Utils
{
	public class Grid
	{
		public int row;

		public int col;

		private List<List<Node>> nodes = new List<List<Node>>();

		public Grid(int col, int row)
		{
			this.col = col;
			this.row = row;
			for (int i = 0; i < col; i++)
			{
				List<Node> list = new List<Node>();
				for (int j = 0; j < row; j++)
				{
					list.Add(new Node(i, j, false));
				}
				this.nodes.Add(list);
			}
		}

		public void Clear()
		{
			for (int i = 0; i < this.col; i++)
			{
				for (int j = 0; j < this.row; j++)
				{
					if (this.getNode(i, j).isWalk)
					{
						this.getNode(i, j).g = 0f;
						this.getNode(i, j).h = 0f;
						this.getNode(i, j).f = 0f;
						this.getNode(i, j).isOpened = false;
						this.getNode(i, j).isClosed = false;
						this.getNode(i, j).parent = null;
					}
				}
			}
		}

		public Node getNode(int x, int y)
		{
			return this.nodes[x][y];
		}

		public bool isWalk(int x, int y)
		{
			return this.isInside(x, y) && this.nodes[x][y].isWalk;
		}

		public bool isInside(int x, int y)
		{
			return x >= 0 && x < this.col && y >= 0 && y < this.row;
		}

		public void setWalk(int x, int y, bool isWalk)
		{
			this.nodes[x][y].isWalk = isWalk;
		}

		public List<Node> find(Node node, DiagonalMovement diagonalMovement)
		{
			List<Node> list = new List<Node>();
			int x = node.x;
			int y = node.y;
			bool flag4;
			bool flag3;
			bool flag2;
			bool flag = flag2 = (flag3 = (flag4 = false));
			bool flag8;
			bool flag7;
			bool flag6;
			bool flag5 = flag6 = (flag7 = (flag8 = false));
			if (this.isWalk(x, y + 1))
			{
				list.Add(this.getNode(x, y + 1));
				flag2 = true;
			}
			if (this.isWalk(x + 1, y))
			{
				list.Add(this.getNode(x + 1, y));
				flag = true;
			}
			if (this.isWalk(x, y - 1))
			{
				list.Add(this.getNode(x, y - 1));
				flag3 = true;
			}
			if (this.isWalk(x - 1, y))
			{
				list.Add(this.getNode(x - 1, y));
				flag4 = true;
			}
			switch (diagonalMovement)
			{
			case DiagonalMovement.ALWAYS:
				flag5 = (flag6 = (flag7 = (flag8 = true)));
				break;
			case DiagonalMovement.NEVER:
				return list;
			case DiagonalMovement.IFATMOSTONEOBSTACLE:
				flag6 = (flag4 | flag2);
				flag5 = (flag2 | flag);
				flag7 = (flag | flag3);
				flag8 = (flag3 | flag4);
				break;
			case DiagonalMovement.ONLYWHENNOOBSTACLES:
				flag6 = (flag4 & flag2);
				flag5 = (flag2 & flag);
				flag7 = (flag & flag3);
				flag8 = (flag3 & flag4);
				break;
			}
			if (flag6 && this.isWalk(x - 1, y + 1))
			{
				list.Add(this.getNode(x - 1, y + 1));
			}
			if (flag5 && this.isWalk(x + 1, y + 1))
			{
				list.Add(this.getNode(x + 1, y + 1));
			}
			if (flag7 && this.isWalk(x + 1, y - 1))
			{
				list.Add(this.getNode(x + 1, y - 1));
			}
			if (flag8 && this.isWalk(x - 1, y - 1))
			{
				list.Add(this.getNode(x - 1, y - 1));
			}
			return list;
		}
	}
}
