using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//左と右のノーツの移送処理

public class Notes : MonoBehaviour
{
    RectTransform rect;

    public float speed = 50f;
    public bool RorL;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!RorL)
        {
            rect.localPosition += Vector3.right * speed * Time.deltaTime;
            if (0 < rect.localPosition.x)
            {
                //Destroy(gameObject, 0.1f);
            }
        }
        if (RorL)
        {
            rect.localPosition += Vector3.left * speed * Time.deltaTime;
            if (0 > rect.localPosition.x)
            {
                //Destroy(gameObject, 0.1f);
            }
        }
    }
}