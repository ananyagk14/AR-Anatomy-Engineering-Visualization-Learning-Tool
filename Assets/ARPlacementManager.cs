using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARPlacementManager : MonoBehaviour
{
    public GameObject selectedPrefab;

    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool isPlaced = false;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // ❌ stop reposition after placed
        if (isPlaced || selectedPrefab == null) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    // remove old if any
                    if (spawnedObject != null)
                    {
                        Destroy(spawnedObject);
                    }

                    spawnedObject = Instantiate(selectedPrefab, hitPose.position, hitPose.rotation);

                    isPlaced = true; // 🔒 lock position
                }
            }
        }
    }

    // ❤️🫀 Select model (Heart/Kidney/Engine)
    public void SetModel(GameObject newPrefab)
    {
        selectedPrefab = newPrefab;

        // 🔥 allow new placement
        isPlaced = false;

        // remove old model
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }
    }

    // 🔄 Reset button
    public void ResetPlacement()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }

        isPlaced = false;
        selectedPrefab = null;
    }
}