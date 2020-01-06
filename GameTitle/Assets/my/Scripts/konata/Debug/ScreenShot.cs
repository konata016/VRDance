using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShot : MonoBehaviour
{
    public int num;
    RenderTexture render;
    // Start is called before the first frame update
    void Start()
    {
        render = new RenderTexture(1280, 720, 32, RenderTextureFormat.ARGB32);
        GetComponent<Camera>().targetTexture = render;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            CaptureSceneCameraView();
            Debug.Log(num);
        }
    }
    void CaptureSceneCameraView()
    {
        // 保存するパス
        var filePath = string.Format("Assets/my/ScreenShot/image" + num + ".png", Application.dataPath);

        Texture2D tex = new Texture2D(render.width, render.height, TextureFormat.RGB24, false);
        RenderTexture.active = render;
        tex.ReadPixels(new Rect(0, 0, render.width, render.height), 0, 0);
        tex.Apply();

        // PNGに変換
        byte[] bytes = tex.EncodeToPNG();
        // 保存する
        File.WriteAllBytes(filePath, bytes);
    }

}
