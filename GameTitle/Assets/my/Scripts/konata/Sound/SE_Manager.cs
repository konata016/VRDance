using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 流したい音をセットして、別のスクリプトで呼べるようにするやつ
/// </summary>
public class SE_Manager : MonoBehaviour
{
    AudioSource[] seArr;

    /// <summary>
    /// SEの種類名
    /// </summary>
    public enum SE_NAME
    {
        //メインゲーム
        Excellent, Good, But, Miss,
        PlDamage, PlAttack,

        //セレクト画面
        LoadComplete,
        Step,Jump,Cancel,
        SceneChange
    }

    [System.Serializable]
    public class SeData
    {
        [HideInInspector] public string name;
        public AudioClip audio;
    }
    public List<SeData> seDataList = new List<SeData>()
    {
        //リストの初期化
        //メインゲーム
        new SeData{name=SE_NAME.Excellent.ToString(),audio=null},
        new SeData{name=SE_NAME.Good.ToString(),audio=null},
        new SeData{name=SE_NAME.But.ToString(),audio=null},
        new SeData{name=SE_NAME.Miss.ToString(),audio=null},

        new SeData{name=SE_NAME.PlDamage.ToString(),audio=null},
        new SeData{name=SE_NAME.PlAttack.ToString(),audio=null},

        //セレクト
        new SeData{name=SE_NAME.LoadComplete.ToString(),audio=null},
        new SeData{name=SE_NAME.Step.ToString(),audio=null},
        new SeData{name=SE_NAME.Jump.ToString(),audio=null},
        new SeData{name=SE_NAME.Cancel.ToString(),audio=null},
        new SeData{name=SE_NAME.SceneChange.ToString(),audio=null}

    };

    static SE_Manager SE_Manager_;

    // Start is called before the first frame update
    void Start()
    {
        //オーディオをアタッチする
        for (int i = 0; i < 10; i++)
        {
            gameObject.AddComponent<AudioSource>();

        }
        seArr = GetComponents<AudioSource>();

        for (int i = 0; i < 10; i++)
            seArr[i].volume = 0.5f;

        SE_Manager_ = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// SEの再生、どのSEを使うか選択して！
    /// </summary>
    /// <param name="seType"></param>
    public static void SePlay(SE_NAME seType)
    {
        //被って流せる音は最大10つまで
        //再生中でないオーディオを探す
        foreach (AudioSource se in SE_Manager_.seArr)
        {
            if (!se.isPlaying)
            {
                se.PlayOneShot(SE_Manager_.seDataList[(int)seType].audio);
                break;
            }
        }
    }
}
