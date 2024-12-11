using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum Languages {
	Undefined,
	Unknown,

	English, Russian, Ukrainian, French, Italian, Spanish, Arabic, Portuguese, German
	
	/*Polish,
	Czech,
	Belarusian,
	Chinese,
	ChineseSimplified,
	ChineseTraditional,

	Japanese,
	Korean,

	Afrikaans,
	
	Basque,
	Bulgarian,
	Catalan,
	Danish,
	Dutch,
	Estonian,
	Faroese,
	Finnish,
	Greek,
	Hebrew,
	Icelandic,
	Indonesian,
	Latvian,
	Lithuanian,
	Norwegian,
	
	Romanian,
	Slovak,
	Slovenian,
	Swedish,
	Thai,
	Turkish,
	Vietnamese,
	Hungarian*/
}

public static class Localisation{

	private static Languages _currentLanguage = Languages.Undefined;
	private static Dictionary<string,string> _strings;
	private static XmlDocument _loadedLanguageFile;
	private static bool _languageLoaded = false;
	private static TextAsset _newbyLanguage;

	public static void DetectLanguage()
	{

		_currentLanguage = (Languages)Enum.Parse(typeof(Languages), Application.systemLanguage.ToString());
		
#if UNITY_EDITOR
		if (EditorPrefs.HasKey("TestLanguage"))
		{
			_currentLanguage = (Languages)Enum.Parse(typeof(Languages), EditorPrefs.GetString("TestLanguage"));
		}
#endif
		Debug.Log("DetectedLanguage: " + _currentLanguage);
	}

	public static void LoadLanguage()
	{
		DetectLanguage();
		_loadedLanguageFile = new XmlDocument();
		_strings = new Dictionary<string, string>();

		Debug.Log("LoadLanguage: " + _currentLanguage);

		_newbyLanguage = (TextAsset)Resources.Load("Localisation/" + _currentLanguage.ToString(), typeof(TextAsset));
		if (_newbyLanguage == null) //if no localisation as system language, load english
		{
			_newbyLanguage = (TextAsset)Resources.Load("Localisation/English", typeof(TextAsset));
		}
		_loadedLanguageFile.LoadXml(_newbyLanguage.text);

		XmlElement root = _loadedLanguageFile.DocumentElement;
		XmlNodeList nodes = root.SelectNodes("//string"); //ignore all nodes but <string> 
		foreach (XmlNode node in nodes)
		{
			int i = 0;
			string key = node.Attributes["name"].Value;
			if (!_strings.ContainsKey(key))
				_strings.Add(key, node.InnerText);
			else
			{
				Debug.LogErrorFormat("An element with '{0}' key already exists in the localisation dictionary. Node number {1}", key, i);
				//throw new ArgumentException("An element with'"+key+"' key already exists in the dictionary");\
			}
			i++;

		}
		_languageLoaded = true;
	}

	public static Languages GetCurrentLanguage()
	{
		if (_languageLoaded == false)
		{
			LoadLanguage();
		}
		return _currentLanguage;
	}


	public static string GetString(string SearchString)
	{
		if (_languageLoaded == false)
		{
			LoadLanguage();
		}
		if (_strings.ContainsKey(SearchString))
		{
			return _strings[SearchString];
		}
		else
		{
			Debug.LogError("Unknown string: '" + SearchString + "'");
			return SearchString;
			//TODO: make better way to show missing alias
		}

	}
}