using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
	public class FacebookUtil
	{
		public static int Share(Action finishCallback = null, Action cancelCallback = null)
		{
			int result = 0;
			RuntimePlatform platform = Application.platform;
			if (platform != RuntimePlatform.IPhonePlayer)
			{
				if (platform == RuntimePlatform.Android)
				{
					Utils.CallAndroiSDKdFunc<int>("FacebookHelper", "share", out result, Array.Empty<object>());
				}
				else
				{
					result = 0;
				}
			}
			else
			{
				result = 0;
			}
			JavaInvokeCShape.GetInstance().OnShareHandle = finishCallback;
			return result;
		}
	}
}
