using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLastLevelMsg : MonoBehaviour
{
    [SerializeField] private string text;

    private void Awake()
    {
        if (G00301.currentLevel >= 300)
        {
            Text txt = base.GetComponent<Text>();
            txt.text = text;
        }
    }
}
