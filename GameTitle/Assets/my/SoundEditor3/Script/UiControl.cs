using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControl : MonoBehaviour
{
    public Text hitObjName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.localPosition.x == 7)
        {
            hitObjName.text = "Enemy: " + other.name;
        }
        else if (other.gameObject.transform.localPosition.x == 8)
        {
            hitObjName.text = "Player: " + other.name;
        }
    }
}
