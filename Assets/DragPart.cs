using UnityEngine;

public class DragPart : MonoBehaviour
{
    private Camera cam;
    private GameObject selectedPart;

    private float distanceFromCamera;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        // 🔹 TOUCH START → SELECT PART
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = cam.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                selectedPart = hit.collider.gameObject;

                // Save distance from camera
                distanceFromCamera = Vector3.Distance(cam.transform.position, selectedPart.transform.position);
            }
        }

        // 🔹 DRAG → MOVE PART
        if (touch.phase == TouchPhase.Moved && selectedPart != null)
        {
            Ray ray = cam.ScreenPointToRay(touch.position);

            Vector3 newPos = ray.GetPoint(distanceFromCamera);

            selectedPart.transform.position = newPos;
        }

        // 🔹 RELEASE
        if (touch.phase == TouchPhase.Ended)
        {
            selectedPart = null;
        }
    }
}