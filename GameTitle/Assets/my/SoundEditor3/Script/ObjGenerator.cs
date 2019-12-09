using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjGenerator : MonoBehaviour
{
    public Slider slider;
    public GameObject laneObj0;     //生成するオブジェクト
    public GameObject laneObj1;     //生成づるオブジェト
    public GameObject groupObj;     //生成するオブジェクトをまとめる親オブジェクト

    float speed;
    float move;

    //エディターオブジェクトプレハブ内の構成要素
    public enum PARENT_OBJ
    {
        FrgFalse, FrgTrue,
        EnemyAttackType,
        PlStep
    }

    // Start is called before the first frame update
    void Start()
    {
        Instant();
        speed = StepData.GetStepData.Count / StepData.GetSoundMaxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (SoundControl.onMusic)
        {
            if (slider.value != 0)
            {
                //グループの移動
                move -= speed * Time.deltaTime;
                groupObj.transform.position = new Vector3(0, move, 0);
            }
            else
            {
                //初期座標へ
                groupObj.transform.position = Vector3.zero;
            }
        }
        else
        {
            //ポジションを自然数に変換する
            int num = -StepData.GetTimeNearBeatTime(slider.value);
            groupObj.transform.position = new Vector3(0, num, 0);
            move = num;
        }
    }

    //オブジェクトの生成
    void Instant()
    {
        GameObject instant;
        for (int i = 0; i < StepData.GetStepData.Count; i++)
        {
            if (i % 2 == 0)
            {
                instant = Instantiate(laneObj0, new Vector3(0, i, 0), new Quaternion());
                instant.transform.parent = groupObj.transform;
                InputText(i);
            }
            else
            {
                instant = Instantiate(laneObj1, new Vector3(0, i, 0), new Quaternion());
                instant.transform.parent = groupObj.transform;
                InputText(i);
            }

        }

        //テキストデータ反映用
        void InputText(int num)
        {
            //短縮用
            GameObject objTrue = instant.transform.GetChild((int)PARENT_OBJ.FrgTrue).gameObject;
            GameObject objFlase = instant.transform.GetChild((int)PARENT_OBJ.FrgFalse).gameObject;
            GameObject objEnemyAttack = instant.transform.GetChild((int)PARENT_OBJ.EnemyAttackType).gameObject;
            GameObject objPl = instant.transform.GetChild((int)PARENT_OBJ.PlStep).gameObject;

            //敵の攻撃座標の読み込み
            for (int i=0; i < StepData.GetStepData[num].enemyAttackPos.Length; i++)
            {
                if (StepData.GetStepData[num].enemyAttackPos[i])
                {
                    ObjChange(objFlase.transform.GetChild(i).gameObject, objTrue.transform.GetChild(i).gameObject);
                }
            }

            //敵の攻撃種類の読み込み
            switch (StepData.GetStepData[num].ememyAttackType)
            {
                case StepData.ENEMY_ATTACK_TYPE.WaveWide:
                    ObjChange(objEnemyAttack.transform.GetChild(0).gameObject, objEnemyAttack.transform.GetChild(1).gameObject);
                    break;
                case StepData.ENEMY_ATTACK_TYPE.Throw:
                    ObjChange(objEnemyAttack.transform.GetChild(0).gameObject, objEnemyAttack.transform.GetChild(2).gameObject);
                    break;
                default:
                    break;
            }

            //プレイヤーステップのタイミングデータ読み込み
            switch (StepData.GetStepData[num].plStep)
            {
                case StepData.PL_STEP_TIMING.Step:
                    ObjChange(objPl.transform.GetChild(0).gameObject, objPl.transform.GetChild(1).gameObject);
                    break;
                default:
                    break;
            }

            //表示、非表示の切り替え用のやつ
            void ObjChange(GameObject startActiveObj, GameObject endActiveObj)
            {
                startActiveObj.gameObject.SetActive(false);
                endActiveObj.SetActive(true);
            }
        }
    }
}
