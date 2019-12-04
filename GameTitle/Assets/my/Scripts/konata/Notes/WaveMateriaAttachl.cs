using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMateriaAttachl : MonoBehaviour
{
    public Vector3[] pos = new Vector3[4];
    public Material[] materialArr = new Material[3];
    public Material material0;
    public Material m;
    public Renderer mesh;
    bool onStart;

    // Start is called before the first frame update
    void Start()
    {
        m = mesh.materials[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (!onStart)
        {
            Vector3 v3 = transform.position;
            if (v3.z < pos[1].z)
            {
                gameObject.GetComponent<MeshRenderer>().materials[1] =material0;
            }
            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    //Vector3 v3 = transform.GetChild(i).gameObject.transform.position;
            //    //if (v3.z < pos[1].z)
            //    //{
            //        GetComponent<Renderer>().materials[1] = materialArr[0];
            //    //}
            //    //else if (pos[1].z > pos[2].z && pos[2].z <= pos[3].z)
            //    //{
            //    //    transform.GetChild(i).gameObject.GetComponent<Renderer>().materials[1] = materialArr[1];
            //    //}
            //    //else if (pos[2].z > pos[3].z)
            //    //{
            //    //    transform.GetChild(i).gameObject.GetComponent<Renderer>().materials[1] = materialArr[2];
            //    //}
            //}
            onStart = true;
        }
    }
}
