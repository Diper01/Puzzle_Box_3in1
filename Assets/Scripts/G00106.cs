using System;
using UnityEngine;

public class G00106 : MonoBehaviour
{
	public Action OnClickAdsHandle;

	public Action OnClickCloseHandle;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnClickADS()
	{
		Action expr_06 = this.OnClickAdsHandle;
		if (expr_06 != null)
		{
			expr_06();
		}
		DialogManager.GetInstance().Close(null);
	}

	public void OnClickClose()
	{
		Action expr_06 = this.OnClickCloseHandle;
		if (expr_06 != null)
		{
			expr_06();
		}
		DialogManager.GetInstance().Close(null);
	}
}
