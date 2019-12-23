using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [Header("プレイヤーノーツ")]
    public float uiNotesFixTime = -0.2f;
    public float groundNotesFixTime = -0.18f;

    public static GameDirector GetGameDirector { get; private set; }

    //受け渡し用

    private void Awake()
    {
        GetGameDirector = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 地面の位置(足の高さに合わせたもの)
    /// </summary>
    public static Vector3 GetGroundPos { get { return GroundManager.GetGroundPos; } }//FootJudgment_Right.groundPosition; } }
}
