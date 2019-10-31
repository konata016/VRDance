using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] float enemyHP;

    float musicTime;
    float leftTime;

    AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        music = gameObject.GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "")
        {
            //float damage = other.gameObject.GetComponent<PlAttackAction>().atk;
        }
    }

    private void DamageCal(float damage)
    {
        if(enemyHP >= enemyHP * 0.2f)
        enemyHP -= damage;
    }
}
