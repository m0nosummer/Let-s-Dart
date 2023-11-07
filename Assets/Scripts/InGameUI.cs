using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public GameObject[] startCards;
    public GameObject[] inGameCards;
    
    [SerializeField] private TargetManager targetManager;
    [SerializeField] private DartManager dartManager;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject cardsPanel;
    [SerializeField] private GameObject selectCardsPanel;
    [SerializeField] private Vector3 spawnPoint;
    
    private int _screenW = Screen.width;
    private int _screenH = Screen.height;
    private void Start() // 씬 변경 후 첫 시작
    {
        selectCardsPanel.SetActive(true); // TODO : 애니메이션 추가
        dartManager.SetStartCards();
    }
    public void SetStage()
    {
        selectCardsPanel.SetActive(false);
        dartManager.SetInGameCards();
        
        inGameCards[0].GetComponent<Card>().SelectCard();
        dartManager.SpawnDart(0, spawnPoint);
    }

    public void Undo()
    {
        
    }
    public void PauseGame()
    {
        
    }

    public void ShootDart()
    {
        
    }
}
