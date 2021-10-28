using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletDamage;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Hit " + collider.tag);
        ObjectPooler.objectPoolerInstance.SpawnObject("BulletHit", transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}
