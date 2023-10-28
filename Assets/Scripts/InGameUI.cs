using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject cardsPanel;
    [SerializeField] private GameObject selectCardsPanel;
    [SerializeField] private DartManager dartManager;
    
    private int _screenW = Screen.width;
    private int _screenH = Screen.height;
    private void Start()
    {
        selectCardsPanel.SetActive(true); // TODO : 애니메이션 추가
        dartManager.SetDart();
    }
    public void SetStage()
    {
        float dist = _screenW / 8;
    }

    public void Undo()
    {
        
    }
    public void PauseGame()
    {
        
    }

    public void SelectCard()
    {
        
    }

    public void ShootDart()
    {
        
    }
}
