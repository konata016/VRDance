using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWave : MonoBehaviour
{
    GameObject cube;

    float rad = 0;

    bool flag = false;

    [SerializeField] float speed = 5.5f;
    [SerializeField] float max = 2.0f;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        //cube.GetComponent<GameObject>();
        pos = this.transform.position;

        speed *= Mathf.Deg2Rad;
    }

    // Update is called once per frame
    void Update()
    {
        WaveMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wave")
        {
            flag = true;
            pos = this.transform.position;
            //Debug.Log("ok");
        }
    }

    private void WaveMove()
    {
        if (flag)
        {
           this.transform.position = new Vector3(pos.x, max * Mathf.Sin(rad) + pos.y, pos.z);
            rad += speed;
            if (rad >= Mathf.PI)
            {
                flag = false;
                rad = 0;
                this.transform.position = new Vector3(pos.x, StepDetermination.groundPosition.y, pos.z);
            }
        }
    }
}
