using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class G00302 : MonoBehaviour
{
	public Image img_block;

	public Image img_star;

	[SerializeField]
	public List<Sprite> sprites = new List<Sprite>();

	[SerializeField]
	public List<Color> colors = new List<Color>();

	private int index;

	private int color;

	private M00301.Node_type type;

	private M00301.Direction direction;

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

	public M00301.Node_type Type
	{
		get
		{
			return this.type;
		}
		set
		{
			this.type = value;
		}
	}

	public M00301.Direction Direction
	{
		get
		{
			return this.direction;
		}
		set
		{
			this.direction = value;
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

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Init(int idx, int color, M00301.Node_type type)
	{
		this.Index = idx;
		this.Color = color;
		this.Type = type;
		this.SetPosition(idx);
		this.SetColor(color);
		this.SetType(type);
		this.img_star.gameObject.SetActive(false);
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
		this.SetPosition(M00301.GetInstance().GetRow(index), M00301.GetInstance().GetCol(index));
	}

	public void SetPosition(int row, int col)
	{
		base.transform.localPosition = new Vector3((float)col * M00301.GetInstance().Cell_height + M00301.GetInstance().Cell_height / 2f - 282f, 281f - (float)row * M00301.GetInstance().Cell_width - M00301.GetInstance().Cell_width / 2f, 0f);
	}

	public void SetType(M00301.Node_type idx)
	{
		if (this.sprites[(int)idx] != null)
		{
			this.Type = idx;
			this.img_block.sprite = this.sprites[(int)idx];
		}
		this.ReloadBlock();
	}

	public void SetColor(int idx)
	{
		Color arg_0C_0 = this.colors[idx];
		this.Color = idx;
		this.img_block.color = this.colors[idx];
	}

	public void SetDirection(M00301.Direction diec)
	{
		this.Direction = diec;
		this.ReloadBlock();
	}

	public void ShowStar()
	{
		this.img_star.gameObject.SetActive(true);
	}

	public void HideStar()
	{
		this.img_star.gameObject.SetActive(false);
	}

	public void ReloadBlock()
	{
		if (this.Type == M00301.Node_type.SEGMENT)
		{
			Transform transform = this.img_block.transform;
			switch (this.Direction)
			{
			case M00301.Direction.UP:
				this.img_block.sprite = this.sprites[3];
				transform.GetComponent<RectTransform>().sizeDelta = new Vector2(this.width, this.height + 15f * (this.height / 100f));
				transform.localPosition = new Vector3(0f, -this.Height / 2f, -1f);
				return;
			case M00301.Direction.RIGHT:
				this.img_block.sprite = this.sprites[2];
				transform.GetComponent<RectTransform>().sizeDelta = new Vector2(this.width + 15f * (this.width / 100f), this.height);
				transform.localPosition = new Vector3(-this.Width / 2f, 0f, -1f);
				return;
			case M00301.Direction.DOWN:
				this.img_block.sprite = this.sprites[3];
				transform.GetComponent<RectTransform>().sizeDelta = new Vector2(this.width, this.height + 15f * (this.height / 100f));
				transform.localPosition = new Vector3(0f, this.Height / 2f, -1f);
				return;
			case M00301.Direction.LEFT:
				this.img_block.sprite = this.sprites[2];
				transform.GetComponent<RectTransform>().sizeDelta = new Vector2(this.width + 15f * (this.width / 100f), this.height);
				transform.localPosition = new Vector3(this.Width / 2f, 0f, -1f);
				return;
			default:
				transform.GetComponent<RectTransform>().sizeDelta = new Vector2(this.width, this.height);
				transform.localPosition = new Vector3(0f, 0f, -1f);
				break;
			}
		}
	}

	public Vector3 GetPosition()
	{
		return base.transform.localPosition;
	}
}
