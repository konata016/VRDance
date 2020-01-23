using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSceneUiManager : MonoBehaviour
{
    public static bool checkScene { get; set; }

    public GameObject select;
    public GameObject check;

    // Start is called before the first frame update
    void Start()
    {
        checkScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkScene)
        {
            select.SetActive(false);
            check.SetActive(true);
        }
        else
        {
            check.SetActive(false);
            select.SetActive(true);
        }
    }
}
