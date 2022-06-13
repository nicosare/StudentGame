using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dictionary<string,string> answers;
    public void TriggerDialogue()
    {
        FindObjectOfType<NPCDialogue>().StartDialogue(dialogue);
    }
}
