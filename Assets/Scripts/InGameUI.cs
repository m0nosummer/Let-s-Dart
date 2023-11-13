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
    public Vector3 topRight;
    public Vector3 bottomLeft;
    public int stageLevel = 1;
    
    [SerializeField] private TargetManager targetManager;
    [SerializeField] private DartManager dartManager;
    [SerializeField] private GameObject selectCardsPanel;
    [SerializeField] private Camera mainCamera;
    
    private float _screenWidth;
    private float _screenHeight;
    private float _playPanelRatio;
    private RectTransform _rtPlayPanel;
    
    public float ScreenWidth
    {
        get => _screenWidth;
        set => _screenWidth = value;
    }
    public float ScreenHeight
    {
        get => _screenHeight;
        set => _screenHeight = value;
    }
    public float PlayPanelRatio
    {
        get => _playPanelRatio;
        set => _playPanelRatio = value;
    }
    
    private void Start() // 씬 변경 후 첫 시작
    {
        selectCardsPanel.SetActive(true); // TODO : 애니메이션 추가
        dartManager.SetStartCards();
        
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f, 0));
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0));
        
        _rtPlayPanel = playPanel.GetComponent<RectTransform>();
    }
    public void SetStage()
    {
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f, 0));
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0));
        
        _screenHeight = topRight.y - bottomLeft.y;
        _screenWidth = topRight.x - bottomLeft.x;
        _playPanelRatio = _rtPlayPanel.rect.height / _rtPlayPanel.rect.width;
        
        selectCardsPanel.SetActive(false);
        dartManager.SetInGameCards();
        targetManager.SpawnTargets(_screenWidth, _playPanelRatio);
        targetManager.SetTargetHp(stageLevel);
        
        inGameCards[0].GetComponent<Card>().SelectCard(); // 시작 시 기본 카드 선택
        dartManager.SpawnDart(0, 
            playPanel.transform.position + Vector3.down * (_screenWidth * _playPanelRatio) / 2);
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
