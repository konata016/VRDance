using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// unity風カメラ移動
/// </summary>
public class CameraControl : MonoBehaviour
{
    public float speed = 10;
    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        //マウスカーソルの非表示とロック
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //右クリックを押した状態でマウスを動かすとカメラの向きが変わる
        if (Input.GetMouseButton(1))
        {
            xRotation += Input.GetAxis("Mouse X");
            yRotation += Input.GetAxis("Mouse Y");
            transform.rotation = Quaternion.Euler(-yRotation, xRotation, 0);
        }

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        float yMove = 0;
        if (Input.GetKey(KeyCode.Space)) yMove = 1;
        if (Input.GetKey(KeyCode.LeftShift)) yMove = -1;

        //移動
        transform.Translate(new Vector3(xMove, yMove, zMove) * speed * Time.deltaTime);

    }
}
