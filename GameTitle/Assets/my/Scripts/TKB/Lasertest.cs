using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasertest : MonoBehaviour
{
    [SerializeField]
    GameObject ePosition; //発射位置
    [SerializeField]
    GameObject tGround; //床
    [SerializeField]
    GameObject LaserTraces; //レーザー痕プレハブ

    List<GameObject> Traces = new List<GameObject>();

    LineRenderer lineRenderer;
    Vector3 hitPos;　        //レーザーヒット座標
    Vector3 tmpPos;　        //ヒット座標記憶用
    Vector3 targetPos;       //レーザーの目標座標
    float atkPosx;   //攻撃座標X

    float lazerDistance = 20.0f;
    float lazerStartPointDistance = 0.15f;
    public float lineWidth = 1.0f;   //レーザーの太さ
    public float tracesSpace = 0.06f; //痕の間隔
    public float laserDelay = 1.0f; //レーザーが動き始めるタイミング

    private float laserSpeed;
    private float speedAcceleration;

    float RowCutAccelerationCal(float x)
    {
        float y = x * x - laserDelay;

        if (y < 0) return 0;

        return y;
    }

    void Reset()
    {
        atkPosx = 8.0f;
        laserSpeed = 0f;
        speedAcceleration = 0f;       

        Traces.ForEach(t =>
        {
            Destroy(t);
        });
        Traces.Clear();

        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        targetPos = tGround.transform.position + new Vector3(atkPosx, 0, 0);
        tmpPos = targetPos;
    }

    void Start()
    {
        atkPosx = 8.0f;
        laserSpeed = 0f;
        speedAcceleration = 0f;

        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        targetPos = tGround.transform.position + new Vector3(atkPosx, 0, 0);
        tmpPos = targetPos;
    }

    void Update()
    {
        OnRay();
    }

    void OnRay()
    { 
        Vector3 rayStartPosition = ePosition.transform.position;
        RaycastHit hit;

        //if (Input.GetKey(KeyCode.A)) atkPosx -= 0.2f;       
        //if (Input.GetKey(KeyCode.D)) atkPosx += 0.2f;
        if (Input.GetKey(KeyCode.R)) Reset();

        speedAcceleration += Time.deltaTime;
        laserSpeed = RowCutAccelerationCal(speedAcceleration);
        atkPosx -= laserSpeed;
        if (atkPosx <= -10.0f) atkPosx = -10.0f;
        
        targetPos = tGround.transform.position + new Vector3(atkPosx, 0, 0);

        Ray ray = new Ray(rayStartPosition, - (ePosition.transform.position - targetPos));

        lineRenderer.SetPosition(0, rayStartPosition);

        if (Physics.Raycast(ray, out hit, lazerDistance))
        {
            hitPos = hit.point;
            lineRenderer.SetPosition(1, hitPos);

            Vector3 pos = hitPos + (hit.normal * 0.01f);
            if (tmpPos.x - tracesSpace > hitPos.x)
            {
                tmpPos = pos;
                //ヒットしたオブジェクトの法線方向に回転
                Quaternion rot = Quaternion.FromToRotation(-1 * LaserTraces.transform.forward, hit.normal);

                Traces.Add(Instantiate(LaserTraces, pos, rot));
            }
        }
        else
        {
            lineRenderer.SetPosition(1, targetPos + -(ePosition.transform.position - targetPos));
        }

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.1f);
    }
}