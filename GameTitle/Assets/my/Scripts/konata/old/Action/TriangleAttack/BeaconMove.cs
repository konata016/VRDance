using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconMove : MonoBehaviour
{
    public GameObject[] posArray = new GameObject[4];
    public GameObject[] beaconArray = new GameObject[4];
    public GameObject triangleManager;

    public float speed = 5;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        //マネージャーからの数値変更
        speed = PlAttackManager.GetTriangle.beaconSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (count != posArray.Length)
        {
            count = 0;
            for (int i = 0; i < posArray.Length; i++)
            {
                float dis = Vector3.Distance(transform.position, posArray[i].transform.position);
                beaconArray[i].transform.position = Vector3.Slerp(transform.position,posArray[i].transform.position,
                    (speed * Time.time) / dis);

                if (Vector3.Distance(beaconArray[i].transform.position, posArray[i].transform.position) < 0.1f)
                {
                    count++;
                }
            }
        }
        else triangleManager.SetActive(true);
    }
}
