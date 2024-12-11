using Assets.Scripts.Configs;
using Assets.Scripts.GameManager;
using System;
using System.Collections.Generic;
using UnityEngine;

public class M003 : MonoBehaviour
{
	public const int CHECK_POINT_ROW = 4;

	public const int CHECK_POINT_CELL_HEIGHT = 150;

	public const int CHECK_POINT_CELL_WIDTH = 150;

	public const int LV_CELL_HEIGHT = 150;

	public Action<G00300> DoClickCheckPointHandler;

	public Action<G00305> DoClickLvHandler;

	private Dictionary<string, int> m_lv_score = new Dictionary<string, int>();

	private int m_lv;

	private int m_checkPoint;

	private int m_topCheckPoint;

	private Dictionary<string, TG003> m_config_G003;

	private Dictionary<string, TG00301> m_config_G00301;

	private int m_gamebox_height = 113;

	private static M003 m_instance;

	internal Dictionary<string, TG003> Config_G003
	{
		get
		{
			return this.m_config_G003;
		}
		set
		{
			this.m_config_G003 = value;
		}
	}

	internal Dictionary<string, TG00301> Config_G00301
	{
		get
		{
			return this.m_config_G00301;
		}
		set
		{
			this.m_config_G00301 = value;
		}
	}

	public Dictionary<string, int> Lv_score
	{
		get
		{
			return this.m_lv_score;
		}
		set
		{
			this.m_lv_score = value;
		}
	}

	public int Lv
	{
		get
		{
			return this.m_lv;
		}
		set
		{
			this.m_lv = value;
		}
	}

	public int Gamebox_height
	{
		get
		{
			return this.m_gamebox_height;
		}
		set
		{
			this.m_gamebox_height = value;
		}
	}

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

	public static M003 GetInstance()
	{
		return M003.m_instance;
	}

	private void Awake()
	{
		M003.m_instance = this;
	}

	private void Start()
	{
		GM.GetInstance().SaveG003ScoreRecord(301, 0);
		this.LoadConfig();
		this.LoadScore();
	}

	public void LoadConfig()
	{
		this.Config_G003 = Configs.TG003;
		this.Config_G00301 = Configs.TG00301;
	}

	public void LoadScore()
	{
		this.Lv_score.Clear();
		this.TopCheckPoint = GM.GetInstance().GetScoreRecord(3);
		this.TopCheckPoint = ((this.TopCheckPoint == 0) ? 30000 : this.TopCheckPoint);
		foreach (KeyValuePair<string, TG003> current in this.Config_G003)
		{
			this.Lv_score.Add(current.Value.ID.ToString(), GM.GetInstance().GetG003ScoreRecord(current.Value.ID));
		}
	}

	public List<string> GetcheckPointByLv(int lv)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, TG00301> current in this.Config_G00301)
		{
			if (current.Value.G003 == lv)
			{
				list.Add(current.Key);
			}
		}
		return list;
	}

	public void StartNewGame(string idx)
	{
		GlobalEventHandle.EmitClickPageButtonHandle("Game03", this.Config_G00301[idx].ID);
	}

	public void UnlockLv(int lv)
	{
		GM.GetInstance().SaveG003ScoreRecord(lv, 0);
	}

	public int GetRow(int index)
	{
		return index / 4;
	}

	public int GetCol(int index)
	{
		return index % 4;
	}

	public int GetCheckPointCount(int lv = 0)
	{
		if (lv != 0)
		{
			return this.GetcheckPointByLv(lv).Count;
		}
		return this.Config_G00301.Count;
	}

	public int GetLvCount()
	{
		return this.Config_G003.Count;
	}

	public int GetGameBoxHeight(int lv = 0)
	{
		return this.GetCheckPointCount(lv) / 4 * 150;
	}

	public int GetGameBoxHeightEx(int lv = 0)
	{
		return (this.GetCheckPointCount(lv) / 4 + ((this.GetCheckPointCount(0) % 4 != 0) ? 2 : 1)) * 150;
	}

	public int GetLvBoxHeight()
	{
		return this.GetLvCount() * 150;
	}
}
