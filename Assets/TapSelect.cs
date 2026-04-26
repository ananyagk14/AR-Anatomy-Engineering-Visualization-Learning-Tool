using UnityEngine;
using TMPro;

public class TapSelect : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    private GameObject selectedObject;
    private Color originalColor;

    void Update()
    {
        // Check touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject tapped = hit.collider.gameObject;

                // 🔥 FIX: Get PartInfo from parent if needed
                PartInfo info = tapped.GetComponent<PartInfo>();

                if (info == null)
                {
                    info = tapped.GetComponentInParent<PartInfo>();
                }

                // Reset previous selection
                if (selectedObject != null)
                {
                    Renderer prevRenderer = selectedObject.GetComponent<Renderer>();
                    if (prevRenderer != null)
                    {
                        prevRenderer.material.color = originalColor;
                    }
                }

                // Highlight new selection
                Renderer renderer = tapped.GetComponent<Renderer>();

                if (renderer != null)
                {
                    selectedObject = tapped;
                    originalColor = renderer.material.color;
                    renderer.material.color = Color.cyan; // Highlight color
                }

                // Show info
                if (info != null)
                {
                    titleText.text = info.partName;
                    descriptionText.text = info.description;
                }
                else
                {
                    // Debug message if missing PartInfo
                    titleText.text = "No Data Found";
                    descriptionText.text = "PartInfo script missing";
                }
            }
        }
    }
}