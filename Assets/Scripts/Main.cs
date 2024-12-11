using Assets.Scripts.Configs;
using Assets.Scripts.GameManager;
using Assets.Scripts.Utils;
using System;
using UnityEngine;

public class Main : MonoBehaviour
{
	private void Awake()
	{
		this.InitializeGameConfig();
		Configs.LoadConfig();
		GM.GetInstance().Init();
		AchiveData.Initialize();
		TaskData.Initialize();
		LoginData.Initialize();
		GlobalTimer.Initialize();
	}

	private void Start()
	{
	}

	private void Update()
	{
		GlobalTimer.GetInstance().Update();
	}

	private void InitializeGameConfig()
	{
		Application.targetFrameRate = 60;
		Input.multiTouchEnabled = false;
	}
}
