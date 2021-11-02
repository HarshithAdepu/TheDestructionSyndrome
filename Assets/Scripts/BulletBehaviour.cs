using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletDamage;
    [SerializeField] float bulletForce;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Hit " + collider.tag);
        ObjectPooler.objectPoolerInstance.SpawnObject("BulletHit", transform.position, transform.rotation);
        if (collider.GetComponent<Rigidbody2D>() != null)
            collider.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<Rigidbody2D>().velocity * bulletForce * Time.deltaTime, ForceMode2D.Force);
        gameObject.SetActive(false);
    }
}
