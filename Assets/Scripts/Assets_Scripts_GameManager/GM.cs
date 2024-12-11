using Assets.Scripts.Configs;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameManager
{
	internal class GM
	{
		private static GM g_intance;

		private int diamond;

		private int lv = 1;

		private int exp;

		private int m_consumeCount;

		private int m_gameId;

		private int m_skinID = 1;

		private bool m_isPlayVedioAds;

		private System.Random m_random = new System.Random();

		public int Exp
		{
			get
			{
				return this.exp;
			}
			set
			{
				this.exp = value;
			}
		}

		public int Diamond
		{
			get
			{
				return this.diamond;
			}
			set
			{
				this.diamond = value;
			}
		}

		public int Lv
		{
			get
			{
				return this.lv;
			}
			set
			{
				this.lv = value;
			}
		}

		public int ConsumeCount
		{
			get
			{
				return this.m_consumeCount;
			}
			set
			{
				this.m_consumeCount = value;
			}
		}

		public int GameId
		{
			get
			{
				return this.m_gameId;
			}
			set
			{
				this.m_gameId = value;
			}
		}

		public int SkinID
		{
			get
			{
				return this.m_skinID;
			}
			set
			{
				this.m_skinID = value;
			}
		}

		public bool IsPlayVedioAds
		{
			get
			{
				return this.m_isPlayVedioAds;
			}
			set
			{
				this.m_isPlayVedioAds = value;
			}
		}

		public static GM GetInstance()
		{
			if (GM.g_intance == null)
			{
				GM.g_intance = new GM();
			}
			return GM.g_intance;
		}

		public bool Init()
		{
			this.SaveInstallTime();
			this.LoadLocalData();
			this.InitEvent();
			return true;
		}

		public int AddDiamond(int value, bool isPlayAni = true)
		{
			this.Diamond += value;
			PlayerPrefs.SetInt("LocalData_Diamond", this.Diamond);
			GlobalEventHandle.EmitGetDiamondHandle(value, isPlayAni);
			return this.Diamond;
		}

		public int AddDiamondBase(int value)
		{
			this.Diamond += value;
			PlayerPrefs.SetInt("LocalData_Diamond", this.Diamond);
			return this.Diamond;
		}

		public void ConsumeGEM(int value)
		{
			this.Diamond -= value;
			PlayerPrefs.SetInt("LocalData_Diamond", this.Diamond);
			GlobalEventHandle.EmitConsumeDiamondHandle(value);
		}

		public bool isFullGEM(int value)
		{
			return this.Diamond >= value;
		}

		public bool AddExp(int value)
		{
			this.Exp += value;
			if (this.Lv >= 120)
			{
				this.Lv = 120;
				this.Exp = ((Configs.Configs.TPlayers[120.ToString()].Exp < this.Exp) ? Configs.Configs.TPlayers[120.ToString()].Exp : this.Exp);
			}
			PlayerPrefs.SetInt("LocalData_Exp", this.Exp);
			GlobalEventHandle.EmitAddExpHandle(false);
			TPlayer tPlayer = null;
			if (Configs.Configs.TPlayers.ContainsKey(this.Lv.ToString()))
			{
				tPlayer = Configs.Configs.TPlayers[this.Lv.ToString()];
			}
			if (tPlayer == null)
			{
				return false;
			}
			if (this.Exp <= tPlayer.Exp)
			{
				return false;
			}
			while (this.exp > tPlayer.Exp)
			{
				this.Exp -= tPlayer.Exp;
				this.Lv++;
				AchiveData.GetInstance().Add(1, 1, true);
				if (Configs.Configs.TPlayers.ContainsKey(this.Lv.ToString()))
				{
					tPlayer = Configs.Configs.TPlayers[this.Lv.ToString()];
				}
				if (tPlayer == null)
				{
					break;
				}
			}
			PlayerPrefs.SetInt("LocalData_Exp", this.Exp);
            Debug.Log("LocalData_Lv "+ Lv);
			PlayerPrefs.SetInt("LocalData_Lv", this.Lv);
			GlobalEventHandle.EmitAddExpHandle(true);
			//AppsflyerUtils.TrackLevel(this.lv);
			return true;
		}

		public void ResetToNewGame()
		{
		}

		public void ResetConsumeCount()
		{
			this.ConsumeCount = 0;
		}

		public void AddConsumeCount()
		{
			int consumeCount = this.ConsumeCount + 1;
			this.ConsumeCount = consumeCount;
		}

		public bool isSavedGame()
		{
			return this.GetSavedGameID() != 0;
		}

		public int GetSavedGameID()
		{
			return PlayerPrefs.GetInt("LocalData_GameId", 0);
		}

		public void SetSavedGameID(int value)
		{
			this.GameId = value;
            Debug.Log("LocalData_GameId"+ value);
			PlayerPrefs.SetInt("LocalData_GameId", value);
		}

		public string GetSavedGameMap()
		{
			return PlayerPrefs.GetString("LocalData_OldGame", "");
		}

		public string GetSavedLife()
		{
			return PlayerPrefs.GetString("LocalData_OldLife", "");
		}

		public Vector2 GetSavedPos()
		{
			return new Vector2(PlayerPrefs.GetFloat("LocalData_OldPosX", 0f), PlayerPrefs.GetFloat("LocalData_OldPosY", 0f));
		}

		public void SaveGame(string value, string life, float x = -9999999f, float y = -9999999f)
		{
			if (!value.Equals(""))
			{
				PlayerPrefs.SetString("LocalData_OldGame", value);
			}
			if (!life.Equals(""))
			{
				PlayerPrefs.SetString("LocalData_OldLife", life);
			}
			if (x != -9999999f)
			{
				PlayerPrefs.SetFloat("LocalData_OldPosX", x);
			}
			if (y != -9999999f)
			{
				PlayerPrefs.SetFloat("LocalData_OldPosY", y);
			}
		}

		public void SaveScore(int gameID, int score)
		{
			string[] array = PlayerPrefs.GetString("LocalData_OldScore", "0,0,0").Split(new char[]
			{
				','
			});
			if (gameID > array.Length)
			{
				return;
			}
			array[gameID - 1] = score.ToString();
			PlayerPrefs.SetString("LocalData_OldScore", string.Format("{0},{1},{2}", array[0], array[1], array[2]));
		}

		public int GetScore(int gameID)
		{
			string[] arg_25_0 = PlayerPrefs.GetString("LocalData_OldScore", "0,0,0").Split(new char[]
			{
				','
			});
			List<int> list = new List<int>();
			string[] array = arg_25_0;
			for (int i = 0; i < array.Length; i++)
			{
				string value = array[i];
				list.Add(Convert.ToInt32(value));
			}
			if (gameID > list.Count)
			{
				return 0;
			}
			return list[gameID - 1];
		}

		public void SaveScoreRecord(int gameID, int score)
		{
			string[] array = PlayerPrefs.GetString("LocalData_Record_Score", "0,0,0").Split(new char[]
			{
				','
			});
			if (gameID > array.Length)
			{
				return;
			}
			if (score > Convert.ToInt32(array[gameID - 1]))
			{
				array[gameID - 1] = score.ToString();
				if (gameID == 3)
				{
					///AppsflyerUtils.TrackNewLevel(3, score);
				}
				PlayerPrefs.SetString("LocalData_Record_Score", string.Format("{0},{1},{2}", array[0], array[1], array[2]));
				GlobalEventHandle.EmitRefreshMaxScoreHandle(array);
			}
		}

		public int GetScoreRecord(int gameID)
		{
			string[] arg_25_0 = PlayerPrefs.GetString("LocalData_Record_Score", "0,0,0").Split(new char[]
			{
				','
			});
			List<int> list = new List<int>();
			string[] array = arg_25_0;
			for (int i = 0; i < array.Length; i++)
			{
				string value = array[i];
				list.Add(Convert.ToInt32(value));
			}
			if (gameID > list.Count)
			{
				return 0;
			}
			return list[gameID - 1];
		}

		public void SaveG003ScoreRecord(int LvID, int CheckPoint)
		{
			string @string = PlayerPrefs.GetString("LocalData_G003_Record_Score_" + LvID.ToString(), "-1");
			if (CheckPoint > Convert.ToInt32(@string))
			{
				///AppsflyerUtils.TrackNewLevel(3, CheckPoint);
				PlayerPrefs.SetString("LocalData_G003_Record_Score_" + LvID.ToString(), CheckPoint.ToString());
			}
		}

		public int GetG003ScoreRecord(int LvID)
		{
			return Convert.ToInt32(PlayerPrefs.GetString("LocalData_G003_Record_Score_" + LvID.ToString(), "-1"));
		}

		public bool isFirstGame()
		{
			return PlayerPrefs.GetInt("LocalData_FirstGame", 0) == 0;
		}

		public void SetFristGame()
		{
			PlayerPrefs.SetInt("LocalData_FirstGame", 1);
		}

		public bool IsFirstFinishGame()
		{
			return PlayerPrefs.GetInt("LocalData_IsFirstFinish", 0) < 2;
		}

		public void SetFirstFinishGame()
		{
			int @int = PlayerPrefs.GetInt("LocalData_IsFirstFinish", 0);
			if (@int < 10)
			{
				PlayerPrefs.SetInt("LocalData_IsFirstFinish", @int + 1);
			}
		}

		public int GetLocalSkinID()
		{
			return PlayerPrefs.GetInt("LocalData_SkinID", 1);
		}

		public void SetLocalSkinID(int id)
		{
			this.SkinID = id;
			PlayerPrefs.SetInt("LocalData_SkinID", id);
			Action expr_17 = GlobalEventHandle.DoTransiformSkin;
			if (expr_17 == null)
			{
				return;
			}
			expr_17();
		}

		public void SaveInstallTime()
		{
			if (PlayerPrefs.GetString("LocalData_InitTime", "-1").Equals("-1"))
			{
				PlayerPrefs.SetString("LocalData_InitTime", DateTime.Now.ToString("yyyy-MM-dd"));
			}
		}

		private DateTime GetInstallTime()
		{
			string @string = PlayerPrefs.GetString("LocalData_InitTime", "-1");
			if (@string.Equals("-1"))
			{
				return DateTime.Now;
			}
			string[] array = @string.Split(new char[]
			{
				'-'
			});
			return new DateTime(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
		}

		public bool IsNewUser()
		{
			return (DateTime.Now - this.GetInstallTime()).Days == 0;
		}

		public List<int> GetSkinData()
		{
			List<int> list = new List<int>();
			string[] array = PlayerPrefs.GetString("LocalData_SkinData", "0,1").Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string value = array[i];
				list.Add(Convert.ToInt32(value));
			}
			return list;
		}

		public void SetSkinData(int skinID, int status)
		{
			string[] array = PlayerPrefs.GetString("LocalData_SkinData", "0,1").Split(new char[]
			{
				','
			});
			if (skinID > array.Length)
			{
				return;
			}
			array[skinID - 1] = status.ToString();
			PlayerPrefs.SetString("LocalData_SkinData", string.Format("{0},{1}", array[0], array[1]));
		}

		public DateTime GetSkinFreeTime(int skinID)
		{
			List<DateTime> list = new List<DateTime>();
			string[] array = PlayerPrefs.GetString("LocalData_SkinFreeTime", "-1,-1").Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (text.Equals("-1"))
				{
					list.Add(DateTime.Now);
				}
				else
				{
					string[] array2 = text.Split(new char[]
					{
						'-'
					});
					list.Add(new DateTime(int.Parse(array2[0]), int.Parse(array2[1]), int.Parse(array2[2])));
				}
			}
			return list[skinID - 1];
		}

		public void SetSkinFreeTime(int skinID, DateTime time)
		{
			List<DateTime> list = new List<DateTime>();
			string[] array = PlayerPrefs.GetString("LocalData_SkinFreeTime", "-1,-1").Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (text.Equals("-1"))
				{
					list.Add(DateTime.Now);
				}
				else
				{
					string[] array2 = text.Split(new char[]
					{
						'-'
					});
					list.Add(new DateTime(int.Parse(array2[0]), int.Parse(array2[1]), int.Parse(array2[2])));
				}
			}
			if (skinID > list.Count)
			{
				return;
			}
			list[skinID - 1] = time;
			string text2 = "";
			for (int j = 0; j < list.Count; j++)
			{
				text2 += list[j].ToString("yyyy-MM-dd");
				if (j < list.Count - 1)
				{
					text2 += ",";
				}
			}
			PlayerPrefs.SetString("LocalData_SkinFreeTime", text2);
		}

		public void ResetSkinFreeTime()
		{
			List<int> skinData = this.GetSkinData();
			for (int i = 0; i < skinData.Count; i++)
			{
				if (skinData[i] == 2)
				{
					DateTime skinFreeTime = this.GetSkinFreeTime(i + 1);
					if ((DateTime.Now - skinFreeTime.AddDays(3.0)).Days > 0)
					{
						this.SetSkinData(i + 1, 1);
						if (this.SkinID == i + 1)
						{
							this.SetLocalSkinID(1);
						}
					}
				}
			}
		}

		public bool isFirstShare()
		{
			return PlayerPrefs.GetInt("LocalData_FirstShare", 0) == 0;
		}

		public void ResetFirstShare(int value = 0)
		{
			PlayerPrefs.SetInt("LocalData_FirstShare", value);
		}

		public bool IsRandomStatus(int value)
		{
			return this.m_random.Next(1, 100) < value;
		}

		private void LoadLocalData()
		{
			this.Diamond = PlayerPrefs.GetInt("LocalData_Diamond", 0);
			this.Lv = PlayerPrefs.GetInt("LocalData_Lv", 1);
			this.exp = PlayerPrefs.GetInt("LocalData_Exp", 0);
			this.GameId = this.GetSavedGameID();
			this.SkinID = this.GetLocalSkinID();
		}

		private void InitEvent()
		{
		}
	}
}
