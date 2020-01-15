using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject foot;

    public static bool GetEndProcess { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(panel.transform.position, foot.transform.position);

        if (dis < 0.1f)
        {
            GetEndProcess = true;
        }
        else
        {
            GetEndProcess = false;
        }
    }
}
