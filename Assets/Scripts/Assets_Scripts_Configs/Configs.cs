using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Configs
{
	internal class Configs
	{
		public static Dictionary<string, TPlayer> TPlayers = new Dictionary<string, TPlayer>();

		public static Dictionary<string, TLife> TLifes = new Dictionary<string, TLife>();

		public static Dictionary<string, TLife> TLifes2 = new Dictionary<string, TLife>();

		public static Dictionary<string, TAchive> TAchives = new Dictionary<string, TAchive>();

		public static Dictionary<string, TTask> TTasks = new Dictionary<string, TTask>();

		public static Dictionary<string, TActivity> TActivitys = new Dictionary<string, TActivity>();

		public static Dictionary<string, TSound> TSounds = new Dictionary<string, TSound>();

		public static Dictionary<string, TLanguage> TLanguages = new Dictionary<string, TLanguage>();

		public static Dictionary<string, TG003> TG003 = new Dictionary<string, TG003>();

		public static Dictionary<string, TG00301> TG00301 = new Dictionary<string, TG00301>();

		private static void LoadPlayers()
		{
			Configs.TPlayers.Clear();
			foreach (TPlayer current in Configs.GetJsonArray<TPlayer>((Resources.Load("Config/Player") as TextAsset).text))
			{
				if (!Configs.TPlayers.ContainsKey(current.ID.ToString()))
				{
					Configs.TPlayers.Add(current.ID.ToString(), current);
				}
				else
				{
					Configs.TPlayers[current.ID.ToString()] = current;
				}
			}
		}

		private static void LoadLifes()
		{
			Configs.TLifes.Clear();
			foreach (TLife current in Configs.GetJsonArray<TLife>((Resources.Load("Config/Life") as TextAsset).text))
			{
				if (!Configs.TLifes.ContainsKey(current.ID.ToString()))
				{
					Configs.TLifes.Add(current.ID.ToString(), current);
				}
				else
				{
					Configs.TLifes[current.ID.ToString()] = current;
				}
			}
		}

		private static void LoadLifes2()
		{
			Configs.TLifes2.Clear();
			foreach (TLife current in Configs.GetJsonArray<TLife>((Resources.Load("Config/Life2") as TextAsset).text))
			{
				if (!Configs.TLifes2.ContainsKey(current.ID.ToString()))
				{
					Configs.TLifes2.Add(current.ID.ToString(), current);
				}
				else
				{
					Configs.TLifes2[current.ID.ToString()] = current;
				}
			}
		}

		private static void LoadAchives()
		{
			Configs.TAchives.Clear();
			foreach (TAchive current in Configs.GetJsonArray<TAchive>((Resources.Load("Config/Achive") as TextAsset).text))
			{
				if (!Configs.TAchives.ContainsKey(current.ID.ToString()))
				{
					Configs.TAchives.Add(current.ID.ToString(), current);
				}
				else
				{
					Configs.TAchives[current.ID.ToString()] = current;
				}
			}
		}

		private static void LoadTasks()
		{
			Configs.TTasks.Clear();
			foreach (TTask current in Configs.GetJsonArray<TTask>((Resources.Load("Config/Task") as TextAsset).text))
			{
				if (!Configs.TTasks.ContainsKey(current.ID.ToString()))
				{
					Configs.TTasks.Add(current.ID.ToString(), current);
				}
				else
				{
					Configs.TTasks[current.ID.ToString()] = current;
				}
			}
		}

		private static void LoadActivity()
		{
			Configs.TActivitys.Clear();
			foreach (TActivity current in Configs.GetJsonArray<TActivity>((Resources.Load("Config/Activity") as TextAsset).text))
			{
				if (!Configs.TActivitys.ContainsKey(current.ID.ToString()))
				{
					Configs.TActivitys.Add(current.ID.ToString(), current);
				}
				else
				{
					Configs.TActivitys[current.ID.ToString()] = current;
				}
			}
		}

		private static void LoadSound()
		{
			Configs.TSounds.Clear();
			foreach (TSound current in Configs.GetJsonArray<TSound>((Resources.Load("Config/Sound") as TextAsset).text))
			{
				if (!Configs.TSounds.ContainsKey(current.ID.ToString()))
				{
					Configs.TSounds.Add(current.ID.ToString(), current);
				}
				else
				{
					Configs.TSounds[current.ID.ToString()] = current;
				}
			}
		}

		private static void LoadLanguage()
		{
			Configs.TLanguages.Clear();
			foreach (TLanguage current in Configs.GetJsonArray<TLanguage>((Resources.Load("Config/Language") as TextAsset).text))
			{
				if (!Configs.TLanguages.ContainsKey(current.ID.ToString()))
				{
					Configs.TLanguages.Add(current.ID.ToString(), current);
				}
				else
				{
					Configs.TLanguages[current.ID.ToString()] = current;
				}
			}
		}

		private static void LoadG003()
		{
			Configs.TG003.Clear();
			foreach (TG003 current in Configs.GetJsonArray<TG003>((Resources.Load("Config/G003") as TextAsset).text))
			{
				if (!Configs.TG003.ContainsKey(current.ID.ToString()))
				{
					Configs.TG003.Add(current.ID.ToString(), current);
				}
				else
				{
					Configs.TG003[current.ID.ToString()] = current;
				}
			}
		}

		private static void LoadG00301()
		{
			Configs.TG00301.Clear();
			foreach (TG00301 current in Configs.GetJsonArray<TG00301>((Resources.Load("Config/G00301") as TextAsset).text))
			{
				if (!Configs.TG00301.ContainsKey(current.ID.ToString()))
				{
					Configs.TG00301.Add(current.ID.ToString(), current);
				}
				else
				{
					Configs.TG00301[current.ID.ToString()] = current;
				}
			}
		}

		private static List<T> GetJsonArray<T>(string json)
		{
			return JsonUtility.FromJson<Wrapper<T>>("{ \"Array\": " + json + "}").Array;
		}

		public static void LoadConfig()
		{
			Configs.LoadPlayers();
			Configs.LoadLifes();
			Configs.LoadLifes2();
			Configs.LoadAchives();
			Configs.LoadTasks();
			Configs.LoadActivity();
			Configs.LoadSound();
			//Configs.LoadLanguage();
			Configs.LoadG003();
			Configs.LoadG00301();
		}
	}
}
