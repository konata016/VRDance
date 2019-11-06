using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipplesShaderControl : MonoBehaviour
{
    public Shader shader;
    Material material;
    float timer;
    
    void Start()
    {
       material  = new Material(shader);
        GetComponent<Renderer>().material = material;
    }
    
    void Update()
    {
        if (timer < 1)
        {
            timer += Time.deltaTime;
            GetComponent<Renderer>().material.SetFloat("Vector1_A66F919D", timer);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
