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
    Vector3 move;

    //Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //マウスカーソルの非表示とロック
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //rb = GetComponent<Rigidbody>();
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

        move.x = Input.GetAxis("Horizontal")*speed* Time.deltaTime;
        move.z = Input.GetAxis("Vertical")* speed * Time.deltaTime;
        move.y = 0;
        if (Input.GetKey(KeyCode.Space)) move.y = 1* speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift)) move.y = -1* speed * Time.deltaTime;

        //移動
        transform.Translate(move);
        //rb.velocity = move * speed * Time.deltaTime;

    }
}
