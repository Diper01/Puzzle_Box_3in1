using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private IPausable currentGame;
    public static PauseManager instance;
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    public void RegisterGame(IPausable game)
    {
        Debug.Log("[labu] Pause manager registring new game + " + game);
        currentGame = game;
    }

    public void Pause()
    {
        Debug.Log("[labu] PauseManager Pause");
        currentGame?.Pause();
    }

    public void Resume()
    {
        Debug.Log("[labu] PauseManager Resume");
        currentGame?.Resume();
    }
}
