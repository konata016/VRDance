using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTextManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DebugPanel.text1 = "ポーズ：" + PauseCheck.actionPause;
        DebugPanel.text2 = "攻撃力：" + PlActionControl2.GetDamage;
        DebugPanel.text3 = "判　定：" + NotesManager.GetRank;

    }
}
