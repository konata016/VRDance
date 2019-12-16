using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeBoxPos : MonoBehaviour
{
    [SerializeField] GameObject boxPos;
    [SerializeField] GameObject centerEyePos;
    private void Update()
    {
        //Debug.Log("BoxPos : " + boxPos.transform.position);
        //Debug.Log("CenterPos : " + centerEyePos.transform.position);
    }
    /// <summary>
    /// シーン移行のためのボックスの位置設定
    /// </summary>
    public void BoxPosChange()
    {
        boxPos.transform.position = centerEyePos.transform.position;// VRカメラの位置を取得
    }
}
