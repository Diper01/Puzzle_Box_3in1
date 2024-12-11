using System;
using System.Collections.Generic;
using UnityEngine;

public class NumberControl : MonoBehaviour
{
	public enum NmberDigit
	{
		ONE,
		TWO,
		THREE,
		FOUR,
		FIVE,
		SIX,
		SEVEN,
		EIGHT,
		NIGHT,
		TEN,
		TEN_ONE,
		TEN_TWO,
		TEN_THREE,
		TEN_FOUR,
		TEN_FIVE,
		TEN_SIX,
		TEN_SEVEN,
		TEN_EIGHT,
		TEN_NIGHT,
		TEN_TEN,
		ADD_ONE,
		ADD_TWO,
		ADD_THREE
	}

	[Serializable]
	public struct NumberSprite
	{
		public NumberControl.NmberDigit number;

		public Sprite sprite;
	}

	public NumberControl.NumberSprite[] numberSpriteArry;

	private Dictionary<NumberControl.NmberDigit, Sprite> numberSpriteDict;

	private SpriteRenderer sprite;

	private NumberControl.NmberDigit number;

	public NumberControl.NmberDigit Number1
	{
		get
		{
			return this.number;
		}
		set
		{
			this.SetNumber(value);
		}
	}

	public void SetNumber(NumberControl.NmberDigit newNumber)
	{
		this.number = newNumber;
		if (this.numberSpriteDict.ContainsKey(newNumber))
		{
			this.sprite.sprite = this.numberSpriteDict[newNumber];
		}
	}

	public int GetNumber()
	{
		switch (this.number)
		{
		case NumberControl.NmberDigit.ONE:
			return 1;
		case NumberControl.NmberDigit.TWO:
			return 2;
		case NumberControl.NmberDigit.THREE:
			return 3;
		case NumberControl.NmberDigit.FOUR:
			return 4;
		case NumberControl.NmberDigit.FIVE:
			return 5;
		case NumberControl.NmberDigit.SIX:
			return 6;
		case NumberControl.NmberDigit.SEVEN:
			return 7;
		case NumberControl.NmberDigit.EIGHT:
			return 8;
		case NumberControl.NmberDigit.NIGHT:
			return 9;
		case NumberControl.NmberDigit.TEN:
			return 10;
		case NumberControl.NmberDigit.TEN_ONE:
			return 11;
		case NumberControl.NmberDigit.TEN_TWO:
			return 12;
		case NumberControl.NmberDigit.TEN_THREE:
			return 13;
		case NumberControl.NmberDigit.TEN_FOUR:
			return 14;
		case NumberControl.NmberDigit.TEN_FIVE:
			return 15;
		case NumberControl.NmberDigit.TEN_SIX:
			return 16;
		case NumberControl.NmberDigit.TEN_SEVEN:
			return 17;
		case NumberControl.NmberDigit.TEN_EIGHT:
			return 18;
		case NumberControl.NmberDigit.TEN_NIGHT:
			return 19;
		case NumberControl.NmberDigit.TEN_TEN:
			return 20;
		case NumberControl.NmberDigit.ADD_ONE:
			return 1;
		case NumberControl.NmberDigit.ADD_TWO:
			return 2;
		case NumberControl.NmberDigit.ADD_THREE:
			return 3;
		default:
			return 0;
		}
	}

	internal int GetNumber(Game.NumType type)
	{
		throw new NotImplementedException();
	}

	private void Awake()
	{
		this.sprite = base.transform.Find("number").GetComponent<SpriteRenderer>();
		this.numberSpriteDict = new Dictionary<NumberControl.NmberDigit, Sprite>();
		for (int i = 0; i < this.numberSpriteArry.Length; i++)
		{
			if (!this.numberSpriteDict.ContainsKey(this.numberSpriteArry[i].number))
			{
				this.numberSpriteDict.Add(this.numberSpriteArry[i].number, this.numberSpriteArry[i].sprite);
			}
		}
	}
}
