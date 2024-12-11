using UnityEngine;
using System.Collections;
using System;

public class LocalisedString : MonoBehaviour {

	public AdditionalSettingForLanguage[] AdditionalSettingForLanguages;
	TextMesh CurrentTextMesh;

	// Use this for initialization
	void Start () {
		CurrentTextMesh = transform.GetComponent<TextMesh> ();
		LocaliseString ();
		ApplyAdditionalStringSetting ();
	}

	void LocaliseString(){
		string CurrentLocalisedString = Localisation.GetString (CurrentTextMesh.text);
		CurrentTextMesh.text = CurrentLocalisedString;
	}

	void ApplyAdditionalStringSetting(){
		if(AdditionalSettingForLanguages.Length > 0){
			for(int i = 0;i < AdditionalSettingForLanguages.Length; i++){
				if(Localisation.GetCurrentLanguage() == AdditionalSettingForLanguages[i].Language){
					CurrentTextMesh.fontSize =  AdditionalSettingForLanguages[i].FontSize;
					if(AdditionalSettingForLanguages[i].FontFile != null){
					CurrentTextMesh.font = AdditionalSettingForLanguages[i].FontFile;
					CurrentTextMesh.GetComponent<Renderer>().sharedMaterial = AdditionalSettingForLanguages[i].FontFile.material;

					}
				}
			}
		}
	}
}

[Serializable]
public class AdditionalSettingForLanguage{
	public Languages Language;
	public int FontSize;
	public Font FontFile;
}
