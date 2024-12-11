using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expander : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Localisation.GetCurrentLanguage() == Languages.French)
        {
            transform.localScale = new Vector3(1.5f, 1f, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
