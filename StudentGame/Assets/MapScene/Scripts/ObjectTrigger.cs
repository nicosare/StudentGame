using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTrigger : MonoBehaviour
{
    public GameObject player;
    public Text actionText;
    public GameObject actionButton;
    public bool NPC;
    public bool Door;
    public bool Car;
    public string action;
    private Animator cameraAnimator;
    private Camera camera;

    private void Start()
    {
        camera = FindObjectOfType<Camera>();
        cameraAnimator = camera.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            cameraAnimator.SetBool("ZoomIn", true);
            actionButton.SetActive(true);
            actionText.text = "[F] - " + action;

            if (Door)
                this.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            cameraAnimator.SetBool("ZoomIn", false);
            actionButton.SetActive(false);
            if (NPC)
                FindObjectOfType<NPCDialogue>().EndDialogue();
            if (Door)
                this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    public void TriggerAction()
    {
        actionButton.SetActive(false);
        if (NPC)
            GetComponent<NPCDialogueTrigger>().TriggerDialogue();
        if (Car)
            GetComponent<ActionCar>().GetInCar();
    }
    private void Update()
    {
        if (actionButton.activeSelf && Input.GetKeyDown(KeyCode.F))
            TriggerAction();
    }
}
