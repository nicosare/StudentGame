using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Car2d : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    // педаль газа
    [HideInInspector]
    public float throttleInput;
    // педаль тормоза
    [HideInInspector]
    public float brakeInput = 0f;
    // положение руля
    [HideInInspector]
    public float steerInput = 0f;

    // скорость км/ч
    [HideInInspector]
    public float kmh = 0f;
    // скорость м/с
    [HideInInspector]
    float ms = 0f;

    float maxVelMS = 48f / 3.6f;

    [SerializeField]
    AnimationCurve accel;

    // максимальный угол поворота
    float maxRotAngle = 120f;
    // скорость, после которой ухудшается поворот
    float goodSpeed = 20f / 3.6f;
    // велечина ухудшения поворота
    float deltaRot = 60f;
    // угол поворота
    float rotAngle = 0f;

    public bool[] chekpoints = new bool[3] { false, false, false };
    public int circle = 1;

    void MoveCar()
    {
        rb.velocity += maxVelMS * throttleInput * (Vector2)transform.up * accel.Evaluate(ms / maxVelMS);
    }
    void RotateCar()
    {
        if (ms <= goodSpeed)
            rotAngle = maxRotAngle;
        else
            rotAngle = maxRotAngle - Mathf.Lerp(0f, deltaRot, (ms - goodSpeed) / (maxVelMS - goodSpeed));

        transform.Rotate(0f, 0f, rotAngle * steerInput * Time.deltaTime);
    }
    void Brakes()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.deltaTime * brakeInput);
    }

    private void FixedUpdate()
    {
        ms = rb.velocity.magnitude;
        kmh = 3.6f * ms;

        if (throttleInput != 0f)
            MoveCar();

        if (steerInput != 0f)
        {
            RotateCar();
            var oldDir = rb.velocity / ms;
            var newDir = (Vector2)transform.up + oldDir;
            newDir.Normalize();
            rb.velocity = ms * transform.up;
        }

        if (brakeInput != 0f)
            Brakes();

        if (isAllCheckComplited())
        {
            circle++;
            ResetCheckpoints();
        }
    }

    public void SetCheck(int index)
    {
        if (chekpoints[index] == false)
        {
            if (index == 0)
                chekpoints[index] = true;
            else if (index == 1 && chekpoints[index - 1])
                chekpoints[index] = true;
            else if (index == 2 && chekpoints[index - 1] && chekpoints[index - 2])
                chekpoints[index] = true;
        }
    }

    private bool isAllCheckComplited()
    {
        var isAllCheckComplited = true;

        for (var i = 0; i < chekpoints.Length; i++)
            if (chekpoints[i] == false)
                isAllCheckComplited = false;

        return isAllCheckComplited;
    }

    private void ResetCheckpoints()
    {
        chekpoints = new bool[3] { false, false, false };
    }
}
