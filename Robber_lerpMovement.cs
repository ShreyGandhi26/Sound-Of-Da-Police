using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Robber_lerpMovement : MonoBehaviour
{
    public bool shouldLerp = false;
    public bool collided = false;
    public float timeStartedLerping;
    public float lerpTime;
    public bool canChangeLevel = false;
    public string LevelName;
    public Transform StartPos;
    public Transform EndPos;
    public SpriteRenderer img1;
    public SpriteRenderer brokenCar;
    CarController car;
    public GameObject Effect;
    public bool missed = false;
    GameManager GM;


    // Start is called before the first frame update
    void Start()
    {
        Effect.SetActive(false);
        car = FindObjectOfType<CarController>();
        GM = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLerp == true)
        {
            transform.position = Lerp(StartPos.localPosition, EndPos.localPosition, timeStartedLerping, lerpTime);
            //transform.position = Vector3.Lerp(StartPos.localPosition, EndPos.localPosition, lerpTime * Time.deltaTime);

        }
        else
        {
            transform.localPosition = StartPos.localPosition;
            //timeStartedLerping = 0;
        }
        if (missed)
        {
            if (GM.bg.volume > 0)
            {
                GM.bg.volume -= .35f * Time.deltaTime;
            }
        }

    }

    public void startLerping()
    {
        timeStartedLerping = Time.time;
        shouldLerp = true;
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentComplete = timeSinceStarted / lerpTime;

        var result = Vector3.Lerp(start, end, percentComplete);

        return result;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CarController>())
        {
            canChangeLevel = true;
            // collided = true;
            // transform.position = other.transform.position;
            //car.GetComponent<AudioSource>().enabled = false;
            img1.gameObject.SetActive(false);
            Effect.SetActive(true);
            collided = true;
            EndPos.gameObject.SetActive(false);
            gameObject.GetComponent<Robber_lerpMovement>().enabled = false;

        }
        if (other.CompareTag("endPoint"))
        {
            Invoke("Replay", 5);
            missed = true;
            GM.SFX.clip = GM.MissedSFX;
            GM.SFX.Play();

        }
    }
    void Replay()
    {
        car.GetComponent<CarInput>().currentFuel = car.GetComponent<CarInput>().maxFuel;
        //car.GetComponent<CarInput>().fuel.text = "Fuel :" + car.GetComponent<CarInput>().currentFuel;
        SceneManager.LoadScene(LevelName);
    }

}
