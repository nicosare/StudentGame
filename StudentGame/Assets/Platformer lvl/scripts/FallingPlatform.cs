using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    public void OffCollider ()
    {
        boxCollider2D.enabled = false;
        StartCoroutine(OnColliderCoolDown());
    }

    private IEnumerator OnColliderCoolDown()
    {
        yield return new WaitForSeconds(1f);
        boxCollider2D.enabled = true;
    }
}
