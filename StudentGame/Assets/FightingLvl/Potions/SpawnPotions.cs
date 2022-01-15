using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPotions : MonoBehaviour
{
    public GameObject potion;
    public EnemyFight enemy;
    private bool isStarted;

    private void Start()
    {
        isStarted = false;
    }
    private void Update()
    {
        if (enemy.GetLvl() == 3 && !isStarted)
        {
            StartCoroutine(Spawn());
            isStarted = true;
        }
    }

    IEnumerator Spawn()
    {
        while (GameObject.FindGameObjectsWithTag("HealPotion").Length < 5)
        {
            Instantiate(potion, transform.localPosition = new Vector3(Random.Range(-8f, 8f), 6, -2), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }

    }
}
