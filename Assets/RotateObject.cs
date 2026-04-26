using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public float rotationSpeed = 0.2f;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float rotationY = touch.deltaPosition.x * rotationSpeed;

                // ✅ Only Y axis rotation (side rotation)
                transform.Rotate(0, -rotationY, 0);
            }
        }
    }
}