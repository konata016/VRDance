using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWave : MonoBehaviour
{
    GameObject cube;

    float rad = 0;

    bool Verflag = false;
    bool Wideflag = false;

    [SerializeField]
    float speed = 5.5f;
    [SerializeField]
    float max = 2.0f;
    [SerializeField]
    float wideMax = 2.0f;

    Vector3 pos;

    Color mColor = new Color(59.0f, 199.0f, 229.0f, 0.0f);
    Color startC = new Color(59.0f, 199.0f, 229.0f, 0.0f);
    Color endColor = new Color(255.0f, 0.0f, 0.0f, 0.0f);
    float colorT = 0.0f;
    public float colorSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        //cube.GetComponent<GameObject>();
        pos = this.transform.position;

        speed *= Mathf.Deg2Rad;
    }

    // Update is called once per frame
    void Update()
    {
        WaveMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wave")
        {
            Verflag = true;
            pos = this.transform.position;
            colorT = 0.6f;
            //Debug.Log("ok");
        }
        else if(other.tag == "wide")
        {
            Wideflag = true;
            pos = this.transform.position;
            colorT = 0.6f;
        }
    }

    private void WaveMove()
    {
        if (Wideflag)
        {
            colorT += rad;
            mColor = Color.Lerp(startC, endColor, colorT);
            this.GetComponent<Renderer>().material.SetColor("_EmissionColor", mColor * 0.005f);

            this.transform.position = new Vector3(pos.x, wideMax / 2 * Mathf.Sin(rad) + pos.y, pos.z);
            this.transform.localScale = new Vector3(0.3f, wideMax * Mathf.Sin(rad) + 0.3f, 0.3f);
            rad += speed * Time.deltaTime;
            if (rad >= Mathf.PI)
            {
                Wideflag = false;
                rad = 0;
                this.transform.position = new Vector3(pos.x, StepDetermination.groundPosition.y, pos.z);
                this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
        }
 
        if (Verflag)
        {          
            colorT += rad;
            mColor = Color.Lerp(startC, endColor, colorT);
            this.GetComponent<Renderer>().material.SetColor("_EmissionColor", mColor * 0.005f);

            this.transform.position = new Vector3(pos.x, max / 2 * Mathf.Sin(rad) + pos.y, pos.z);
            this.transform.localScale = new Vector3(0.3f, max * Mathf.Sin(rad) + 0.3f, 0.3f);
            rad += speed * Time.deltaTime;
            if (rad >= Mathf.PI)
            {
                Verflag = false;
                rad = 0;
                this.transform.position = new Vector3(pos.x, StepDetermination.groundPosition.y, pos.z);
                this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);                                           
            }
        }

        if (colorT > 0.0f && (Verflag == false && Wideflag == false))
        {
            colorT -= Time.deltaTime * colorSpeed;
            mColor = Color.Lerp(startC, endColor, colorT);
            this.GetComponent<Renderer>().material.SetColor("_EmissionColor", mColor * 0.005f);

            if (colorT < 0.0f)
            {
                this.GetComponent<Renderer>().material.SetColor("_EmissionColor", startC * 0.005f);
            }
        }
    }
}
