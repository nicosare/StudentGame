using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgNumObj : MonoBehaviour
{
    private bool isMove;
    private Vector2 rndVec;

    private void Update()
    {
        if (!isMove) return;
        transform.Translate(rndVec * Time.deltaTime);
    }
    public void StartMotion(string damage, Color color, Vector2 vector, Vector2 position)
    {
        this.GetComponent<Text>().color = color;
        transform.localPosition = position;
        GetComponent<Text>().text = damage;
        rndVec = vector;
        isMove = true;
        GetComponent<Animation>().Play();
    }

}
