using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Game : MonoBehaviour
{
	public enum NumType
	{
		EMPTY,
		NORMAL,
		SYBOM,
		ROW_CLEAR,
		COLUM_CLEAR,
		COUNT
	}

	[Serializable]
	public struct NumberProfab
	{
		public Game.NumType type;

		public GameObject prefab;
	}

	private sealed class _FillAll_d__20 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int __1__state;

		private object __2__current;

		public Game __4__this;

		object IEnumerator<object>.Current
		{
			get
			{
				return this.__2__current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this.__2__current;
			}
		}

		public _FillAll_d__20(int __1__state)
		{
			this.__1__state = __1__state;
		}

		void IDisposable.Dispose()
		{
		}

		bool IEnumerator.MoveNext()
		{
			int num = this.__1__state;
			Game game = this.__4__this;
			switch (num)
			{
			case 0:
				this.__1__state = -1;
				break;
			case 1:
				this.__1__state = -1;
				goto IL_6D;
			case 2:
				this.__1__state = -1;
				goto IL_6D;
			default:
				return false;
			}
			IL_29:
			this.__2__current = new WaitForSeconds(game.filleTime);
			this.__1__state = 1;
			return true;
			IL_6D:
			if (!game.Fill())
			{
				game.ClearALLMatch();
				goto IL_29;
			}
			this.__2__current = new WaitForSeconds(game.filleTime);
			this.__1__state = 2;
			return true;
		}

		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	public float filleTime;

	public Dictionary<Game.NumType, GameObject> numberPrefabDict;

	public int RandMaxNum;

	public int MinNum;

	public int MaxNum;

	[HideInInspector]
	public const int xColum = 5;

	[HideInInspector]
	public const int yRow = 5;

	[HideInInspector]
	public const int MAX_LENGTH = 5;

	public Game.NumberProfab[] numberPrefabs;

	private NumberObj[,] numberArry;

	private List<NumberObj> sybomList;

	[HideInInspector]
	private Vector2 movingValue = new Vector2(1.1f, -2.2f);

	private NumberObj currentChooseObj;

	private bool actionControl;

	private int clearTimes;

	private void Start()
	{
		this.numberPrefabDict = new Dictionary<Game.NumType, GameObject>();
		for (int i = 0; i < this.numberPrefabs.Length; i++)
		{
			if (!this.numberPrefabDict.ContainsKey(this.numberPrefabs[i].type))
			{
				this.numberPrefabDict.Add(this.numberPrefabs[i].type, this.numberPrefabs[i].prefab);
			}
		}
		this.numberArry = new NumberObj[5, 5];
		for (int j = 0; j < 5; j++)
		{
			for (int k = 0; k < 5; k++)
			{
				this.CreateNewNumber(j, k, Game.NumType.EMPTY);
			}
		}
		this.sybomList = new List<NumberObj>();
		for (int l = 0; l < 5; l++)
		{
			string n = "img_nums/item" + (l + 1).ToString();
			Vector2 vector = base.transform.Find(n).transform.position;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.numberPrefabDict[Game.NumType.SYBOM]);
			gameObject.transform.position = vector;
			gameObject.transform.SetParent(base.transform.Find("img_nums").transform);
			this.sybomList.Add(gameObject.GetComponent<NumberObj>());
			this.sybomList[l].Init((int)vector.x, (int)vector.y, this, Game.NumType.SYBOM);
			if (this.sybomList[l].CanSetNumber())
			{
				this.sybomList[l].NumberComponent.SetNumber((NumberControl.NmberDigit)UnityEngine.Random.Range(20, 23));
			}
			if (l == 0)
			{
				this.sybomList[l].transform.localScale *= 2f;
			}
			UnityEngine.Debug.Log(vector);
		}
		base.StartCoroutine(this.FillAll());
	}

	public Vector2 GetNumPos(float x, float y)
	{
		return new Vector2(x - 2f, y - 1f);
	}

	public NumberObj CreateNewNumber(int x, int y, Game.NumType type)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.numberPrefabDict[type], this.GetNumPos((float)x, (float)y), Quaternion.identity);
		gameObject.transform.SetParent(base.transform.Find("img_box").transform);
		this.numberArry[x, y] = gameObject.GetComponent<NumberObj>();
		this.numberArry[x, y].Init(x, y, this, type);
		if (this.numberArry[x, y].CanSetNumber())
		{
			this.numberArry[x, y].NumberComponent.SetNumber((NumberControl.NmberDigit)UnityEngine.Random.Range(0, this.RandMaxNum));
		}
		return this.numberArry[x, y];
	}

