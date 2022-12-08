using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Pendu currentGame;
    public IHMController IHM; 
    // Start is called before the first frame update
    void Start()
    {
        currentGame = new Pendu(this);
    }
    public void Restart()
    {
        currentGame = new Pendu(this);
    }
    public void Exit() 
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;

        }
        else
        {
            Application.Quit();
        }
    }
}