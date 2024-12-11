using System;
using UnityEngine;
using UnityEngine.UI;

public class OverlayNumber : MonoBehaviour
{
	private int endNumber;

	private int startNumber;

	private int step;

	private const int MAX_STEP = 30;

	public string m_formatString = "{0}";

	public int StartNumber
	{
		get
		{
			return this.startNumber;
		}
		set
		{
			this.startNumber = value;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.startNumber == this.endNumber)
		{
			return;
		}
		if (this.step < 0)
		{
			this.StartNumber += this.step;
			this.StartNumber = ((this.StartNumber < this.endNumber) ? this.endNumber : this.StartNumber);
		}
		else
		{
			this.StartNumber += this.step;
			this.StartNumber = ((this.StartNumber > this.endNumber) ? this.endNumber : this.StartNumber);
		}
		base.GetComponent<Text>().text = string.Format((this.startNumber < 1000) ? "{0}" : this.m_formatString, this.StartNumber);
	}

	public void Reset()
	{
		this.endNumber = 0;
		this.startNumber = 0;
		Text component = base.GetComponent<Text>();
		if (component == null)
		{
			return;
		}
		component.text = "0";
	}

	public void setNum(int endNumber)
	{
		if (base.GetComponent<Text>() == null)
		{
			return;
		}
		this.endNumber = endNumber;
		this.DoScoll();
	}

	public void SetStartNumber(int number = 0)
	{
		this.StartNumber = number;
		this.endNumber = number;
		base.GetComponent<Text>().text = string.Format((this.startNumber < 1000) ? "{0}" : this.m_formatString, this.StartNumber);
	}

	private void DoScoll()
	{
		int num = this.endNumber - this.StartNumber;
		if (num == 0)
		{
			return;
		}
		this.step = (int)Math.Ceiling((double)((float)num / 30f));
	}
}
