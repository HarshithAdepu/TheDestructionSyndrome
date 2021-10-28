using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectBehaviour : MonoBehaviour
{
    public Animator effectanimator;
    private void Start()
    {

    }
    private void OnEnable()
    {
        effectanimator.SetTrigger("BulletHit");
    }
    private void DisableEffect()
    {
        this.gameObject.SetActive(false);
    }
}
