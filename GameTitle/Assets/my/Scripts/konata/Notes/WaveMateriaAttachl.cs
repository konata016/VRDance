using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMateriaAttachl : MonoBehaviour
{
    public Vector3[] pos = new Vector3[4];
    public GameObject[] shaderObj = new GameObject[3];
    bool onStart;

    // Start is called before the first frame update
    void Start()
    {
        //m = GetComponent<Renderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 v3 = transform.GetChild(i).gameObject.transform.position;
            if (v3.z < pos[1].z)
            {
                //デザさんにお願いしてMaterialの順番を変える

                Material[] materials = transform.GetChild(i).gameObject.GetComponent<Renderer>().materials;
                materials[1] = shaderObj[0].GetComponent<Renderer>().materials[1];
                transform.GetChild(i).gameObject.GetComponent<Renderer>().materials = materials;
            }
            else if (v3.z <= pos[1].z && v3.z > pos[2].z)
            {

            }

        }
    }

    void SetMaterial()
    {

    }
}
