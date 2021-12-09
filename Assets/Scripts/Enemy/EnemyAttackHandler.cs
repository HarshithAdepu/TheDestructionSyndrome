using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    [SerializeField] float attackCoolDown, attackDamage;
    float timer;
    bool readyToAttack;
    void Start()
    {
        readyToAttack = false;
    }

    void Update()
    {
        if (timer >= attackCoolDown)
        {
            readyToAttack = true;
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            if (readyToAttack)
            {
                other.gameObject.GetComponent<PlayerHealthManager>().Damage((int)attackDamage);
                readyToAttack = false;
            }
        }

    }
}
