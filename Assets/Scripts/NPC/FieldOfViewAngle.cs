using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewAngle : MonoBehaviour
{
    [SerializeField]
    private float viewAngle;
    [SerializeField]
    private float viewDistance;
    [SerializeField]
    private LayerMask targetLayer;

    private Pig pig;

    private void Awake()
    {
        pig = GetComponent<Pig>();
    }

    private void Update()
    {
        View();
    }

    private Vector3 BoundaryAngle(float angle)
    {
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    private void View()
    {
        Vector3 leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Debug.DrawRay(transform.position + transform.up, leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, rightBoundary, Color.red);

        Collider[] target = Physics.OverlapSphere(transform.position, viewDistance, targetLayer);
        for (int i = 0; i < target.Length; i++)
        {
            Transform targetTransform = target[i].transform;
            if (targetTransform.name == "Player")
            {
                Vector3 direction = (targetTransform.position - transform.position).normalized;
                float angle = Vector3.Angle(direction, transform.forward);

                if (angle < viewAngle * 0.5f)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + transform.up, direction, out hit, viewDistance))
                    {
                        if (hit.transform.name == "Player")
                        {
                            Debug.DrawRay(transform.position + transform.up, direction, Color.blue);
                            Debug.Log("플레이어 발견");
                            pig.Run(hit.transform.position);
                        }
                    }
                }
            }
        }
    }
}
