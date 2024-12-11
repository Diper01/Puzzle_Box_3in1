using UnityEngine;
using UnityEngine.UI;
using System;


public class LocalisedUIText : MonoBehaviour {

	[Serializable]
	private struct AdditionalSettingForUIText
	{
		public Languages language;
		public Font fontFile;
	}


    private Text currentText;

    [SerializeField] AdditionalSettingForUIText[] additionalSettingForUIText;

	private void Start ()
	{
		Text text = GetComponent<Text>();
		if (text != null)
		{
			currentText = text;
			LocaliseString();
			ApplyAdditionalStringSetting();
		}
	}
	
	private void LocaliseString()
	{
		currentText.text = Localisation.GetString (currentText.text);
	}

	private void ApplyAdditionalStringSetting()
	{
		if (additionalSettingForUIText.Length > 0)
		{
			for (int i = 0; i < additionalSettingForUIText.Length; i++)
			{
				if (Localisation.GetCurrentLanguage() == additionalSettingForUIText[i].language)
				{
					if (additionalSettingForUIText[i].fontFile != null)
					{
						currentText.font = additionalSettingForUIText[i].fontFile;
					}
				}
			}
		}
	}
}

