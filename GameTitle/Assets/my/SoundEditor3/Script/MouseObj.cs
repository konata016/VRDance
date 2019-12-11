using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //マウスに
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = worldPos;
    }

   

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0)) //マウスがクリックされたら
        {
            //触っているオブジェクトのタグによって処理が変わる
            switch (other.gameObject.tag)
            {
                case "True":
                    ObjChange(GetFrgObj(other.gameObject, 0));
                    break;

                case "False":
                    ObjChange(GetFrgObj(other.gameObject, 1));
                    break;

                case "AttNothing":
                    ObjChange(GetModeObj(other.gameObject, 1));
                    break;

                case "AttWave":
                    ObjChange(GetModeObj(other.gameObject, 2));
                    break;

                case "AttThrow":
                    ObjChange(GetModeObj(other.gameObject, 0));
                    break;

                case "PlNothing":
                    ObjChange(GetModeObj(other.gameObject, 1));
                    break;

                case "PlNormalStep":
                    ObjChange(GetModeObj(other.gameObject, 0));
                    break;

                default: break;
            }
        }

        //表示、非表示の切り替え用のやつ
        void ObjChange(GameObject changeObj)
        {
            other.gameObject.SetActive(false);
            changeObj.SetActive(true);
        }
    }

    //フラグ用のオブジェクト取得
    GameObject GetFrgObj(GameObject obj, int childNum)
    {

        int num = (int)obj.transform.position.x;
        GameObject parentObj = obj.transform.parent.gameObject.transform.parent.gameObject;
        GameObject childObj = parentObj.transform.GetChild(childNum).gameObject.transform.GetChild(num).gameObject;

        return childObj;
    }

    //モード切替用のオブジェクト取得用
    GameObject GetModeObj(GameObject obj,int num)
    {
        GameObject parentObj = obj.transform.parent.gameObject;
        GameObject childObj = parentObj.transform.GetChild(num).gameObject;
        Debug.Log(parentObj.name);

        return childObj;
    }

}
