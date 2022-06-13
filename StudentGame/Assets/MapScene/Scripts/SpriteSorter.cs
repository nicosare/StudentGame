using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    [SerializeField]
    private float offset;
    [SerializeField]
    private bool isStatic;
    private int sortingOederBase = 60;
    private new Renderer renderer;
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }
    private void LateUpdate()
    {
        renderer.sortingOrder = (int)(sortingOederBase - transform.position.y * 10 + offset);
        if (isStatic)
            Destroy(this);
    }
}
