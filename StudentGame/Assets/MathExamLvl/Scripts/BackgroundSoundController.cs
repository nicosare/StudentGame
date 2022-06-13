using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundController : MonoBehaviour
{
    void Update()
    {
        if (SceneController.haveEnd)
            this.GetComponents<AudioSource>()[1].Stop();
    }
}
