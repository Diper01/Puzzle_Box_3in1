using Assets.Scripts.Configs;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.GameManager
{
	internal class TaskData
	{
		[Serializable]
		private sealed class __c
		{
			public static readonly TaskData.__c __9 = new TaskData.__c();

			public static Comparison<TTask> __9__13_0;

			internal int _Read_b__13_0(TTask a, TTask b)
			{
				return a.ID.CompareTo(b.ID);
			}
		}

		public const int TASK_STATUS_RECEIVED = -3;

		public const int TASK_STATUS_FINISHED = -2;

		public const int TASK_STATUS_FINISHING = -1;

		private static TaskData m_instance;

		private Dictionary<string, LocalData> tasks = new Dictionary<string, LocalData>();

		public static TaskData GetInstance()
		{
			if (TaskData.m_instance == null)
			{
				TaskData.m_instance = new TaskData();
			}
			return TaskData.m_instance;
		}

		public static void Initialize()
		{
			TaskData.GetInstance().Read();
		}

		public bool isFinised(int type)
		{
			return this.Get(type).status == -2;
		}

		public LocalData Get(int type)
		{
			return this.tasks[type.ToString()];
		}

		public void Add()
		{
			foreach (KeyValuePair<string, TTask> current in Configs.Configs.TTasks)
			{
				this.Add(current.Value.ID, 1, true);
			}
		}

		public void Add(int type, int value, bool isMerge = true)
		{
			if (!this.tasks.ContainsKey(type.ToString()))
			{
				return;
			}
			LocalData localData = this.tasks[type.ToString()];
			if (isMerge)
			{
				localData.value += value;
			}
			else
			{
				localData.value = value;
			}
			this.tasks[type.ToString()] = localData;
			this.SetLocalData(localData.type, localData.value, localData.key, localData.status);
			GlobalEventHandle.EmitRefreshTaskHandle(type);
			if (localData.status != -1)
			{
				return;
			}
			TTask tTask = Configs.Configs.TTasks[localData.key.ToString()];
			if (localData.value >= tTask.Value)
			{
				localData.status = -2;
			}
			this.tasks[type.ToString()] = localData;
			this.SetLocalData(localData.type, localData.value, localData.key, localData.status);
			DotManager.GetInstance().CheckTask();
			GlobalEventHandle.EmitRefreshTaskHandle(type);
		}

		public bool Finish(int type)
		{
			if (!this.tasks.ContainsKey(type.ToString()))
			{
				return false;
			}
			LocalData localData = this.tasks[type.ToString()];
			if (localData.status != -2)
			{
				return false;
			}
			TTask tTask = Configs.Configs.TTasks[localData.key.ToString()];
			if (localData.value < tTask.Value)
			{
				return false;
			}
			localData.status = -3;
			this.tasks[type.ToString()] = localData;
			this.SetLocalData(localData.type, localData.value, localData.key, localData.status);
			DotManager.GetInstance().CheckTask();
			return true;
		}

		public void Reset()
		{
			this.tasks.Clear();
			foreach (KeyValuePair<string, TTask> current in Configs.Configs.TTasks)
			{
				LocalData localData = this.GetLocalData(current.Value.ID);
				localData.type = -1;
				localData.key = current.Value.ID;
				localData.status = -1;
				localData.value = 0;
				this.SetLocalData(localData.type, localData.value, localData.key, localData.status);
			}
			this.Read();
		}

		private void Read()
		{
			List<TTask> list = new List<TTask>();
			foreach (KeyValuePair<string, TTask> current in Configs.Configs.TTasks)
			{
				bool flag = false;
				using (List<TTask>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.ID == current.Value.ID)
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
			List<TTask> arg_A7_0 = list;
			Comparison<TTask> arg_A7_1;
			if ((arg_A7_1 = TaskData.__c.__9__13_0) == null)
			{
				arg_A7_1 = (TaskData.__c.__9__13_0 = new Comparison<TTask>(TaskData.__c.__9._Read_b__13_0));
			}
			arg_A7_0.Sort(arg_A7_1);
			foreach (TTask current2 in list)
			{
				LocalData localData = this.GetLocalData(current2.ID);
				if (localData.type == 0)
				{
					localData.type = -1;
					localData.key = current2.ID;
					localData.status = -1;
					localData.value = 0;
					this.SetLocalData(localData.type, localData.value, localData.key, localData.status);
				}
				this.tasks.Add(current2.ID.ToString(), localData);
			}
		}

		private string GetLocalKey(int key)
		{
			return string.Format("LocalData_Task_{0}", key);
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
			PlayerPrefs.SetString(this.GetLocalKey(id), value2);
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
