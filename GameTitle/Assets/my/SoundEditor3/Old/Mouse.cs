using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    RaycastHit hit;

    [Header("True:False")]
    public GameObject[] frgObj = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //マウスがクリックされたら
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //マウスのポジションを取得してRayに代入

            if (Physics.Raycast(ray, out hit))  //マウスのポジションからRayを投げて何かに当たったらhitに入れる
            {
                string objName = hit.collider.gameObject.name; //オブジェクト名を取得して変数に入れる
                

                Debug.Log(objName); //オブジェクト名をコンソールに表示

                GameObject obj;
                switch (hit.collider.gameObject.tag)
                {
                    case "True":
                        obj = GetObj(hit.collider.gameObject, 0);
                        hit.collider.gameObject.SetActive(false);
                        obj.SetActive(true);
                        break;

                    case "False":
                        obj = GetObj(hit.collider.gameObject, 1);
                        hit.collider.gameObject.SetActive(false);
                        obj.SetActive(true);

                        break;
                    default:break;
                }
            }
        }
    }

    void FrgObj(GameObject obj)
    {
        Vector3 v3 = obj.transform.position;
        if (obj.name == "False_")
        {
            GameObject tmpObj= Instantiate(frgObj[0], v3,new Quaternion())as GameObject;
            Debug.Log(tmpObj.transform.position);
            Destroy(obj, 0.1f);
        }
        else
        {
            GameObject tmpObj = Instantiate(frgObj[1], v3, new Quaternion()) as GameObject;
            Destroy(obj, 0.1f);                                                                                                                                                                 
        }
    }

    GameObject GetObj(GameObject obj,int childNum) {

        int num = (int)obj.transform.position.x;
        GameObject parentObj = obj.transform.parent.gameObject.transform.parent.gameObject;
        GameObject childObj = parentObj.transform.GetChild(childNum).gameObject.transform.GetChild(num).gameObject;
        Debug.Log(parentObj.name);

        return childObj;
    }
}
