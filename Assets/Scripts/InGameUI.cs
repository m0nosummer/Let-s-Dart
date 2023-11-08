using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public GameObject[] startCards;
    public GameObject[] inGameCards;
    public GameObject infoPanel;
    public GameObject playPanel;
    public GameObject cardsPanel;
    public int stageLevel = 1;
    
    [SerializeField] private TargetManager targetManager;
    [SerializeField] private DartManager dartManager;
    [SerializeField] private GameObject selectCardsPanel;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector3 spawnPoint;
    
    private float _screenW;
    private float _screenH;
    private RectTransform _rtPlayPanel;
    
    private void Start() // 씬 변경 후 첫 시작
    {
        selectCardsPanel.SetActive(true); // TODO : 애니메이션 추가
        dartManager.SetStartCards();
        
        _screenH = mainCamera.orthographicSize;
        _screenW = _screenH * Screen.width / Screen.height;
        
        _rtPlayPanel = playPanel.GetComponent<RectTransform>();
    }
    public void SetStage()
    {
        selectCardsPanel.SetActive(false);
        
        dartManager.SetInGameCards();
        targetManager.SetTargets(_screenW, _screenH, _rtPlayPanel.rect.height / _rtPlayPanel.rect.width);
        // targetManager.SetTargetHP(stageLevel);
        
        inGameCards[0].GetComponent<Card>().SelectCard(); // 시작 시 기본 카드 선택
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
