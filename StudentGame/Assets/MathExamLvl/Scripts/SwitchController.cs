using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    private Slider switch_;
    public GameObject bomb;
    public GameObject panel;
    private void Start()
    {
        switch_ = this.GetComponent<Slider>();
    }

    public void Toggle()
    {
        if (switch_.value == 1)
        {
            bomb.SetActive(true);
            switch_.interactable = false;
        }
        else if (switch_.value == -1)
        {
            panel.SetActive(true);
            switch_.interactable = false;
        }
    }
}
