using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class G002 : MonoBehaviour
{
    private struct PathRDM
    {
        public List<Node> paths;

        public int index;
    }

    private sealed class __c__DisplayClass30_0
    {
        public G002 __4__this;

        public G00201 block;

        internal void _OnClickBox_b__0()
        {
            M002.GetInstance().AddLife();
            this.__4__this.bloodList[2].GetComponent<G00201>().setNum(M002.GetInstance().GetBlood());
        }

        internal void _OnClickBox_b__1()
        {
            this.__4__this.blocks[this.block.Index] = this.block;
            this.__4__this.BackMap(1);
            M002.GetInstance().IsPlaying = false;
            M002.GetInstance().Delete(this.block.Index);
            if (this.block.Index == M002.GetInstance().CoinIndex)
            {
                GM.GetInstance().AddDiamond(1, true);
                M002.GetInstance().CoinIndex = -1;
                this.__4__this.m_img_coin.gameObject.SetActive(false);
            }
            TaskData.GetInstance().Add(100102, 1, true);
        }
    }

    private sealed class __c__DisplayClass35_0
    {
        public GameList obj;

        internal void _OnClickReturn_b__0()
        {
            M002.GetInstance().Score = 0;
            GM.GetInstance().SaveScore(2, 0);
            GM.GetInstance().SetSavedGameID(0);
            GM.GetInstance().ResetToNewGame();
            GM.GetInstance().ResetConsumeCount();
            GlobalEventHandle.EmitDoGoHome();
            GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
            this.obj.HideTopBtn();
        }
    }

    [Serializable]
    private sealed class __c
    {
        public static readonly G002.__c __9 = new G002.__c();

        public static Action __9__35_1;

        public static Action __9__35_2;

        public static Comparison<G002.PathRDM> __9__46_0;

        internal void _OnClickReturn_b__35_1()
        {
            GM.GetInstance().SaveScore(2, 0);
            GM.GetInstance().SetSavedGameID(0);
            GM.GetInstance().ResetToNewGame();
            GM.GetInstance().ResetConsumeCount();
            M002.GetInstance().Score = 0;
            M002.GetInstance().StartNewGame();
        }

        internal void _OnClickReturn_b__35_2()
        {
            M002.GetInstance().IsPause = false;
        }

        internal int _FindPath_b__46_0(G002.PathRDM a, G002.PathRDM b)
        {
            return -1 * a.paths.Count.CompareTo(b.paths.Count);
        }
    }

    private sealed class __c__DisplayClass45_0
    {
        public G002 __4__this;

        public int index;

        internal void _Delete_b__0()
        {
            this.__4__this.PlayDoubleAni();
            this.__4__this.BackMap(2);
            this.__4__this.blocks[this.index].setNum(M002.GetInstance().GetNumber(this.index));
            M002.GetInstance().Down();
        }
    }

    private sealed class __c__DisplayClass52_0
    {
        public GameObject node;

        internal void _PlayNewNumber_b__0()
        {
            UnityEngine.Object.Destroy(this.node);
        }
    }

    public GameObject m_map;

    public GameObject m_mask;

    private List<G00201> blocks = new List<G00201>();

    public GameObject m_objEndPos;

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

    public GameObject m_img_coin;

    public Text m_txt_tips;

    public GameObject txt_score;

    public float m_downSpeed = 10f;

    public int m_backLength = 50;

    public int m_deleteBackLength = 100;

    public float m_upSpeed = 500f;

    public bool m_isBack;

    public float m_backTotal;

    public Image m_img_finger;

    public Image m_img_double;

    private int m_dobleTotal;

    private Vector3 m_saveFingerPos = Vector3.zero;

    private int m_step;

    private string[] m_data_tiptxts = new string[]
    {
        "tap_to_start",
        "tap_the_screen_again_and_compose_more_numbers",
        "tap_this_column_to_launch_the_squares",
        "drag_the_life_bar_can_change_the_launch_position!"
    };

    private void Start()
    {
        this.LoadUI();
        this.LoadBlock();
        this.InitEvent();
    }

    private void Update()
    {
        if (!M002.GetInstance().IsStart)
        {
            return;
        }
        if (M002.GetInstance().IsPause)
        {
            return;
        }
        if (M002.GetInstance().IsGameOver)
        {
            return;
        }
        if (!M002.GetInstance().IsFinishGuide)
        {
            return;
        }
        if (this.m_isBack)
        {
            return;
        }
        this.DownMap();
    }

    private void OnDestroy()
    {
        this.RemoveEvent();
    }

    private void InitEvent()
    {
        M002.GetInstance().DoRefreshHandle += new Action(this.RefreshMap);
        M002.GetInstance().DoDeleteHandle += new Action<List<int>, int>(this.Delete);
        M002.GetInstance().DoDropHandle += new Action<List<sDropData>, List<int>>(this.Drop);
        M002 expr_47 = M002.GetInstance();
        expr_47.DoClickBlock = (Action<G00201>)Delegate.Combine(expr_47.DoClickBlock, new Action<G00201>(this.OnClickBlock));
        M002.GetInstance().DoNullDelete += new Action(this.OnNullDelete);
        M002.GetInstance().DoCompMaxNumber += new Action<int>(this.PlayNewNumber);
        M002 expr_99 = M002.GetInstance();
        expr_99.DoVedioRefresh = (Action)Delegate.Combine(expr_99.DoVedioRefresh, new Action(this.UseVedioRefresh));
        M002.GetInstance().OnClickReturnHandle = new Action<GameList>(this.OnClickReturn);
        M002.GetInstance().OnRandomCoinHandle = new Action(this.OnRandomCoin);
    }

    private void RemoveEvent()
    {
    }

    public void OnClickProp(int type)
    {
        if (M002.GetInstance().IsPlaying)
        {
            return;
        }
        if (!M002.GetInstance().IsStart)
        {
            return;
        }
        int value = Constant.COMMON_CONFIG_PROP[type - 1];
        if (!GM.GetInstance().isFullGEM(value))
        {
            ToastManager.Show("not_enough_coins", true);
            return;
        }
        if (M002.GetInstance().CurPropId != 0)
        {
            M002.GetInstance().CurPropId = 0;
            M002.GetInstance().IsPause = false;
            this.ControlPropsPannel(true);
            return;
        }
        M002.GetInstance().CurPropId = type;
        M002.GetInstance().IsPause = true;
        this.ControlPropsPannel(false);
    }

    public void OnClickBox(int idx)
    {
        if (!M002.GetInstance().IsStart)
        {
            M002.GetInstance().IsStart = true;
        }
        if (M002.GetInstance().IsPause)
        {
            return;
        }
        if (M002.GetInstance().IsPlaying)
        {
            return;
        }
        if (!M002.GetInstance().IsFinishGuide)
        {
            switch (this.m_step)
            {
                case 0:
                    idx = 2;
                    break;
                case 1:
                    idx = 2;
                    break;
                case 2:
                    idx = 3;
                    break;
            }
            this.m_step++;
            if (this.m_step >= 4)
            {
                M002.GetInstance().FinishGuide();
                this.m_txt_tips.text = Localisation.GetString(this.m_data_tiptxts[0]);
                //AppsflyerUtils.TrackTutorialCompletion(2, 1);
            }
        }
        this.m_dobleTotal = 0;
        int num = M002.GetInstance().AddBock(idx);
        if (num == -1)
        {
            return;
        }
        int number = M002.GetInstance().GetNumber(num, idx);
        G00201 block = M002.GetInstance().CreateBlock(number, M002.GetInstance().GetIndex(num, idx), this.m_map);
        Vector3 localPosition = block.transform.localPosition;
        block.transform.position = this.bloodPosList[idx].transform.position;
        block.transform.SetParent(this.m_map.transform, true);
        Vector3 localPosition2 = block.transform.localPosition;
        float duration = (localPosition.y - localPosition2.y) / this.m_upSpeed;
        block.transform.DOLocalMove(localPosition, duration, false).OnStart(delegate
        {
            M002.GetInstance().AddLife();
            this.bloodList[2].GetComponent<G00201>().setNum(M002.GetInstance().GetBlood());
        }).OnComplete(delegate
        {
            this.blocks[block.Index] = block;
            this.BackMap(1);
            M002.GetInstance().IsPlaying = false;
            M002.GetInstance().Delete(block.Index);
            if (block.Index == M002.GetInstance().CoinIndex)
            {
                GM.GetInstance().AddDiamond(1, true);
                M002.GetInstance().CoinIndex = -1;
                this.m_img_coin.gameObject.SetActive(false);
            }
            TaskData.GetInstance().Add(100102, 1, true);
        });
        M002.GetInstance().IsPlaying = true;
        this.HideTxtTips();
        this.StopFingerAni();
        AudioManager.GetInstance().PlayEffect("sound_eff_click_1");
    }

    public void OnTouchStartBox(BaseEventData eventData)
    {
        if (M002.GetInstance().IsPause)
        {
            return;
        }
        if (M002.GetInstance().IsPlaying)
        {
            return;
        }
        PointerEventData pointerEventData = (PointerEventData)eventData;
        int num = -1;
        for (int i = 0; i < this.bloodPosList.Count; i++)
        {
            GameObject expr_33 = this.bloodPosList[i];
            Vector2 point;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(expr_33.GetComponent<RectTransform>(), pointerEventData.pressPosition, pointerEventData.pressEventCamera, out point);
            if (expr_33.GetComponent<RectTransform>().rect.Contains(point))
            {
                num = i;
                break;
            }
        }
        if (num == -1)
        {
            return;
        }
        this.bloodList[2].transform.SetParent(this.bloodPosList[num].transform, false);
    }

    public void OnTouchMoveBox(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        int num = -1;
        for (int i = 0; i < this.bloodPosList.Count; i++)
        {
            GameObject expr_19 = this.bloodPosList[i];
            Vector2 point;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(expr_19.GetComponent<RectTransform>(), pointerEventData.position, pointerEventData.pressEventCamera, out point);
            if (expr_19.GetComponent<RectTransform>().rect.Contains(point))
            {
                num = i;
                break;
            }
        }
        if (num == -1)
        {
            return;
        }
        this.bloodList[2].transform.SetParent(this.bloodPosList[num].transform, false);
    }

    public void onTouchEndBox(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        int num = -1;
        for (int i = 0; i < this.bloodPosList.Count; i++)
        {
            GameObject expr_19 = this.bloodPosList[i];
            Vector2 point;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(expr_19.GetComponent<RectTransform>(), pointerEventData.position, pointerEventData.pressEventCamera, out point);
            if (expr_19.GetComponent<RectTransform>().rect.Contains(point))
            {
                num = i;
                break;
            }
        }
        this.bloodList[2].transform.SetParent(this.bloodPosList[2].transform, false);
        if (num == -1)
        {
            return;
        }
        this.OnClickBox(num);
    }

    public void OnTouchMap(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        Vector2 vector;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_map.GetComponent<RectTransform>(), pointerEventData.pressPosition, pointerEventData.pressEventCamera, out vector);
        int num = (int)Math.Floor((double)((this.m_map.GetComponent<RectTransform>().sizeDelta.x / 2f + vector.x) / 110f));
        if (num < 0 || num >= 5)
        {
            return;
        }
        this.OnClickBox(num);
    }

    public void OnClickReturn(GameList obj)
    {
        Debug.Log("[labu] G002 OnClickReturn");
        if (M002.GetInstance().IsPlaying || M002.GetInstance().IsPause)
		{
			return;
		}
        M002.GetInstance().IsPause = true;
		int arg_79_0 = M002.GetInstance().Score;
		Action arg_79_1 = delegate
		{
			M002.GetInstance().Score = 0;
			GM.GetInstance().SaveScore(2, 0);
			GM.GetInstance().SetSavedGameID(0);
			GM.GetInstance().ResetToNewGame();
			GM.GetInstance().ResetConsumeCount();
			GlobalEventHandle.EmitDoGoHome();
			GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
			obj.HideTopBtn();
		};
		Action arg_79_2;
		if ((arg_79_2 = G002.__c.__9__35_1) == null)
		{
			arg_79_2 = (G002.__c.__9__35_1 = new Action(G002.__c.__9._OnClickReturn_b__35_1));
		}
		Action arg_79_3;
		if ((arg_79_3 = G002.__c.__9__35_2) == null)
		{
			arg_79_3 = (G002.__c.__9__35_2 = new Action(G002.__c.__9._OnClickReturn_b__35_2));
		}
		Utils.ShowPause(arg_79_0, arg_79_1, arg_79_2, arg_79_3);
	}

	private void LoadUI()
	{
		this.RefreshScore(false);
		this.InitLife();
		this.ShowTxtTips();
		RectTransform component = this.m_map.GetComponent<RectTransform>();
		RectTransform arg_5E_0 = this.m_mask.GetComponent<RectTransform>();
		component.anchoredPosition = GM.GetInstance().GetSavedPos();
		arg_5E_0.anchoredPosition = GM.GetInstance().GetSavedPos() + new Vector2(0f, component.sizeDelta.y);
		this.m_saveFingerPos = this.m_img_finger.transform.localPosition;
		if (!M002.GetInstance().IsFinishGuide)
		{
			this.PlayFingerAni();
			this.m_txt_tips.text = Localisation.GetString(this.m_data_tiptxts[this.m_step]);
		}
	}

	private void RefreshScore(bool isAni = true)
	{
		if (isAni)
		{
			this.txt_score.GetComponent<OverlayNumber>().setNum(M002.GetInstance().Score);
			return;
		}
		this.txt_score.GetComponent<OverlayNumber>().Reset();
		this.txt_score.GetComponent<OverlayNumber>().setNum(M002.GetInstance().Score);
	}

	private void LoadBlock()
	{
		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int number = M002.GetInstance().GetNumber(i, j);
				if (number == 0)
				{
					this.blocks.Add(null);
				}
				else
				{
					int index = M002.GetInstance().GetIndex(i, j);
					G00201 item = M002.GetInstance().CreateBlock(number, index, this.m_map);
					this.blocks.Add(item);
				}
			}
		}
	}

	private void InitLife()
	{
		for (int i = 0; i < 5; i++)
		{
			int num = M002.GetInstance().BloodList[i];
			if (num != 0)
			{
				this.CreateNewLife(num, this.bloodPosList[i], i).transform.localPosition = new Vector3(0f, 0f, 0f);
			}
		}
	}

	private void RefreshMap()
	{
		foreach (G00201 current in this.blocks)
		{
			if (!(current == null))
			{
				M002.GetInstance().FreeBlock(current.gameObject);
			}
		}
		this.blocks.Clear();
		for (int i = 0; i < this.bloodList.Count; i++)
		{
			GameObject gameObject = this.bloodList[i];
			if (!(gameObject == null))
			{
				this.bloodList[i] = null;
				M002.GetInstance().FreeBlock(gameObject);
			}
		}
		this.LoadBlock();
		this.LoadUI();
		this.OnRandomCoin();
	}

	private void DownMap()
	{
		float y = this.m_downSpeed * Time.deltaTime;
		RectTransform component = this.m_map.GetComponent<RectTransform>();
		RectTransform arg_40_0 = this.m_mask.GetComponent<RectTransform>();
		component.anchoredPosition -= new Vector2(0f, y);
		arg_40_0.anchoredPosition -= new Vector2(0f, y);
		GM.GetInstance().SaveGame("", "", component.anchoredPosition.x, component.anchoredPosition.y);
		this.GameOver();
	}

	private void BackMap(int type)
	{
		float num = (float)((type == 1) ? this.m_backLength : this.m_deleteBackLength);
		switch (type)
		{
		case 1:
			num = (float)this.m_backLength;
			break;
		case 2:
			num = (float)this.m_deleteBackLength;
			break;
		case 3:
			num = 770f;
			break;
		}
		this.m_backTotal += num;
		if (this.m_map.transform.localPosition.y + this.m_backTotal > 0f)
		{
			this.m_backTotal = 0f - this.m_map.transform.localPosition.y;
		}
		this.m_isBack = true;
		Vector3 byValue = new Vector3(0f, this.m_backTotal, 0f);
		this.m_map.transform.DOKill(false);
		this.m_map.transform.DOBlendableLocalMoveBy(byValue, 0.1f, false).OnComplete(delegate
		{
			this.m_isBack = false;
			this.m_backTotal = 0f;
		});
		this.m_mask.transform.DOKill(false);
		this.m_mask.transform.DOBlendableLocalMoveBy(byValue, 0.1f, false);
	}

	private void OnClickBlock(G00201 block)
	{
	}

	private void OnNullDelete()
	{
		this.ShowGuide();
		this.PlayFingerAni();
	}

	private void Delete(List<int> list, int index)
	{
		AudioManager.GetInstance().PlayEffect("sound_eff_clear_2");
		List<G002.PathRDM> list2 = this.FindPath(list, index);
		if (list2.Count < 0)
		{
			return;
		}
		this.m_dobleTotal++;
		int count = list2[0].paths.Count;
		Sequence sequence = DOTween.Sequence();
		foreach (G002.PathRDM current in list2)
		{
			G00201 g = this.blocks[current.index];
			g.ShowScore();
			this.blocks[current.index] = null;
			int row = M002.GetInstance().GetRow(current.index);
			int col = M002.GetInstance().GetCol(current.index);
			if (current.paths.Count >= 2)
			{
				Vector2 b = new Vector2((float)current.paths[0].x, (float)current.paths[0].y);
				Vector2 vector = new Vector2((float)current.paths[1].x, (float)current.paths[1].y) - b;
				int index2 = M002.GetInstance().GetIndex(row + (int)vector.y, col + (int)vector.x);
				Tween t = g.DelayMove(index2, (float)(count - current.paths.Count) * 0.1f);
				sequence.Insert(0f, t);
			}
		}
		sequence.OnComplete(delegate
		{
			this.PlayDoubleAni();
			this.BackMap(2);
			this.blocks[index].setNum(M002.GetInstance().GetNumber(index));
			M002.GetInstance().Down();
		});
		M002.GetInstance().IsPlaying = true;
		this.RefreshScore(true);
	}

	private List<G002.PathRDM> FindPath(List<int> list, int index)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 7;
		int num4 = 5;
		foreach (int current in list)
		{
			int row = M002.GetInstance().GetRow(current);
			int col = M002.GetInstance().GetCol(current);
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
				int index2 = M002.GetInstance().GetIndex(i, j);
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
		List<G002.PathRDM> list3 = new List<G002.PathRDM>();
		foreach (sGridRMD current2 in list2)
		{
			int index3 = M002.GetInstance().GetIndex(current2.gameRow, current2.gameCol);
			if (index3 != index)
			{
				M002.GetInstance().GetRow(index3);
				M002.GetInstance().GetCol(index3);
				list3.Add(new G002.PathRDM
				{
					paths = aStar.Find(new AVec2(current2.gridCol, current2.gridRow), new AVec2(px, py)),
					index = index3
				});
			}
		}
		List<G002.PathRDM> arg_20D_0 = list3;
		Comparison<G002.PathRDM> arg_20D_1;
		if ((arg_20D_1 = G002.__c.__9__46_0) == null)
		{
			arg_20D_1 = (G002.__c.__9__46_0 = new Comparison<G002.PathRDM>(G002.__c.__9._FindPath_b__46_0));
		}
		arg_20D_0.Sort(arg_20D_1);
		return list3;
	}

	private void Drop(List<sDropData> dropList, List<int> newList)
	{
		Sequence sequence = DOTween.Sequence();
		foreach (sDropData current in dropList)
		{
			G00201 g = this.blocks[current.srcIdx];
			this.blocks[current.srcIdx] = null;
			this.blocks[current.dstIdx] = g;
			g.Index = current.dstIdx;
			Tween t = g.Move(current.dstIdx);
			sequence.Insert(0f, t);
		}
		sequence.OnComplete(delegate
		{
			M002.GetInstance().IsPlaying = false;
			M002.GetInstance().AutoDelete();
			this.ShowGuide();
			this.PlayFingerAni();
		});
		if (M002.GetInstance().CoinIndex > 0)
		{
			int row = M002.GetInstance().GetRow(M002.GetInstance().CoinIndex);
			int col = M002.GetInstance().GetCol(M002.GetInstance().CoinIndex);
			for (int i = 6; i > row; i--)
			{
				if (M002.GetInstance().GetNumber(i, col) == 0)
				{
					Vector3 endValue = new Vector3((float)col * 110f + 55f - 275f, (float)i * 110f + 55f - 385f, 0f);
					this.m_img_coin.transform.DOLocalMove(endValue, 0.3f, false);
					M002.GetInstance().CoinIndex = M002.GetInstance().GetIndex(i, col);
					break;
				}
			}
		}
		M002.GetInstance().IsPlaying = true;
	}

	private void UseVedioRefresh()
	{
		bool flag = false;
		if (this.m_mask.transform.localPosition.y <= 0f)
		{
			flag = true;
		}
		if (!flag)
		{
			float num = this.m_objEndPos.transform.position.y + 0.55f;
			int idx = 0;
			for (int i = 0; i < this.blocks.Count; i++)
			{
				G00201 g = this.blocks[i];
				if (!(g == null) && g.transform.position.y <= num)
				{
					flag = true;
					idx = i;
					break;
				}
			}
			if (!flag)
			{
				return;
			}
			foreach (int current in M002.GetInstance().Use(idx, 2))
			{
				if (current < this.blocks.Count)
				{
					G00201 g2 = this.blocks[current];
					this.blocks[current] = null;
					if (!(g2 == null))
					{
						ParticlesControl.GetInstance().PlayExplodeEffic(g2.transform.position, g2.GetCurrentColor());
						M002.GetInstance().FreeBlock(g2.gameObject);
					}
				}
			}
		}
		this.BackMap(3);
		M002.GetInstance().IsStart = true;
		M002.GetInstance().IsGameOver = false;
	}

	private void ShowTxtTips()
	{
		this.m_txt_tips.gameObject.SetActive(true);
		if (DOTween.IsTweening(this.m_txt_tips, false))
		{
			return;
		}
		Sequence expr_25 = DOTween.Sequence();
		expr_25.Append(this.m_txt_tips.transform.DOScale(0.95f, 1f));
		expr_25.Append(this.m_txt_tips.transform.DOScale(1f, 1f));
		expr_25.SetLoops(-1);
		expr_25.SetTarget(this.m_txt_tips);
	}

	private void HideTxtTips()
	{
		this.m_txt_tips.gameObject.SetActive(false);
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

	private void PlayNewNumber(int number)
	{
		GameObject original = Resources.Load("Prefabs/G00204") as GameObject;
		GameObject node = UnityEngine.Object.Instantiate<GameObject>(original, base.transform, false);
		node.transform.Find("prefab_max").GetComponent<G00201>().setNum(number);
		node.transform.localScale = new Vector3(1f, 0f, 0f);
		Sequence expr_72 = DOTween.Sequence();
		expr_72.Append(node.transform.DOScaleY(1f, 0.1f));
        //[labu] Changed AppendInterval from 0.5f for achievemant last longer
        expr_72.AppendInterval(1.5f);
		expr_72.OnComplete(delegate
		{
			UnityEngine.Object.Destroy(node);
		});
		AudioManager.GetInstance().PlayEffect("sound_eff_newNum");
	}

	private void OnRandomCoin()
	{
		if (M002.GetInstance().CoinIndex > 0)
		{
			int num = M002.GetInstance().CoinIndex / 5;
			int num2 = M002.GetInstance().CoinIndex % 5;
			this.m_img_coin.transform.localPosition = new Vector3((float)num2 * 110f + 55f - 275f, (float)num * 110f + 55f - 385f, 0f);
			this.m_img_coin.gameObject.SetActive(true);
			return;
		}
		this.m_img_coin.gameObject.SetActive(false);
	}

	private void GameOver()
	{
		if (M002.GetInstance().IsPlaying)
		{
			return;
		}
		bool flag = false;
		if (this.m_mask.transform.localPosition.y <= 0f)
		{
			flag = true;
		}
		if (!flag)
		{
			float num = this.m_objEndPos.transform.position.y + 0.55f;
			foreach (G00201 current in this.blocks)
			{
				if (!(current == null) && current.transform.position.y <= num)
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			return;
		}
		M002.GetInstance().IsStart = false;
		M002.GetInstance().IsGameOver = true;
		if (M002.GetInstance().FinishCount < 1000)
		{
			M002.GetInstance().FinishCount++;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/finish") as GameObject);
			gameObject.GetComponent<Finish>().Load(2, M002.GetInstance().GetMapMaxNumber());
			DialogManager.GetInstance().show(gameObject, true);
			return;
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/G00203") as GameObject);
		gameObject2.GetComponent<G00203>().Load(M002.GetInstance().Score, M002.GetInstance().MaxScore);
		DialogManager.GetInstance().show(gameObject2, true);
	}

	private void ControlPropsPannel(bool isDel)
	{
		if (isDel)
		{
			for (int i = 0; i < this.m_tranformObjs.Count; i++)
			{
				if (i < this.m_tranformObjs.Count - 1)
				{
					this.m_tranformObjs[i].transform.SetParent(this.m_map.transform, true);
					this.m_tranformObjs[i].GetComponent<G00201>().RemoveClick();
				}
				else
				{
					this.m_tranformObjs[i].transform.SetParent(this.m_pannel_props.transform, true);
				}
			}
			this.m_tranformObjs.Clear();
			GlobalEventHandle.EmitDoUseProps(true, this.m_tranformObjs);
			return;
		}
		foreach (G00201 current in this.blocks)
		{
			if (!(current == null))
			{
				current.AddClickListener();
				this.m_tranformObjs.Add(current.gameObject);
			}
		}
		this.m_tranformObjs.Add(base.transform.Find(string.Format("img_pros/item{0}", M002.GetInstance().CurPropId)).gameObject);
		GlobalEventHandle.EmitDoUseProps(false, this.m_tranformObjs);
	}

	private void ShowGuide()
	{
		if (M002.GetInstance().IsFinishGuide)
		{
			return;
		}
		this.m_txt_tips.gameObject.SetActive(true);
		this.m_txt_tips.text = Localisation.GetString(this.m_data_tiptxts[this.m_step]);
		switch (this.m_step)
		{
		case 1:
			break;
		case 2:
			this.m_saveFingerPos += new Vector3(110f, 0f, 0f);
			return;
		case 3:
		{
			Vector3 vector = this.bloodPosList[this.bloodPosList.Count - 1].transform.position;
			vector = base.transform.InverseTransformPoint(vector);
			this.m_saveFingerPos = vector + new Vector3(70f, -70f, 0f);
			break;
		}
		default:
			return;
		}
	}

	private void PlayFingerAni()
	{
		if (M002.GetInstance().IsFinishGuide)
		{
			return;
		}
		this.m_img_finger.gameObject.SetActive(true);
		this.m_img_finger.transform.localPosition = this.m_saveFingerPos;
		DOTween.Kill(this.m_img_finger, false);
		Sequence expr_46 = DOTween.Sequence();
		expr_46.Append(this.m_img_finger.transform.DOBlendableLocalMoveBy(new Vector3(0f, -10f, 0f), 0.5f, false));
		expr_46.Append(this.m_img_finger.transform.DOBlendableLocalMoveBy(new Vector3(0f, 10f, 0f), 0.3f, false));
		expr_46.SetLoops(-1);
		expr_46.SetTarget(this.m_img_finger);
	}

	private void StopFingerAni()
	{
		DOTween.Kill(this.m_img_finger, false);
		this.m_img_finger.gameObject.SetActive(false);
	}

	private G00201 CreateNewLife(int number, GameObject parent, int idx)
	{
		G00201 g = M002.GetInstance().CreateBlock(number, 0, parent);
		this.bloodList[idx] = g.gameObject;
		return g;
	}
}
