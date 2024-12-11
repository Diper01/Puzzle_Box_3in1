using System;
using System.Collections.Generic;

namespace Assets.Scripts.Configs
{
	[Serializable]
	internal class Wrapper<T>
	{
		public List<T> Array;
	}
}
