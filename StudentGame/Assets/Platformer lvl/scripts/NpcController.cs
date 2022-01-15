using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    private DialogueTrigger DialogueTrigger;
    private bool isActive = false;
    private void Awake()
    {
        DialogueTrigger = GetComponent<DialogueTrigger>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero") && !isActive)
        {
            isActive = true;
            DialogueTrigger.TriggerDialogue();
        }
    }
}
