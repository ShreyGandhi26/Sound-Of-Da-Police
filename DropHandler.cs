using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    private Dragables dragablesName;
    public bool dropped = false;
    public List<Transform> SnapPoints;
    private GameObject loadedprefab;
    public float snapRange = 3f;



    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;
        float closestDist = -1;
        Transform closestSnapPoint = null;


        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            loadedprefab = Resources.Load(Dragables.CurrentDraggedObject) as GameObject;
            Vector3 mousePoint = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector3 mouseAsReq = new Vector3(mousePoint.x, mousePoint.y, 0);
            // print(mouseAsReq);
            foreach (Transform SnapPoint in SnapPoints)
            {
                float currentDistance = Vector2.Distance(mouseAsReq, SnapPoint.localPosition);

                if (closestSnapPoint == null || currentDistance < closestDist)
                {
                    closestSnapPoint = SnapPoint;
                    closestDist = currentDistance;
                }
            }
            if (closestSnapPoint != null && closestDist <= snapRange)
            {

                mouseAsReq = closestSnapPoint.localPosition;
                GameObject prefab = Instantiate(loadedprefab, mouseAsReq, Quaternion.identity);
                prefab.name = loadedprefab.name;
                //prefab.transform.SetParent(closestSnapPoint);
                //closestSnapPoint.GetComponent<SpriteRenderer>().enabled = false;
                closestSnapPoint.gameObject.tag = "Not Snap Point";
                dropped = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dragablesName = GetComponentInChildren<Dragables>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
