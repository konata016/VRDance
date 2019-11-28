using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ATK //プレイヤーが与えられるダメージがここにあると思うのか？
{
    public string swordTag;
    public float swordDMG;


}

public class EnemyState : MonoBehaviour
{  
    [SerializeField] float enemyHPmax;
    private float enemyHP;
    private float konjoHP;
    private float konjoDamage;

    private float musicTime;
    private float leftTime;
    private float konjoTime = 0.0f;

    private GameObject musicObj;
    private AudioSource audioSource;
    private AudioClip clip;

    ATK atk = new ATK();

    // Start is called before the first frame update
    void Start()
    {
        musicObj = GameObject.Find("GameManager");
        audioSource = musicObj.GetComponent<AudioSource>();
        musicTime = audioSource.clip.length;
        leftTime = musicTime - musicObj.GetComponent<AudioSource>().time;
    }

    // Update is called once per frame
    void Update()
    {
        leftTime = musicTime - musicObj.GetComponent<AudioSource>().time;

        if (enemyHP < enemyHPmax * 0.2f)
        {
            konjoTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == atk.swordTag)
        {
            DamageCal(atk.swordDMG);
        }
    }

    private void DamageCal(float damage)
    {
        if (enemyHP >= enemyHPmax * 0.2f) //80%以上
        { 
            enemyHP -= damage;

            if(enemyHP < enemyHPmax * 0.2f)
            {
                konjoHP = enemyHP;
                konjoDamage = enemyHP / leftTime;
            }
        }

        else                              //80%以下
        {
            enemyHP -= konjoDamage * konjoTime;
            konjoTime = 0.0f;
        }
    }
}