using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    [Header("Car Setting")]
    public float Car_acceleration = 20f;
    public float turnFactor = 3.5f;
    public float driftFactor = .5f;
    public float MaxSpeed = 20f;

    public float accelerationInput = 0;
    float steeringInput = 0;
    public float rotationAngle = 0;
    float velocityVsUp = 0;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        ApplyForwardForce();

        killExtraDrift();

        ControlSteer();
    }

    void ApplyForwardForce()
    {

        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);
        if (velocityVsUp > MaxSpeed && accelerationInput > 0)
            return;

        if (velocityVsUp < -MaxSpeed * .5 && accelerationInput < 0)
            return;

        if (rb.velocity.sqrMagnitude > MaxSpeed * MaxSpeed && accelerationInput > 0)
            return;

        if (accelerationInput == 0)
        {
            rb.drag = Mathf.Lerp(rb.drag, 3f, Time.fixedDeltaTime * 3);
        }
        else rb.drag = 0;

        Vector2 forwardForce = transform.up * accelerationInput * Car_acceleration;

        rb.AddForce(forwardForce, ForceMode2D.Force);
    }

    void ControlSteer()
    {
        float minSpeed = (rb.velocity.magnitude / 8);
        minSpeed = Mathf.Clamp01(minSpeed);

        rotationAngle -= steeringInput * turnFactor * minSpeed;

        rb.MoveRotation(rotationAngle);
    }

    void killExtraDrift()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void setInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
        if (accelerationInput < 0)
        {
            steeringInput = -steeringInput;
        }
        else
        {
            steeringInput = inputVector.x;
        }
    }

}
