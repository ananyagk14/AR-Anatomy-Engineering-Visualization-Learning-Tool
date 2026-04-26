using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Distance between touches
            float currentDistance = Vector2.Distance(touch0.position, touch1.position);

            // First time touching
            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                initialDistance = currentDistance;
                initialScale = transform.localScale;
            }
            else
            {
                if (Mathf.Approximately(initialDistance, 0))
                    return;

                float scaleFactor = currentDistance / initialDistance;

                transform.localScale = initialScale * scaleFactor;
            }
        }
    }
}