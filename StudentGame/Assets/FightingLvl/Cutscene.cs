using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    private bool isEnable;

    public void On()
    {
        isEnable = true;
    }

    public void Off()
    {
        isEnable = false;

    }

    public bool IsEnabled()
    {
        return isEnable;
    }
}
