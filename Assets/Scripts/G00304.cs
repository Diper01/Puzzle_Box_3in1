using Assets.Scripts.GameManager;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class G00304 : MonoBehaviour
{
	public Image img_block;

	[SerializeField]
	public List<Color> colors = new List<Color>();

	public List<Color> colors2 = new List<Color>();

	private int index;

	private int row;

	private int col;

	private int color;

	private float width;

	private float height;

	public int Index
	{
		get
		{
			return this.index;
		}
		set
		{
			this.index = value;
		}
	}

	public int Row
	{
		get
		{
			return this.row;
		}
		set
		{
			this.row = value;
		}
	}

	public int Col
	{
		get
		{
			return this.col;
		}
		set
		{
			this.col = value;
		}
	}

	public int Color
	{
		get
		{
			return this.color;
		}
		set
		{
			this.color = value;
		}
	}

	public float Width
	{
		get
		{
			return this.width;
		}
		set
		{
			this.width = value;
		}
	}

	public float Height
	{
		get
		{
			return this.height;
		}
		set
		{
			this.height = value;
		}
	}

	public G00304(int idx, int color)
	{
		this.index = idx;
		this.color = color;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Init(int idx, int color)
	{
		this.index = idx;
		this.color = color;
		this.SetPosition(idx);
		this.SetColor(color);
	}

	public void SetContentSize(float x, float y)
	{
		this.SetContentSize(new Vector2(x, y));
	}

	public void SetContentSize(Vector2 v)
	{
		this.Width = v.x;
		this.Height = v.y;
		this.img_block.GetComponent<RectTransform>().sizeDelta = v;
	}

	public void SetPosition(int index)
	{
		this.index = index;
		this.SetPosition(M00301.GetInstance().GetRow(index), M00301.GetInstance().GetCol(index));
	}

	public void SetPosition(int row, int col)
	{
		this.row = row;
		this.col = col;
		base.transform.localPosition = new Vector3((float)col * M00301.GetInstance().Cell_height + M00301.GetInstance().Cell_height / 2f - 282f, 281f - (float)row * M00301.GetInstance().Cell_width - M00301.GetInstance().Cell_width / 2f, 0f);
	}

	public void SetColor(int color)
	{
		int skinID = GM.GetInstance().SkinID;
		List<Color> list;
		if (skinID != 1)
		{
			if (skinID != 2)
			{
				list = this.colors;
			}
			else
			{
				list = this.colors2;
			}
		}
		else
		{
			list = this.colors;
		}
		Color arg_37_0 = list[color];
		this.Color = color;
		this.img_block.color = list[color];
	}
}
