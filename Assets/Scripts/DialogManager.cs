using Assets.Scripts.GameManager;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
	private struct DialogData
	{
		public GameObject parent;

		public GameObject child;

		public bool isIgnoreBack;

		public DialogData(GameObject parent, GameObject child, bool isIgnoreBack = false)
		{
			this.parent = parent;
			this.child = child;
			this.isIgnoreBack = isIgnoreBack;
		}
	}

	private static DialogManager Instance;

	private Stack<DialogManager.DialogData> m_childs = new Stack<DialogManager.DialogData>();

	private GameObject m_mask;

	public GameObject m_prefab_mask;

	public Transform m_parent;

	private void Awake()
	{
		DialogManager.Instance = this;
	}

	private void Start()
	{
		this.Init();
	}

	private void Update()
	{
	}

	public static DialogManager GetInstance()
	{
		return DialogManager.Instance;
	}

	public void show(GameObject obj, bool isIgnoreBack = false)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_prefab_mask, this.m_parent);
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		obj.transform.SetParent(gameObject.transform, false);
		obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		Tween t = obj.transform.DOScale(1.1f, 0.2f);
		Tween t2 = obj.transform.DOScale(1f, 0.1f).SetEase(Ease.InOutBack);
		Sequence expr_9A = DOTween.Sequence();
		expr_9A.Append(t);
		expr_9A.Append(t2);
		expr_9A.SetTarget(gameObject);
		this.m_childs.Push(new DialogManager.DialogData(gameObject, obj, isIgnoreBack));
		AudioManager.GetInstance().PlayEffect("sound_eff_popup");
	}

	public void Close(Action callfunc = null)
	{
		if (this.m_childs.Count <= 0)
		{
			return;
		}
		DialogManager.DialogData expr_1A = this.m_childs.Pop();
		expr_1A.child.transform.DOKill(false);
		DOTween.Kill(expr_1A.parent, false);
		UnityEngine.Object.Destroy(expr_1A.parent);
		if (callfunc != null)
		{
			callfunc();
		}
	}

	public bool IsShow()
	{
		return this.m_childs.Count > 0;
	}

	public bool isIgnoreBack()
	{
		return this.m_childs.Peek().isIgnoreBack;
	}

	private void Init()
	{
		this.m_mask = UnityEngine.Object.Instantiate<GameObject>(this.m_prefab_mask, this.m_parent);
		this.m_mask.SetActive(false);
		this.m_mask.transform.localPosition = new Vector3(0f, 0f, 0f);
	}
}
