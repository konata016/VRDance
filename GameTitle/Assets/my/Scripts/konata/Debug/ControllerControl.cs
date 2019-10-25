using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//キーボードからオブジェクトの位置、サイズを変更することができるデバッグ用のやつ

public class ControllerControl : MonoBehaviour
{
    public GameObject textObj;
    public GameObject headDisplay;
    GameObject infoObj;
    int arrayCount;

    enum TRANSFORM
    {
        PosX, PosY, PosZ,
        SizX, SizY, SizZ,
        RollX, RollY, RollZ,
    }
    TRANSFORM objData = new TRANSFORM();

    // Start is called before the first frame update
    void Start()
    {
        infoObj = Instantiate(textObj, headDisplay.transform);
        infoObj.transform.parent = headDisplay.transform;
        infoObj.transform.localPosition = new Vector3(0, -1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) objData++;
        if (Input.GetKeyDown(KeyCode.DownArrow)) objData--;

        if (objData > TRANSFORM.SizZ) objData = TRANSFORM.PosX;
        if (objData < TRANSFORM.PosX) objData = TRANSFORM.SizZ;

        Vector3 pos = transform.position;
        Vector3 siz = transform.localScale;
        Quaternion p = transform.rotation;

        switch (objData)
        {
            case TRANSFORM.PosX: pos.x += ChangeNum(); break;
            case TRANSFORM.PosY: pos.y += ChangeNum(); break;
            case TRANSFORM.PosZ: pos.z += ChangeNum(); break;
            case TRANSFORM.SizX: siz.x += ChangeNum(); break;
            case TRANSFORM.SizY: siz.y += ChangeNum(); break;
            case TRANSFORM.SizZ: siz.y += ChangeNum(); break;
            case TRANSFORM.RollX:
                break;
            case TRANSFORM.RollY:
                break;
            case TRANSFORM.RollZ:
                break;
        }

        transform.position = pos;
        transform.localScale = siz;

        TextOutput();
    }

    float ChangeNum()
    {
        float num = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            num -= 0.01f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            num += 0.01f;
        }

        return num;
    }

    void TextOutput()
    {
        infoObj.transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + objData;
    }
}
