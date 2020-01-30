using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmagePlayer : MonoBehaviour
{
    [SerializeField]
    GameObject damagePre;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlBeam")
        {         
            Instantiate(damagePre, new Vector3(other.transform.position.x + Random.Range(-0.5f, 0.5f),
                                               other.transform.position.y + Random.Range(0.0f, 1.0f),
                                               other.transform.position.z),
                                               Quaternion.identity);
        }
    }
}
