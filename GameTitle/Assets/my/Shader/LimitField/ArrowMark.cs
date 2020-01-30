using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMark : MonoBehaviour
{
    public float fix = 0.3f;
    public GameObject fieldCenterObj;
    public GameObject headDisplay;
    public GameObject arrowMarkObj;

    public float dis;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 90;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        dis = headDisplay.transform.position.z - fieldCenterObj.transform.position.z;

        if (dis > fix)
        {
            arrowMarkObj.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
        }
        else if (dis < -fix)
        {
            arrowMarkObj.transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1, 0));
        }

        if (dis > fix || dis < -fix)
        {
            arrowMarkObj.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            arrowMarkObj.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
