using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class G00103 : MonoBehaviour
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

	private sealed class __c__DisplayClass41_0
	{
		public G00101 toObj;

		public int number;

		public G00103 __4__this;

		public GameObject obj;

		internal void _MoveBloodToMap_b__0()
		{
			this.toObj.setNum(this.number);
			switch (this.__4__this.m_step)
			{
			case 5:
			case 7:
			case 9:
			case 11:
				this.__4__this.Delete();
				break;
			case 6:
			case 8:
			case 10:
				break;
			default:
				return;
			}
		}

		internal void _MoveBloodToMap_b__1()
		{
			this.obj.GetComponent<G00105>().FadeOut(this.toObj.GetCurrentColor());
		}

		internal void _MoveBloodToMap_b__2()
		{
			UnityEngine.Object.Destroy(this.obj);
			int step = this.__4__this.m_step;
			if (step == 2)
			{
				this.__4__this.Drop();
				return;
			}
			if (step != 4)
			{
				return;
			}
			this.__4__this.SetTipsText("continue,_compose_three_same_numbers_to_break_a_whole_line.");
			this.__4__this.PlayFingerMoveAni(2);
			this.__4__this.m_isPuase = false;
		}
	}

	private sealed class __c__DisplayClass42_0
	{
		public G00101 block;

		internal void _Delete_b__2()
		{
			UnityEngine.Object.Destroy(this.block.gameObject);
		}
	}

	private sealed class __c__DisplayClass42_1
	{
		public G00101 block;

		internal void _Delete_b__3()
		{
			UnityEngine.Object.Destroy(this.block.gameObject);
		}
	}

	private sealed class __c__DisplayClass56_0
	{
		public GameObject obj;

		public G00103 __4__this;

		public int idx;

		internal void _BackToInitPosition_b__0()
		{
			this.obj.GetComponent<G00105>().DisableDrag = false;
			this.__4__this.PlayFingerMoveAni(this.idx);
		}
	}

	public const int CONSTANT_MAP_ROW = 5;

	public const int CONSTANT_MAP_COL = 5;

	public const int CONSTANT_CELL_WIDTH = 120;

	public const int CONSTANT_CELL_HEIGHT = 120;

	public GameObject m_panel_top;

	public GameObject m_btn_return;

	public GameObject m_btn_skip;

	public Text m_tips01;

	public Image m_img_finger;

	public string[] m_tipTxt;

	public GameObject gameBox;

	public GameObject bloodBox;

	public GameObject m_mask;

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

	private List<G00101> m_blocks = new List<G00101>();

	private int m_step;

	private int[] m_lifes = new int[]
	{
		1,
		1,
		1,
		2,
		-1
	};

	private int[][] m_nextMap;

	private int[] m_maps;

	private bool m_isPuase;

	private Vector3 m_saveFingerPos;

	private List<G00103.TransformControl> m_transformList;

	private Vector3 m_fingerRect;

	private Vector3 m_fingerRect2;

	private void Start()
	{
		this.LoadUI();
	}

	private void Update()
	{
		Utils.BackListener(base.gameObject, delegate
		{
			this.OnClickReturn();
		});
	}

	private void OnDestroy()
	{
		DOTween.Kill(this.m_img_finger, false);
	}

	private void InitEvent()
	{
	}

	private void RemoveEvent()
	{
	}

	public void OnClickSkip()
	{
		this.StartGame();
	}

	public void StartGame()
	{
		PlayerPrefs.SetInt("LocalData_guide_game01", 1);
		GlobalEventHandle.EmitClickPageButtonHandle("Game01", 0);
		UnityEngine.Object.Destroy(base.gameObject);
//		AppsflyerUtils.TrackTutorialCompletion(1, 1);
	}

	public void OnTouchScreen()
	{
		if (this.m_step != 0)
		{
			return;
		}
		this.m_step++;
		this.PlayFingerMoveAni(1);
		this.bloodList[0].GetComponent<G00105>().DisableDrag = false;
	}

	public void OnClickReturn()
	{
		UnityEngine.Object.Destroy(base.gameObject);
		GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
	}

	private void LoadUI()
	{
		this.LoadData();
		this.InitMap();
		this.InitLife();
		this.InPageAni();
	}

	private void LoadData()
	{
		this.m_saveFingerPos = this.m_img_finger.transform.localPosition;
		this.m_maps = this.m_nextMap[this.m_step];
	}

	private void InitMap()
	{
		for (int i = 0; i < this.m_maps.Length; i++)
		{
			int num = this.m_maps[i];
			if (num == 0)
			{
				this.m_blocks.Add(null);
			}
			else
			{
				G00101 g = this.CreateBlock(num, i, this.gameBox);
				this.SetPosition(g, i);
				this.m_blocks.Add(g);
				if (i != 1)
				{
					g.RemoveClick();
				}
			}
		}
	}

	private void InitLife()
	{
		for (int i = 0; i < this.m_lifes.Length; i++)
		{
			int number = this.m_lifes[i];
			G00105 g = this.CreateNewLife(number, this.bloodBox, i);
			g.transform.localPosition = this.bloodPosList[i].transform.localPosition;
			g.transform.localScale = this.bloodPosList[i].transform.localScale;
			this.bloodList[i] = g.gameObject;
		}
	}

	private void InPageAni()
	{
		this.SetTipsText("this_is_the_life_bar,_drag_the_square_on_the_right_to_move_it.");
		this.m_img_finger.gameObject.SetActive(false);
		Vector3 localPosition = this.gameBox.transform.localPosition;
		Vector3 localPosition2 = this.bloodBox.transform.localPosition;
		this.gameBox.transform.localPosition = new Vector3(localPosition.x - 500f, localPosition.y, localPosition.z);
		this.bloodBox.transform.localPosition = new Vector3(localPosition2.x + 500f, localPosition2.y, localPosition2.z);
		this.gameBox.transform.DOLocalMove(localPosition, 1f, false).SetEase(Ease.OutBack);
		this.bloodBox.transform.DOLocalMove(localPosition2, 1f, false).SetEase(Ease.OutBack).OnComplete(delegate
		{
			this.OnTouchScreen();
			this.m_isPuase = false;
		});
	}

	public void OnClick()
	{
		if (this.m_isPuase)
		{
			return;
		}
		this.m_step++;
		this.m_isPuase = true;
		this.StopFingerAni();
		switch (this.m_step)
		{
		case 2:
			this.MoveBloodToMap(this.m_blocks[1], 1);
			break;
		case 4:
			this.MoveBloodToMap(this.m_blocks[0], 0);
			break;
		case 5:
			this.MoveBloodToMap(this.m_blocks[2], 2);
			break;
		case 7:
			this.MoveBloodToMap(this.m_blocks[1], 1);
			break;
		case 9:
			this.MoveBloodToMap(this.m_blocks[1], 1);
			break;
		case 11:
			this.MoveBloodToMap(this.m_blocks[1], 1);
			break;
		}
		AudioManager.GetInstance().PlayEffect("sound_eff_click_1");
	}

	private void MoveBloodToMap(G00101 toObj, int idx)
	{
		int blood = this.GetBlood();
		this.m_maps[idx] += blood;
		int number = this.m_maps[idx];
		GameObject obj = this.bloodList[0];
		obj.transform.SetParent(base.transform);
		Sequence expr_67 = DOTween.Sequence();
		expr_67.Append(obj.transform.DOMove(toObj.transform.position, 0.2f, false));
		expr_67.InsertCallback(0.2f, delegate
		{
			toObj.setNum(number);
			switch (this.m_step)
			{
			case 5:
			case 7:
			case 9:
			case 11:
				this.Delete();
				break;
			case 6:
			case 8:
			case 10:
				break;
			default:
				return;
			}
		});
		expr_67.AppendCallback(delegate
		{
			obj.GetComponent<G00105>().FadeOut(toObj.GetCurrentColor());
		});
		expr_67.Append(obj.transform.DOScale(1.5f, 0.5f));
		expr_67.OnComplete(delegate
		{
			UnityEngine.Object.Destroy(obj);
			int step = this.m_step;
			if (step == 2)
			{
				this.Drop();
				return;
			}
			if (step != 4)
			{
				return;
			}
			this.SetTipsText("continue,_compose_three_same_numbers_to_break_a_whole_line.");
			this.PlayFingerMoveAni(2);
			this.m_isPuase = false;
		});
		for (int i = 0; i < 5; i++)
		{
			if (i + 1 < 5)
			{
				this.bloodList[i] = this.bloodList[i + 1];
				if (this.bloodList[i + 1] != null)
				{
					this.bloodList[i + 1].transform.DOKill(false);
					this.bloodList[i + 1].transform.DOLocalMove(this.bloodPosList[i].transform.localPosition, 0.2f, false);
					this.bloodList[i + 1].transform.DOScale(this.bloodPosList[i].transform.localScale, 0.2f);
					this.bloodList[i + 1].GetComponent<G00105>().DisableDrag = false;
				}
			}
			else
			{
				this.bloodList[i] = null;
			}
		}
	}

	private void Delete()
	{
		Sequence sequence = DOTween.Sequence();
		switch (this.m_step)
		{
		case 5:
			for (int i = 0; i < this.m_maps.Length; i++)
			{
				if (i != 2)
				{
					G00101 block = this.m_blocks[i];
					sequence.Insert((float)i * 0.3f, block.transform.DOLocalMove(this.GetToPosition(i + 1), 0.3f, false).OnComplete(delegate
					{
						UnityEngine.Object.Destroy(block.gameObject);
					}));
				}
			}
			sequence.OnComplete(delegate
			{
				this.m_maps[2] = this.m_maps[2] + 1;
				this.m_blocks[2].setNum(this.m_maps[2]);
				this.AddNewLife();
				this.Drop();
			});
			return;
		case 6:
		case 8:
		case 10:
			break;
		case 7:
		case 9:
		case 11:
			for (int j = 0; j < this.m_maps.Length; j++)
			{
				if (j != 1)
				{
					G00101 block = this.m_blocks[j];
					sequence.Insert(0f, block.transform.DOLocalMove(this.GetToPosition(1), 0.3f, false).OnComplete(delegate
					{
						UnityEngine.Object.Destroy(block.gameObject);
					}));
				}
			}
			sequence.OnComplete(delegate
			{
				this.m_maps[1] = this.m_maps[1] + 1;
				this.m_blocks[1].setNum(this.m_maps[1]);
				this.AddNewLife();
				this.Drop();
			});
			break;
		default:
			return;
		}
	}

	private void Drop()
	{
		this.m_step++;
		Dictionary<int, int> dictionary = new Dictionary<int, int>
		{
			{
				3,
				1
			},
			{
				6,
				2
			},
			{
				8,
				3
			},
			{
				10,
				4
			},
			{
				12,
				5
			}
		};
		int[] array = this.m_nextMap[dictionary[this.m_step]];
		Sequence sequence = DOTween.Sequence();
		for (int i = 0; i < array.Length; i++)
		{
			int num = array[i];
			if (num != 0)
			{
				G00101 g = this.CreateBlock(num, i, this.gameBox);
				this.SetPosition(g, 1, i);
				this.m_blocks[i] = g;
				sequence.Insert(0f, g.transform.DOLocalMove(this.GetToPosition(i), 0.3f, false));
				this.m_maps[i] = num;
			}
		}
		sequence.OnComplete(delegate
		{
			int step = this.m_step;
			if (step != 3)
			{
				switch (step)
				{
				case 6:
				{
					this.SetTipsText("tap_a_square_to_launch_it.");
					DOTween.Kill(this.m_img_finger, false);
					Sequence expr_6B = DOTween.Sequence();
					expr_6B.AppendCallback(delegate
					{
						this.m_img_finger.transform.localPosition = this.m_mask.transform.InverseTransformPoint(this.m_blocks[1].transform.position) + this.m_fingerRect2;
						this.m_img_finger.gameObject.SetActive(true);
					});
					expr_6B.Append(this.m_img_finger.transform.DOBlendableLocalMoveBy(new Vector3(0f, -10f, 0f), 0.5f, false));
					expr_6B.Append(this.m_img_finger.transform.DOBlendableLocalMoveBy(new Vector3(0f, 10f, 0f), 0.5f, false));
					expr_6B.SetLoops(-1);
					expr_6B.SetTarget(this.m_img_finger);
					this.m_isPuase = false;
					return;
				}
				case 8:
				{
					this.m_transformList.Add(new G00103.TransformControl(this.m_blocks[1].transform.parent, this.m_blocks[1].transform));
					this.m_transformList.Add(new G00103.TransformControl(this.m_img_finger.transform.parent, this.m_img_finger.transform));
					this.ToMask(this.m_transformList, "try_and_remove_a_number！", false, new Vector3(0f, -404f, 0f));
					DOTween.Kill(this.m_img_finger, false);
					Sequence expr_196 = DOTween.Sequence();
					expr_196.AppendCallback(delegate
					{
						this.m_img_finger.transform.localPosition = this.m_mask.transform.InverseTransformPoint(this.m_blocks[1].transform.position) + this.m_fingerRect2;
						this.m_img_finger.gameObject.SetActive(true);
					});
					expr_196.Append(this.m_img_finger.transform.DOBlendableLocalMoveBy(new Vector3(0f, -10f, 0f), 0.5f, false));
					expr_196.Append(this.m_img_finger.transform.DOBlendableLocalMoveBy(new Vector3(0f, 10f, 0f), 0.5f, false));
					expr_196.SetLoops(-1);
					expr_196.SetTarget(this.m_img_finger);
					this.m_isPuase = false;
					return;
				}
				case 10:
				{
					this.m_transformList.Add(new G00103.TransformControl(this.m_blocks[1].transform.parent, this.m_blocks[1].transform));
					this.m_transformList.Add(new G00103.TransformControl(this.m_img_finger.transform.parent, this.m_img_finger.transform));
					this.ToMask(this.m_transformList, "try_again_.", false, new Vector3(0f, -404f, 0f));
					DOTween.Kill(this.m_img_finger, false);
					Sequence expr_2C1 = DOTween.Sequence();
					expr_2C1.AppendCallback(delegate
					{
						this.m_img_finger.transform.localPosition = this.m_mask.transform.InverseTransformPoint(this.m_blocks[1].transform.position) + this.m_fingerRect2;
						this.m_img_finger.gameObject.SetActive(true);
					});
					expr_2C1.Append(this.m_img_finger.transform.DOBlendableLocalMoveBy(new Vector3(0f, -10f, 0f), 0.5f, false));
					expr_2C1.Append(this.m_img_finger.transform.DOBlendableLocalMoveBy(new Vector3(0f, 10f, 0f), 0.5f, false));
					expr_2C1.SetLoops(-1);
					expr_2C1.SetTarget(this.m_img_finger);
					this.m_isPuase = false;
					return;
				}
				case 12:
				{
					Sequence expr_357 = DOTween.Sequence();
					expr_357.AppendInterval(0.5f);
					expr_357.AppendCallback(delegate
					{
						Utils.ShowConfirm(delegate
						{
							this.StartGame();
						}, this.m_tipTxt[this.m_tipTxt.Length - 1], true);
					});
					return;
				}
				}
				this.m_isPuase = false;
				return;
			}
			this.SetTipsText("continue,_watch_the_numbers_when_composing");
			this.PlayFingerMoveAni(0);
			this.m_isPuase = false;
		});
	}

	private void AddNewLife()
	{
		int num = 0;
		int i = 0;
		while (i < 5)
		{
			if (this.m_lifes[i] == 0)
			{
				num = i;
				int step = this.m_step;
				if (step == 5)
				{
					this.m_lifes[i] = -2;
					break;
				}
				this.m_lifes[i] = 1;
				break;
			}
			else
			{
				i++;
			}
		}
		int number = this.m_lifes[num];
		G00105 g = this.CreateNewLife(number, this.bloodBox, num);
		g.transform.localPosition = this.bloodPosList[num].transform.localPosition;
		g.transform.localScale = new Vector3(1f, 1f, 1f);
		this.bloodList[num] = g.gameObject;
		g.transform.DOScale(this.bloodPosList[num].transform.localScale, 0.3f);
	}

	private void PlayFingerAni()
	{
		this.m_img_finger.gameObject.SetActive(true);
		this.m_img_finger.transform.localPosition = this.m_saveFingerPos;
		DOTween.Kill(this.m_img_finger, false);
		Sequence expr_39 = DOTween.Sequence();
		expr_39.Append(this.m_img_finger.transform.DOBlendableLocalMoveBy(new Vector3(0f, -10f, 0f), 0.5f, false));
		expr_39.Append(this.m_img_finger.transform.DOBlendableLocalMoveBy(new Vector3(0f, 10f, 0f), 0.3f, false));
		expr_39.SetLoops(-1);
		expr_39.SetTarget(this.m_img_finger);
	}

	private int GetBlood()
	{
		int result = this.m_lifes[0];
		for (int i = 0; i < this.m_lifes.Length; i++)
		{
			if (i + 1 < this.m_lifes.Length)
			{
				this.m_lifes[i] = this.m_lifes[i + 1];
			}
			else
			{
				this.m_lifes[i] = 0;
			}
		}
		return result;
	}

	private void SetPosition(G00101 block, int index)
	{
		int row = index / 5;
		int col = index % 5;
		this.SetPosition(block, row, col);
	}

	private void SetPosition(G00101 block, int row, int col)
	{
		block.transform.localPosition = new Vector3((float)(col * 120 + 60 - 180), (float)(row * 120 + 60 - 60), 0f);
	}

	private Vector3 GetToPosition(int index)
	{
		int num = 0;
		return new Vector3((float)(index * 120 + 60 - 180), (float)(num * 120 + 60 - 60), 0f);
	}

	private G00101 CreateBlock(int number, int idx, GameObject parent)
	{
		GameObject expr_06 = this.CreateBlock();
		expr_06.SetActive(true);
		expr_06.GetComponent<G00101>().Init(number, idx);
		expr_06.GetComponent<G00101>().RemoveClick();
		expr_06.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
		expr_06.transform.SetParent(parent.transform, false);
		return expr_06.GetComponent<G00101>();
	}

	private GameObject CreateBlock()
	{
		return UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/G00101") as GameObject);
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

	private void onBegainDragLife(GameObject obj, PointerEventData eventData)
	{
		if (obj != this.bloodList[0])
		{
			return;
		}
		obj.transform.DOKill(false);
		obj.transform.DOScale(1f, 0.1f);
		obj.transform.SetParent(base.transform);
	}

	private void OnDragLife(GameObject obj, PointerEventData eventData)
	{
		UnityEngine.Debug.Log("OnDragLife");
		if (obj != this.bloodList[0])
		{
			return;
		}
		if (this.m_isPuase)
		{
			return;
		}
		this.StopFingerAni();
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
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, obj.transform.position);
		if (RectTransformUtility.RectangleContainsScreenPoint(this.gameBox.GetComponent<RectTransform>(), screenPoint, eventData.pressEventCamera))
		{
			Vector2 vector = this.gameBox.GetComponent<RectTransform>().InverseTransformPoint(obj.transform.position);
			Vector2 expr_79 = this.gameBox.GetComponent<RectTransform>().sizeDelta;
			float num = expr_79.x / 2f + vector.x;
			double arg_B3_0 = (double)(expr_79.y / 2f + vector.y);
			int num2 = (int)Math.Floor((double)(num / 120f));
			int num3 = (int)Math.Floor(arg_B3_0 / (double)120f);
			if (num2 < 0 || num2 >= 5)
			{
				return;
			}
			if (num3 < 0 || num3 >= 5)
			{
				return;
			}
			switch (this.m_step)
			{
			case 1:
				if (num2 == 1)
				{
					this.OnClick();
					return;
				}
				this.BackToInitPosition(obj, 1);
				return;
			case 2:
			case 5:
			case 7:
			case 9:
				break;
			case 3:
				if (num2 == 0)
				{
					this.OnClick();
					return;
				}
				this.BackToInitPosition(obj, 0);
				return;
			case 4:
				if (num2 == 2)
				{
					this.OnClick();
					return;
				}
				this.BackToInitPosition(obj, 2);
				return;
			case 6:
				if (num2 == 1)
				{
					this.OnClick();
					return;
				}
				this.BackToInitPosition(obj, 1);
				return;
			case 8:
				if (num2 == 1)
				{
					this.OnClick();
					return;
				}
				this.BackToInitPosition(obj, 1);
				return;
			case 10:
				if (num2 == 1)
				{
					this.OnClick();
					return;
				}
				this.BackToInitPosition(obj, 1);
				return;
			default:
				return;
			}
		}
		else
		{
			switch (this.m_step)
			{
			case 1:
				this.BackToInitPosition(obj, 1);
				return;
			case 2:
			case 5:
			case 7:
			case 9:
				break;
			case 3:
				this.BackToInitPosition(obj, 0);
				return;
			case 4:
				this.BackToInitPosition(obj, 2);
				return;
			case 6:
				this.BackToInitPosition(obj, 1);
				return;
			case 8:
				this.BackToInitPosition(obj, 1);
				return;
			case 10:
				this.BackToInitPosition(obj, 1);
				break;
			default:
				return;
			}
		}
	}

	private void BackToInitPosition(GameObject obj, int idx)
	{
		obj.GetComponent<G00105>().DisableDrag = true;
		obj.transform.DOKill(false);
		obj.transform.SetParent(this.bloodBox.transform);
		obj.transform.DOLocalMove(this.bloodPosList[0].transform.localPosition, 0.1f, false);
		obj.transform.DOScale(this.bloodPosList[0].transform.localScale, 0.1f).OnComplete(delegate
		{
			obj.GetComponent<G00105>().DisableDrag = false;
			this.PlayFingerMoveAni(idx);
		});
	}

	private void ToMask(List<G00103.TransformControl> list, string txt, bool isOut, Vector3 tipsPos)
	{
		this.m_mask.transform.Find("txt").GetComponent<Text>().text = Localisation.GetString(txt);
	}

	private void SetTipsText(string txt)
	{
		this.m_tips01.text = Localisation.GetString(txt); 
	}

	private void PlayFingerMoveAni(int endIdx)
	{
		DOTween.Kill(this.m_img_finger, false);
		Sequence expr_12 = DOTween.Sequence();
		expr_12.AppendCallback(delegate
		{
			this.m_img_finger.transform.localPosition = this.m_mask.transform.InverseTransformPoint(this.bloodList[0].transform.position) + this.m_fingerRect2;
			this.m_img_finger.gameObject.SetActive(true);
		});
		expr_12.Append(this.m_img_finger.transform.DOLocalMove(this.m_mask.transform.InverseTransformPoint(this.m_blocks[endIdx].transform.position) + this.m_fingerRect2, 1f, false));
		expr_12.AppendInterval(0.5f);
		expr_12.SetLoops(-1);
		expr_12.SetTarget(this.m_img_finger);
	}

	private void StopFingerAni()
	{
		DOTween.Kill(this.m_img_finger, false);
		this.m_img_finger.gameObject.SetActive(false);
	}

	public G00103()
	{
		int[][] expr_62 = new int[6][];
		int arg_6E_1 = 0;
		int[] expr_6A = new int[3];
		expr_6A[1] = 1;
		expr_62[arg_6E_1] = expr_6A;
		expr_62[1] = new int[]
		{
			1,
			0,
			1
		};
		int arg_90_1 = 2;
		int[] expr_88 = new int[3];
		expr_88[0] = 3;
		expr_88[1] = 1;
		expr_62[arg_90_1] = expr_88;
		expr_62[3] = new int[]
		{
			3,
			0,
			3
		};
		expr_62[4] = new int[]
		{
			2,
			0,
			2
		};
		expr_62[5] = new int[]
		{
			2,
			0,
			2
		};
		this.m_nextMap = expr_62;
		this.m_isPuase = true;
		this.m_saveFingerPos = Vector3.zero;
		this.m_transformList = new List<G00103.TransformControl>();
		this.m_fingerRect = new Vector3(70f, -70f);
		this.m_fingerRect2 = new Vector3(100f, -70f);
		
	}
}
