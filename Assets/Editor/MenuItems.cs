using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class MenuItems
{
    [MenuItem("Localisation/Reset to system language", false, -10)]
    private static void ClearTestLanguage()
    {
        EditorPrefs.DeleteKey("TestLanguage");
    }
    //Переключение языков из меню
    [MenuItem("Localisation/Undefined",false,1)]
	private static void SetLanguageUndefined()
	{
		EditorPrefs.SetString("TestLanguage","Undefined");
	}
	[MenuItem("Localisation/Unknown",false,1)]
	private static void SetLanguageUnknown()
	{
		EditorPrefs.SetString("TestLanguage","Unknown");
	}
	[MenuItem("Localisation/Russian",false,12)]
	private static void SetLanguageRU()
	{
		EditorPrefs.SetString("TestLanguage","Russian");
	}
	[MenuItem("Localisation/Ukrainian",false,12)]
	private static void SetLanguageUA()
	{
		EditorPrefs.SetString("TestLanguage","Ukrainian");
	}
	[MenuItem("Localisation/Belarusian",false,12)]
	private static void SetLanguageBR()
	{
		EditorPrefs.SetString("TestLanguage","Belarusian");
	}
	[MenuItem("Localisation/English",false,23)]
	private static void SetLanguageEN()
	{
		EditorPrefs.SetString("TestLanguage","English");
	}
	[MenuItem("Localisation/Italian",false,23)]
	private static void SetLanguageIT()
	{
		EditorPrefs.SetString("TestLanguage","Italian");
	}
	[MenuItem("Localisation/Spanish",false,23)]
	private static void SetLanguageSP()
	{
		EditorPrefs.SetString("TestLanguage","Spanish");
	}
	[MenuItem("Localisation/French",false,23)]
	private static void SetLanguageFR()
	{
		EditorPrefs.SetString("TestLanguage","French");
	}
	[MenuItem("Localisation/German",false,23)]
	private static void SetLanguageDE()
	{
		EditorPrefs.SetString("TestLanguage","German");
	}
	[MenuItem("Localisation/Polish",false,23)]
	private static void SetLanguagePL()
	{
		EditorPrefs.SetString("TestLanguage","Polish");
	}
	[MenuItem("Localisation/Czech",false,23)]
	private static void SetLanguageCZ()
	{
		EditorPrefs.SetString("TestLanguage","Czech");
	}
	[MenuItem("Localisation/Chinese",false,34)]
	private static void SetLanguageCN()
	{
		EditorPrefs.SetString("TestLanguage","Chinese");
	}
    [MenuItem("Localisation/Chinese Simplified", false, 34)]
    private static void SetLanguageCNs()
    {
        EditorPrefs.SetString("TestLanguage", "ChineseSimplified");
    }
    [MenuItem("Localisation/Chinese Traditional", false, 34)]
    private static void SetLanguageCNt()
    {
        EditorPrefs.SetString("TestLanguage", "ChineseTraditional");
    }
    [MenuItem("Localisation/Japanese",false,34)]
	private static void SetLanguageJP()
	{
		EditorPrefs.SetString("TestLanguage","Japanese");
	}
	[MenuItem("Localisation/Korean",false,34)]
	private static void SetLanguageKR()
	{
		EditorPrefs.SetString("TestLanguage","Korean");
	}
	[MenuItem("Localisation/Afrikaans")]
	private static void SetLanguageAF()
	{
		EditorPrefs.SetString("TestLanguage","Afrikaans");
	}
	[MenuItem("Localisation/Arabic")]
	private static void SetLanguageAR()
	{
		EditorPrefs.SetString("TestLanguage","Arabic");
	}
	[MenuItem("Localisation/Basque")]
	private static void SetLanguageBS()
	{
		EditorPrefs.SetString("TestLanguage","Basque");
	}
	[MenuItem("Localisation/Bulgarian")]
	private static void SetLanguageBG()
	{
		EditorPrefs.SetString("TestLanguage","Bulgarian");
	}
	[MenuItem("Localisation/Catalan")]
	private static void SetLanguageCT()
	{
		EditorPrefs.SetString("TestLanguage","Catalan");
	}
	[MenuItem("Localisation/Danish")]
	private static void SetLanguageDA()
	{
		EditorPrefs.SetString("TestLanguage","Danish");
	}
	[MenuItem("Localisation/Dutch")]
	private static void SetLanguageDC()
	{
		EditorPrefs.SetString("TestLanguage","Dutch");
	}
	[MenuItem("Localisation/Estonian")]
	private static void SetLanguageET()
	{
		EditorPrefs.SetString("TestLanguage","Estonian");
	}
	[MenuItem("Localisation/Faroese")]
	private static void SetLanguageFA()
	{
		EditorPrefs.SetString("TestLanguage","Faroese");
	}
	[MenuItem("Localisation/Finnish")]
	private static void SetLanguageFN()
	{
		EditorPrefs.SetString("TestLanguage","Finnish");
	}
	[MenuItem("Localisation/Greek")]
	private static void SetLanguageGR()
	{
		EditorPrefs.SetString("TestLanguage","Greek");
	}
	[MenuItem("Localisation/Hebrew")]
	private static void SetLanguageHR()
	{
		EditorPrefs.SetString("TestLanguage","Hebrew");
	}
	[MenuItem("Localisation/Icelandic")]
	private static void SetLanguageIC()
	{
		EditorPrefs.SetString("TestLanguage","Icelandic");
	}
	[MenuItem("Localisation/Indonesian")]
	private static void SetLanguageIN()
	{
		EditorPrefs.SetString("TestLanguage","Indonesian");
	}
	[MenuItem("Localisation/Latvian")]
	private static void SetLanguageLT()
	{
		EditorPrefs.SetString("TestLanguage","Latvian");
	}
	[MenuItem("Localisation/Lithuanian")]
	private static void SetLanguageLI()
	{
		EditorPrefs.SetString("TestLanguage","Lithuanian");
	}
	[MenuItem("Localisation/Norwegian")]
	private static void SetLanguageNO()
	{
		EditorPrefs.SetString("TestLanguage","Norwegian");
	}
	[MenuItem("Localisation/Portuguese")]
	private static void SetLanguagePR()
	{
		EditorPrefs.SetString("TestLanguage","Portuguese");
	}
	[MenuItem("Localisation/Romanian")]
	private static void SetLanguageRO()
	{
		EditorPrefs.SetString("TestLanguage","Romanian");
	}
	[MenuItem("Localisation/Slovak")]
	private static void SetLanguageSL()
	{
		EditorPrefs.SetString("TestLanguage","Slovak");
	}
	[MenuItem("Localisation/Slovenian")]
	private static void SetLanguageSN()
	{
		EditorPrefs.SetString("TestLanguage","Slovenian");
	}
	[MenuItem("Localisation/Swedish")]
	private static void SetLanguageSW()
	{
		EditorPrefs.SetString("TestLanguage","Swedish");
	}
	[MenuItem("Localisation/Thai")]
	private static void SetLanguageTI()
	{
		EditorPrefs.SetString("TestLanguage","Thai");
	}
	[MenuItem("Localisation/Turkish")]
	private static void SetLanguageTU()
	{
		EditorPrefs.SetString("TestLanguage","Turkish");
	}
	[MenuItem("Localisation/Vietnamese")]
	private static void SetLanguageVN()
	{
		EditorPrefs.SetString("TestLanguage","Vietnamese");
	}
	[MenuItem("Localisation/Hungarian")]
	private static void SetLanguageHU()
	{
		EditorPrefs.SetString("TestLanguage","Hungarian");
	}

//Добавление объектов на сцену при помощи меню

	//Добавление локализированного 3D текста
    [MenuItem("GameObject/3D Object/Localised 3D Text")]
	private static void CreateLocalised3DText()
	{
		Object Localised3DTextPrefab = AssetDatabase.LoadAssetAtPath("Assets/Localisation/Prefabs/3DText.prefab",typeof(GameObject));
		GameObject Localised3DText = PrefabUtility.InstantiateAttachedAsset(Localised3DTextPrefab) as GameObject;
		Localised3DText.name = "Localised3DText";
		Selection.activeGameObject = Localised3DText;
	}

	//Добавление компонента локализации к UI
	[MenuItem ("Component/UI/Localisation", priority=30)]
	private static void  AssignUITextLocalisation () {
		if (Selection.activeTransform != null) {
			if(Selection.activeTransform.GetComponent<UnityEngine.UI.Text>() != null){
				if(Selection.activeTransform.GetComponent<LocalisedUIText>() == null){
					GameObject selectedGameObject = Selection.activeGameObject;
					selectedGameObject.AddComponent<LocalisedUIText> ();
				}else{
					EditorUtility.DisplayDialog ("Can't add script","Can't add 'Localisation' because a 'Localisation' is already added to the game object! A GameObject can only contain one 'Localisation' component","Ok");
				}
			}else{
				EditorUtility.DisplayDialog ("Can't add script","Can't add 'Localisation' because a 'Text' component is missed","Ok");
			}
		}


	}
	[MenuItem ("Component/UI/Localisation", true, priority=30)]
	private static bool  ValidateUITextLocalisation () {
		if (Selection.activeTransform != null) {
			return Selection.activeTransform.GetComponent<UnityEngine.UI.Text>() != null;
		}
		return false;
	}

	//Удаление PlayerPrefs
	[MenuItem("Tools/Clear PlayerPrefs")]
	private static void NewMenuOption()
	{
		PlayerPrefs.DeleteAll();
	}

    //Переключить язык на следующий из доступных
    [MenuItem("Tools/Switch to next available Language")]
    private static void SwitchLanguageToNextAvailable()
    {
        string currentLanguage = Localisation.GetCurrentLanguage().ToString();
        List<string> avalibleLanguage = new List<string>();

        string currentDirectory = Directory.GetCurrentDirectory();
        string[] files = Directory.GetFiles(currentDirectory + "\\Assets\\Resources\\Localisation");
        foreach (var item in files)
        {
            string clearFileName = Path.GetFileNameWithoutExtension(item);
            clearFileName = Path.GetFileNameWithoutExtension(clearFileName);
            clearFileName = Path.GetFileNameWithoutExtension(clearFileName);

            if (!avalibleLanguage.Contains(clearFileName))
            {
                avalibleLanguage.Add(clearFileName);
            }
        }


        int indexCurrentLanguage = avalibleLanguage.IndexOf(currentLanguage);

        if (indexCurrentLanguage < avalibleLanguage.Count - 1)
        {
            indexCurrentLanguage++;
        }
        else
        {
            indexCurrentLanguage = 0;
        }
        EditorPrefs.SetString("TestLanguage", avalibleLanguage[indexCurrentLanguage]);
    }
}

