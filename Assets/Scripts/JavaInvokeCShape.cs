using System;
using UnityEngine;

public class JavaInvokeCShape : MonoBehaviour
{
	private static JavaInvokeCShape m_instance;

	public Action OnAdsComplateHandle;

	public Action OnAdsCancelHandle;

	public Action OnShareHandle;

	private void Awake()
	{
		JavaInvokeCShape.m_instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public static JavaInvokeCShape GetInstance()
	{
		return JavaInvokeCShape.m_instance;
	}

	public void OnAdsComplateCallback(string channel)
	{
		Action expr_06 = this.OnAdsComplateHandle;
		if (expr_06 == null)
		{
			return;
		}
		expr_06();
	}

	public void OnAdsCancelCallback(string channel)
	{
		Action expr_06 = this.OnAdsCancelHandle;
		if (expr_06 == null)
		{
			return;
		}
		expr_06();
	}

	public void OnShareSuccess(string channel)
	{
		ToastManager.Show("share sucess", false);
		Action expr_11 = this.OnShareHandle;
		if (expr_11 == null)
		{
			return;
		}
		expr_11();
	}
}