//	[IteratorStateMachine(typeof(Game._003CFillAll_003Ed__20))]
	public IEnumerator FillAll()
	{
		Game._FillAll_d__20 expr_06 = new Game._FillAll_d__20(0);
		expr_06.__4__this = this;
		return expr_06;
	}

	public bool Fill()
	{
		bool result = false;
		for (int i = 1; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				NumberObj numberObj = this.numberArry[j, i];
				if (numberObj.CanMove())
				{
					NumberObj numberObj2 = this.numberArry[j, i - 1];
					if (numberObj2.Type == Game.NumType.EMPTY)
					{
						numberObj.Move(j, i - 1, this.filleTime);
						this.numberArry[j, i - 1] = numberObj;
						this.numberArry[j, i] = numberObj2;
						result = true;
					}
				}
			}
		}
		for (int k = 0; k < 5; k++)
		{
			NumberObj numberObj3 = this.numberArry[k, 4];
			if (numberObj3.Type == Game.NumType.EMPTY)
			{
				UnityEngine.Object.Destroy(numberObj3.gameObject);
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.numberPrefabDict[Game.NumType.NORMAL], this.GetNumPos((float)k, 5f), Quaternion.identity);
				gameObject.transform.SetParent(base.transform.Find("img_box").transform);
				this.numberArry[k, 4] = gameObject.GetComponent<NumberObj>();
				this.numberArry[k, 4].Init(k, 5, this, Game.NumType.NORMAL);
				this.numberArry[k, 4].Move(k, 4, this.filleTime);
				this.numberArry[k, 4].NumberComponent.SetNumber((NumberControl.NmberDigit)UnityEngine.Random.Range(0, this.RandMaxNum));
				result = true;
			}
		}
		return result;
	}

	public void moveSybomNumber(NumberObj tempObj)
	{
		this.actionControl = true;
		if (this.sybomList.Count<NumberObj>() > 0)
		{
			this.movingValue = new Vector2(1.1f, -2.2f);
			Tweener t = DOTween.To(() => this.movingValue, delegate(Vector2 x1)
			{
				this.movingValue = x1;
			}, new Vector2(tempObj.transform.position.x, tempObj.transform.position.y), 0.3f);
			Sequence expr_77 = DOTween.Sequence();
			expr_77.Append(t);
			expr_77.OnComplete(new TweenCallback(this.OnComplete1));
		}
	}

	public void OnComplete1()
	{
		int num = this.sybomList[0].NumberComponent.GetNumber() + this.currentChooseObj.NumberComponent.GetNumber();
		UnityEngine.Debug.Log(num);
		if (num >= 20)
		{
			num = 20;
		}
		this.currentChooseObj.NumberComponent.SetNumber((NumberControl.NmberDigit)(num - 1));
		UnityEngine.Object.Destroy(this.sybomList[0].gameObject);
		this.actionControl = false;
		if (this.sybomList.Count != 1)
		{
			this.sybomList.RemoveAt(0);
			this.clearTimes = 0;
			for (int i = 0; i < this.sybomList.Count<NumberObj>(); i++)
			{
				if (i == 0)
				{
					this.sybomList[i].transform.position = new Vector2(1.1f, -2.2f);
					this.sybomList[i].transform.localScale *= 2f;
				}
				else
				{
					string n = "img_nums/item" + (i + 1).ToString();
					Vector2 v = base.transform.Find(n).transform.position;
					this.sybomList[i].transform.position = v;
				}
			}
			base.StartCoroutine(this.FillAll());
			return;
		}
		UnityEngine.Debug.Log("游戏失败！！！！");
	}

	public bool ClearNumber(int x, int y)
	{
		if (this.numberArry[x, y].CanClear() && !this.numberArry[x, y].ClearCompoent.IsClearing)
		{
			this.numberArry[x, y].ClearCompoent.Clear();
			this.numberArry[x, y] = this.CreateNewNumber(x, y, Game.NumType.EMPTY);
			return true;
		}
		return false;
	}

	private bool ClearALLMatch()
	{
		bool result = false;
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				if (this.numberArry[i, j].CanClear())
				{
					List<NumberObj> list = this.MatchNumber(this.numberArry[i, j], i, j);
					if (list != null)
					{
						for (int k = 0; k < list.Count; k++)
						{
							if (this.ClearNumber(list[k].X, list[k].Y))
							{
								result = true;
							}
						}
						this.clearTimes++;
						if (this.sybomList.Count<NumberObj>() < 5)
						{
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.numberPrefabDict[Game.NumType.SYBOM]);
							gameObject.transform.SetParent(base.transform.Find("img_nums").transform);
							this.sybomList.Add(gameObject.GetComponent<NumberObj>());
							gameObject.GetComponent<NumberObj>().NumberComponent.SetNumber((NumberControl.NmberDigit)UnityEngine.Random.Range(20, 23));
							for (int l = 0; l < this.sybomList.Count<NumberObj>(); l++)
							{
								if (l != 0)
								{
									string n = "img_nums/item" + (l + 1).ToString();
									Vector2 v = base.transform.Find(n).transform.position;
									this.sybomList[l].transform.position = v;
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	public void AddNumber(float x, float y)
	{
		if (this.sybomList.Count == 0)
		{
			UnityEngine.Debug.Log("游戏已经失败！！！");
			return;
		}
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				NumberObj numberObj = this.numberArry[i, j];
				if (numberObj.transform.position.x == x && numberObj.transform.position.y == y)
				{
					numberObj.transform.SetAsFirstSibling();
					this.currentChooseObj = numberObj;
					this.moveSybomNumber(numberObj);
					break;
				}
			}
		}
	}

	public List<NumberObj> MatchNumber(NumberObj number, int posX, int posY)
	{
		if (number.Type != Game.NumType.NORMAL && number.CanSetNumber())
		{
			return null;
		}
		NumberControl.NmberDigit number2 = number.NumberComponent.Number1;
		List<NumberObj> list = new List<NumberObj>();
		List<NumberObj> list2 = new List<NumberObj>();
		List<NumberObj> list3 = new List<NumberObj>();
		list.Add(number);
		for (int i = 0; i <= 1; i++)
		{
			for (int j = 1; j < 5; j++)
			{
				int num;
				if (i == 0)
				{
					num = posX - j;
				}
				else
				{
					num = posX + j;
				}
				if (num < 0 || num >= 5 || !this.numberArry[num, posY].CanSetNumber() || this.numberArry[num, posY].NumberComponent.Number1 != number2)
				{
					break;
				}
				list.Add(this.numberArry[num, posY]);
			}
		}
		if (list.Count >= 3)
		{
			for (int k = 0; k < list.Count; k++)
			{
				for (int l = 0; l <= 1; l++)
				{
					for (int m = 0; m < 5; m++)
					{
						int num2;
						if (l == 0)
						{
							num2 = posY - m;
						}
						else
						{
							num2 = posY + m;
						}
						if (num2 < 0 || num2 >= 5 || !this.numberArry[list[k].X, num2].CanSetNumber() || this.numberArry[list[k].X, num2].NumberComponent.Number1 != number2)
						{
							break;
						}
						list2.Add(this.numberArry[list[k].X, num2]);
					}
				}
				if (list2.Count<NumberObj>() >= 2)
				{
					for (int n = 0; n < list2.Count<NumberObj>(); n++)
					{
						list3.Add(list2[n]);
					}
					break;
				}
				list2.Clear();
			}
		}
		for (int num3 = 0; num3 < list.Count<NumberObj>(); num3++)
		{
			list3.Add(list[num3]);
		}
		list2.Add(number);
		for (int num4 = 0; num4 <= 1; num4++)
		{
			for (int num5 = 1; num5 < 5; num5++)
			{
				int num6;
				if (num4 == 0)
				{
					num6 = posY - num5;
				}
				else
				{
					num6 = posY + num5;
				}
				if (num6 < 0 || num6 >= 5 || !this.numberArry[posX, num6].CanSetNumber() || this.numberArry[posX, num6].NumberComponent.Number1 != number2)
				{
					break;
				}
				list2.Add(this.numberArry[posX, num6]);
			}
		}
		if (list2.Count<NumberObj>() >= 3)
		{
			for (int num7 = 0; num7 < list2.Count<NumberObj>(); num7++)
			{
				for (int num8 = 0; num8 <= 1; num8++)
				{
					for (int num9 = 1; num9 < 5; num9++)
					{
						int num10;
						if (num8 == 0)
						{
							num10 = posX - num9;
						}
						else
						{
							num10 = posX + num9;
						}
						if (num10 < 0 || num10 >= 5 || !this.numberArry[num10, list2[num7].Y].CanSetNumber() || this.numberArry[num10, list2[num7].Y].NumberComponent.Number1 != number2)
						{
							break;
						}
						list.Add(this.numberArry[num10, list2[num7].Y]);
					}
				}
				if (list.Count<NumberObj>() >= 2)
				{
					for (int num11 = 0; num11 < list.Count<NumberObj>(); num11++)
					{
						list3.Add(list[num11]);
					}
					break;
				}
				list.Clear();
			}
		}
		for (int num12 = 0; num12 < list2.Count<NumberObj>(); num12++)
		{
			list3.Add(list2[num12]);
		}
		if (list3.Count<NumberObj>() > 3)
		{
			return list3;
		}
		return null;
	}

	private void Update()
	{
		if (this.sybomList[0] && this.actionControl)
		{
			this.sybomList[0].transform.position = this.movingValue;
		}
	}
}
