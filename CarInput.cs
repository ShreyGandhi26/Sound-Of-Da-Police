using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarInput : MonoBehaviour
{
    CarController carController;
    public bool SirenIsOn;
    GameObject fuel;
    public float fuelInterval = 2;
    public float currentFuel;
    public float maxFuel;
    public float fuelsubtractor = 0.5f;
    public AudioSource policeSiren;
    float timer = 0;

    private void Awake()
    {
        carController = GetComponent<CarController>();
        //SirenIsOn = true;
        currentFuel = maxFuel;
        fuel = GameObject.FindGameObjectWithTag("Fuel");
        GetComponent<AudioSource>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            carController.accelerationInput = 0;
            carController.GetComponent<Rigidbody2D>().drag = Mathf.Lerp(carController.GetComponent<Rigidbody2D>().drag, 3f, Time.fixedDeltaTime * 5);
        }
        else
        {
            Vector2 inputVector = Vector2.zero;
            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");
            carController.setInputVector(inputVector);

            if (inputVector.y > 0)
            {
                timer += Time.deltaTime;
                if (timer > fuelInterval)
                {
                    currentFuel -= fuelsubtractor;
                    //fuel.GetComponent<Image>().fillAmount = currentFuel;
                    timer = 0;
                }
            }
        }

        if (currentFuel <= 0)
        {
            carController.GetComponent<Rigidbody2D>().drag = Mathf.Lerp(carController.GetComponent<Rigidbody2D>().drag, 3f, Time.fixedDeltaTime * 5);
            //carController.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            enabled = false;
            carController.enabled = false;
        }

        fuel.GetComponent<Image>().fillAmount = currentFuel;

        if (Input.GetKeyDown(KeyCode.H))
        {

            if (SirenIsOn == true)
            {
                SirenIsOn = false;
                GetComponent<AudioSource>().enabled = true;
                GetComponentInChildren<Animator>().enabled = true;
            }
            else
            {
                SirenIsOn = true;
                GetComponent<AudioSource>().enabled = false;
                GetComponentInChildren<Animator>().enabled = false;
            }
        }


    }

}
