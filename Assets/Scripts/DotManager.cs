using Assets.Scripts.GameManager;
using System;
using UnityEngine;

public class DotManager : MonoBehaviour
{
	public GameObject m_achievDot;

	public GameObject m_loginDot;

	public GameObject m_taskDot;

	private static DotManager m_instance;

	private void Awake()
	{
		DotManager.m_instance = this;
	}

	private void Start()
	{
		this.Check();
	}

	private void Update()
	{
	}

	public static DotManager GetInstance()
	{
		return DotManager.m_instance;
	}

	public void Check()
	{
		this.CheckAchiev();
		this.CheckTask();
	}

	public void CheckAchiev()
	{
		bool active = false;
		for (int i = 1; i <= 6; i++)
		{
			if (AchiveData.GetInstance().Get(i).status == -2)
			{
				active = true;
				break;
			}
		}
		this.m_achievDot.SetActive(active);
	}

	public void CheckTask()
	{
		bool active = false;
		int[] array = new int[]
		{
			100101,
			100102,
			100103,
			100104,
			100105
		};
		for (int i = 0; i < array.Length; i++)
		{
			int type = array[i];
			if (TaskData.GetInstance().Get(type).status == -2)
			{
				active = true;
				break;
			}
		}
		this.m_taskDot.SetActive(active);
	}

	public void CheckLogin()
	{
		bool active = false;
		int[] signInData = LoginData.GetInstance().GetSignInData();
		for (int i = 0; i < signInData.Length; i++)
		{
			if (signInData[i] == 1)
			{
				active = true;
				break;
			}
		}
		this.m_loginDot.SetActive(active);
	}
}
