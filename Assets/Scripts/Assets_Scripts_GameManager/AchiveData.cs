using Assets.Scripts.Configs;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.GameManager
{
	internal class AchiveData
	{
		[Serializable]
		private sealed class __c
		{
			public static readonly AchiveData.__c __9 = new AchiveData.__c();

			public static Comparison<TAchive> __9__12_0;

			internal int _Read_b__12_0(TAchive a, TAchive b)
			{
				return a.Type.CompareTo(b.Type);
			}
		}

		public const int ACHIVE_STATUS_RECEIVED = -3;

		public const int ACHIVE_STATUS_FINISHED = -2;

		public const int ACHIVE_STATUS_FINISHING = -1;

		private static AchiveData m_instance;

		private Dictionary<string, LocalData> achives = new Dictionary<string, LocalData>();

		public static AchiveData GetInstance()
		{
			if (AchiveData.m_instance == null)
			{
				AchiveData.m_instance = new AchiveData();
			}
			return AchiveData.m_instance;
		}

		public static void Initialize()
		{
			AchiveData.GetInstance().Read();
		}

		public bool isFinised(int type)
		{
			return this.Get(type).status == -2;
		}

		public LocalData Get(int type)
		{
			return this.achives[type.ToString()];
		}

		public void Add(int type, int value, bool isMerge = true)
		{
			if (!this.achives.ContainsKey(type.ToString()))
			{
				return;
			}
			LocalData localData = this.achives[type.ToString()];
			if (isMerge)
			{
				localData.value += value;
			}
			else
			{
				localData.value = value;
			}
			this.achives[type.ToString()] = localData;
			this.SetLocalData(localData.type, localData.value, localData.key, localData.status);
			GlobalEventHandle.EmitRefreshAchiveHandle(type);
			DotManager.GetInstance().CheckAchiev();
			if (localData.status != -1)
			{
				return;
			}
			TAchive tAchive = Configs.Configs.TAchives[localData.key.ToString()];
			if (localData.value >= tAchive.Value)
			{
				localData.status = -2;
			}
			this.achives[type.ToString()] = localData;
			this.SetLocalData(localData.type, localData.value, localData.key, localData.status);
			GlobalEventHandle.EmitRefreshAchiveHandle(type);
		}

		public bool Finish(int type)
		{
			if (!this.achives.ContainsKey(type.ToString()))
			{
				return false;
			}
			LocalData localData = this.achives[type.ToString()];
			if (localData.status != -2)
			{
				return false;
			}
			TAchive tAchive = Configs.Configs.TAchives[localData.key.ToString()];
			if (tAchive.Next == 0)
			{
				localData.status = -3;
			}
			else
			{
				TAchive tAchive2 = Configs.Configs.TAchives[tAchive.Next.ToString()];
				if (localData.value >= tAchive2.Value)
				{
					localData.status = -2;
					localData.key = tAchive2.ID;
				}
				else
				{
					localData.status = -1;
					localData.key = tAchive2.ID;
				}
			}
			this.achives[type.ToString()] = localData;
			this.SetLocalData(localData.type, localData.value, localData.key, localData.status);
			DotManager.GetInstance().CheckAchiev();
			return true;
		}

		public void Update(int type)
		{
			if (!this.achives.ContainsKey(type.ToString()))
			{
				return;
			}
			LocalData localData = this.achives[type.ToString()];
			if (localData.status != -3)
			{
				return;
			}
			TAchive tAchive = Configs.Configs.TAchives[localData.key.ToString()];
			if (tAchive.Next == 0)
			{
				return;
			}
			TAchive tAchive2 = Configs.Configs.TAchives[tAchive.Next.ToString()];
			if (localData.value >= tAchive2.Value)
			{
				localData.status = -2;
				localData.key = tAchive2.ID;
			}
			else
			{
				localData.status = -1;
				localData.key = tAchive2.ID;
			}
			this.achives[type.ToString()] = localData;
			this.SetLocalData(localData.type, localData.value, localData.key, localData.status);
		}

		private void Read()
		{
			List<TAchive> list = new List<TAchive>();
			foreach (KeyValuePair<string, TAchive> current in Configs.Configs.TAchives)
			{
				bool flag = false;
				using (List<TAchive>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.Type == current.Value.Type)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					list.Add(current.Value);
				}
			}
			List<TAchive> arg_A7_0 = list;
			Comparison<TAchive> arg_A7_1;
			if ((arg_A7_1 = AchiveData.__c.__9__12_0) == null)
			{
				arg_A7_1 = (AchiveData.__c.__9__12_0 = new Comparison<TAchive>(AchiveData.__c.__9._Read_b__12_0));
			}
			arg_A7_0.Sort(arg_A7_1);
			foreach (TAchive current2 in list)
			{
				LocalData localData = this.GetLocalData(current2.Type);
				if (localData.type == 0)
				{
					Dictionary<int, int> dictionary = new Dictionary<int, int>
					{
						{
							1,
							1
						},
						{
							2,
							0
						},
						{
							3,
							0
						},
						{
							4,
							0
						},
						{
							5,
							0
						},
						{
							6,
							0
						}
					};
					localData.type = current2.Type;
					localData.key = current2.ID;
					localData.status = -1;
					localData.value = dictionary[current2.Type];
					this.SetLocalData(localData.type, localData.value, localData.key, localData.status);
				}
				else
				{
					this.Update(localData.type);
				}
				this.achives.Add(current2.Type.ToString(), localData);
			}
		}

		private string GetLocalKey(int key)
		{
			return string.Format("LocalData_Achive_{0}", key);
		}

		private void SetLocalData(int type, int value, int id, int status)
		{
			string value2 = string.Format("{0},{1},{2},{3}", new object[]
			{
				type,
				value,
				id,
				status
			});
			PlayerPrefs.SetString(this.GetLocalKey(type), value2);
		}

		private LocalData GetLocalData(int type)
		{
			return this.ConvertLocalData(PlayerPrefs.GetString(this.GetLocalKey(type), "nil"));
		}

		private LocalData ConvertLocalData(string str)
		{
			LocalData result = default(LocalData);
			if (str.Equals("nil"))
			{
				result.type = 0;
				result.key = 0;
				result.status = 0;
				result.value = 0;
			}
			else
			{
				string[] array = str.Split(new char[]
				{
					','
				});
				result.type = int.Parse(array[0]);
				result.value = int.Parse(array[1]);
				result.key = int.Parse(array[2]);
				result.status = int.Parse(array[3]);
			}
			return result;
		}
	}
}
