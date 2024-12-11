using Assets.Scripts.Configs;
using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class M002 : MonoBehaviour, IPausable
{
	public const int CONSTANT_MAP_ROW = 7;

	public const int CONSTANT_MAP_COL = 5;

	public const float CONSTANT_CELL_WIDTH = 110f;

	public const float CONSTANT_CELL_HEIGHT = 110f;

	private int m_score;

	private int m_maxScore;

	private static M002 m_instance;

	private System.Random rd = new System.Random();

	private bool isPlaying;

	private bool m_isStart;

    [SerializeField]
	private bool m_isPuase;

	private bool m_isGameOver;

	private bool m_isFinishGuide;

	private int m_coinIndex = -1;

	private int m_finishCount;







	public Action<G00201> DoClickBlock;





	public Action DoVedioRefresh;

	public Action<GameList> OnClickReturnHandle;

	public Action OnRandomCoinHandle;

	private Queue m_blockPool = new Queue();

	private int[,] map = new int[7, 5];

	private List<int> bloodList = new List<int>();

	private int m_curPropId;

	private List<int> m_vedioLife = new List<int>
	{
		2,
		8,
		32,
		64
	};

	public event Action DoRefreshHandle;

	public event Action<List<int>, int> DoDeleteHandle;

	public event Action<List<sDropData>, List<int>> DoDropHandle;

	public event Action DoNullDelete;

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

    public void Pause()
    {
        IsPause = true;
    }
    public void Resume()
    {
        m_isPuase = false;
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

	public bool IsStart
	{
		get
		{
			return this.m_isStart;
		}
		set
		{
			this.m_isStart = value;
		}
	}

	public bool IsPause
	{
		get
		{
			return this.m_isPuase;
		}
		set
		{
			this.m_isPuase = value;
		}
	}

	public bool IsGameOver
	{
		get
		{
			return this.m_isGameOver;
		}
		set
		{
			this.m_isGameOver = value;
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

	public bool IsFinishGuide
	{
		get
		{
			return this.m_isFinishGuide;
		}
		set
		{
			this.m_isFinishGuide = value;
		}
	}

	public int CoinIndex
	{
		get
		{
			return this.m_coinIndex;
		}
		set
		{
			this.m_coinIndex = value;
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
		M002.m_instance = this;
	}

	private void Start()
	{
		this.Score = GM.GetInstance().GetScore(2);
		this.MaxScore = GM.GetInstance().GetScoreRecord(2);
		this.IsFinishGuide = (PlayerPrefs.GetInt("LocalData_guide_game02", 0) == 1);
		this.LoadMapData();
		this.LoadBlood();
		this.SaveGame();
		GM.GetInstance().SetSavedGameID(2);
        PauseManager.instance.RegisterGame(this);
    }

	private void Update()
	{
	}

	public static M002 GetInstance()
	{
		return M002.m_instance;
	}

	public void StartNewGame()
	{
		GM.GetInstance().SetSavedGameID(2);
		GM.GetInstance().ResetToNewGame();
      
		this.IsPause = false;
		this.IsStart = false;
		this.IsGameOver = false;
		this.IsFinishGuide = (PlayerPrefs.GetInt("LocalData_guide_game02", 0) == 1);
		this.CoinIndex = -1;
		this.Score = 0;
		this.MaxScore = GM.GetInstance().GetScoreRecord(2);
		this.InitMap();
		this.InitNewGame();
		this.FillLife();
		this.SaveGame();
		this.DoRefreshHandle();
	}

	public G00201 CreateBlock(int number, int idx, GameObject parent)
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
		gameObject.GetComponent<G00201>().Init(number, idx);
		gameObject.transform.SetParent(parent.transform, false);
		return gameObject.GetComponent<G00201>();
	}

	public void FreeBlock(GameObject obj)
	{
		obj.SetActive(false);
		obj.transform.DOKill(false);
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
		if (row < 7)
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

	public List<int> Use(int idx, int id)
	{
		List<int> list = new List<int>();
		switch (id)
		{
		case 1:
			this.setNumber(idx, 0);
			list.Add(idx);
			break;
		case 2:
		{
			int row = this.GetRow(idx);
			int num = (row + 3 >= 7) ? 7 : (row + 3);
			for (int i = row; i < num; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					this.setNumber(i, j, 0);
					int index = this.GetIndex(i, j);
					if (!list.Contains(index))
					{
						list.Add(index);
					}
				}
			}
			break;
		}
		case 3:
		{
			int number = this.GetNumber(idx);
			for (int k = 0; k < 5; k++)
			{
				for (int l = 0; l < 7; l++)
				{
					if (this.GetNumber(l, k) == number)
					{
						this.setNumber(l, k, 0);
						list.Add(this.GetIndex(l, k));
					}
				}
			}
			break;
		}
		}
		this.SaveGame();
		return list;
	}

	public int AddBock(int col)
	{
		for (int i = 6; i >= 0; i--)
		{
			if (this.GetNumber(i, col) == 0)
			{
				this.setNumber(i, col, this.GetBlood());
				return i;
			}
		}
		return -1;
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
		List<int> arg = new List<int>();
		for (int i = 0; i < 5; i++)
		{
			int num = 0;
			Queue queue = new Queue();
			for (int j = 6; j >= 0; j--)
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
					int row2 = 6 - k;
					this.setNumber(row2, i, number);
					list.Add(new sDropData(num2, this.GetIndex(row2, col)));
				}
			}
		}
		this.SaveGame();
		this.DoDropHandle(list, arg);
	}

	public void AutoDelete()
	{
		bool flag = false;
		for (int i = 6; i >= 0; i--)
		{
			for (int j = 4; j >= 0; j--)
			{
				int index = this.GetIndex(i, j);
				List<int> deletList = this.GetDeletList(index);
				if (deletList.Count > 1)
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
		this.RandomCoinPosition();
	}

	public void RandomCoinPosition()
	{
		if (this.CoinIndex >= 0)
		{
			return;
		}
		if (this.rd.Next(1, 100) < 70)
		{
			return;
		}
		List<int> list = new List<int>();
		for (int i = 0; i < 5; i++)
		{
			for (int j = 6; j >= 0; j--)
			{
				if (this.GetNumber(j, i) == 0)
				{
					list.Add(this.GetIndex(j, i));
					break;
				}
			}
		}
		UnityEngine.Debug.Log(list);
		if (list.Count <= 0)
		{
			return;
		}
		this.CoinIndex = list[this.rd.Next(0, list.Count)];
		Action expr_89 = this.OnRandomCoinHandle;
		if (expr_89 == null)
		{
			return;
		}
		expr_89();
	}

	public int AddLife()
	{
		int expr_00 = 2;
		if (this.IsFinishGuide)
		{
			this.BloodList[2] = this.GetNewBloodNumber();
			return expr_00;
		}
		this.BloodList[2] = 2;
		return expr_00;
	}

	public int GetBlood()
	{
		return this.BloodList[2];
	}

	public void FinishGuide()
	{
		this.IsFinishGuide = true;
		PlayerPrefs.SetInt("LocalData_guide_game02", 1);
	}

	public int GetMapMaxNumber()
	{
		int num = 1;
		for (int i = 0; i < 7; i++)
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
		if (GM.GetInstance().isSavedGame() && this.IsFinishGuide)
		{
			this.InitBlood();
			this.LoadLocalLife();
			return;
		}
		this.FillLife();
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

	public void FillLife()
	{
		this.InitBlood();
		if (this.IsFinishGuide)
		{
			this.bloodList[2] = this.GetNewBloodNumber();
			return;
		}
		this.bloodList[2] = 2;
	}

	public void LoadMapData()
	{
		this.InitMap();
		if (GM.GetInstance().isSavedGame() && this.IsFinishGuide)
		{
			this.LoadLocalData();
			return;
		}
		this.InitNewGame();
	}

	private void InitMap()
	{
		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				this.setNumber(i, j, 0);
			}
		}
	}

	private void InitNewGame()
	{
		this.rd = new System.Random();
		GM.GetInstance().SaveGame("", "", 0f, 0f);
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
		int row = this.GetRow(idx);
		int col = this.GetCol(idx);
		if (this.Map[row, col] == 0)
		{
			return;
		}
		if (list.Contains(idx))
		{
			return;
		}
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

	private GameObject CreateBlock()
	{
		return UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/G00201") as GameObject);
	}

	private void Delete(List<int> list, int index)
	{
		if (list.Count < 2)
		{
			this.SaveGame();
			this.DoNullDelete();
			return;
		}
		this.IsPlaying = true;
		int row = this.GetRow(index);
		int col = this.GetCol(index);
		int num = this.GetNumber(row, col);
		for (int i = list.Count / 2 + list.Count % 2; i > 0; i--)
		{
			num *= 2;
		}
		this.setNumber(row, col, num);
		foreach (int current in list)
		{
			int number = this.GetNumber(current);
			if (current != index)
			{
				//UnityEngine.Debug.Log(Math.Log((double)number, 2.0));
				this.AddScore((int)Math.Log((double)number, 2.0));
				GM.GetInstance().AddExp((int)Math.Log((double)number, 2.0) * 2);
				AchiveData.GetInstance().Add(2, 1, true);
				TaskData.GetInstance().Add(100101, 1, true);
				this.setNumber(this.GetRow(current), this.GetCol(current), 0);
			}
			else
			{
				if (TaskData.GetInstance().Get(100104).value < number)
				{
					TaskData.GetInstance().Add(100104, number, false);
				}
				if (AchiveData.GetInstance().Get(6).value < number)
				{
					AchiveData.GetInstance().Add(6, number, false);
					//AppsflyerUtils.TrackComp(2, number);
					if (number > 64)
					{
						Action<int> expr_175 = this.DoCompMaxNumber;
						if (expr_175 != null)
						{
							expr_175(number);
						}
					}
				}
			}
		}
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
		foreach (KeyValuePair<string, TLife> current in Configs.TLifes2)
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
		GM.GetInstance().SaveScoreRecord(2, this.MaxScore);
	}

	private void SaveGame()
	{
		int num = 35;
		string text = "";
		for (int i = 0; i < 7; i++)
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
		if (GM.GetInstance().isSavedGame())
		{
			GM.GetInstance().SaveGame(text, text2, -9999999f, -9999999f);
		}
		else
		{
			GM.GetInstance().SaveGame(text, text2, -9999999f, -9999999f);
		}
		GM.GetInstance().SaveScore(2, this.Score);
	}

	public void PrintMap(string tag)
	{
		UnityEngine.Debug.Log(tag + ":------------------------");
		for (int i = 0; i < 7; i++)
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
