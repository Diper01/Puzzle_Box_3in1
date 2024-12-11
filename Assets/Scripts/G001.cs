using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class G001 : MonoBehaviour
{
	private struct TransformControl
	{
		public Transform parent;

		public Transform self;

		public TransformControl(Transform parent, Transform self)
		{
			this.parent = parent;
			this.self = self;
		}
	}

	private struct PathRDM
	{
		public List<Node> paths;

		public int index;
	}

	private sealed class __c__DisplayClass31_0
	{
		public GameList obj;

		public G001 __4__this;

		internal void _OnClickReturn_b__0()
		{
			M001.GetInstance().Score = 0;
			GM.GetInstance().SaveScore(1, 0);
			GM.GetInstance().SetSavedGameID(0);
			GM.GetInstance().ResetToNewGame();
			GM.GetInstance().ResetConsumeCount();
			GlobalEventHandle.EmitDoGoHome();
			GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
			this.obj.HideTopBtn();
		}

		internal void _OnClickReturn_b__1()
		{
			GM.GetInstance().SaveScore(1, 0);
			GM.GetInstance().SetSavedGameID(0);
			GM.GetInstance().ResetToNewGame();
			GM.GetInstance().ResetConsumeCount();
			M001.GetInstance().Score = 0;
			M001.GetInstance().StartNewGame();
			this.__4__this.m_tips_time = 0f;
			this.__4__this.m_markTips = true;
		}

		internal void _OnClickReturn_b__2()
		{
			this.__4__this.m_markTips = true;
		}
	}

	private sealed class __c__DisplayClass37_0
	{
		public G001 __4__this;

		public G00101 block;

		internal void _OnClickBlock_b__0()
		{
			M001.GetInstance().IsPlaying = true;
			this.__4__this.ControlPropsPannel(true);
			this.__4__this.UseProps(this.block);
			this.__4__this.m_markTips = true;
		}
	}

	private sealed class __c__DisplayClass38_0
	{
		public int number;

		public G00101 toObj;

		public G001 __4__this;

		public GameObject obj;

		public TweenCallback __9__3;

		internal void _MoveBloodToMap_b__0()
		{
			if (this.number <= 0)
			{
				M001.GetInstance().FreeBlock(this.toObj.gameObject);
			}
			else
			{
				this.toObj.setNum(this.number);
			}
			this.__4__this.m_dobleTotal = 0;
			M001.GetInstance().IsPlaying = false;
			M001.GetInstance().Delete(this.toObj.Index);
			TaskData.GetInstance().Add(100102, 1, true);
			this.__4__this.GameOver();
		}

		internal void _MoveBloodToMap_b__1()
		{
			this.obj.GetComponent<G00105>().FadeOut(this.toObj.GetCurrentColor());
		}

		internal void _MoveBloodToMap_b__2()
		{
			UnityEngine.Object.Destroy(this.obj);
		}

		internal void _MoveBloodToMap_b__3()
		{
			this.__4__this.bloodList[0].GetComponent<G00105>().DisableDrag = false;
		}
	}

	private sealed class __c__DisplayClass41_0
	{
		public GameObject obj;

		internal void _OnEndDragLife_b__0()
		{
			this.obj.GetComponent<G00105>().DisableDrag = false;
		}

		internal void _OnEndDragLife_b__2()
		{
			this.obj.GetComponent<G00105>().DisableDrag = false;
		}

		internal void _OnEndDragLife_b__1()
		{
			this.obj.GetComponent<G00105>().DisableDrag = false;
		}
	}

	private sealed class __c__DisplayClass42_0
	{
		public G00101 block;

		public G001 __4__this;

		internal void _UseProps_b__0()
		{
			foreach (int current in M001.GetInstance().Use(this.block.Index))
			{
				if (current < this.__4__this.blocks.Count)
				{
					G00101 g = this.__4__this.blocks[current];
					if (!(g == null))
					{
						ParticlesControl.GetInstance().PlayExplodeEffic(g.transform.position, g.GetCurrentColor());
						M001.GetInstance().FreeBlock(g.gameObject);
					}
				}
			}
			Sequence expr_9C = DOTween.Sequence();
			expr_9C.AppendInterval(0.5f);
			TweenCallback arg_C7_1;
			if ((arg_C7_1 = G001.__c.__9__42_1) == null)
			{
				arg_C7_1 = (G001.__c.__9__42_1 = new TweenCallback(G001.__c.__9._UseProps_b__42_1));
			}
			expr_9C.AppendCallback(arg_C7_1);
		}
	}

	[Serializable]
	private sealed class __c
	{
		public static readonly G001.__c __9 = new G001.__c();

		public static TweenCallback __9__42_1;

		public static Comparison<G001.PathRDM> __9__44_0;

		internal void _UseProps_b__42_1()
		{
			M001.GetInstance().Down();
			M001.GetInstance().CurPropId = 0;
		}

		internal int _FindPath_b__44_0(G001.PathRDM a, G001.PathRDM b)
		{
			return -1 * a.paths.Count.CompareTo(b.paths.Count);
		}
	}

	private sealed class __c__DisplayClass43_0
	{
		public G001 __4__this;

		public int index;

		internal void _Delete_b__0()
		{
			this.__4__this.PlayDoubleAni();
			this.__4__this.blocks[this.index].setNum(M001.GetInstance().GetNumber(this.index));
			this.__4__this.AddNewLife(this.__4__this.blocks[this.index]);
			M001.GetInstance().Down();
		}
	}

	private sealed class __c__DisplayClass48_0
	{
		public Transform image;

		public Action callfunc;

		internal void _PlayUseProp_b__0()
		{
			this.image.gameObject.SetActive(false);
			Action expr_17 = this.callfunc;
			if (expr_17 == null)
			{
				return;
			}
			expr_17();
		}
	}

	private sealed class __c__DisplayClass50_0
	{
		public G001 __4__this;

		public int idx;

		internal void _UseHeart_b__0()
		{
			G001.__c__DisplayClass50_1 __c__DisplayClass50_ = new G001.__c__DisplayClass50_1();
			this.__4__this.blocks[this.idx].setNum(M001.GetInstance().GetNumber(this.idx));
			this.__4__this.m_img_heart.SetActive(false);
			__c__DisplayClass50_.obj = UnityEngine.Object.Instantiate<GameObject>(this.__4__this.blocks[this.idx].gameObject);
			G00101 expr_73 = __c__DisplayClass50_.obj.GetComponent<G00101>();
			expr_73.transform.DOScale(1.5f, 0.5f);
			expr_73.FadeOut().OnComplete(new TweenCallback(__c__DisplayClass50_._UseHeart_b__1));
			__c__DisplayClass50_.obj.transform.SetParent(this.__4__this.transform, false);
			__c__DisplayClass50_.obj.transform.position = this.__4__this.blocks[this.idx].transform.position;
			AudioManager.GetInstance().PlayEffect("sound_eff_click_1");
			this.__4__this.GameOver();
		}
	}

	private sealed class __c__DisplayClass50_1
	{
		public GameObject obj;

		internal void _UseHeart_b__1()
		{
			M001.GetInstance().HeartIndex = -1;
			M001.GetInstance().AutoDelete();
			UnityEngine.Object.Destroy(this.obj);
		}
	}

	private sealed class __c__DisplayClass51_0
	{
		public GameObject node;

		internal void _PlayNewNumber_b__0()
		{
			UnityEngine.Object.Destroy(this.node);
		}
	}

	public GameObject gameBox;

	private List<G00101> blocks = new List<G00101>();

	public GameObject bloodBox;

	[SerializeField]
	public List<GameObject> bloodPosList = new List<GameObject>();

	private List<GameObject> bloodList = new List<GameObject>
	{
		null,
		null,
		null,
		null,
		null
	};

	public GameObject m_pannel_props;

	private List<GameObject> m_tranformObjs = new List<GameObject>();

	private GameObject bgPs;

	public GameObject txt_score;

	public Image m_img_double;

	public Transform[] m_img_props;

	public GameObject m_line;

	public GameObject m_figner;

	public GameObject m_mask;

	public GameObject m_img_heart;

	private int m_dobleTotal;

	private List<G001.TransformControl> m_transformList = new List<G001.TransformControl>();

	private Vector3 m_fingerRect2 = new Vector3(100f, -70f);

	private float m_tips_time;

	private bool m_markTips = true;

	private int m_guideStatus;

	private void Start()
	{
		this.LoadUI();
		this.LoadBlock();
		this.InitEvent();
		this.runGuide();
	}

	private void Update()
	{
		if (M001.GetInstance().BloodList[1] == 0 && this.m_markTips)
		{
			this.m_tips_time += Time.deltaTime;
			if (this.m_tips_time >= 30f)
			{
				this.m_tips_time = 0f;
				this.m_markTips = false;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/G00106") as GameObject);
				G00106 expr_69 = gameObject.GetComponent<G00106>();
				expr_69.OnClickAdsHandle = delegate
				{
					this.OnClickProp(1);
					this.m_markTips = false;
				};
				expr_69.OnClickCloseHandle = delegate
				{
					this.m_markTips = true;
				};
				DialogManager.GetInstance().show(gameObject, false);
			}
		}
	}

	private void OnDestroy()
	{
		this.RemoveEvent();
	}

	private void InitEvent()
	{
		M001.GetInstance().DoRefreshHandle += new Action(this.RefreshMap);
		M001.GetInstance().DoDeleteHandle += new Action<List<int>, int>(this.Delete);
		M001.GetInstance().DoDropHandle += new Action<List<sDropData>, List<int>>(this.Drop);
		M001 expr_47 = M001.GetInstance();
		expr_47.DoClickBlock = (Action<G00101>)Delegate.Combine(expr_47.DoClickBlock, new Action<G00101>(this.OnClickBlock));
		M001 expr_6D = M001.GetInstance();
		expr_6D.DoFillLife = (Action)Delegate.Combine(expr_6D.DoFillLife, new Action(this.InitLife));
		M001.GetInstance().DoCompMaxNumber += new Action<int>(this.PlayNewNumber);
		M001.GetInstance().OnClickReturnHandle = new Action<GameList>(this.OnClickReturn);
		M001.GetInstance().OnRandomHeartHandle = new Action(this.OnRandomHeart);
		M001.GetInstance().OnUseHeartHandle = new Action<int>(this.UseHeart);
	}

	private void RemoveEvent()
	{
	}

	public void OnClickProp(int type)
	{
		this.m_markTips = true;
		if (M001.GetInstance().IsPlaying)
		{
			return;
		}
		if (M001.GetInstance().HeartIndex != -1)
		{
			return;
		}
		//AppsflyerUtils.TrackClickPro(type);
		int value = Constant.COMMON_CONFIG_PROP[type - 1];
		if (!GM.GetInstance().isFullGEM(value))
		{
			return;
		}
		if (M001.GetInstance().CurPropId != 0)
		{
			M001.GetInstance().CurPropId = 0;
			this.ControlPropsPannel(true);
			return;
		}
		M001.GetInstance().CurPropId = type;
		this.ControlPropsPannel(false);
	}

	public void StopAllParticle()
	{
	}

	public void BeginAllParticle()
	{
	}

	public void OnClickReturn(GameList obj)
	{
		if (M001.GetInstance().IsPlaying)
		{
			return;
		}
		this.m_markTips = false;
		Utils.ShowPause(M001.GetInstance().Score, delegate
		{
			M001.GetInstance().Score = 0;
			GM.GetInstance().SaveScore(1, 0);
			GM.GetInstance().SetSavedGameID(0);
			GM.GetInstance().ResetToNewGame();
			GM.GetInstance().ResetConsumeCount();
			GlobalEventHandle.EmitDoGoHome();
			GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
			obj.HideTopBtn();
		}, delegate
		{
			GM.GetInstance().SaveScore(1, 0);
			GM.GetInstance().SetSavedGameID(0);
			GM.GetInstance().ResetToNewGame();
			GM.GetInstance().ResetConsumeCount();
			M001.GetInstance().Score = 0;
			M001.GetInstance().StartNewGame();
			this.m_tips_time = 0f;
			this.m_markTips = true;
		}, delegate
		{
			this.m_markTips = true;
		});
	}

	private void LoadUI()
	{
		this.RefreshScore(false);
		this.InitLife();
		this.m_img_heart.SetActive(false);
	}

	private void RefreshScore(bool isAni = true)
	{
		if (isAni)
		{
			this.txt_score.GetComponent<OverlayNumber>().setNum(M001.GetInstance().Score);
			return;
		}
		this.txt_score.GetComponent<OverlayNumber>().Reset();
		this.txt_score.GetComponent<OverlayNumber>().setNum(M001.GetInstance().Score);
		this.txt_score.GetComponent<Text>().text = string.Format((M001.GetInstance().Score < 1000) ? "{0}" : "{0:0,00}", M001.GetInstance().Score);
	}

	private void LoadBlock()
	{
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int number = M001.GetInstance().GetNumber(i, j);
				int index = M001.GetInstance().GetIndex(i, j);
				G00101 item = M001.GetInstance().CreateBlock(number, index, this.gameBox);
				this.blocks.Add(item);
			}
		}
	}

	private void InitLife()
	{
		for (int i = 0; i < 5; i++)
		{
			int num = M001.GetInstance().BloodList[i];
			if (num != 0)
			{
				G00105 g = this.CreateNewLife(num, this.bloodBox, i);
				g.transform.localPosition = this.bloodPosList[i].transform.localPosition;
				g.transform.localScale = this.bloodPosList[i].transform.localScale;
				if (i == 0)
				{
					g.DisableDrag = false;
				}
			}
		}
		this.m_markTips = true;
	}

	private void RefreshMap()
	{
		foreach (G00101 current in this.blocks)
		{
			if (!(current == null))
			{
				M001.GetInstance().FreeBlock(current.gameObject);
			}
		}
		this.blocks.Clear();
		for (int i = 0; i < this.bloodList.Count; i++)
		{
			GameObject gameObject = this.bloodList[i];
			if (!(gameObject == null))
			{
				this.bloodList[i] = null;
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		this.LoadBlock();
		this.LoadUI();
	}

	private void OnClickBlock(G00101 block)
	{
		if (M001.GetInstance().CurPropId != 0)
		{
			this.m_dobleTotal = 0;
			ParticlesControl.GetInstance().StopChooseAllEffic();
			if (M001.GetInstance().CurPropId == 1)
			{
					M001.GetInstance().IsPlaying = true;
					this.ControlPropsPannel(true);
					this.UseProps(block);
					this.m_markTips = true;				
			}
			else
			{
				M001.GetInstance().IsPlaying = true;
				this.ControlPropsPannel(true);
				this.UseProps(block);
			}
		}
		else
		{
			this.MoveBloodToMap(block);
		}
		this.BeginAllParticle();
	}

	private void MoveBloodToMap(G00101 toObj)
	{
		if (this.m_guideStatus == 0)
		{
			this.m_guideStatus = 1;
			PlayerPrefs.SetInt("LocalData_guide_game0102", 1);
			DOTween.Kill(this.m_figner, false);
			this.ToMask(this.m_transformList, "", true, Vector3.zero);
			//AppsflyerUtils.TrackTutorialCompletion(1, 2);
		}
		int blood = M001.GetInstance().GetBlood();
		M001.GetInstance().AddNumber(toObj.Index, blood);
		int number = M001.GetInstance().GetNumber(toObj.Index);
		GameObject obj = this.bloodList[0];
		obj.transform.SetParent(base.transform);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(obj.transform.DOMove(toObj.transform.position, 0.2f, false));
		sequence.InsertCallback(0.2f, delegate
		{
			if (number <= 0)
			{
				M001.GetInstance().FreeBlock(toObj.gameObject);
			}
			else
			{
				toObj.setNum(number);
			}
			this.m_dobleTotal = 0;
			M001.GetInstance().IsPlaying = false;
			M001.GetInstance().Delete(toObj.Index);
			TaskData.GetInstance().Add(100102, 1, true);
			this.GameOver();
		});
		if (number > 0)
		{
			sequence.AppendCallback(delegate
			{
				obj.GetComponent<G00105>().FadeOut(toObj.GetCurrentColor());
			});
			sequence.Append(obj.transform.DOScale(1.5f, 0.5f));
		}
		sequence.OnComplete(delegate
		{
			UnityEngine.Object.Destroy(obj);
		});
		TweenCallback __9__3 = null;
		for (int i = 0; i < 5; i++)
		{
			if (i + 1 < 5)
			{
				this.bloodList[i] = this.bloodList[i + 1];
				if (this.bloodList[i + 1] != null)
				{
					this.bloodList[i + 1].transform.DOKill(false);
					this.bloodList[i + 1].transform.DOLocalMove(this.bloodPosList[i].transform.localPosition, 0.2f, false);
					if (i == 0)
					{
						Tweener arg_241_0 = this.bloodList[i + 1].transform.DOScale(this.bloodPosList[i].transform.localScale, 0.2f);
						TweenCallback arg_241_1;
						if ((arg_241_1 = __9__3) == null)
						{
							arg_241_1 = (__9__3 = delegate
							{
								this.bloodList[0].GetComponent<G00105>().DisableDrag = false;
							});
						}
						arg_241_0.OnComplete(arg_241_1);
					}
					else
					{
						this.bloodList[i + 1].transform.DOScale(this.bloodPosList[i].transform.localScale, 0.2f);
					}
				}
			}
			else
			{
				this.bloodList[i] = null;
			}
		}
		M001.GetInstance().IsPlaying = true;
	}

	private void onBegainDragLife(GameObject obj, PointerEventData eventData)
	{
		if (obj != this.bloodList[0])
		{
			return;
		}
		if (M001.GetInstance().IsPlaying)
		{
			return;
		}
		if (M001.GetInstance().HeartIndex != -1)
		{
			return;
		}
		obj.transform.DOKill(false);
		obj.transform.DOScale(1f, 0.1f);
	}

	private void OnDragLife(GameObject obj, PointerEventData eventData)
	{
		if (obj != this.bloodList[0])
		{
			return;
		}
		if (M001.GetInstance().IsPlaying)
		{
			return;
		}
		if (M001.GetInstance().HeartIndex != -1)
		{
			return;
		}
		this.m_markTips = false;
		obj.transform.DOKill(false);
		obj.transform.DOScale(1f, 0.1f);
		if (this.m_guideStatus != 0 && obj.transform.parent != base.transform)
		{
			obj.transform.SetParent(base.transform);
		}
		Vector2 a;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out a);
		obj.transform.localPosition = a + new Vector2(0f, 100f);
	}

	private void OnEndDragLife(GameObject obj, PointerEventData eventData)
	{
		if (obj != this.bloodList[0])
		{
			return;
		}
		if (M001.GetInstance().IsPlaying)
		{
			return;
		}
		if (M001.GetInstance().HeartIndex != -1)
		{
			return;
		}
		this.m_markTips = true;
		if (!eventData.dragging)
		{
			obj.GetComponent<G00105>().DisableDrag = true;
			obj.transform.DOKill(false);
			obj.transform.DOScale(this.bloodPosList[0].transform.localScale, 0.1f).OnComplete(delegate
			{
				obj.GetComponent<G00105>().DisableDrag = false;
			});
			return;
		}
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, obj.transform.position);
		if (!RectTransformUtility.RectangleContainsScreenPoint(this.gameBox.GetComponent<RectTransform>(), screenPoint, eventData.pressEventCamera))
		{
			obj.GetComponent<G00105>().DisableDrag = true;
			obj.transform.DOKill(false);
			obj.transform.DOLocalMove(base.transform.InverseTransformPoint(this.bloodPosList[0].transform.position), 0.1f, false);
			obj.transform.DOScale(this.bloodPosList[0].transform.localScale, 0.1f).OnComplete(delegate
			{
				obj.GetComponent<G00105>().DisableDrag = false;
			});
			return;
		}
		Vector2 vector = this.gameBox.GetComponent<RectTransform>().InverseTransformPoint(obj.transform.position);
		Vector2 expr_120 = this.gameBox.GetComponent<RectTransform>().sizeDelta;
		float num = expr_120.x / 2f + vector.x;
		double arg_15B_0 = (double)(expr_120.y / 2f + vector.y);
		int num2 = (int)Math.Floor((double)(num / 120f));
		int num3 = (int)Math.Floor(arg_15B_0 / (double)120f);
		if (num2 < 0 || num2 >= 5)
		{
			return;
		}
		if (num3 < 0 || num3 >= 5)
		{
			return;
		}
		int index = M001.GetInstance().GetIndex(num3, num2);
		G00101 component = this.blocks[index].GetComponent<G00101>();
		if (this.m_guideStatus == 0)
		{
			if (index == 12)
			{
				this.MoveBloodToMap(component);
			}
			else
			{
				obj.GetComponent<G00105>().DisableDrag = true;
				obj.transform.DOKill(false);
				obj.transform.DOLocalMove(base.transform.InverseTransformPoint(this.bloodPosList[0].transform.position), 0.1f, false);
				obj.transform.DOScale(this.bloodPosList[0].transform.localScale, 0.1f).OnComplete(delegate
				{
					obj.GetComponent<G00105>().DisableDrag = false;
				});
			}
		}
		else
		{
			this.MoveBloodToMap(component);
		}
		AudioManager.GetInstance().PlayEffect("sound_eff_click_1");
	}

	private void UseProps(G00101 block)
	{
		this.PlayUseProp(block, M001.GetInstance().CurPropId, delegate
		{
			foreach (int current in M001.GetInstance().Use(block.Index))
			{
				if (current < this.blocks.Count)
				{
					G00101 g = this.blocks[current];
					if (!(g == null))
					{
						ParticlesControl.GetInstance().PlayExplodeEffic(g.transform.position, g.GetCurrentColor());
						M001.GetInstance().FreeBlock(g.gameObject);
					}
				}
			}
			Sequence expr_9C = DOTween.Sequence();
			expr_9C.AppendInterval(0.5f);
			TweenCallback arg_C7_1;
			if ((arg_C7_1 = G001.__c.__9__42_1) == null)
			{
				arg_C7_1 = (G001.__c.__9__42_1 = new TweenCallback(G001.__c.__9._UseProps_b__42_1));
			}
			expr_9C.AppendCallback(arg_C7_1);
		});
		//AppsflyerUtils.TrackUsePro(M001.GetInstance().CurPropId);
	}

	private void Delete(List<int> list, int index)
	{
		AudioManager.GetInstance().PlayEffect("sound_eff_clear_1");
		List<G001.PathRDM> list2 = this.FindPath(list, index);
		if (list2.Count < 0)
		{
			return;
		}
		this.m_dobleTotal++;
		int count = list2[0].paths.Count;
		Sequence sequence = DOTween.Sequence();
		foreach (G001.PathRDM current in list2)
		{
			G00101 g = this.blocks[current.index];
			g.ShowScore();
			this.blocks[current.index] = null;
			int row = M001.GetInstance().GetRow(current.index);
			int col = M001.GetInstance().GetCol(current.index);
			if (current.paths.Count >= 2)
			{
				Vector2 b = new Vector2((float)current.paths[0].x, (float)current.paths[0].y);
				Vector2 vector = new Vector2((float)current.paths[1].x, (float)current.paths[1].y) - b;
				int index2 = M001.GetInstance().GetIndex(row + (int)vector.y, col + (int)vector.x);
				Tween t = g.DelayMove(index2, (float)(count - current.paths.Count) * 0.1f);
				sequence.Insert(0f, t);
			}
		}
		sequence.AppendInterval(0.1f);
		sequence.OnComplete(delegate
		{
			this.PlayDoubleAni();
			this.blocks[index].setNum(M001.GetInstance().GetNumber(index));
			this.AddNewLife(this.blocks[index]);
			M001.GetInstance().Down();
		});
		M001.GetInstance().IsPlaying = true;
		this.RefreshScore(true);
	}

	private List<G001.PathRDM> FindPath(List<int> list, int index)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 5;
		int num4 = 5;
		foreach (int current in list)
		{
			int row = M001.GetInstance().GetRow(current);
			int col = M001.GetInstance().GetCol(current);
			if (row > num)
			{
				num = row;
			}
			if (row < num3)
			{
				num3 = row;
			}
			if (col > num2)
			{
				num2 = col;
			}
			if (col < num4)
			{
				num4 = col;
			}
		}
		int row2 = Math.Abs(num - num3) + 1;
        Assets.Scripts.Utils.Grid grid = new Assets.Scripts.Utils.Grid(Math.Abs(num2 - num4) + 1, row2);
		List<sGridRMD> list2 = new List<sGridRMD>();
		int py = 0;
		int px = 0;
		int i = num3;
		int num5 = 0;
		while (i < num + 1)
		{
			int j = num4;
			int num6 = 0;
			while (j < num2 + 1)
			{
				int index2 = M001.GetInstance().GetIndex(i, j);
				grid.getNode(num6, num5).isWalk = list.Contains(index2);
				if (index2 == index)
				{
					py = num5;
					px = num6;
				}
				else if (list.Contains(index2))
				{
					list2.Add(new sGridRMD(i, j, num5, num6));
				}
				j++;
				num6++;
			}
			i++;
			num5++;
		}
		AStar aStar = new AStar(grid, DiagonalMovement.NEVER, HeuristicType.MANHATTAN);
		List<G001.PathRDM> list3 = new List<G001.PathRDM>();
		foreach (sGridRMD current2 in list2)
		{
			int index3 = M001.GetInstance().GetIndex(current2.gameRow, current2.gameCol);
			if (index3 != index)
			{
				M001.GetInstance().GetRow(index3);
				M001.GetInstance().GetCol(index3);
				list3.Add(new G001.PathRDM
				{
					paths = aStar.Find(new AVec2(current2.gridCol, current2.gridRow), new AVec2(px, py)),
					index = index3
				});
			}
		}
		List<G001.PathRDM> arg_20D_0 = list3;
		Comparison<G001.PathRDM> arg_20D_1;
		if ((arg_20D_1 = G001.__c.__9__44_0) == null)
		{
			arg_20D_1 = (G001.__c.__9__44_0 = new Comparison<G001.PathRDM>(G001.__c.__9._FindPath_b__44_0));
		}
		arg_20D_0.Sort(arg_20D_1);
		return list3;
	}

	private void AddNewLife(G00101 block)
	{
		int num = M001.GetInstance().AddLife();
		if (num == -1)
		{
			return;
		}
		int number = M001.GetInstance().BloodList[num];
		G00105 expr_2F = this.CreateNewLife(number, this.bloodBox, num);
		expr_2F.transform.position = this.bloodPosList[num].transform.position;
		expr_2F.transform.localScale = new Vector3(1f, 1f, 1f);
		expr_2F.transform.DOScale(this.bloodPosList[num].transform.localScale, 0.3f);
	}

	private void Drop(List<sDropData> dropList, List<int> newList)
	{
		Sequence sequence = DOTween.Sequence();
		foreach (sDropData current in dropList)
		{
			G00101 g = this.blocks[current.srcIdx];
			this.blocks[current.srcIdx] = null;
			this.blocks[current.dstIdx] = g;
			g.Index = current.dstIdx;
			Tween t = g.Move(current.dstIdx);
			sequence.Insert(0f, t);
		}
		for (int i = 0; i < 5; i++)
		{
			int num = 0;
			foreach (int current2 in newList)
			{
				int col = M001.GetInstance().GetCol(current2);
				if (i == col)
				{
					M001.GetInstance().GetRow(current2);
					int number = M001.GetInstance().GetNumber(current2);
					G00101 g2 = M001.GetInstance().CreateBlock(number, current2, this.gameBox);
					g2.SetPosition(5 + num, i);
					this.blocks[current2] = g2;
					Tween t2 = g2.Move(current2);
					sequence.Insert(0f, t2);
					num++;
				}
			}
		}
		sequence.AppendInterval(0.2f);
		sequence.OnComplete(delegate
		{
			M001.GetInstance().IsPlaying = false;
			M001.GetInstance().AutoDelete();
			this.GameOver();
		});
		M001.GetInstance().IsPlaying = true;
	}

	private void PlayDoubleAni()
	{
		if (this.m_dobleTotal < 2)
		{
			return;
		}
		this.m_img_double.transform.Find("txt").GetComponent<Text>().text = string.Format("{0}", this.m_dobleTotal);
		DOTween.Kill(this.m_img_double, false);
		Sequence arg_92_0 = DOTween.Sequence();
		this.m_img_double.gameObject.SetActive(true);
		this.m_img_double.DOKill(false);
		this.m_img_double.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		arg_92_0.Append(this.m_img_double.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
		arg_92_0.AppendInterval(0.5f);
		arg_92_0.OnComplete(delegate
		{
			this.m_img_double.gameObject.SetActive(false);
		});
		UnityEngine.Debug.Log("Double:" + this.m_dobleTotal);
	}

	private void PlayUseProp(G00101 toBlock, int propID, Action callfunc)
	{
		if (propID > this.m_img_props.Length)
		{
			return;
		}
		Transform image = this.m_img_props[propID - 1];
		Vector3 vector = base.GetComponent<RectTransform>().InverseTransformPoint(toBlock.transform.position);
		Vector3 vector2 = new Vector3(vector.x, 700f, 0f);
		image.localPosition = vector2;
		image.gameObject.SetActive(true);
		DOTween.Kill(image, false);
		float num = 800f;
		Sequence sequence = DOTween.Sequence();
		switch (propID)
		{
		case 1:
		{
			float num2 = Math.Abs(vector.y - vector2.y) / num;
			sequence.Append(image.transform.DOLocalMove(vector, num2, false).SetEase(Ease.OutBounce));
			break;
		}
		case 2:
		{
			vector = new Vector3(vector.x + 80f, vector.y, vector.z);
			float num2 = Math.Abs(vector.y - vector2.y) / num;
			image.transform.localRotation = default(Quaternion);
			sequence.Append(image.transform.DOLocalMove(vector, num2, false).SetEase(Ease.OutBounce));
			sequence.AppendInterval(0.2f);
			sequence.Append(image.transform.DOLocalRotate(new Vector3(0f, 0f, -50f), 0.2f, RotateMode.LocalAxisAdd));
			sequence.Append(image.transform.DOLocalRotate(new Vector3(0f, 0f, 90f), 0.1f, RotateMode.LocalAxisAdd));
			break;
		}
		case 3:
		{
			vector2 = new Vector3(0f, vector2.y, vector2.y);
			vector = this.gameBox.transform.localPosition;
			float num2 = Math.Abs(vector.y - vector2.y) / num;
			image.transform.localScale = Vector3.one;
			image.GetComponent<Image>().color = Color.white;
			sequence.Append(image.transform.DOLocalMove(vector, num2, false).SetEase(Ease.OutBounce));
			sequence.AppendInterval(0.2f);
			sequence.Append(image.transform.DOScale(15f, 0.5f));
			sequence.Insert(num2 + 0.2f, image.GetComponent<Image>().DOFade(0f, 0.5f));
			break;
		}
		}
		sequence.OnComplete(delegate
		{
			image.gameObject.SetActive(false);
			Action expr_17 = callfunc;
			if (expr_17 == null)
			{
				return;
			}
			expr_17();
		});
	}

	private void OnRandomHeart()
	{
		DOTween.Kill(this.m_img_heart, false);
		this.m_img_heart.transform.DOKill(false);
		this.m_img_heart.SetActive(true);
		this.m_img_heart.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		Sequence expr_54 = DOTween.Sequence();
		expr_54.Append(this.m_img_heart.transform.DOScale(1.1f, 0.5f));
		expr_54.Append(this.m_img_heart.transform.DOScale(1f, 0.3f));
		expr_54.OnComplete(delegate
		{
			Sequence expr_05 = DOTween.Sequence();
			expr_05.Append(this.m_img_heart.transform.DOScale(1.1f, 0.5f));
			expr_05.Append(this.m_img_heart.transform.DOScale(1f, 0.5f));
			expr_05.SetLoops(-1);
			expr_05.SetTarget(this.m_img_heart);
		});
		expr_54.SetTarget(this.m_img_heart);
		int num = M001.GetInstance().HeartIndex / 5;
		int num2 = M001.GetInstance().HeartIndex % 5;
		Vector3 position = new Vector3((float)(num2 * 120 + 60 - 300), (float)(num * 120 + 60 - 300), 0f);
		this.m_img_heart.transform.position = this.gameBox.transform.TransformPoint(position);
	}

	public void UseHeart(int idx)
	{
		DOTween.Kill(this.m_img_heart, false);
		int num = idx / 5;
		int num2 = idx % 5;
		Vector3 position = new Vector3((float)(num2 * 120 + 60 - 300), (float)(num * 120 + 60 - 300), 0f);
		Vector3 endValue = this.gameBox.transform.TransformPoint(position);
		this.m_img_heart.transform.localScale = new Vector3(1f, 1f, 1f);
		this.m_img_heart.transform.DOJump(endValue, 0.1f, 1, 0.5f, false).OnComplete(delegate
		{
			this.blocks[idx].setNum(M001.GetInstance().GetNumber(idx));
			this.m_img_heart.SetActive(false);
			GameObject obj = UnityEngine.Object.Instantiate<GameObject>(this.blocks[idx].gameObject);
			G00101 expr_73 = obj.GetComponent<G00101>();
			expr_73.transform.DOScale(1.5f, 0.5f);
			expr_73.FadeOut().OnComplete(delegate
			{
				M001.GetInstance().HeartIndex = -1;
				M001.GetInstance().AutoDelete();
				UnityEngine.Object.Destroy(obj);
			});
			obj.transform.SetParent(this.transform, false);
			obj.transform.position = this.blocks[idx].transform.position;
			AudioManager.GetInstance().PlayEffect("sound_eff_click_1");
			this.GameOver();
		});
	}

	private void PlayNewNumber(int number)
	{
		GameObject original = Resources.Load("Prefabs/G00104") as GameObject;
		GameObject node = UnityEngine.Object.Instantiate<GameObject>(original, base.transform, false);
		node.transform.Find("prefab_max").GetComponent<G00101>().setNum(number);
		node.transform.localScale = new Vector3(1f, 0f, 0f);
		Sequence expr_72 = DOTween.Sequence();
		expr_72.Append(node.transform.DOScaleY(1f, 0.1f));
        expr_72.AppendInterval(1f);
		expr_72.OnComplete(delegate
		{
			UnityEngine.Object.Destroy(node);
		});
		AudioManager.GetInstance().PlayEffect("sound_eff_newNum");
	}

	private void GameOver()
	{
		if (M001.GetInstance().IsPlaying)
		{
			return;
		}
		if (M001.GetInstance().HeartIndex != -1)
		{
			return;
		}
		if (!M001.GetInstance().IsGameOver())
		{
			return;
		}
		if (M001.GetInstance().FinishCount < 1000)
		{
			M001.GetInstance().FinishCount++;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/finish") as GameObject);
			gameObject.GetComponent<Finish>().Load(1, M001.GetInstance().GetMapMaxNumber());
			DialogManager.GetInstance().show(gameObject, true);
		}
		else
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/G00102") as GameObject);
			gameObject2.GetComponent<G00102>().Load(M001.GetInstance().Score, M001.GetInstance().MaxScore);
			DialogManager.GetInstance().show(gameObject2, true);
		}
		this.m_markTips = false;
	}

	private void ControlPropsPannel(bool isDel)
	{
		if (isDel)
		{
			for (int i = 0; i < this.m_tranformObjs.Count; i++)
			{
				if (i < this.m_tranformObjs.Count - 1)
				{
					this.m_tranformObjs[i].transform.SetParent(this.gameBox.transform, true);
				}
				else
				{
					this.m_tranformObjs[i].transform.SetParent(this.m_pannel_props.transform, true);
					this.m_tranformObjs[i].transform.Find("img01").gameObject.SetActive(false);
				}
			}
			this.m_tranformObjs.Clear();
			GlobalEventHandle.EmitDoUseProps(true, this.m_tranformObjs);
			return;
		}
		foreach (G00101 current in this.blocks)
		{
			this.m_tranformObjs.Add(current.gameObject);
		}
		GameObject gameObject = base.transform.Find(string.Format("img_pros/item{0}", M001.GetInstance().CurPropId)).gameObject;
		gameObject.transform.Find("img01").gameObject.SetActive(true);
		this.m_tranformObjs.Add(gameObject);
		GlobalEventHandle.EmitDoUseProps(false, this.m_tranformObjs);
	}

	private G00105 CreateNewLife(int number, GameObject parent, int idx)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/G00105") as GameObject);
		gameObject.transform.SetParent(parent.transform, false);
		G00105 expr_2D = gameObject.GetComponent<G00105>();
		expr_2D.Init(number, idx);
		expr_2D.OnDownHandle = new Action<GameObject, PointerEventData>(this.onBegainDragLife);
		expr_2D.OnDragHandle = new Action<GameObject, PointerEventData>(this.OnDragLife);
		expr_2D.OnUpHandle = new Action<GameObject, PointerEventData>(this.OnEndDragLife);
		this.bloodList[idx] = gameObject.gameObject;
		return expr_2D;
	}

	private void runGuide()
	{
		this.m_guideStatus = PlayerPrefs.GetInt("LocalData_guide_game0102", 0);
		if (this.m_guideStatus != 0)
		{
			this.m_mask.SetActive(false);
			return;
		}
		this.m_transformList.Add(new G001.TransformControl(this.gameBox.transform, this.blocks[11].transform));
		this.m_transformList.Add(new G001.TransformControl(this.gameBox.transform, this.blocks[12].transform));
		this.m_transformList.Add(new G001.TransformControl(this.gameBox.transform, this.blocks[13].transform));
		this.m_transformList.Add(new G001.TransformControl(this.bloodBox.transform, this.bloodList[0].transform));
		this.ToMask(this.m_transformList, "", false, Vector3.zero);
		DOTween.Kill(this.m_figner, false);
		Sequence expr_102 = DOTween.Sequence();
		expr_102.AppendCallback(delegate
		{
			this.m_figner.transform.localPosition = this.m_mask.transform.InverseTransformPoint(this.blocks[12].transform.position) + this.m_fingerRect2;
			this.m_figner.gameObject.SetActive(true);
		});
		expr_102.Append(this.m_figner.transform.DOBlendableLocalMoveBy(new Vector3(0f, -10f, 0f), 0.5f, false));
		expr_102.Append(this.m_figner.transform.DOBlendableLocalMoveBy(new Vector3(0f, 10f, 0f), 0.5f, false));
		expr_102.SetLoops(-1);
		expr_102.SetTarget(this.m_figner);
		Canvas expr_19C = this.bloodList[0].AddComponent<Canvas>();
		expr_19C.overrideSorting = true;
		expr_19C.sortingOrder = 13;
		this.bloodList[0].AddComponent<GraphicRaycaster>().enabled = true;
	}

	private void ToMask(List<G001.TransformControl> list, string txt, bool isOut, Vector3 tipsPos)
	{
		this.m_mask.SetActive(!isOut);
		if (isOut)
		{
			foreach (G001.TransformControl current in list)
			{
				current.self.SetParent(current.parent, true);
			}
			this.m_transformList.Clear();
			return;
		}
		using (List<G001.TransformControl>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				enumerator.Current.self.SetParent(this.m_mask.transform, true);
			}
		}
	}
}
