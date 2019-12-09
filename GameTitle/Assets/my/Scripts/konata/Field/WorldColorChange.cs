using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WorldColorChange : MonoBehaviour
{
    public Color color;
    public PostProcessVolume post;
    ColorGrading colorGrading;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach(PostProcessEffectSettings item in post.profile.settings)
        {
            if(item as ColorGrading)
            {
                colorGrading = item as ColorGrading;
            }
        }

        if (colorGrading)
        {
            colorGrading.colorFilter.value= color;
        }
    }
}
