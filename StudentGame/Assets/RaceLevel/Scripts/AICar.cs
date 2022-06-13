using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : MonoBehaviour
{
    [SerializeField]
    Car2d car;

    public GameObject[] waypoints;
    int currentWP = 0;
    float speed = 10f;
    public GameObject levelController;

    private void Update()
    {
        if (levelController.GetComponent<LevelController>().isStartGame)
        {
            if (Vector2.Distance(this.transform.position, waypoints[currentWP].transform.position) < 2)
                currentWP++;
            if (currentWP > waypoints.Length - 1)
                currentWP = 0;
            if (Vector2.Distance(this.transform.position, waypoints[currentWP].transform.position) > 3)
                car.throttleInput = 1f/4f;
            else
                car.brakeInput = 2;

            DoRorate();

            if (car.circle == 4)
            {
                levelController.GetComponent<LevelController>().Lose();
            }
        }
    }

    private void DoRorate()
    {
        Vector2 direction = waypoints[currentWP].transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
