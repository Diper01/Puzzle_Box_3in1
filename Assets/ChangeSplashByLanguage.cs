using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class ChangeSplashByLanguage : MonoBehaviour {

    public Sprite English;
    public Sprite Russian;
    public Sprite French;

    [SerializeField] Image splashImage;

    // Use this for initialization
    void OnEnable()
    {
        if (Application.systemLanguage == SystemLanguage.Russian
         || Application.systemLanguage == SystemLanguage.Ukrainian
         || Application.systemLanguage == SystemLanguage.Belarusian)
        {
            splashImage.sprite = Russian;
        } else if(Application.systemLanguage == SystemLanguage.French)
        {
            splashImage.sprite = French;
        }
        else
        {
            splashImage.sprite = English;
        }
    }
}
