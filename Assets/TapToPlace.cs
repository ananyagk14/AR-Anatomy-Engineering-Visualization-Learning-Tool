using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class TapToPlaceWithReset : MonoBehaviour
{
    public GameObject objectToPlace;

    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool isPlaced = false;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();

        // 🔥 Preload model (hidden)
        spawnedObject = Instantiate(objectToPlace);
        spawnedObject.SetActive(false);
    }

    void Update()
    {
        if (isPlaced) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    spawnedObject.transform.position = hitPose.position;
                    spawnedObject.transform.rotation = hitPose.rotation;

                    spawnedObject.SetActive(true);

                    isPlaced = true;
                }
            }
        }
    }

    // 🔄 RESET FUNCTION
    public void ResetPlacement()
    {
        if (spawnedObject != null)
        {
            spawnedObject.SetActive(false); // hide model
        }

        isPlaced = false; // allow placing again
    }
}