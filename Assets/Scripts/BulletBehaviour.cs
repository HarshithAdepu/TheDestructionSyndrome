using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletDamage;
    public float bulletSpeed;
    public float bulletImpactForce;
    public float bulletSpread;
    public Vector2 bulletDirection;
    private void OnEnable()
    {
        bulletDirection = GameObject.Find("Player").transform.right;
        bulletDamage = GameManager.instance.currentWeapon.damage;
        bulletSpeed = GameManager.instance.currentWeapon.speed;
        bulletImpactForce = GameManager.instance.currentWeapon.impactforce;
        bulletSpread = GameManager.instance.currentWeapon.spread;

        float x = Random.Range(-1f, 1f) * bulletSpread;
        float y = Random.Range(-1f, 1f) * bulletSpread;

        GetComponent<Rigidbody2D>().velocity = (bulletDirection + new Vector2(x, y)) * bulletSpeed;
    }
    private void OnDisable()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Hit " + collider.tag);
        ObjectPooler.objectPoolerInstance.SpawnObject("BulletHit", transform.position, transform.rotation);

        switch (collider.tag)
        {
            case "Enemy":
                if (collider.GetComponent<Rigidbody2D>() != null)
                    collider.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<Rigidbody2D>().velocity * bulletImpactForce * Time.deltaTime, ForceMode2D.Force);
                gameObject.SetActive(false);
                break;
            case "Wall": gameObject.SetActive(false); break;
            case "Window": gameObject.SetActive(false); break;
            default: break;
        }
    }
    void Update()
    {
    }
}
