using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBreaker : MonoBehaviour
{
    CarController car;

    private void Awake()
    {
        car = FindObjectOfType<CarController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CarController>() != null)
        {
            car.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Destroy(gameObject);
        }
    }
}
