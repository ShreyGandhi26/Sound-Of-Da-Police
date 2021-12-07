using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberSlowAbility : MonoBehaviour
{
    Robber_lerpMovement rob;
    public float newLerpTime;

    private void Awake()
    {
        rob = FindObjectOfType<Robber_lerpMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponents<Robber_lerpMovement>() != null)
        {
            rob.lerpTime = newLerpTime;

            Destroy(gameObject);
        }
    }

}
