using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Car2d car;
    int currentCircle = 1;

    float pressedBreak = 0f;
    public GameObject levelController;
    public ParticleSystem particleSystemSmoke;
    private float particleEmissionRate = 0;
    private ParticleSystem.EmissionModule particleSystemSmokeEmissionModule;
    public TrailRenderer trackLeftWheel;
    public TrailRenderer trackRightWheel;

    void Awake()
    {
        particleSystemSmokeEmissionModule = particleSystemSmoke.emission;
        trackLeftWheel.emitting = false;
        trackRightWheel.emitting = false;
    }

    void Update()
    {
        if (levelController.GetComponent<LevelController>().isStartGame)
        {
            if (Input.GetKeyDown(KeyCode.W))
                this.GetComponent<AudioSource>().Play();
            else if (Input.GetKeyUp(KeyCode.W))
                this.GetComponent<AudioSource>().Stop();

            if (Input.GetKey(KeyCode.W))
            {
                car.throttleInput = 1f / 9f;
                particleEmissionRate = 50;
            }
            else
            {
                car.throttleInput = 0f;
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                this.GetComponents<AudioSource>()[1].Play();
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                this.GetComponents<AudioSource>()[1].Stop();

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                trackLeftWheel.emitting = true;
                trackRightWheel.emitting = true;
            }
            else
            {
                trackLeftWheel.emitting = false;
                trackRightWheel.emitting = false;
            }

            car.steerInput = Input.GetAxis("Horizontal") * -1;

            if (Input.GetKey(KeyCode.S))
                pressedBreak = Mathf.Min(pressedBreak + 0.02f, 1f);
            else
                pressedBreak = 0f;
            car.brakeInput = pressedBreak;

            if (currentCircle != car.circle && car.circle < 4)
            {
                currentCircle++;
                levelController.GetComponent<LevelController>().ChangeTextCircle(currentCircle);
            }

            if (car.circle == 4)
            {
                levelController.GetComponent<LevelController>().Win();
            }
        }

        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 10);
        particleSystemSmokeEmissionModule.rateOverTime = particleEmissionRate;
    }
}
