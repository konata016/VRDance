using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantTestObj : MonoBehaviour
{
    public GameObject obj;
    public GameObject groupObj;
    public Material material0, material1;
    // Start is called before the first frame update
    void Start()
    {
        int num = 0;
        for (int i = 0; i < 5000; i++)
        {
            for (int f = 0; f < 6; f++)
            {
                GameObject instant = Instantiate(obj, new Vector3(f, i, 0.001f), new Quaternion());
                instant.transform.parent = groupObj.transform;
                if (f % 2 == num)
                {
                    instant.GetComponent<Renderer>().material = material0;
                }
                else
                {
                    instant.GetComponent<Renderer>().material = material1;
                }
            }
            if (num == 0) num = 1;
            else num = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
