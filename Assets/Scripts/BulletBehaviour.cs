using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletDamage;
    public float bulletSpeed;
    [SerializeField] float bulletImpactForce;

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
    }
    private void OnDisable()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Hit " + collider.tag);

        ObjectPooler.objectPoolerInstance.SpawnObject("BulletHit", transform.position, transform.rotation);

        if (collider.GetComponent<Rigidbody2D>() != null)
            collider.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<Rigidbody2D>().velocity * bulletImpactForce * Time.deltaTime, ForceMode2D.Force);

        gameObject.SetActive(false);
    }
}
