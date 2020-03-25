using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 別の足との高さを見比べる
/// </summary>
public class Foot : MonoBehaviour
{
    public GroundManager.EVENT Event;
    public GameObject anotherFoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //省略用
        float anotherFootPos = anotherFoot.transform.position.y;
        float y = transform.position.y;

        if (Event == GroundManager.EVENT.Up)
        {
            //端数を切り捨て、見比べる
            //別の足と同じ位置にある場合Downのイベント発生
            if (Mathf.Floor(y * 100) / 100 <= Mathf.Floor(anotherFootPos * 100) / 100)
            {
                Event = GroundManager.EVENT.Vs;
            }
        }
    }
}
