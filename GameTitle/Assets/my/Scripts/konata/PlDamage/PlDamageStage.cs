using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlDamageStage : MonoBehaviour
{
    public GameObject[] stageObjArr;
    public float lostPoint = -10;
    public float fallTime = 3;
    public static int life { get; private set; }
    bool onFall;

    // Start is called before the first frame update
    void Start()
    {
        life = stageObjArr.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTrigger())
        {
            if (life != -1)
            {
                life--;
                onFall = true;
            }
        }

        if (life != -1)
        {
            StageControl();
        }
    }

    void StageControl()
    {
        if (onFall)
        {
            FallMove(stageObjArr[life]);
            onFall = false; 
        }
        HiddenObj();

        void HiddenObj()
        {
            for (int i = 0; i < stageObjArr.Length; i++)
            {
                var obj = stageObjArr[i];
                var pos = obj.transform.position;
                if (lostPoint > pos.y - 1)
                {
                    obj.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }

    void FallMove(GameObject obj)
    {
        DOTween
               .To(value => Move(value), 0, 1, fallTime)
               .SetEase(Ease.InQuart);

        void Move(float value)
        {
            var pos = obj.transform.position;
            pos.y = Mathf.Lerp(pos.y, lostPoint, value);
            obj.transform.position = pos;
        }
    }

    bool OnTrigger()
    {
        bool isDamage = Input.GetKeyDown(KeyCode.D);
        return isDamage;
    }
}
