using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject damageCanvas;
    [SerializeField]
    TextMeshProUGUI damageTMPro;

    private Vector3 movePos;
    private float moveRad;
    private float moveScale; 

    private float plDamageValue;
    private string plDamageText;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        moveScale = 0.01f;
        plDamageText = "Hit!";
        damageTMPro.text = plDamageText;
        damageTMPro.color = new Color(1f, 1f, 1f, 0.1f);
        moveRad = 0f;
        movePos = damageTMPro.rectTransform.localPosition;        
    }

    // Update is called once per frame
    void Update()
    {
        if(damageTMPro.color.a < 1000f)
            damageTMPro.color *= 1.18f;
        else
        {
            Destroy(damageCanvas);
        }

        if (moveRad < 180f)
        {
            moveRad += Time.deltaTime * 840f;
            movePos.y += Mathf.Sin(Mathf.Deg2Rad * moveRad) * moveScale;
        }

        damageTMPro.rectTransform.localPosition 
            = new Vector3(movePos.x, movePos.y, movePos.z);
    }
}