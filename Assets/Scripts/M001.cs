using Assets.Scripts.Configs;
using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class M001 : MonoBehaviour
{
	private struct SortMap
	{
		public int idx;

		public int number;

		public SortMap(int idx, int number)
		{
			this.idx = idx;
			this.number = number;
		}
	}

	[Serializable]
	private sealed class __c
	{
		public static readonly M001.__c __9 = new M001.__c();

		public static Comparison<M001.SortMap> __9__97_0;

		internal int _UseHeart_b__97_0(M001.SortMap a, M001.SortMap b)
		{
			return a.number.CompareTo(b.number);
		}
	}

	public const int CONSTANT_MAP_ROW = 5;

	public const int CONSTANT_MAP_COL = 5;

	public const int CONSTANT_CELL_WIDTH = 120;

	public const int CONSTANT_CELL_HEIGHT = 120;

	private static M001 m_instance;

	private System.Random rd = new System.Random();

	private bool isPlaying;

	private int m_finishCount;

	private int m_score;

	private int m_maxScore;







	public Action<G00101> DoClickBlock;

	public Action DoFillLife;



	public Action OnRandomHeartHandle;

	public Action<int> OnUseHeartHandle;

	public Action<GameList> OnClickReturnHandle;

	private Queue m_blockPool = new Queue();

	private int[,] map = new int[5, 5];

	private List<int> bloodList = new List<int>();

	private int m_curPropId;

	private int m_heartIndex = -1;

	public event Action DoRefreshHandle;

	public event Action<List<int>, int> DoDeleteHandle;

	public event Action<List<sDropData>, List<int>> DoDropHandle;

	public event Action<int> DoCompMaxNumber;

	public int[,] Map
	{
		get
		{
			return this.map;
		}
		set
		{
			this.map = value;
		}
	}

	public bool IsPlaying
	{
		get
		{
			return this.isPlaying;
		}
		set
		{
			this.isPlaying = value;
		}
	}

	public List<int> BloodList
	{
		get
		{
			return this.bloodList;
		}
		set
		{
			this.bloodList = value;
		}
	}

	public int CurPropId
	{
		get
		{
			return this.m_curPropId;
		}
		set
		{
			this.m_curPropId = value;
		}
	}

	public int Score
	{
		get
		{
			return this.m_score;
		}
		set
		{
			this.m_score = value;
		}
	}

	public int MaxScore
	{
		get
		{
			return this.m_maxScore;
		}
		set
		{
			this.m_maxScore = value;
		}
	}

	public int HeartIndex
	{
		get
		{
			return this.m_heartIndex;
		}
		set
		{
			this.m_heartIndex = value;
		}
	}

	public int FinishCount
	{
		get
		{
			return this.m_finishCount;
		}
		set
		{
			this.m_finishCount = value;
		}
	}

	private void Awake()
	{
		M001.m_instance = this;
	}

	private void Start()
	{
		this.Score = GM.GetInstance().GetScore(1);
		this.MaxScore = GM.GetInstance().GetScoreRecord(1);
		this.LoadMapData();
		this.LoadBlood();
		this.SaveGame();
		GM.GetInstance().SetSavedGameID(1);
	}

	private void Update()
	{
	}

	public static M001 GetInstance()
	{
		return M001.m_instance;
	}

	public void StartNewGame()
	{
		GM.GetInstance().SetSavedGameID(1);
		GM.GetInstance().ResetToNewGame();
		this.m_heartIndex = -1;
		this.Score = 0;
		this.MaxScore = GM.GetInstance().GetScoreRecord(1);
		this.InitMap();
		this.InitNewGame();
		this.FillLife(true);
		this.SaveGame();
		this.DoRefreshHandle();
	}

	public G00101 CreateBlock(int number, int idx, GameObject parent)
	{
		GameObject gameObject;
		if (this.m_blockPool.Count > 0)
		{
			gameObject = (this.m_blockPool.Dequeue() as GameObject);
		}
		else
		{
			gameObject = this.CreateBlock();
		}
		gameObject.SetActive(true);
		gameObject.GetComponent<G00101>().Init(number, idx);
		gameObject.transform.SetParent(parent.transform, false);
		return gameObject.GetComponent<G00101>();
	}

	public void FreeBlock(GameObject obj)
	{
		obj.SetActive(false);
		obj.transform.DOKill(false);
		obj.GetComponent<G00101>().RemoveClick();
		obj.GetComponent<G00101>().StopFade();
		if (!this.m_blockPool.Contains(obj))
		{
			this.m_blockPool.Enqueue(obj);
		}
	}

	public int GetRow(int index)
	{
		return index / 5;
	}

	public int GetCol(int index)
	{
		return index % 5;
	}

	public int GetNumber(int row, int col)
	{
		return this.Map[row, col];
	}

	public int GetNumber(int index)
	{
		return this.GetNumber(this.GetRow(index), this.GetCol(index));
	}

	public void setNumber(int row, int col, int value)
	{
		this.Map[row, col] = value;
	}

	public void setNumber(int index, int value)
	{
		this.setNumber(this.GetRow(index), this.GetCol(index), value);
	}

	public void AddNumber(int index, int value = 1)
	{
		int row = this.GetRow(index);
		int col = this.GetCol(index);
		this.AddNumber(row, col, value);
	}

	public void AddNumber(int row, int col, int value = 1)
	{
		int num = this.GetNumber(row, col) + value;
		num = ((num < 0) ? 0 : num);
		this.setNumber(row, col, num);
	}

	public int GetIndex(int row, int col)
	{
		return row * 5 + col;
	}

	public int GetLeftIdx(int row, int col)
	{
		col--;
		if (col >= 0)
		{
			return this.GetIndex(row, col);
		}
		return -1;
	}

	public int GetRightIdx(int row, int col)
	{
		col++;
		if (col < 5)
		{
			return this.GetIndex(row, col);
		}
		return -1;
	}

	public int GetUpIdx(int row, int col)
	{
		row++;
		if (row < 5)
		{
			return this.GetIndex(row, col);
		}
		return -1;
	}

	public int GetDownIdx(int row, int col)
	{
		row--;
		if (row >= 0)
		{
			return this.GetIndex(row, col);
		}
		return -1;
	}

	public List<int> Use(int idx)
	{
		
		GM.GetInstance().ConsumeGEM(Constant.COMMON_CONFIG_PROP[this.CurPropId - 1]);
		
		List<int> list = new List<int>();
		switch (this.CurPropId)
		{
		case 1:
			this.setNumber(idx, 0);
			list.Add(idx);
			AudioManager.GetInstance().PlayEffect("sound_eff_item_bomb");
			break;
		case 2:
		{
			int row = this.GetRow(idx);
			int col = this.GetCol(idx);
			for (int i = 0; i < 5; i++)
			{
				this.setNumber(row, i, 0);
				int index = this.GetIndex(row, i);
				if (!list.Contains(index))
				{
					list.Add(index);
				}
			}
			for (int j = 0; j < 5; j++)
			{
				this.setNumber(j, col, 0);
				int index2 = this.GetIndex(j, col);
				if (!list.Contains(index2))
				{
					list.Add(index2);
				}
			}
			AudioManager.GetInstance().PlayEffect("sound_eff_item_hammer");
			break;
		}
		case 3:
		{
			int number = this.GetNumber(idx);
			for (int k = 0; k < 5; k++)
			{
				for (int l = 0; l < 5; l++)
				{
					if (this.GetNumber(l, k) == number)
					{
						this.setNumber(l, k, 0);
						list.Add(this.GetIndex(l, k));
					}
				}
			}
			AudioManager.GetInstance().PlayEffect("sound_eff_item_star");
			break;
		}
		}
		AchiveData.GetInstance().Add(4, 1, true);
		this.SaveGame();
		return list;
	}

	public void Delete(int index)
	{
		if (this.GetNumber(index) <= 0)
		{
			this.Down();
			return;
		}
		List<int> deletList = this.GetDeletList(index);
		this.Delete(deletList, index);
	}

	public void Down()
	{
		List<sDropData> list = new List<sDropData>();
		List<int> list2 = new List<int>();
		for (int i = 0; i < 5; i++)
		{
			int num = 0;
			Queue queue = new Queue();
			for (int j = 0; j < 5; j++)
			{
				if (this.GetNumber(j, i) != 0)
				{
					num++;
					queue.Enqueue(this.GetIndex(j, i));
				}
			}
			for (int k = 0; k < num; k++)
			{
				int num2 = (int)queue.Dequeue();
				int row = this.GetRow(num2);
				int col = this.GetCol(num2);
				int number = this.GetNumber(row, col);
				if (row != k)
				{
					this.setNumber(row, col, 0);
					this.setNumber(k, i, number);
					list.Add(new sDropData(num2, this.GetIndex(k, col)));
				}
			}
			num = 0;
			queue.Clear();
			for (int l = 0; l < 5; l++)
			{
				if (this.GetNumber(l, i) == 0)
				{
					num++;
					queue.Enqueue(this.GetIndex(l, i));
				}
			}
			for (int m = 0; m < num; m++)
			{
				int num3 = (int)queue.Dequeue();
				this.GetRow(num3);
				this.GetCol(num3);
				this.setNumber(num3, this.rd.Next(1, 6));
				list2.Add(num3);
			}
		}
		this.SaveGame();
		this.DoDropHandle(list, list2);
	}

	public void AutoDelete()
	{
		bool flag = false;
		for (int i = 4; i >= 0; i--)
		{
			for (int j = 4; j >= 0; j--)
			{
				int index = this.GetIndex(i, j);
				List<int> deletList = this.GetDeletList(index);
				if (deletList.Count > 2)
				{
					this.Delete(deletList, index);
					return;
				}
			}
		}
		if (flag)
		{
			return;
		}
		this.UseHeart();
	}

	public int AddLife()
	{
		int result = -1;
		for (int i = 0; i < 5; i++)
		{
			if (this.BloodList[i] == 0)
			{
				this.BloodList[i] = this.GetNewBloodNumber();
				result = i;
				break;
			}
		}
		return result;
	}

	public int GetBlood()
	{
		if (this.IsGameOver())
		{
			return 0;
		}
		int result = this.BloodList[0];
		for (int i = 0; i < 5; i++)
		{
			if (i + 1 < 5)
			{
				this.BloodList[i] = this.BloodList[i + 1];
			}
			else
			{
				this.BloodList[i] = 0;
			}
		}
		return result;
	}

	public bool IsGameOver()
	{
		bool result = true;
		using (List<int>.Enumerator enumerator = this.BloodList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current != 0)
				{
					result = false;
					break;
				}
			}
		}
		return result;
	}

	public int GetMapMaxNumber()
	{
		int num = 1;
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int number = this.GetNumber(i, j);
				num = ((num < number) ? number : num);
			}
		}
		return num;
	}

	public void LoadBlood()
	{
		if (GM.GetInstance().isSavedGame())
		{
			this.InitBlood();
			this.LoadLocalLife();
			return;
		}
		this.FillLife(true);
	}

	public void InitBlood()
	{
		this.bloodList.Clear();
		for (int i = 0; i < 5; i++)
		{
			this.bloodList.Add(0);
		}
	}

	public void LoadLocalLife()
	{
		this.InitBlood();
		string[] array = GM.GetInstance().GetSavedLife().Split(new char[]
		{
			','
		});
		int num = 0;
		while (num < array.Length && num < this.bloodList.Count)
		{
			this.bloodList[num] = int.Parse(array[num]);
			num++;
		}
	}

	public void FillLife(bool isNewGame = false)
	{
		this.InitBlood();
		if (isNewGame)
		{
			for (int i = 0; i < 5; i++)
			{
				this.bloodList[i] = 1;
			}
		}
		else
		{
			for (int j = 0; j < 5; j++)
			{
				this.bloodList[j] = this.GetNewBloodNumber();
			}
		}
		this.SaveGame();
	}

	public void LoadMapData()
	{
		this.InitMap();
		if (GM.GetInstance().isSavedGame())
		{
			this.LoadLocalData();
			return;
		}
		this.InitNewGame();
	}

	private void InitMap()
	{
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				this.setNumber(i, j, 0);
			}
		}
	}

	private void InitNewGame()
	{
		if (PlayerPrefs.GetInt("LocalData_guide_game0102", 0) != 0)
		{
			this.rd = new System.Random();
			for (int i = 0; i < 5; i++)
			{
				string str = "";
				for (int j = 0; j < 5; j++)
				{
					this.RandomNewGameNumber(i, j);
					str = str + this.Map[i, j].ToString() + ", ";
				}
			}
			return;
		}
		this.Map = new int[,]
		{
			{
				4,
				1,
				1,
				5,
				5
			},
			{
				1,
				3,
				2,
				4,
				2
			},
			{
				3,
				2,
				1,
				2,
				1
			},
			{
				3,
				2,
				1,
				2,
				3
			},
			{
				5,
				4,
				5,
				3,
				2
			}
		};
	}

	private void RandomNewGameNumber(int row, int col)
	{
		this.setNumber(row, col, this.rd.Next(1, 6));
		if (this.GetDeletList(this.GetIndex(row, col)).Count > 2)
		{
			this.RandomNewGameNumber(row, col);
		}
	}

	private void LoadLocalData()
	{
		string[] array = GM.GetInstance().GetSavedGameMap().Split(new char[]
		{
			','
		});
		for (int i = 0; i < this.map.Length; i++)
		{
			this.setNumber(i, int.Parse(array[i]));
		}
	}

	private List<int> GetDeletList(int idx)
	{
		List<int> list = new List<int>();
		this.FillDeleteList(list, idx);
		return list;
	}

	private void FillDeleteList(List<int> list, int idx)
	{
		if (list.Contains(idx))
		{
			return;
		}
		int row = this.GetRow(idx);
		int col = this.GetCol(idx);
		list.Add(idx);
		int[] array = new int[]
		{
			this.GetLeftIdx(row, col),
			this.GetRightIdx(row, col),
			this.GetDownIdx(row, col),
			this.GetUpIdx(row, col)
		};
		for (int i = 0; i < array.Length; i++)
		{
			int num = array[i];
			if (num != -1)
			{
				int row2 = this.GetRow(num);
				int col2 = this.GetCol(num);
				if (this.Map[row, col] == this.Map[row2, col2])
				{
					this.FillDeleteList(list, num);
				}
			}
		}
	}

	private void OnRandomHeart(int idx)
	{
		if (this.HeartIndex > 0)
		{
			return;
		}
		if (this.rd.Next(1, 100) < 97)
		{
			return;
		}
		this.HeartIndex = idx;
		Action expr_2A = this.OnRandomHeartHandle;
		if (expr_2A == null)
		{
			return;
		}
		expr_2A();
	}

	public void UseHeart()
	{
		if (this.m_heartIndex == -1)
		{
			return;
		}
		List<M001.SortMap> list = new List<M001.SortMap>();
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				list.Add(new M001.SortMap(this.GetIndex(i, j), this.GetNumber(i, j)));
			}
		}
		List<M001.SortMap> arg_63_0 = list;
		Comparison<M001.SortMap> arg_63_1;
		if ((arg_63_1 = M001.__c.__9__97_0) == null)
		{
			arg_63_1 = (M001.__c.__9__97_0 = new Comparison<M001.SortMap>(M001.__c.__9._UseHeart_b__97_0));
		}
		arg_63_0.Sort(arg_63_1);
		int idx = list[0].idx;
		this.AddNumber(idx, 1);
		Action<int> expr_83 = this.OnUseHeartHandle;
		if (expr_83 == null)
		{
			return;
		}
		expr_83(idx);
	}

	private GameObject CreateBlock()
	{
		return UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/G00101") as GameObject);
	}

	private void Delete(List<int> list, int index)
	{
		if (list.Count < 3)
		{
			this.SaveGame();
			return;
		}
		this.IsPlaying = true;
		int row = this.GetRow(index);
		int col = this.GetCol(index);
		this.AddNumber(row, col, 1);
		foreach (int current in list)
		{
			int number = this.GetNumber(current);
			if (current != index)
			{
				this.AddScore(number);
				GM.GetInstance().AddExp(number);
				AchiveData.GetInstance().Add(2, 1, true);
				TaskData.GetInstance().Add(100101, 1, true);
				this.setNumber(this.GetRow(current), this.GetCol(current), 0);
			}
			else
			{
				if (TaskData.GetInstance().Get(100103).value < number)
				{
					TaskData.GetInstance().Add(100103, number, false);
				}
				if (AchiveData.GetInstance().Get(3).value < number)
				{
					AchiveData.GetInstance().Add(3, number, false);
					//AppsflyerUtils.TrackComp(1, number);
					if (number > 5)
					{
						Action<int> expr_FA = this.DoCompMaxNumber;
						if (expr_FA != null)
						{
							expr_FA(number);
						}
					}
				}
			}
		}
		this.OnRandomHeart(this.GetIndex(row, col));
		this.SaveGame();
		this.DoDeleteHandle(list, index);
	}

	private int GetNewBloodNumber()
	{
		int num = this.RandomBlood();
		if (num == 0)
		{
			num = this.GetNewBloodNumber();
		}
		return num;
	}

	private int RandomBlood()
	{
		int num = 0;
		List<int> list = new List<int>();
		List<TLife> list2 = new List<TLife>();
		foreach (KeyValuePair<string, TLife> current in Configs.TLifes)
		{
			TLife value = current.Value;
			num += value.Percent;
			list.Add(num);
			list2.Add(value);
		}
		int num2 = this.rd.Next(1, num + 1);
		for (int i = 0; i < list.Count; i++)
		{
			if (num2 <= list[i])
			{
				num2 = list2[i].Number;
				break;
			}
		}
		return num2;
	}

	private void AddScore(int number)
	{
		number *= 10;
		this.Score += number;
		if (this.Score <= this.MaxScore)
		{
			return;
		}
		this.MaxScore = this.Score;
		AchiveData.GetInstance().Add(5, this.MaxScore, false);
		GM.GetInstance().SaveScoreRecord(1, this.MaxScore);
	}

	private void SaveGame()
	{
		int num = 25;
		string text = "";
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				if (this.GetIndex(i, j) < num - 1)
				{
					text = text + this.GetNumber(i, j) + ",";
				}
				else
				{
					text += this.GetNumber(i, j);
				}
			}
		}
		string text2 = "";
		for (int k = 0; k < this.bloodList.Count; k++)
		{
			if (k < this.bloodList.Count - 1)
			{
				text2 = text2 + this.bloodList[k] + ",";
			}
			else
			{
				text2 += this.bloodList[k];
			}
		}
		GM.GetInstance().SaveGame(text, text2, -9999999f, -9999999f);
		GM.GetInstance().SaveScore(1, this.Score);
	}

	private void PrintMap()
	{
		for (int i = 0; i < 5; i++)
		{
			string text = "";
			for (int j = 0; j < 5; j++)
			{
				text = text + this.GetNumber(i, j) + ", ";
			}
			UnityEngine.Debug.Log(text);
		}
		UnityEngine.Debug.Log("------------------------");
	}
}
