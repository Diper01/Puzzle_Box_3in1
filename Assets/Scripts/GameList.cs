using Assets.Scripts.Configs;
using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameList : MonoBehaviour
{
	[Serializable]
	private sealed class __c
	{
		public static readonly GameList.__c __9 = new GameList.__c();

		public static Action __9__20_1;

		internal void _Update_b__20_1()
		{
			Utils.ExitGame();
		}
	}

	private sealed class __c__DisplayClass29_0
	{
		public GameList __4__this;

		public Vector3 savePos;

		internal void _PlayGuideAni_b__0()
		{
			this.__4__this.m_img_finger.localPosition = this.savePos;
		}
	}

	private sealed class __c__DisplayClass36_0
	{
		public GameList __4__this;

		public Transform img_box;

		internal void _PlayRecordAni_b__0()
		{
			this.__4__this.m_node001.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
		}

		internal void _PlayRecordAni_b__1()
		{
			this.__4__this.m_node002.transform.localPosition = this.__4__this.m_savePage2Block;
			this.__4__this.m_node002.GetComponent<Image>().color = Color.white;
			this.img_box.GetComponent<Image>().color = Color.white;
			this.__4__this.m_maxNumber03.GetComponent<G00201>().FadeIn();
			this.__4__this.m_maxNumber03.transform.localScale = Vector3.one;
			this.__4__this.m_maxNumber02.GetComponent<G00201>().setNum(this.__4__this.m_record_number);
			this.__4__this.m_maxNumber03.GetComponent<G00201>().setNum(this.__4__this.m_record_number);
			this.__4__this.m_arrow.GetComponent<Image>().color = Color.white;
		}

		internal void _PlayRecordAni_b__2()
		{
			this.__4__this.m_node002.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
			this.img_box.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
			this.__4__this.m_maxNumber03.GetComponent<G00201>().HideNumber();
			this.__4__this.m_maxNumber02.GetComponent<G00201>().setNum(this.__4__this.m_record_number * 2);
			this.__4__this.m_maxNumber03.GetComponent<G00201>().setNum(this.__4__this.m_record_number * 2);
		}
	}

	public GameObject gamelist;

	public GameObject content;

	public Dictionary<int, GameObject> m_gameDict = new Dictionary<int, GameObject>();


	public RectTransform m_btn_return;

	public G00101 m_maxNumber01;

	public G00201 m_maxNumber02;

	public G00201 m_maxNumber03;

	public Text m_txt_maxScore01;

	public Text m_txt_maxScore02;

	public Text m_txt_maxScore03;

	public GameObject m_node001;

	public GameObject m_node002;

	public GameObject m_arrow;

	public Transform m_img_finger;

	public Transform m_img_arrow;

	private int m_record_number = 64;

	private Vector3 m_savePage2Block = Vector3.zero;

	private void Start()
	{
		this.m_savePage2Block = this.m_node002.transform.localPosition;
		this.Init();
		this.PlayRecordAni();
		this.InitEvent();
		if (GM.GetInstance().isFirstGame())
		{
			GM.GetInstance().SetFristGame();
			base.transform.Find("list/view").GetComponent<PageView>().OnDragHandle = delegate
			{
				this.StopGuideAni();
			};
			this.PlayGuideAni();
		}
	}

	private void Update()
	{
		Utils.BackListener(base.gameObject, delegate
		{
			if (GM.GetInstance().GameId != 0)
			{
				this.OnClickReturn();
				return;
			}
			Action arg_39_0;
			if ((arg_39_0 = GameList.__c.__9__20_1) == null)
			{
				arg_39_0 = (GameList.__c.__9__20_1 = new Action(GameList.__c.__9._Update_b__20_1));
			}
			Utils.ShowConfirmOrCancel(arg_39_0, null, "do_you_want_to_quit_the_game?", true);
		});
	}

	private void OnEnable()
	{
		this.PlayRecordAni();
	}

	private void OnDestroy()
	{
		GlobalEventHandle.DoGoHome -= new Action(this.Init);
	}

	public void OnClickStartGame(int id)
	{
		AudioManager.GetInstance().PlayEffect("sound_eff_button");
		this.gamelist.SetActive(false);
		this.content.SetActive(true);
		this.LoadGame(id, 0, true);
		GM.GetInstance().GameId = id;
	}



	public void OnClickReturn()
	{
		if (GM.GetInstance().GameId == 0)
		{
			this.HideTopBtn();
			GlobalEventHandle.EmitDoGoHome();
			GlobalEventHandle.EmitClickPageButtonHandle("main", 0);
			return;
		}
		switch (GM.GetInstance().GameId)
		{
		case 1:
		{
			Action<GameList> expr_36 = M001.GetInstance().OnClickReturnHandle;
			if (expr_36 == null)
			{
				return;
			}
			expr_36(this);
			return;
		}
		case 2:
		{
			Action<GameList> expr_4C = M002.GetInstance().OnClickReturnHandle;
			if (expr_4C == null)
			{
				return;
			}
			expr_4C(this);
			return;
		}
		case 3:
		{
			Action<GameList> expr_62 = M00301.GetInstance().OnClickReturnHandle;
			if (expr_62 == null)
			{
				return;
			}
			expr_62(this);
			return;
		}
		default:
			return;
		}
	}

	public void LoadGame(int id, int value = 0, bool isPageIn = true)
	{
		this.PlayRecordAni();
		
		if (this.content.activeSelf)
		{
			this.ShowTopBtn();
		}
		else
		{
			this.HideTopBtn();
		}
		if (id == 0)
		{
			return;
		}
		foreach (KeyValuePair<int, GameObject> current in this.m_gameDict)
		{
			current.Value.SetActive(false);
		}
		if (this.m_gameDict.ContainsKey(id))
		{
			this.m_gameDict[id].SetActive(true);
			switch (id)
			{
			case 1:
				M001.GetInstance().StartNewGame();
				break;
			case 2:
				M002.GetInstance().StartNewGame();
				break;
			case 3:
				M00301.GetInstance().StartNewGame(value);
				break;
			}
		}
		else
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>
			{
				{
					1,
					"Prefabs/G001"
				},
				{
					2,
					"Prefabs/G002"
				},
				{
					3,
					"Prefabs/G00301"
				}
			};
			if (!dictionary.ContainsKey(id))
			{
				return;
			}
			GameObject gameObject = Resources.Load(dictionary[id]) as GameObject;
			gameObject = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			gameObject.transform.SetParent(this.content.transform, false);
			if (id == 3 && value != 0)
			{
				M00301.GetInstance().StartNewGame(value);
			}
			this.m_gameDict.Add(id, gameObject);
		}
		if (isPageIn)
		{
			this.PlayGameIn();
		}
		//AppsflyerUtils.TrackPlayGame(id);
	}

	public void HideTopBtn()
	{
		this.m_btn_return.gameObject.SetActive(false);
	}

	public void ShowTopBtn()
	{
		this.m_btn_return.gameObject.SetActive(true);
	}

	public void PlayGuideAni()
	{
		Sequence arg_23_0 = DOTween.Sequence();
		Vector3 savePos = this.m_img_finger.localPosition;
		arg_23_0.Append(this.m_img_finger.DOLocalMove(savePos + new Vector3(200f, 0f, 0f), 1.5f, false));
		arg_23_0.AppendCallback(delegate
		{
			this.m_img_finger.localPosition = savePos;
		});
		arg_23_0.SetLoops(-1);
		arg_23_0.SetTarget(this.m_img_finger);
		this.m_img_finger.gameObject.SetActive(true);
	}

	public void StopGuideAni()
	{
		DOTween.Kill(this.m_img_finger, false);
		this.m_img_finger.gameObject.SetActive(false);
	}

	private void Init()
	{
		bool flag = GM.GetInstance().isSavedGame();
		this.gamelist.SetActive(!flag);
		this.content.SetActive(flag);
		this.LoadGame(GM.GetInstance().GetSavedGameID(), GM.GetInstance().GetScore(3), false);
		this.RefreshRecord(3);
		this.RefreshMaxScore(new string[]
		{
			GM.GetInstance().GetScoreRecord(1).ToString(),
			GM.GetInstance().GetScoreRecord(2).ToString(),
			GM.GetInstance().GetScoreRecord(3).ToString()
		});
	}

	private void InitEvent()
	{
		GlobalEventHandle.DoGoHome += new Action(this.Init);
		GlobalEventHandle.OnRefreshAchiveHandle = (Action<int>)Delegate.Combine(GlobalEventHandle.OnRefreshAchiveHandle, new Action<int>(this.RefreshRecord));
		GlobalEventHandle.OnRefreshMaxScoreHandle = (Action<string[]>)Delegate.Combine(GlobalEventHandle.OnRefreshMaxScoreHandle, new Action<string[]>(this.RefreshMaxScore));
	}

	private void PlayGameIn()
	{
		if (this.content == null)
		{
			return;
		}
		this.content.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		Sequence expr_38 = DOTween.Sequence();
		expr_38.Append(this.content.transform.DOScale(1.1f, 0.3f));
		expr_38.Append(this.content.transform.DOScale(1f, 0.1f));
	}

	private void RefreshRecord(int id = 3)
	{
		if (id != 3 && id != 6)
		{
			return;
		}
		int num = 5;
		LocalData localData = AchiveData.GetInstance().Get(3);
		num = ((localData.value > num) ? localData.value : num);
		this.m_maxNumber01.setNum(num);
		num = 64;
		localData = AchiveData.GetInstance().Get(6);
		num = ((localData.value > num) ? localData.value : num);
		this.m_maxNumber02.setNum(num);
		this.m_maxNumber03.setNum(num);
		this.m_record_number = num;
	}

	public void RefreshMaxScore(string[] array)
	{
		this.m_txt_maxScore01.text = array[0];
		this.m_txt_maxScore02.text = array[1];
		if (Configs.TG00301.ContainsKey(array[2]))
		{
			this.m_txt_maxScore03.text = string.Format("{0}/300", Configs.TG00301[array[2]].Level);
			return;
		}
		this.m_txt_maxScore03.text = string.Format("{0}/300", 0);
	}

	private void PlayRecordAni()
	{
		DOTween.Kill(this.m_node001, false);
		DOTween.Kill(this.m_node002, false);
		if (!this.gamelist.activeSelf)
		{
			return;
		}
		Sequence expr_3A = DOTween.Sequence();
		expr_3A.AppendCallback(delegate
		{
			this.m_node001.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
		});
		expr_3A.Append(this.m_node001.transform.DOBlendableLocalRotateBy(new Vector3(0f, 0f, 5f), 0.5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
		expr_3A.Append(this.m_node001.transform.DOBlendableRotateBy(new Vector3(0f, 0f, -10f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
		expr_3A.Append(this.m_node001.transform.DOBlendableRotateBy(new Vector3(0f, 0f, 5f), 0.5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
		expr_3A.SetLoops(-1);
		expr_3A.SetTarget(this.m_node001);
		Vector3 arg_116_0 = this.m_node002.transform.localPosition;
		Vector3 localPosition = this.m_maxNumber02.transform.localPosition;
		Transform img_box = this.m_node002.transform.Find("img_02");
		Sequence expr_148 = DOTween.Sequence();
		expr_148.AppendCallback(delegate
		{
			this.m_node002.transform.localPosition = this.m_savePage2Block;
			this.m_node002.GetComponent<Image>().color = Color.white;
			img_box.GetComponent<Image>().color = Color.white;
			this.m_maxNumber03.GetComponent<G00201>().FadeIn();
			this.m_maxNumber03.transform.localScale = Vector3.one;
			this.m_maxNumber02.GetComponent<G00201>().setNum(this.m_record_number);
			this.m_maxNumber03.GetComponent<G00201>().setNum(this.m_record_number);
			this.m_arrow.GetComponent<Image>().color = Color.white;
		});
		expr_148.Append(this.m_arrow.GetComponent<Image>().DOFade(0.5f, 0.5f));
		expr_148.Append(this.m_arrow.GetComponent<Image>().DOFade(1f, 0.5f));
		expr_148.Append(this.m_arrow.GetComponent<Image>().DOFade(0.5f, 0.5f));
		expr_148.Append(this.m_arrow.GetComponent<Image>().DOFade(1f, 0.5f));
		expr_148.Append(this.m_arrow.GetComponent<Image>().DOFade(0f, 0.5f));
		expr_148.Append(this.m_node002.transform.DOLocalMove(localPosition, 0.5f, false).OnComplete(delegate
		{
			this.m_node002.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
			img_box.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
			this.m_maxNumber03.GetComponent<G00201>().HideNumber();
			this.m_maxNumber02.GetComponent<G00201>().setNum(this.m_record_number * 2);
			this.m_maxNumber03.GetComponent<G00201>().setNum(this.m_record_number * 2);
		}));
		expr_148.Append(this.m_maxNumber03.GetComponent<G00201>().FadeOut());
		expr_148.Insert(3f, this.m_maxNumber03.transform.DOScale(1.5f, 0.5f));
		expr_148.AppendInterval(1f);
		expr_148.SetLoops(-1);
		expr_148.SetTarget(this.m_node002);
	}
}
