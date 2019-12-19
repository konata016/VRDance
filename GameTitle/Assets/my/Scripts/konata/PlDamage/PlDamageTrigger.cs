using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlDamageTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wave" || other.gameObject.tag == "throw")
        {
            PlDamageStage.OnDamageTrigger = true;

            //SE
            SE_Manager.SePlay(SE_Manager.SE_NAME.PlAttack);
        }
    }
}
