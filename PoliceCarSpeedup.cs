using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarSpeedup : MonoBehaviour
{
    CarController car;
    public float NewCar_acceleration;
    public float NewMaxSpeed;

    private void Awake()
    {
        car = FindObjectOfType<CarController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CarController>() != null)
        {
            car.Car_acceleration = NewCar_acceleration;
            car.MaxSpeed = NewMaxSpeed;
            Destroy(gameObject);
        }
    }

}
