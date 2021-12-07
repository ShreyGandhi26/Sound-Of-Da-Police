using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetTheRoad : MonoBehaviour
{

    public string goName;
    public GameObject[] UI_roads;

    // Start is called before the first frame update
    void Start()
    {
        goName = GetComponentInParent<Dragables>().name;
    }

    // Update is called once per frame
    void Update()
    {
        UI_roads = GameObject.FindGameObjectsWithTag("UI_Road");
        //print(UI_roads.Length);
    }

    public void OnCrossButton()
    {

        Destroy(GetComponentInParent<Dragables>().gameObject);
        UI_roads = GameObject.FindGameObjectsWithTag("UI_Road");

        for (int i = 0; i < UI_roads.Length; i++)
        {
            //print("In Loop");
            if (goName == UI_roads[i].name)
            {
                //print(UI_roads[i]);
                UI_roads[i].GetComponent<Image>().enabled = true;
                UI_roads[i].transform.localPosition = Vector3.zero;
                //UI_roads[i].GetComponent<RectTransform>().position = Vector3.zero;  
                //print("here");
            }
        }
    }
}
