using Assets.Scripts.Utils;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.GameManager
{
	internal class GlobalTimer
	{
		private static GlobalTimer m_instance;

		private float m_gameTotalTime;

		private float m_gameMiniTime;

		private DateTime m_saveTimer;



		public event Action RefreshHandle;

		public static GlobalTimer GetInstance()
		{
			if (GlobalTimer.m_instance == null)
			{
				GlobalTimer.m_instance = new GlobalTimer();
			}
			return GlobalTimer.m_instance;
		}

		public static void Initialize()
		{
			GlobalTimer.m_instance = new GlobalTimer();
			GlobalTimer.m_instance.m_saveTimer = GlobalTimer.m_instance.GetLocalTime();
			GlobalTimer.m_instance.SaveTime();
		}

		public void Update()
		{
			if (GM.GetInstance().GameId != 0)
			{
				this.m_gameMiniTime += Time.deltaTime;
			}
			this.m_gameTotalTime += Time.deltaTime;
			if ((DateTime.Now - GlobalTimer.m_instance.m_saveTimer).Days == 0)
			{
				return;
			}
			GlobalTimer.m_instance.m_saveTimer = DateTime.Now;
			GlobalTimer.m_instance.SaveTime();
			TaskData.GetInstance().Reset();
			Action expr_82 = this.RefreshHandle;
			if (expr_82 != null)
			{
				expr_82();
			}
			LoginData.GetInstance().RunSerialDay();
			DotManager.GetInstance().Check();
			Utils.Utils.ShowLoginRewards();
			GM.GetInstance().ResetSkinFreeTime();
			GM.GetInstance().ResetFirstShare(0);
		}

		public void ResetTotalTime()
		{
			this.m_gameTotalTime = 0f;
		}

		public void ResetMiniTime()
		{
			this.m_gameMiniTime = 0f;
		}

		public void TrackTotalTime()
		{
			if (this.m_gameTotalTime > 0f)
			{
//				AppsflyerUtils.TrackTotalTime(this.m_gameTotalTime);
				UnityEngine.Debug.Log("Total Time:" + this.m_gameTotalTime);
				this.ResetTotalTime();
			}
		}

		public void TrackMiniTime()
		{
			if (this.m_gameMiniTime > 0f)
			{
				//AppsflyerUtils.TrackMiniTime(GM.GetInstance().GameId, this.m_gameMiniTime);
				UnityEngine.Debug.Log("Mini Time:" + this.m_gameMiniTime);
				this.ResetMiniTime();
			}
		}

		private DateTime ConvertLongToDateTime(long timeStamp)
		{
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
			long ticks = long.Parse(timeStamp + "0000");
			TimeSpan value = new TimeSpan(ticks);
			return dateTime.Add(value);
		}

		private long ConvertDateTimeToLong(DateTime time)
		{
			DateTime d = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
			return (long)(time - d).TotalSeconds;
		}

		public static long GetTimeStamp(bool bflag = false)
		{
			TimeSpan timeSpan = DateTime.Now - new DateTime(1970, 1, 1, 8, 0, 0, 0);
			if (!bflag)
			{
				return Convert.ToInt64(timeSpan.TotalMilliseconds);
			}
			return Convert.ToInt64(timeSpan.TotalSeconds);
		}

		private void SaveTime()
		{
			PlayerPrefs.SetString("LocalData_Timer", DateTime.Now.ToString("yyyy-MM-dd"));
		}

		private DateTime GetLocalTime()
		{
			string @string = PlayerPrefs.GetString("LocalData_Timer", "-1");
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
	}
}
