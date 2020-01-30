using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlDamageFootTrigger : MonoBehaviour
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
        if (!PlDamageStage.GetNoDamageTrigger)
        {
            if (other.gameObject.tag == "wide")
            {
                PlDamageStage.OnDamageTrigger = true;

                //SE
                SE_Manager.SePlay(SE_Manager.SE_NAME.PlDamage);
            }
        }
    }
}
