using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void PlayGame()
    {
        // transform.DOScale(new Vector3(1.2f, 1.2f, 0), 1.0f).OnComplete(() =>
        // {
        //     SceneManager.LoadSceneAsync(1);
        // });
        SceneManager.LoadSceneAsync(1);
    }

    public void Settings()
    {
        
    }

    public void Share()
    {
        
    }

    public void Shop()
    {
        
    }

    public void Collection()
    {
        
    }

    public void LeaderBoard()
    {
        
    }

    public void WatchAD()
    {
        
    }
}
