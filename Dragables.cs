using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dragables : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public delegate void DragEndedDelegate(Dragables dragableObj);

    public DragEndedDelegate dragEndedCallback;


    public static string CurrentDraggedObject;
    private DropHandler dropHandler;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        CurrentDraggedObject = gameObject.name;
        dropHandler.dropped = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //dragEndedCallback(this);
        if (dropHandler.dropped == true)
        {
            GetComponent<Image>().enabled = false;
            //gameObject.SetActive(false);
            CurrentDraggedObject = null;
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dropHandler = GetComponentInParent<DropHandler>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
