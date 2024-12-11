using Assets.Scripts.Configs;
using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
	[Serializable]
	private sealed class __c
	{
		public static readonly MainScene.__c __9 = new MainScene.__c();

		public static Action __9__38_0;

		internal void _OnClickFacebook_b__38_0()
		{
			if (GM.GetInstance().isFirstShare())
			{
				GM.GetInstance().ResetFirstShare(1);
				GM.GetInstance().AddDiamond(5, true);
			}
			//AppsflyerUtils.TrackShare();
		}
	}

	private sealed class __c__DisplayClass47_0
	{
		public int toNum;

		public MainScene __4__this;

		internal void _OnGetDiamond_b__0()
		{
			this.__4__this.txt_diamond.GetComponent<OverlayNumber>().setNum(this.toNum);
		}
	}

	private sealed class __c__DisplayClass55_0
	{
		public GameObject asset;

		public MainScene __4__this;

		public System.Random rd;

		public Sequence actions;

		public Action callback;

		internal void _PlayGEMAni_b__0()
		{
			MainScene.__c__DisplayClass55_1 __c__DisplayClass55_ = new MainScene.__c__DisplayClass55_1();
			__c__DisplayClass55_.prefab = UnityEngine.Object.Instantiate<GameObject>(this.asset);
			__c__DisplayClass55_.prefab.transform.SetParent(this.__4__this.panel_maxTop.transform, false);
			__c__DisplayClass55_.prefab.transform.localPosition = new Vector3((float)this.rd.Next(-80, 80), (float)this.rd.Next(0, 80), 0f);
			__c__DisplayClass55_.prefab.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			Vector3 position = this.__4__this.icon_gem.transform.position;
			Tweener t = __c__DisplayClass55_.prefab.transform.DOMove(position, 0.8f, false).SetEase(Ease.InBack).OnComplete(new TweenCallback(__c__DisplayClass55_._PlayGEMAni_b__2));
			this.actions.Insert(0f, t);
		}

		internal void _PlayGEMAni_b__1()
		{
			Action expr_06 = this.callback;
			if (expr_06 == null)
			{
				return;
			}
			expr_06();
		}
	}

	private sealed class __c__DisplayClass55_1
	{
		public GameObject prefab;

		internal void _PlayGEMAni_b__2()
		{
			UnityEngine.Object.Destroy(this.prefab);
		}
	}

	private Dictionary<string, GameObject> nodes = new Dictionary<string, GameObject>();

	private string curBtnType = "main";

	public GameObject txt_lv;

	public GameObject img_exp;

	public GameObject icon_gem;

	public Text txt_ads_timer;

	public GameObject txt_diamond;

	public GameObject panel_cotent;

	public GameObject panel_handle;

	public GameObject panel_top;

	public GameObject panel_maxTop;

	public RectTransform m_btn_achiev;

	public RectTransform m_btn_task;

	public RectTransform m_btn_activity;

	public RectTransform m_btn_start;

	public RectTransform m_btn_skin;

	public RectTransform m_img_buttons;

	public GameObject gamelist;

	public GameObject content;

	public PageView gameview;

	public GameList gameContral;

	public Dictionary<string, Vector2> m_nodesPositions = new Dictionary<string, Vector2>();

	public GameObject m_splash;

	public int music_sound;

	private void Start()
	{
		this.LoadData();
		this.InitUI();
		this.InitEvent();
		this.RunSplash();
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
		this.DestroyEvent();
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			GlobalTimer.GetInstance().TrackMiniTime();
			GlobalTimer.GetInstance().TrackTotalTime();
			return;
		}
	}

	private void OnApplicationQuit()
	{
		GlobalTimer.GetInstance().TrackMiniTime();
		GlobalTimer.GetInstance().TrackTotalTime();
	}

	public void OnClickStartGame(int id = 0)
	{
		this.gameContral.StopGuideAni();
		int num = (new int[]
		{
			2,
			1,
			3,
			0
		})[this.gameview.PageIdx];
		if (num == 0)
		{
			return;
		}
		if (num == 1)
		{
			if (PlayerPrefs.GetInt("LocalData_guide_game01", 0) == 0)
			{
				GlobalEventHandle.EmitClickPageButtonHandle("G00103", 0);
				return;
			}
		}
		else if (num == 3 && id == 0)
		{
			GlobalEventHandle.EmitClickPageButtonHandle("G003", 0);
			return;
		}
		this.gamelist.SetActive(false);
		this.content.SetActive(true);
		this.gameContral.gameObject.SetActive(true);
		this.gameContral.LoadGame(num, id, true);
		this.DoPageOutAni();
		AudioManager.GetInstance().PlayEffect("sound_eff_button");
	}

	public void onClickBottom(string type)
	{
		string text = type;
		if (text.Equals("shop02"))
		{
			type = "shop";
		}
		this.gameContral.StopGuideAni();
		if (!this.curBtnType.Equals(type))
		{
			this.curBtnType = type;
			//AppsflyerUtils.TrackClickButton(type);
			foreach (KeyValuePair<string, GameObject> current in this.nodes)
			{
				if (current.Value != null)
				{
					current.Value.SetActive(false);
				}
			}
			if (this.nodes[type] == null)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>
				{
					{
						"shop",
						"shop"
					},
					{
						"main",
						"gamelist"
					},
					{
						"achive",
						"achive"
					},
					{
						"task",
						"task"
					},
					{
						"skin",
						"skin"
					},
					{
						"activity",
						"activity"
					},
					{
						"setting",
						"setting"
					},
					{
						"G00103",
						"G00103"
					},
					{
						"G003",
						"G003"
					}
				};
				GameObject gameObject = Resources.Load("Prefabs/" + dictionary[type]) as GameObject;
				if (gameObject != null)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.transform.SetParent(this.panel_cotent.transform, false);
					gameObject2.name = type;
					this.nodes[type] = gameObject2;
				}
			}
			else
			{
				this.nodes[type].SetActive(true);
			}
			if (!type.Equals("main"))
			{
				this.DoPageOutAni();
			}
			else if (GM.GetInstance().GameId == 0)
			{
				this.DoPageInAni();
			}
			AudioManager.GetInstance().PlayEffect("sound_eff_button");
			return;
		}
		if (!type.Equals("main"))
		{
			return;
		}
		if (!this.nodes[type].activeSelf)
		{
			return;
		}
		GlobalEventHandle.EmitDoGoHome();
		this.DoPageInAni();
	}

	public void OnClickSetting()
	{
		GameObject obj = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/setting") as GameObject);
		DialogManager.GetInstance().show(obj, false);
        PauseManager.instance.Pause();
	}

	public void OnClickExpButton(int value)
	{
		PlayerPrefs.DeleteAll();
	}
	public void ClearLocalData()
	{
		PlayerPrefs.DeleteAll();
		GM.GetInstance().Init();
		this.InitUI();
	}

	private void LoadData()
	{
		this.nodes.Add("shop", null);
		this.nodes.Add("main", base.transform.Find("panel_content/gamelist").gameObject);
		this.nodes.Add("achive", null);
		this.nodes.Add("task", null);
		this.nodes.Add("skin", null);
		this.nodes.Add("activity", null);
		this.nodes.Add("setting", null);
		this.nodes.Add("G00103", null);
		this.nodes.Add("G003", null);
		this.m_nodesPositions.Add("buttons", this.m_img_buttons.anchoredPosition);
		this.m_nodesPositions.Add("start", this.m_btn_start.anchoredPosition);
		this.m_nodesPositions.Add("skin", this.m_btn_skin.anchoredPosition);
		UnityEngine.Debug.Log(this.m_btn_skin.anchoredPosition);
		this.m_nodesPositions.Add("task", this.m_btn_task.anchoredPosition);
		this.m_nodesPositions.Add("activity", this.m_btn_activity.anchoredPosition);
		this.m_nodesPositions.Add("achiev", this.m_btn_achiev.anchoredPosition);
	}

	private void InitUI()
	{
		
		this.LoadDiamondUI();
		this.LoadExpUI();
		this.PlayGENTipAni();
		if (GM.GetInstance().isSavedGame())
		{
			this.HideOther();
		}
	}

	private void InitEvent()
	{
		GlobalEventHandle.GetDiamondHandle += new Action<int, bool>(this.OnGetDiamond);
		GlobalEventHandle.ConsumeDiamondHandle += new Action<int>(this.OnConsumeGEM);
		GlobalEventHandle.AddExpHandle += new Action<bool>(this.OnAddExp);
		GlobalEventHandle.DoClickBottom += new Action(this.OnDoClickBottomButton);
		GlobalEventHandle.OnClickPageButtonHandle += new Action<string, int>(this.OnClickPageButton);
		GlobalEventHandle.DoUseProps += new Action<bool, List<GameObject>>(this.OnDoUseProps);	
		this.gameview.OnClickHandle = new Action<int>(this.OnClickStartGame);
	}

	private void DestroyEvent()
	{
		GlobalEventHandle.GetDiamondHandle -= new Action<int, bool>(this.OnGetDiamond);
		GlobalEventHandle.ConsumeDiamondHandle -= new Action<int>(this.OnConsumeGEM);
		GlobalEventHandle.AddExpHandle -= new Action<bool>(this.OnAddExp);
		GlobalEventHandle.DoClickBottom -= new Action(this.OnDoClickBottomButton);
		GlobalEventHandle.DoUseProps -= new Action<bool, List<GameObject>>(this.OnDoUseProps);
	}

	private void LoadDiamondUI()
	{
		this.txt_diamond.GetComponent<OverlayNumber>().SetStartNumber(GM.GetInstance().Diamond);
	}

	private void LoadExpUI()
	{
		this.txt_lv.GetComponent<Text>().text = GM.GetInstance().Lv.ToString();
		if (Configs.TPlayers.ContainsKey(GM.GetInstance().Lv.ToString()))
		{
			TPlayer tPlayer = Configs.TPlayers[GM.GetInstance().Lv.ToString()];
			this.img_exp.GetComponent<Image>().fillAmount = (((float)GM.GetInstance().Exp / (float)tPlayer.Exp >= 1f) ? 1f : ((float)GM.GetInstance().Exp / (float)tPlayer.Exp));
		}
	}

	private void OnGetDiamond(int num, bool isPlayAni)
	{
		if (isPlayAni)
		{
			int toNum = GM.GetInstance().Diamond;
			this.PlayGEMAni(delegate
			{
				this.txt_diamond.GetComponent<OverlayNumber>().setNum(toNum);
			});
		}
		else
		{
			this.txt_diamond.GetComponent<OverlayNumber>().setNum(GM.GetInstance().Diamond);
		}
		AudioManager.GetInstance().PlayEffect("sound_eff_coin");
	}

	private void OnConsumeGEM(int num)
	{
		this.txt_diamond.GetComponent<OverlayNumber>().setNum(GM.GetInstance().Diamond);
		AudioManager.GetInstance().PlayEffect("sound_eff_coin");
	}

	private void OnAddExp(bool isLevelUp)
	{
		this.LoadExpUI();
		if (!isLevelUp)
		{
			return;
		}
		if (GM.GetInstance().GameId == 2)
		{
			M002.GetInstance().IsPause = true;
		}
      
        Debug.Log("ShowLevelUp();");
		this.ShowLevelUp();
	}

	private void OnDoClickBottomButton()
	{
		this.onClickBottom("main");
	}

	private void OnClickPageButton(string name, int value)
	{
		if (name.Equals("Game01"))
		{
			this.curBtnType = "main";
			this.OnClickStartGame(1);
			return;
		}
		if (name.Equals("Game03"))
		{
			this.curBtnType = "main";
			this.OnClickStartGame(value);
			return;
		}
		this.onClickBottom(name);
	}

	private void OnDoUseProps(bool isDel, List<GameObject> objs)
	{
		this.panel_handle.SetActive(!isDel);
		if (isDel)
		{
			return;
		}
		using (List<GameObject>.Enumerator enumerator = objs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				enumerator.Current.transform.SetParent(this.panel_handle.transform, true);
			}
		}
		Transform transform = this.panel_handle.transform.Find("txt_tips");
		Text component = transform.GetComponent<Text>();
		if (transform == null)
		{
			return;
		}
		DOTween.Kill(transform, false);
		Sequence expr_86 = DOTween.Sequence();
		expr_86.Append(component.DOFade(1f, 0f));
		expr_86.Append(component.DOFade(0.5f, 1f));
		expr_86.Append(component.DOFade(1f, 1f));
		expr_86.SetLoops(-1);
		expr_86.SetTarget(transform);
		string[] array = new string[]
		{
            "use_bomb_to_remove_one_square",
            "use_hammer_to_remove_cross_squares",
            "use_a_star_to_remove_squares_with_the_same_number"
        };
		if (GM.GetInstance().GameId == 1)
		{
			component.text = Localisation.GetString(array[M001.GetInstance().CurPropId - 1]);
		}
	}


	private void ShowSubscriptionAwards(int day, int value)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/subscriptionAwards") as GameObject);
		gameObject.GetComponent<SubscriptionDialog>().Load(day, value);
		DialogManager.GetInstance().show(gameObject, false);
		AudioManager.GetInstance().PlayEffect("sound_eff_button");
	}

	private void PlayGEMAni(Action callback)
	{
		GameObject asset = Resources.Load("Prefabs/effect/eff_gem") as GameObject;
		System.Random rd = new System.Random();
		Sequence arg_50_0 = DOTween.Sequence();
		Sequence actions = DOTween.Sequence();
		arg_50_0.AppendCallback(delegate
		{
			GameObject prefab = UnityEngine.Object.Instantiate<GameObject>(asset);
			prefab.transform.SetParent(this.panel_maxTop.transform, false);
			prefab.transform.localPosition = new Vector3((float)rd.Next(-80, 80), (float)rd.Next(0, 80), 0f);
			prefab.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			Vector3 position = this.icon_gem.transform.position;
			Tweener t = prefab.transform.DOMove(position, 0.8f, false).SetEase(Ease.InBack).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(prefab);
			});
			actions.Insert(0f, t);
		}).AppendInterval(0.01f).SetLoops(10);
		actions.OnComplete(delegate
		{
			Action expr_06 = callback;
			if (expr_06 == null)
			{
				return;
			}
			expr_06();
		});
	}

	private void PlayGENTipAni()
	{
		Sequence expr_05 = DOTween.Sequence();
		expr_05.Append(this.icon_gem.transform.DOScale(1.1f, 1f).SetEase(Ease.Linear));
		expr_05.Append(this.icon_gem.transform.DOScale(1f, 1f).SetEase(Ease.Linear));
		expr_05.SetLoops(-1);
	}

	

	private void ShowLevelUp()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/LevelUp") as GameObject);
		DialogManager.GetInstance().show(gameObject, false);
		gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/effect/eff_levelup") as GameObject);
		gameObject.transform.SetParent(base.transform, false);
		UnityEngine.Object.Destroy(gameObject, 5f);
		AudioManager.GetInstance().PlayEffect("sound_eff_levelUp");
	}

	public void DoPageOutAni()
	{
		float num = 1000f;
		Vector2 vector = new Vector2(this.m_nodesPositions["buttons"].x - 500f, this.m_nodesPositions["buttons"].y);
		float duration = Math.Abs(vector.x - this.m_nodesPositions["buttons"].x) / num;
		duration = 0f;
		DOTween.Kill(this.m_img_buttons, false);
		DOTween.To(() => this.m_img_buttons.anchoredPosition, delegate(Vector2 xx)
		{
			this.m_img_buttons.anchoredPosition = xx;
		}, vector, duration).SetTarget(this.m_img_buttons);
		duration = 0f;
		
		vector = new Vector2(this.m_nodesPositions["skin"].x, this.m_nodesPositions["skin"].y - 300f);
		duration = Math.Abs(vector.y - this.m_nodesPositions["skin"].y) / num;
		duration = 0f;
		DOTween.Kill(this.m_btn_skin, false);
		DOTween.To(() => this.m_btn_skin.anchoredPosition, delegate(Vector2 xx)
		{
			this.m_btn_skin.anchoredPosition = xx;
		}, vector, duration).SetTarget(this.m_btn_skin);

		duration = 0f;
		
	}

	public void DoPageInAni()
	{
		float num = 1000f;
		float duration = Math.Abs(this.m_nodesPositions["buttons"].x - this.m_img_buttons.anchoredPosition.x) / num;
		DOTween.Kill(this.m_img_buttons, false);
		DOTween.To(() => this.m_img_buttons.anchoredPosition, delegate(Vector2 xx)
		{
			this.m_img_buttons.anchoredPosition = xx;
		}, this.m_nodesPositions["buttons"], duration).SetTarget(this.m_img_buttons);
		
		duration = Math.Abs(this.m_nodesPositions["skin"].y - this.m_btn_skin.anchoredPosition.y) / num;
		DOTween.Kill(this.m_btn_skin, false);
		DOTween.To(() => this.m_btn_skin.anchoredPosition, delegate(Vector2 xx)
		{
			this.m_btn_skin.anchoredPosition = xx;
		}, this.m_nodesPositions["skin"], duration).SetTarget(this.m_btn_skin);
		
	}

	public void HideOther()
	{
		Vector2 anchoredPosition = new Vector2(this.m_nodesPositions["buttons"].x - 500f, this.m_nodesPositions["buttons"].y);
		this.m_img_buttons.anchoredPosition = anchoredPosition;	
		anchoredPosition = new Vector2(this.m_nodesPositions["skin"].x, this.m_nodesPositions["skin"].y - 300f);
		this.m_btn_skin.anchoredPosition = anchoredPosition;
	}

	private void RunSplash()
	{
		AudioManager.GetInstance().audio_bg.mute = true;
		this.m_splash.SetActive(true);
		this.m_splash.GetComponent<Image>().DOFade(0f, 0.5f).OnComplete(delegate
		{
			AudioManager.GetInstance().audio_bg.mute = false;
			this.m_splash.SetActive(false);
		});
	}
}
