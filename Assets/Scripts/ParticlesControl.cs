using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesControl : MonoBehaviour
{
	private GameObject explodeGObj;

	private List<G00101> blocks;

	private int currentChooseType;

	private Sequence actions;

	private bool isRunninig;

	private static ParticlesControl psControl;

	private List<Sequence> m_actions = new List<Sequence>();

	private void Awake()
	{
		ParticlesControl.psControl = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void SetEffct1()
	{
		int num = 0;
		foreach (G00101 current in this.blocks)
		{
			num++;
			if (this.isRunninig)
			{
				this.m_actions[num - 1].Kill(false);
				current.transform.localScale = Vector3.one;
				if (num == this.blocks.Count)
				{
					this.isRunninig = false;
					this.m_actions.Clear();
				}
			}
			else
			{
				Sequence sequence = DOTween.Sequence();
				Tween t = current.transform.DOScale(new Vector3(0.95f, 0.95f, 0.5f), 0.5f);
				Tween t2 = current.transform.DOScale(new Vector3(1f, 1f, 0.5f), 0.5f);
				sequence.Append(t);
				sequence.Append(t2);
				sequence.SetLoops(-1);
				this.m_actions.Add(sequence);
				if (num == this.blocks.Count)
				{
					this.isRunninig = true;
				}
			}
		}
	}

	private void SetEffct2()
	{
	}

	public static ParticlesControl GetInstance()
	{
		return ParticlesControl.psControl;
	}

	public void PlayExplodeEffic(Vector3 pos, Color color)
	{
		GameObject expr_14 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/effect/eff_comp")) as GameObject;
		expr_14.transform.SetParent(base.transform.parent.transform);
		expr_14.transform.Find("child").GetComponent<ParticleSystem>().startColor = color;
		expr_14.SetActive(true);
		expr_14.transform.position = pos;
		expr_14.GetComponent<ParticleSystem>().Play();
		UnityEngine.Object.Destroy(expr_14, 2f);
	}

	public void PlayExplodeEffic(List<Vector3> poses, List<Color> colors)
	{
		for (int i = poses.Count; i > 0; i--)
		{
			this.PlayExplodeEffic(poses[i], colors[i]);
		}
	}

	public void PlayChooseAllEffic(List<G00101> _blocks, int type)
	{
		this.blocks = _blocks;
		this.currentChooseType = type;
		switch (type)
		{
		case 1:
			this.SetEffct1();
			return;
		case 2:
			this.SetEffct1();
			return;
		case 3:
			this.SetEffct1();
			return;
		default:
			return;
		}
	}

	public void StopChooseAllEffic()
	{
		if (this.currentChooseType <= 0)
		{
			return;
		}
		switch (this.currentChooseType)
		{
		case 1:
			this.SetEffct1();
			return;
		case 2:
			this.SetEffct1();
			return;
		case 3:
			this.SetEffct1();
			return;
		default:
			return;
		}
	}
}
