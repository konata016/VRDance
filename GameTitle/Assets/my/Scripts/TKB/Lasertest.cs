using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasertest : MonoBehaviour
{
    [SerializeField]
    GameObject ePosition;
    [SerializeField]
    GameObject tGround;
    [SerializeField]
    GameObject LaserTrail;

    List<GameObject> Trails = new List<GameObject>();

    LineRenderer lineRenderer;
    Vector3 hitPos;
    Vector3 tmpPos;
    Vector3 targetPos;
    float atkStartx = 10.0f;

    float lazerDistance = 10.0f;
    float lazerStartPointDistance = 0.15f;
    float lineWidth = 1.0f;

    void Reset()
    {
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
    }

    void Start()
    {
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        targetPos = tGround.transform.position + new Vector3(atkStartx, 0, 0);
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

        if (Input.GetKey(KeyCode.A))
        {
            atkStartx -= 0.2f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            atkStartx += 0.2f;
        }
        targetPos = tGround.transform.position + new Vector3(atkStartx, 0, 0);

        Ray ray = new Ray(rayStartPosition, - (ePosition.transform.position - targetPos));

        lineRenderer.SetPosition(0, rayStartPosition);

        if (Physics.Raycast(ray, out hit, lazerDistance))
        {
            hitPos = hit.point;
            lineRenderer.SetPosition(1, hitPos);

            Vector3 pos = hitPos + (hit.normal * 0.01f);
            if (tmpPos.x - 0.2f > hitPos.x)
            {
                tmpPos = pos;
                Quaternion rot = Quaternion.FromToRotation(-1 * LaserTrail.transform.forward, hit.normal);

                Instantiate(LaserTrail, pos, rot);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, targetPos + -(ePosition.transform.position - targetPos));
        }

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.1f);

    }
}