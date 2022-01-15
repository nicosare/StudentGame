using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    private bool isNextLevel = false;
    private bool isEndAnimations = false;
    private bool isStartAnimations = true;
    public bool IsNextLevel
    {
        get { return isNextLevel; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            isNextLevel = true;
            Hero.Instance.EndGame();
        }
    }

    private void Update()
    {
        if (isNextLevel && isStartAnimations)
        {
            isStartAnimations = false;
            StartCoroutine(WaitLizardAnimations());
        }

        if (isEndAnimations)
        {
            SceneManager.LoadScene("FightingLvl");
        }
    }

    private IEnumerator WaitLizardAnimations()
    {
        yield return new WaitForSeconds(2.7f);
        isEndAnimations = true;
    }
}
