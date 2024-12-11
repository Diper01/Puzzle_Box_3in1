using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NumberClear : MonoBehaviour
{
	private sealed class _ClearCoroutine_d__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int __1__state;

		private object __2__current;

		public NumberClear __4__this;

		object IEnumerator<object>.Current
		{
			get
			{
				return this.__2__current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this.__2__current;
			}
		}

		public _ClearCoroutine_d__5(int __1__state)
		{
			this.__1__state = __1__state;
		}

		void IDisposable.Dispose()
		{
		}

		bool IEnumerator.MoveNext()
		{
			int num = this.__1__state;
			NumberClear numberClear = this.__4__this;
			if (num == 0)
			{
				this.__1__state = -1;
				this.__2__current = new WaitForSeconds(0.1f);
				this.__1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.__1__state = -1;
			UnityEngine.Object.Destroy(numberClear.gameObject);
			return false;
		}

		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	private bool isClearing;

	protected NumberObj numberObj;

	public bool IsClearing
	{
		get
		{
			return this.isClearing;
		}
	}

	public virtual void Clear()
	{
		this.isClearing = true;
		base.StartCoroutine(this.ClearCoroutine());
	}

//	[IteratorStateMachine(typeof(NumberClear._003CClearCoroutine_003Ed__5))]
	private IEnumerator ClearCoroutine()
	{
		int num = 0;
		while (num == 0)
		{
			yield return new WaitForSeconds(0.1f);
		}
		if (num != 1)
		{
			yield break;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}
}
