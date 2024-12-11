using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Assets.Scripts.Utils
{
	public class AStar
	{
		[Serializable]
		private sealed class __c
		{
			public static readonly AStar.__c __9 = new AStar.__c();

			public static Comparison<Node> __9__6_0;

			internal int _Find_b__6_0(Node a, Node b)
			{
				return -a.f.CompareTo(b.f);
			}
		}

		private List<Node> openList = new List<Node>();

		private List<Opera> closeList = new List<Opera>();

		private Grid grid;

		private DiagonalMovement diagonalMovement = DiagonalMovement.NEVER;

		private HeuristicType heuristicType;

		public AStar(Grid grid, DiagonalMovement diagonalMovement, HeuristicType heuristictType)
		{
			this.grid = grid;
			this.diagonalMovement = diagonalMovement;
		}

		public List<Node> Find(AVec2 startPos, AVec2 endPos)
		{
			List<Node> result = new List<Node>();
			this.openList.Clear();
			this.closeList.Clear();
			this.grid.Clear();
			Node node = this.grid.getNode(startPos.x, startPos.y);
			Node node2 = this.grid.getNode(endPos.x, endPos.y);
			this.openList.Add(node);
			node.isOpened = true;
			this.closeList.Add(new Opera(node.x, node.y, node.isOpened, node.isClosed));
			while (this.openList.Count > 0)
			{
				Node node3 = this.openList.Last<Node>();
				this.openList.Remove(node3);
				node3.isClosed = true;
				this.closeList.Add(new Opera(node3.x, node3.y, node3.isOpened, node3.isClosed));
				if (node3.x == node2.x && node3.y == node2.y)
				{
					return this.BackTrace(node3, node);
				}
				List<Node> list = this.grid.find(node3, this.diagonalMovement);
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					Node node4 = list[i];
					if (!node4.isClosed)
					{
						float num = (node4.x == node3.x || node4.y == node3.y) ? 1f : 1.4f;
						float num2 = node3.g + num;
						if (!node4.isOpened || num2 < node4.g)
						{
							node4.g = num2;
							node4.h = this.doCost((float)Math.Abs(node4.x - node2.x), (float)Math.Abs(node4.y - node2.y));
							node4.f = node4.g + node4.h;
							node4.parent = node3;
							if (!node4.isOpened)
							{
								this.openList.Add(node4);
								node4.isOpened = true;
								this.closeList.Add(new Opera(node4.x, node4.y, node4.isOpened, node4.isClosed));
							}
						}
					}
					i++;
				}
				List<Node> arg_27B_0 = this.openList;
				Comparison<Node> arg_27B_1;
				if ((arg_27B_1 = AStar.__c.__9__6_0) == null)
				{
					arg_27B_1 = (AStar.__c.__9__6_0 = new Comparison<Node>(AStar.__c.__9._Find_b__6_0));
				}
				arg_27B_0.Sort(arg_27B_1);
			}
			return result;
		}

		public List<Node> BackTrace(Node node, Node startNode)
		{
			List<Node> list = new List<Node>();
			list.Add(node);
			while (node.parent != null)
			{
				node = node.parent;
				list.Insert(0, node);
			}
			return list;
		}

		public float doCost(float dx, float dy)
		{
			if (this.heuristicType == HeuristicType.MANHATTAN)
			{
				return this.manhattan(dx, dy);
			}
			if (HeuristicType.EUCLIDEAN == this.heuristicType)
			{
				return this.euclidean(dx, dy);
			}
			if (HeuristicType.OCTILE == this.heuristicType)
			{
				return this.octile(dx, dy);
			}
			if (HeuristicType.CHEBYSHEV == this.heuristicType)
			{
				return this.chebyshev(dx, dy);
			}
			return 0f;
		}

		private float manhattan(float dx, float dy)
		{
			return dx + dy;
		}

		private float euclidean(float dx, float dy)
		{
			return (float)Math.Sqrt((double)(dx * dx + dy * dy));
		}

		private float octile(float dx, float dy)
		{
			float num = this.euclidean(dx, dy) - 1f;
			if (dx >= dy)
			{
				return num * dy + dx;
			}
			return num * dx + dy;
		}

		private float chebyshev(float dx, float dy)
		{
			if (dx <= dy)
			{
				return dy;
			}
			return dx;
		}
	}
}
