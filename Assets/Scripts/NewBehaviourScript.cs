using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject cardsPanel;
    [SerializeField] private GameObject selectCardsPanel;
    
    private int _screenW = Screen.width;
    private int _screenH = Screen.height;
    private void Start()
    {
        selectCardsPanel.SetActive(true); // TODO : 애니메이션 추가
    }
    private void SetStage()
    {
        float dist = _screenW / 8;
    }

    private void Undo()
    {
        
    }
    private void PauseGame()
    {
        
    }

    private void SelectCard()
    {
        
    }

    private void ShootDart()
    {
        
    }
}
