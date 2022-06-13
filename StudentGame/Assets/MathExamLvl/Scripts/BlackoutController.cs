using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackoutController : MonoBehaviour
{
    public GameObject tablet;
    private float timeStart = 60;
    private bool isStartEnding;
    public Animator animator;


    void Update()
    {
        if(!tablet.GetComponent<TabletController>().isStartGame)
        {
            timeStart -= Time.deltaTime;
            if (timeStart < 0 && !isStartEnding)
            {
                animator.SetBool("isStartAnimation", true);
                isStartEnding = true;
                SceneController.AddEnding(0);
                this.GetComponents<AudioSource>()[0].Play();
                this.GetComponents<AudioSource>()[2].Play();
            }
        }
    }
}
