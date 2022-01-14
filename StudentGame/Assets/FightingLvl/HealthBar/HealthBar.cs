using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private Image image;

    private void Awake()
    {
        image = GameObject.Find("Fill").GetComponent<Image>();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        if (SceneManager.GetActiveScene().name == "Platformer lvl")
            switch (health)
            {
                //case 100:
                //    {
                //        image.color = new Color(0 / 255.0f, 200 / 255.0f, 0 / 255.0f);
                //    }
                //    break;

                case 80:
                    {
                        image.color = new Color(125 / 255.0f, 167 / 255.0f, 0 / 255.0f);
                    }
                    break;

                case 60:
                    {
                        image.color = new Color(168 / 255.0f, 130 / 255.0f, 0 / 255.0f);
                    }
                    break;

                case 40:
                    {
                        image.color = new Color(193 / 255.0f, 84 / 255.0f, 0 / 255.0f);
                    }
                    break;

                case 20:
                    {
                        image.color = new Color(200 / 255.0f, 0 / 255.0f, 0);
                    }
                    break;
            }
    }
}
