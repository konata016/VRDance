using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMateriaAttachl : MonoBehaviour
{
    public Vector3[] pos = new Vector3[5];
    public Material[] materialArr=new Material[3];
    List<GameObject> objList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pos[0].z > pos[1].z && pos[1].z <= pos[2].z)
        {

        }
        else if (pos[1].z > pos[2].z && pos[2].z <= pos[3].z)
        {

        }
        else if (pos[3].z > pos[4].z)
        {

        }
    }
}
