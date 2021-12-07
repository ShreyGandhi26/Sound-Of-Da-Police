using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenListen : MonoBehaviour
{
    CarInput input;
    Robber_lerpMovement rob;
    public float newLerpTime;

    private void Awake()
    {
        input = FindObjectOfType<CarInput>();
        rob = FindObjectOfType<Robber_lerpMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.GetComponent<CarInput>() != null)
        // {
        //     if (input.SirenIsOn == true)
        //         rob.lerpTime = newLerpTime;
        // }
    }
}
