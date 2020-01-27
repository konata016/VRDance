using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

/// <summary>
/// 目的地までの移動と元の場所に戻るプログラム
/// </summary>
public class PopUp : MonoBehaviour
{
    public GameObject headDisplay;
    public float fallTime = 3;

    public PageInstant pageInstant;
    public string nextSceneName = "SelectScene";

    public static bool OnTriggerJump {private get; set; }

    Vector3 tmpPos;

    // Start is called before the first frame update
    void Start()
    {
        tmpPos = transform.position;

        //目的地まで移動する
        Move(gameObject, headDisplay.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        NextScene();
    }

    //移動
    void Move(GameObject obj,Vector3 Destination)
    {
        DOTween
       .To(value => Move(value), 0, 1, fallTime)
       .SetEase(Ease.InQuart);

        void Move(float value)
        {
            var pos = obj.transform.position;
            pos.y = Mathf.Lerp(pos.y, Destination.y, value);
            obj.transform.position = pos;
        }
    }

    //最後のページまで行ったらシーンを切り替えることができる
    void NextScene()
    {
        if (pageInstant.num == pageInstant.pageNumObjList.Count - 1)
        {
            if (OnTrigger())
            {
                Debug.Log("J" + OnTrigger());
                Move(gameObject, tmpPos);
                StartCoroutine(TimeScaleWait(1f));

                OnTriggerJump = false;
            }
        }
    }

    //タイムスケールを戻す
    IEnumerator TimeScaleWait(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        SceneManager.LoadScene(nextSceneName);
    }


    bool OnTrigger()
    {
        return OnTriggerJump || Input.GetKeyDown(KeyCode.UpArrow);
    }
}
