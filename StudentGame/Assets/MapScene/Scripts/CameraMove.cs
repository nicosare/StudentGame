using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform car;
    private Vector3 pos;
    private float Xc, Yc;

    private void Update()
    {
        if (player.gameObject.activeSelf)
        {
            pos = player.position;
            pos.z = -12f;
            transform.position = Vector3.Lerp(transform.position, pos, 3 * Time.deltaTime);

            if (transform.position.x < -12.5f)
            {
                Xc = -12.5f - transform.position.x;
            }
            else if (transform.position.x > 4.5f)
            {
                Xc = 4.5f - transform.position.x;
            }
            else
                Xc = 0;

            if (transform.position.y < -4)
            {
                Yc = -4 - transform.position.y;
            }
            else if (transform.position.y > 8)
            {
                Yc = 8 - transform.position.y;
            }
            else
                Yc = 0;

            if ((Xc != 0) | (Yc != 0))
                transform.position = new Vector3(transform.position.x + Xc,
                    transform.position.y + Yc, transform.position.z);
        }
        else
        {
            pos = car.position;
            pos.z = -12f;
            transform.position = Vector3.Lerp(transform.position, pos, 2 * Time.deltaTime);
        }}
}
