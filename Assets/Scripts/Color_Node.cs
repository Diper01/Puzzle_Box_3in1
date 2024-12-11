using System;

public class Color_Node
{
	private int index;

	private int color;

	private int per;

	private int next;

	private M00301.Node_type type;

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

	public int Per
	{
		get
		{
			return this.per;
		}
		set
		{
			this.per = value;
		}
	}

	public int Next
	{
		get
		{
			return this.next;
		}
		set
		{
			this.next = value;
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

	public Color_Node(int index, int color)
	{
		this.Index = index;
		this.Color = color;
		this.Next = index;
		this.Per = index;
	}

	public Color_Node(int index, int color, M00301.Direction diec, M00301.Node_type type)
	{
		this.Index = index;
		this.Color = color;
		this.Type = type;
		this.Next = index;
		this.Per = index;
	}
}
