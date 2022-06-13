using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPaint : MonoBehaviour
{
    private Dictionary<int, (int, int)> keyValues = new Dictionary<int, (int, int)>()
    {
        {1, (0, 0)},
        {2, (0, 1)},
        {3, (0, 2)},
        {4, (1, 0)},
        {5, (1, 1)},
        {6, (1, 2)},
        {7, (2, 0)},
        {8, (2, 1)},
        {9, (2, 2)}
    };
    private Image color;
    private (int, int) indexes;
    public GameObject panel;
    public Sprite anActive;
    public Sprite active;

    private void Start()
    {
        indexes = keyValues[int.Parse(this.name)];
        color = this.GetComponent<Image>();
    }

    public void ChangeColorAndGiveInfo()
    {
        if (!PanelController.isComplete)
        {
            if (color.sprite == anActive)
            {
                color.sprite = active;
                panel.GetComponent<PanelController>().SetColor(Color.green, indexes);
            }
            else
            {
                color.sprite = anActive;
                panel.GetComponent<PanelController>().SetColor(Color.white, indexes);
            }
        }
    }
}
