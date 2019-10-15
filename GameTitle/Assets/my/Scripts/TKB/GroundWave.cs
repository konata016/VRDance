using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWave : MonoBehaviour
{
    [SerializeField] GameObject cube;

    float rad = 0;

    bool frag = false;

    float speed = 6f;
    float max = 1.5f;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        cube.GetComponent<GameObject>();
        pos = cube.transform.position;

        speed *= Mathf.Deg2Rad;
    }

    // Update is called once per frame
    void Update()
    {
        WaveMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "wave")
            frag = true;
    }

    private void WaveMove()
    {
        if (frag)
        {
            cube.transform.position = new Vector3(pos.x, max * Mathf.Sin(rad), pos.z);
            rad += speed;
            if (rad >= Mathf.PI)
            {
                frag = false;
                cube.transform.position = new Vector3(pos.x, 0.0f, pos.z);
            }
        }
    }
}
