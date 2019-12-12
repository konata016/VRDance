using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーのノーツを表示するため用のブロックを生成する

public class InstantGroundCube : MonoBehaviour
{
    public GameObject instantObj;
    public int[] shaderLine = new int[3];
    public GameObject[] shaderObj = new GameObject[3];
    List<GameObject> objList = new List<GameObject>();

    void Awake()
    {
        float dist = 0.4f;
        float maxX = 1.0f;
        float maxZ = 19.0f;

        float x = -1.0f, y = 0.0f, z = 1.4f;
        Vector3 pos;

        for (x = -1.0f; x <= maxX; x += dist)
        {
            int count = 0;
            int shaderLineCount = 0;
            for (z = -1.4f; z <= maxZ; z += dist)
            {
                pos = new Vector3(x, y, z);
                objList.Add(Instantiate(instantObj, pos, new Quaternion()));
                objList[objList.Count - 1].transform.parent = transform.GetChild(shaderLineCount).gameObject.transform;
                objList[objList.Count - 1].GetComponent<Renderer>().material = shaderObj[shaderLineCount].GetComponent<Renderer>().materials[1];
                if (count == shaderLine[shaderLineCount]) shaderLineCount++;
                count++;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
