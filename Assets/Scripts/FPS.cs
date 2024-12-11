using System;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
	public Text lb_fps;

	private float fpsbydt = 1.5f;

	private float passetime;

	private int framecount;

	private float realtimefps;

	private void Start()
	{
		this.setFPS();
	}

	private void Update()
	{
		this.showFPS();
	}

	private void setFPS()
	{
		Application.targetFrameRate = 60;
	}

	private void showFPS()
	{
		this.framecount++;
		this.passetime += Time.deltaTime;
		if (this.passetime >= this.fpsbydt)
		{
			this.realtimefps = (float)this.framecount / this.passetime;
			this.lb_fps.text = "FPS:" + this.realtimefps;
			this.passetime = 0f;
			this.framecount = 0;
		}
	}
}
