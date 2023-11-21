using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDataViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textStageLevel;
    [SerializeField] private TextMeshProUGUI[] textTargetHps;
    [SerializeField] private TargetManager targetManager;
    
    private void Update()
    {
        textStageLevel.text = "STAGE " + targetManager.StageLevel.ToString();
        // textCurScore.text = ;
        for (int i = 0; i < textTargetHps.Length; i++)
        {
            if (targetManager.TargetComponents == null) break;
            textTargetHps[i].text = targetManager.TargetComponents[i].TargetHp.ToString();
        }
    }
}
