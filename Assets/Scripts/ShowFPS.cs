using System;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
	public float f_UpdateInterval = 0.5f;

	private float f_LastInterval;

	private int i_Frames;

	private float f_Fps;

	private void Start()
	{
		this.f_LastInterval = Time.realtimeSinceStartup;
		this.i_Frames = 0;
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(0f, 100f, 200f, 200f), "FPS:" + this.f_Fps.ToString("f2"));
	}

	private void Update()
	{
		this.i_Frames++;
		if (Time.realtimeSinceStartup > this.f_LastInterval + this.f_UpdateInterval)
		{
			this.f_Fps = (float)this.i_Frames / (Time.realtimeSinceStartup - this.f_LastInterval);
			this.i_Frames = 0;
			this.f_LastInterval = Time.realtimeSinceStartup;
		}
	}
}
