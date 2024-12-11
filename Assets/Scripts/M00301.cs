using Assets.Scripts.Configs;
using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class M00301 : MonoBehaviour
{
	public enum Direction
	{
		NONE,
		UP,
		RIGHT,
		DOWN,
		LEFT
	}

	public enum Node_type
	{
		NONE,
		TARGET,
		SEGMENT
	}

	[Serializable]
	private sealed class __c
	{
		public static readonly M00301.__c __9 = new M00301.__c();

		public static Comparison<int[]> __9__84_0;

		internal int _InitTipsPath_b__84_0(int[] a, int[] b)
		{
			return a[2].CompareTo(b[2]);
		}
	}

	public const float MAP_WIDTH = 565f;

	public const float MAP_HEIGHT = 565f;

	public int m_map_row = 5;

	public int m_map_col = 5;









	public Action<GameList> OnClickReturnHandle;

	private int m_checkPoint;

	private int m_topCheckPoint;

	private float m_cell_width = 113f;

	private float m_cell_height = 113f;

	private TG00301 m_map_config;

	private Color_Node[,] m_maps;

	private List<int[]> m_node_config = new List<int[]>();

	private List<int[]>[] m_path;

    private Assets.Scripts.Utils.Grid m_grid;

	private bool mask_tips_click;

	private bool mask_click;

	private bool mask_isVictory;

	private int mask_isInGuide = -1;

	private List<int> l_guides = new List<int>();

	private static M00301 m_instance;

	public event Action DoRefreshHandle;

	public event Action DoReloadMapsHandle;

	public event Action<int> DoAddBlockHandle;

	public event Action<int> DoRemoveBlockHandle;

	public int CheckPoint
	{
		get
		{
			return this.m_checkPoint;
		}
		set
		{
			this.m_checkPoint = value;
		}
	}

	public int TopCheckPoint
	{
		get
		{
			return this.m_topCheckPoint;
		}
		set
		{
			this.m_topCheckPoint = value;
		}
	}

	public float Cell_width
	{
		get
		{
			return this.m_cell_width;
		}
		set
		{
			this.m_cell_width = value;
		}
	}

	public float Cell_height
	{
		get
		{
			return this.m_cell_height;
		}
		set
		{
			this.m_cell_height = value;
		}
	}

	internal TG00301 Map_config
	{
		get
		{
			return this.m_map_config;
		}
		set
		{
			this.m_map_config = value;
		}
	}

	public Color_Node[,] Maps
	{
		get
		{
			return this.m_maps;
		}
		set
		{
			this.m_maps = value;
		}
	}

	public List<int[]> Node_config
	{
		get
		{
			return this.m_node_config;
		}
		set
		{
			this.m_node_config = value;
		}
	}

	public List<int[]>[] Path
	{
		get
		{
			return this.m_path;
		}
		set
		{
			this.m_path = value;
		}
	}

	public Assets.Scripts.Utils.Grid Grid
	{
		get
		{
			return this.m_grid;
		}
		set
		{
			this.m_grid = value;
		}
	}

	public bool Mask_isVictory
	{
		get
		{
			return this.mask_isVictory;
		}
		set
		{
			this.mask_isVictory = value;
		}
	}

	public static M00301 GetInstance()
	{
		return M00301.m_instance;
	}

	private void Awake()
	{
		M00301.m_instance = this;
	}

	private void Start()
	{
	}

	public void StartNewGame(int idx = 30001)
	{
		this.Mask_isVictory = false;
		this.SaveGame(idx);
		this.RelodConfig(idx);
		this.InitMap();
		this.ReloadNodes();
		this.InitGrid();
		this.DoReloadMapsHandle();
		if (idx == 30001)
		{
			this.StartNoviceGuide(0);
			return;
		}
		this.StopNoviceGuide();
	}

	private void SaveGame(int idx)
	{
		this.CheckPoint = idx;
		GM.GetInstance().SetSavedGameID(3);
		GM.GetInstance().SaveScore(3, this.CheckPoint);
	}

	public void SaveScore()
	{
		this.TopCheckPoint = this.CheckPoint;
		GM.GetInstance().SaveScoreRecord(3, this.TopCheckPoint);
		GM.GetInstance().SaveG003ScoreRecord(this.Map_config.G003, this.TopCheckPoint);
		if (Configs.TG00301[this.Map_config.Next.ToString()].G003 != this.Map_config.G003)
		{
			GM.GetInstance().SaveG003ScoreRecord(Configs.TG00301[this.Map_config.Next.ToString()].G003, 0);
		}
	}

	private void RelodConfig(int idx)
	{
		this.Map_config = Configs.TG00301[idx.ToString()];
		this.m_map_row = this.Map_config.Row;
		this.m_map_col = this.Map_config.Col;
		this.m_cell_width = 565f / (float)this.m_map_col;
		this.m_cell_height = 565f / (float)this.m_map_row;
		this.Maps = new Color_Node[this.m_map_row, this.m_map_col];
	}

	public void InitMap()
	{
		int num = 0;
		for (int i = 0; i < this.m_map_row; i++)
		{
			for (int j = 0; j < this.m_map_col; j++)
			{
				this.Maps[i, j] = new Color_Node(num++, 0);
			}
		}
	}

	public void ReloadNodes()
	{
		this.Node_config.Clear();
		string[] arg_55_0 = this.Map_config.Maps.Replace("[[", "").Replace("]]", "").Replace("],[", "+").Split(new char[]
		{
			'+'
		});
		int num = 0;
		string[] array = arg_55_0;
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				','
			});
			int[] array3 = new int[]
			{
				Convert.ToInt32(array2[0]),
				Convert.ToInt32(array2[1])
			};
			this.Node_config.Add(array3);
			if (array3[1] == 0)
			{
				int row = this.GetRow(num);
				int col = this.GetCol(num);
				this.Maps[row, col].Type = M00301.Node_type.TARGET;
				this.Maps[row, col].Color = array3[0];
			}
			num++;
		}
	}

	public void InitGrid()
	{
		this.Grid = new Assets.Scripts.Utils.Grid(this.m_map_col, this.m_map_row);
		for (int i = 0; i < this.m_map_row; i++)
		{
			for (int j = 0; j < this.m_map_col; j++)
			{
				this.Grid.setWalk(j, i, this.Maps[j, i].Type != M00301.Node_type.TARGET);
			}
		}
	}

	public void ResetNode(Color_Node node)
	{
		node.Next = node.Index;
		node.Per = node.Index;
		node.Color = 0;
		node.Type = M00301.Node_type.NONE;
	}

	public void CleanRestColor(Color_Node node)
	{
		if (node.Index != node.Next)
		{
			int next = node.Next;
			node.Next = node.Index;
			node = this.GetNode(next);
			while (node.Index != node.Next || node.Index != node.Per)
			{
				next = node.Next;
				node.Per = node.Index;
				node.Next = node.Index;
				if (node.Type == M00301.Node_type.SEGMENT)
				{
					node.Type = M00301.Node_type.NONE;
					node.Color = 0;
				}
				this.DoRemoveBlockHandle(node.Index);
				node = this.GetNode(next);
			}
		}
	}

	public void CleanAllColor()
	{
		for (int i = 0; i < this.m_map_row; i++)
		{
			for (int j = 0; j < this.m_map_col; j++)
			{
				if (this.Maps[i, j].Type != M00301.Node_type.TARGET)
				{
					this.SetNode(i, j, 0, M00301.Node_type.NONE);
				}
				this.Maps[i, j].Per = this.Maps[i, j].Index;
				this.Maps[i, j].Next = this.Maps[i, j].Index;
				this.DoRemoveBlockHandle(this.GetIndex(i, j));
			}
		}
	}

	public void CleanAllColor(int color)
	{
		for (int i = 0; i < this.m_map_row; i++)
		{
			for (int j = 0; j < this.m_map_col; j++)
			{
				if (this.Maps[i, j].Color == color)
				{
					if (this.Maps[i, j].Type != M00301.Node_type.TARGET)
					{
						this.SetNode(i, j, 0, M00301.Node_type.NONE);
					}
					this.Maps[i, j].Per = this.Maps[i, j].Index;
					this.Maps[i, j].Next = this.Maps[i, j].Index;
					this.DoRemoveBlockHandle(this.GetIndex(i, j));
				}
			}
		}
	}

	public Vector2 GetMousePosRowCol(Vector2 pos)
	{
		Vector2 b = new Vector2(-282.5f, 282.5f);
		Vector2 expr_18 = pos - b;
		int num = (int)(Mathf.Abs(expr_18.y) / this.Cell_height);
		int num2 = (int)(Mathf.Abs(expr_18.x) / this.Cell_width);
		return new Vector2((float)num, (float)num2);
	}

	public int CheckClickPos(Vector2 pos)
	{
		if (pos.x <= -282.5f || pos.x >= 282.5f || pos.y >= 282.5f || pos.y <= -282.5f)
		{
			return -1;
		}
		Vector2 expr_3D = this.GetMousePosRowCol(pos);
		int num = (int)expr_3D.x;
		int num2 = (int)expr_3D.y;
		Color_Node color_Node = this.Maps[num, num2];
		if (this.mask_isInGuide != -1 && !this.HandelGuideClick(this.GetIndex(num, num2)))
		{
			return -1;
		}
		if (color_Node != null)
		{
			switch (color_Node.Type)
			{
			case M00301.Node_type.NONE:
				return -1;
			case M00301.Node_type.TARGET:
				this.CleanAllColor(color_Node.Color);
				break;
			case M00301.Node_type.SEGMENT:
				this.CleanRestColor(color_Node);
				break;
			default:
				return -1;
			}
			return color_Node.Index;
		}
		return -1;
	}

	public int CheckDropPos(int click, Vector2 pos)
	{
		if (pos.x <= -282.5f || pos.x >= 282.5f || pos.y >= 282.5f || pos.y <= -282.5f)
		{
			return -1;
		}
		bool flag = false;
		Vector2 mousePosRowCol = this.GetMousePosRowCol(pos);
		int index = this.GetIndex((int)mousePosRowCol.x, (int)mousePosRowCol.y);
		Color_Node headQueue = this.GetHeadQueue(this.GetNode(click));
		Color_Node color_Node = this.GetEndQueue(this.GetNode(click));
		if (color_Node.Index == index)
		{
			return -1;
		}
		if (headQueue.Index != color_Node.Index && headQueue.Color == color_Node.Color && color_Node.Type == M00301.Node_type.TARGET)
		{
			return -1;
		}
		if (this.mask_isInGuide != -1 && !this.HandelGuideClick(index))
		{
			return -1;
		}
		List<int> pathNode = this.GetPathNode(color_Node.Index, index);
		if (pathNode.Count > 0 && this.GetNode(index).Type == M00301.Node_type.TARGET && color_Node.Color != this.GetNode(index).Color)
		{
			pathNode.RemoveAt(pathNode.Count - 1);
		}
		if (pathNode.Count > 0)
		{
			pathNode.RemoveAt(0);
		}
		Color_Node color_Node2 = null;
		foreach (int current in pathNode)
		{
			if (flag)
			{
				click = color_Node2.Index;
			}
			headQueue = this.GetHeadQueue(this.GetNode(click));
			color_Node = this.GetEndQueue(this.GetNode(click));
			color_Node2 = this.GetNode(current);
			if (color_Node2 != null)
			{
				switch (color_Node2.Type)
				{
				case M00301.Node_type.NONE:
					color_Node.Next = color_Node2.Index;
					color_Node2.Per = color_Node.Index;
					color_Node2.Color = color_Node.Color;
					color_Node2.Type = M00301.Node_type.SEGMENT;
					this.DoAddBlockHandle(color_Node2.Index);
					break;
				case M00301.Node_type.TARGET:
					if (color_Node.Color == color_Node2.Color && headQueue.Index != color_Node2.Index)
					{
						color_Node.Next = color_Node2.Index;
						color_Node2.Per = color_Node.Index;
						this.DoAddBlockHandle(color_Node2.Index);
						if (this.mask_isInGuide != -1)
						{
							this.StartNoviceGuide(this.mask_isInGuide + 1);
						}
						this.CheckIsVictory();
					}
					break;
				case M00301.Node_type.SEGMENT:
					if (color_Node2.Color == color_Node.Color)
					{
						flag = true;
						while (color_Node2.Index != color_Node.Index)
						{
							int per = color_Node.Per;
							this.ResetNode(color_Node);
							this.DoRemoveBlockHandle(color_Node.Index);
							color_Node = this.GetNode(per);
						}
						color_Node2.Next = color_Node2.Index;
					}
					else
					{
						Color_Node expr_24E = this.GetNode(color_Node2.Per);
						expr_24E.Next = expr_24E.Index;
						this.CleanRestColor(color_Node2);
						color_Node.Next = color_Node2.Index;
						color_Node2.Per = color_Node.Index;
						color_Node2.Color = color_Node.Color;
					}
					break;
				}
			}
		}
		this.DoRefreshHandle();
		this.mask_click = false;
		if (flag)
		{
			return color_Node2.Index;
		}
		return -1;
	}

	public void CheckUpPos(Vector2 pos)
	{
		if (pos.x <= -282.5f || pos.x >= 282.5f || pos.y >= 282.5f || pos.y <= -282.5f)
		{
			return;
		}
		Vector2 expr_3C = this.GetMousePosRowCol(pos);
		int num = (int)expr_3C.x;
		int num2 = (int)expr_3C.y;
		Color_Node color_Node = this.Maps[num, num2];
		Color_Node headQueue = this.GetHeadQueue(color_Node);
		if (color_Node.Type == M00301.Node_type.SEGMENT)
		{
			foreach (int current in this.GetAroundNode(color_Node.Index))
			{
				Color_Node node = this.GetNode(current);
				if (headQueue.Index != node.Index && node.Type == M00301.Node_type.TARGET && node.Color == color_Node.Color)
				{
					color_Node.Next = node.Index;
					node.Per = color_Node.Index;
					this.DoAddBlockHandle(node.Index);
					if (this.mask_isInGuide != -1)
					{
						this.StartNoviceGuide(this.mask_isInGuide + 1);
					}
					this.DoRefreshHandle();
					this.CheckIsVictory();
					break;
				}
			}
		}
	}

	public bool CheckIsVictory()
	{
		for (int i = 0; i < this.m_map_row; i++)
		{
			for (int j = 0; j < this.m_map_col; j++)
			{
				Color_Node node = this.GetNode(i, j);
				if (node.Type == M00301.Node_type.NONE)
				{
					return false;
				}
				if (node.Type == M00301.Node_type.TARGET && !this.CheckIsConnected(node))
				{
					return false;
				}
			}
		}
		UnityEngine.Debug.Log("Congratulations on your victory");
		base.GetComponent<G00301>().ShowVictory();
		return true;
	}

	public bool CheckIsConnected(Color_Node node)
	{
		Color_Node color_Node;
		if (node.Next != node.Index)
		{
			color_Node = this.GetEndQueue(node);
		}
		else
		{
			if (node.Per == node.Index)
			{
				return false;
			}
			color_Node = this.GetHeadQueue(node);
		}
		return color_Node.Type == M00301.Node_type.TARGET && color_Node.Color == node.Color;
	}

	public void InitTipsPath()
	{
		this.Path = new List<int[]>[this.m_map_config.Target];
		for (int i = 0; i < this.m_map_config.Target; i++)
		{
			this.Path[i] = new List<int[]>();
		}
		int num = 0;
		foreach (int[] current in this.Node_config)
		{
			this.Path[current[0] - 1].Add(new int[]
			{
				num++,
				current[0],
				current[1]
			});
		}
		List<int[]>[] path = this.Path;
		for (int j = 0; j < path.Length; j++)
		{
			List<int[]> arg_C6_0 = path[j];
			Comparison<int[]> arg_C6_1;
			if ((arg_C6_1 = M00301.__c.__9__84_0) == null)
			{
				arg_C6_1 = (M00301.__c.__9__84_0 = new Comparison<int[]>(M00301.__c.__9._InitTipsPath_b__84_0));
			}
			arg_C6_0.Sort(arg_C6_1);
		}
		path = this.Path;
		for (int j = 0; j < path.Length; j++)
		{
			List<int[]> list = path[j];
			foreach (int current2 in this.GetAroundNode(list[2][0]))
			{
				if (current2 == list[0][0])
				{
					int[] item = list[1];
					list.RemoveAt(1);
					list.Add(item);
					break;
				}
				if (current2 == list[1][0])
				{
					int[] item2 = list[0];
					list.RemoveAt(0);
					list.Add(item2);
					break;
				}
			}
		}
	}

	public int GetNeedTipsColor()
	{
		List<int[]>[] path = this.Path;
		for (int i = 0; i < path.Length; i++)
		{
			List<int[]> list = path[i];
			for (int j = 0; j < this.m_map_row; j++)
			{
				for (int k = 0; k < this.m_map_col; k++)
				{
					Color_Node node = this.GetNode(j, k);
					int num = 0;
					foreach (int[] current in list)
					{
						if (node.Index == current[0])
						{
							if (node.Color != current[1])
							{
								int result = current[1];
								return result;
							}
						}
						else
						{
							num++;
						}
					}
					if (num >= list.Count && node.Color == list[0][1])
					{
						return node.Color;
					}
				}
			}
		}
		return 0;
	}

	public void GetPrompted()
	{
		if (!this.mask_tips_click)
		{
			this.InitTipsPath();
			this.mask_tips_click = true;
		}
		if (!GM.GetInstance().isFullGEM(20))
		{
			//GlobalEventHandle.EmitClickPageButtonHandle("shop", 0);
			return;
		}
		GM.GetInstance().ConsumeGEM(20);
		int needTipsColor = this.GetNeedTipsColor();
		if (needTipsColor == 0)
		{
			return;
		}
		this.CleanAllColor(needTipsColor);
		bool flag = true;
		Color_Node color_Node = null;
		foreach (int[] current in this.Path[needTipsColor - 1])
		{
			if (flag)
			{
				color_Node = this.GetNode(current[0]);
				flag = false;
			}
			else
			{
				Color_Node node = this.GetNode(current[0]);
				switch (node.Type)
				{
				case M00301.Node_type.NONE:
					color_Node.Next = node.Index;
					node.Per = color_Node.Index;
					node.Color = color_Node.Color;
					node.Type = M00301.Node_type.SEGMENT;
					this.DoAddBlockHandle(node.Index);
					break;
				case M00301.Node_type.TARGET:
					color_Node.Next = node.Index;
					node.Per = color_Node.Index;
					this.DoAddBlockHandle(node.Index);
					if (this.mask_isInGuide != -1)
					{
						this.StartNoviceGuide(this.mask_isInGuide + 1);
					}
					this.CheckIsVictory();
					break;
				case M00301.Node_type.SEGMENT:
				{
					Color_Node expr_FF = this.GetNode(node.Per);
					expr_FF.Next = expr_FF.Index;
					this.CleanRestColor(node);
					color_Node.Next = node.Index;
					node.Per = color_Node.Index;
					node.Color = color_Node.Color;
					break;
				}
				}
				color_Node = node;
			}
		}
		this.DoRefreshHandle();
	}

	public Color_Node GetEndQueue(Color_Node head)
	{
		Color_Node color_Node = head;
		while (color_Node.Index != color_Node.Next)
		{
			color_Node = this.GetNode(color_Node.Next);
		}
		return color_Node;
	}

	public Color_Node GetHeadQueue(Color_Node node)
	{
		Color_Node color_Node = node;
		while (color_Node.Index != color_Node.Per)
		{
			color_Node = this.GetNode(color_Node.Per);
		}
		return color_Node;
	}

	public int GetRow(int index)
	{
		return index / this.m_map_col;
	}

	public int GetCol(int index)
	{
		return index % this.m_map_col;
	}

	public int GetIndex(int row, int col)
	{
		return row * this.m_map_col + col;
	}

	public Color_Node GetNode(int index)
	{
		return this.GetNode(this.GetRow(index), this.GetCol(index));
	}

	public Color_Node GetNode(int row, int col)
	{
		if (row > this.m_map_row || row < 0 || col > this.m_map_col || col < 0)
		{
			return null;
		}
		return this.Maps[row, col];
	}

	public void SetNode(int index, int cr, M00301.Node_type type)
	{
		this.SetNode(this.GetRow(index), this.GetCol(index), cr, type);
	}

	public void SetNode(int row, int col, int cr, M00301.Node_type ty)
	{
		this.Maps[row, col].Color = cr;
		this.Maps[row, col].Type = ty;
	}

	public List<int> GetAroundNode(int idx)
	{
		List<int> list = new List<int>();
		if (idx >= this.m_map_col)
		{
			list.Add(idx - this.m_map_col);
		}
		if (idx % this.m_map_col != this.m_map_col - 1)
		{
			list.Add(idx + 1);
		}
		if (idx < this.m_map_col * this.m_map_row - this.m_map_col)
		{
			list.Add(idx + this.m_map_col);
		}
		if (idx % this.m_map_col != 0)
		{
			list.Add(idx - 1);
		}
		return list;
	}

	public List<int> GetPathNode(int per, int end)
	{
		this.Grid.setWalk(this.GetRow(per), this.GetCol(per), true);
		this.Grid.setWalk(this.GetRow(end), this.GetCol(end), true);
		List<Node> arg_C8_0 = new AStar(this.Grid, DiagonalMovement.NEVER, HeuristicType.MANHATTAN).Find(new AVec2(this.GetRow(per), this.GetCol(per)), new AVec2(this.GetRow(end), this.GetCol(end)));
		this.Grid.setWalk(this.GetRow(per), this.GetCol(per), this.GetNode(per).Type != M00301.Node_type.TARGET);
		this.Grid.setWalk(this.GetRow(end), this.GetCol(end), this.GetNode(end).Type != M00301.Node_type.TARGET);
		List<int> list = new List<int>();
		foreach (Node current in arg_C8_0)
		{
			list.Add(this.GetIndex(current.x, current.y));
		}
		return list;
	}

	public void PrintMaps()
	{
		for (int i = 0; i < this.m_map_row; i++)
		{
			string text = "";
			for (int j = 0; j < this.m_map_col; j++)
			{
				Color_Node color_Node = this.Maps[i, j];
				text = string.Concat(new object[]
				{
					text,
					"[i:",
					color_Node.Index,
					",c:",
					color_Node.Color,
					"t:",
					color_Node.Type,
					",n:",
					color_Node.Next,
					",p:",
					color_Node.Per,
					"]  "
				});
			}
			MonoBehaviour.print(text);
		}
		UnityEngine.Debug.Log("------------------------");
	}

	public void DestroyMap()
	{
		this.mask_tips_click = false;
		base.GetComponent<G00301>().DestroyMap();
	}

	private bool HandelGuideClick(int idx)
	{
		bool result = false;
		using (List<int>.Enumerator enumerator = this.l_guides.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == idx)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	public void StartNoviceGuide(int idx = 0)
	{
		UnityEngine.Debug.Log("Begin Novice guide");
		this.mask_isInGuide = idx;
		int[] collection = new int[0];
		this.l_guides.Clear();
		switch (idx)
		{
		case 0:
			collection = new int[]
			{
				15,
				19,
				20,
				21,
				22,
				23,
				24
			};
			break;
		case 1:
			collection = new int[]
			{
				6,
				7,
				8,
				11,
				16
			};
			break;
		case 2:
			collection = new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				9,
				10,
				13,
				14
			};
			break;
		case 3:
			collection = new int[]
			{
				12,
				17,
				18
			};
			break;
		default:
			idx = -1;
			this.StopNoviceGuide();
			break;
		}
		this.l_guides.AddRange(collection);
		base.GetComponent<G00301>().PlayHandAnimation(idx);
	}

	public void StopNoviceGuide()
	{
		this.mask_isInGuide = -1;
		this.l_guides.Clear();
		base.GetComponent<G00301>().StopHandAnimation();
	}
}
