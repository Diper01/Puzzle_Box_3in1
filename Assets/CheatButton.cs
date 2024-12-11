using Assets.Scripts.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatButton : MonoBehaviour
{
   public void OnClick()
    {
        GM.GetInstance().AddDiamond(10000, true);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale += 1;
        }
    }
}
