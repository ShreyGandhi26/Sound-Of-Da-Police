using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSlowDown : MonoBehaviour
{
    public float NewMaxSpeed = 1.5f;
    public float NewMaxAcceleration = 1.5f;

    public float OriginalSpeed = 7f;
    public float OriginalAcceleration = 7f;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<CarController>() != null)
        {
            other.GetComponent<CarController>().MaxSpeed = NewMaxSpeed;
            other.GetComponent<CarController>().Car_acceleration = NewMaxAcceleration;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<CarController>() != null)
        {

            other.GetComponent<CarController>().MaxSpeed = OriginalSpeed;
            other.GetComponent<CarController>().Car_acceleration = OriginalAcceleration;
        }
    }
}
