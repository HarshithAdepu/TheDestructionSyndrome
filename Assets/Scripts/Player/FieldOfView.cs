using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public static FieldOfView fieldOfViewInstance;
    public float viewRadius, viewAngle;
    [SerializeField] int aimAssistAngle, aimAssistRadius;
    Collider2D[] entitiesInRadius;
    public LayerMask obstacleMask, enemyMask;
    public List<Transform> visibleEntities = new List<Transform>();
    void Awake()
    {
        fieldOfViewInstance = this;
    }
    private void FixedUpdate()
    {
        //FindVisibleEntities();
    }

    void FindVisibleEntities()
    {
        visibleEntities.Clear();
        RaycastHit2D hitData;
        int stepAngle = 1;
        for (int i = 0; i < aimAssistAngle; i++)
        {
            float angle = transform.eulerAngles.y - aimAssistAngle / 2 + stepAngle * i;
            Vector3 dir = DirFromAngle(angle, false);
            Debug.DrawRay(transform.position, dir, Color.red);
            hitData = Physics2D.Raycast(transform.position, dir * aimAssistRadius, aimAssistAngle, enemyMask);
            if (hitData.collider != null)
            {
                if (hitData.transform != visibleEntities[visibleEntities.Count - 2])
                    visibleEntities.Add(hitData.transform);
            }
        }

    }

    public Vector2 DirFromAngle(float angleDeg, bool global)
    {
        if (!global)
            angleDeg += transform.eulerAngles.z;

        return new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));
    }
}
