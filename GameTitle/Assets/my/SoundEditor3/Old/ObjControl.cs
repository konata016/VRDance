using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjControl : MonoBehaviour
{
    public GameObject obj;
    public Material m0, m1, m2;
    GameObject instantObj;
    int countWheel;

    // Start is called before the first frame update
    void Start()
    {
        instantObj = Instantiate(obj, transform);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.x = Mathf.Floor(worldPos.x);
        worldPos.y = Mathf.Floor(worldPos.y);
        instantObj.transform.position = worldPos;

        Change();
    }

    void Change()
    {
        if (0 != Input.GetAxis("Mouse ScrollWheel"))
        {
            countWheel++;
            if (countWheel == 3) countWheel = 0;

            switch (countWheel)
            {
                case 0:instantObj.GetComponent<Renderer>().material = m0;break;
                case 1: instantObj.GetComponent<Renderer>().material = m1; break;
                case 2: instantObj.GetComponent<Renderer>().material = m2; break;
            }
        }
    }
}
