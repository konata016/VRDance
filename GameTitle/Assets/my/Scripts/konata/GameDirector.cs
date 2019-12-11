using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public float uiNotesFixTime = -0.2f;
    public float groundNotesFixTime = -0.18f;

    public static GameDirector GetGameDirector { get; private set; }

    private void Awake()
    {
        GetGameDirector = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
