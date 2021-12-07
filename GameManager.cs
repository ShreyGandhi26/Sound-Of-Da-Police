using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool canDrive = false;
    private Vector2 CarStartPos;
    private Vector3 CarStartRot;
    public GameObject car;
    public GameObject robberCar;
    public GameObject inventory;
    public GameObject Build;
    public GameObject Play;
    public int noOfPoint;
    public AudioSource bg;
    public AudioClip build;
    public AudioClip chase;
    public float levelBasedLocalRotation;
    public string LevelName;
    public string NextLevelName;
    private GameObject[] ResourcesRoads;
    public AudioSource SFX;
    public AudioClip CaughtSFX;
    public AudioClip MissedSFX;
    Vector2 ZeroForce;
    private Robber_lerpMovement robber;
    bool isChangingLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(true);
        Build.SetActive(false);
        Play.SetActive(true);
        car.GetComponent<CarController>().enabled = false;
        car.GetComponent<CarInput>().enabled = false;
        CarStartPos = car.transform.localPosition;
        CarStartRot = car.transform.localEulerAngles;
        ZeroForce = new Vector2(0, 0);
        bg.clip = build;
        bg.Play();
        robber = FindObjectOfType<Robber_lerpMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ResourcesRoads = GameObject.FindGameObjectsWithTag("ResourceRoads");

        noOfPoint = GameObject.FindGameObjectsWithTag("Snap Points").Length;

        if (robber.canChangeLevel == true)
        {
            isChangingLevel = true;
        }
        if (isChangingLevel == true)
        {
            Invoke("nextLevel", 3);
            //robber.canChangeLevel = false;
            isChangingLevel = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(LevelName);
        }
        if (robber.collided)
        {
            SFX.clip = CaughtSFX;
            SFX.Play();
            if (bg.volume > 0)
            {
                bg.volume -= .3f * Time.deltaTime;
            }
            else
            {
                robber.collided = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void PlayButton()
    {
        if (noOfPoint <= 0)
        {
            inventory.SetActive(false);
            Build.SetActive(true);
            Play.SetActive(false);
            car.GetComponent<CarController>().enabled = true;
            car.GetComponent<CarInput>().enabled = true;
            car.GetComponent<AudioSource>().enabled = true;
            car.GetComponentInChildren<Animator>().enabled = true;
            robberCar.GetComponent<Robber_lerpMovement>().startLerping();
            car.GetComponent<CarController>().rotationAngle = levelBasedLocalRotation;
            for (int i = 0; i < ResourcesRoads.Length; i++)
            {
                ResourcesRoads[i].GetComponentInChildren<Canvas>().enabled = false;
            }
            bg.clip = chase;
            bg.Play();
        }
    }

    public void BuildButton()
    {
        inventory.SetActive(true);
        Build.SetActive(false);
        Play.SetActive(true);
        car.GetComponent<CarController>().enabled = false;
        car.GetComponent<CarInput>().enabled = false;
        car.GetComponent<AudioSource>().enabled = false;
        car.GetComponentInChildren<Animator>().enabled = false;
        car.GetComponent<CarInput>().currentFuel = car.GetComponent<CarInput>().maxFuel;
        //car.GetComponent<CarInput>().fuel = "Fuel :" + car.GetComponent<CarInput>().currentFuel;
        car.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        car.GetComponent<Rigidbody2D>().angularVelocity = 0;
        car.transform.localPosition = CarStartPos;
        car.transform.eulerAngles = CarStartRot;
        robberCar.GetComponent<Robber_lerpMovement>().shouldLerp = false;
        for (int i = 0; i < ResourcesRoads.Length; i++)
        {
            ResourcesRoads[i].GetComponentInChildren<Canvas>().enabled = true;
        }
        bg.clip = build;
        bg.Play();
    }

    void nextLevel()
    {
        robber.canChangeLevel = false;
        car.GetComponent<CarInput>().currentFuel = car.GetComponent<CarInput>().maxFuel;
        //car.GetComponent<CarInput>().fuel.text = "Fuel :" + car.GetComponent<CarInput>().currentFuel;
        SceneManager.LoadScene(NextLevelName);
    }


}
