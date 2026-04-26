using UnityEngine;

public class ModelSelector : MonoBehaviour
{
    public GameObject heartPrefab;
    public GameObject kidneyPrefab;
    public GameObject enginePrefab;

    public GameObject selectedPrefab;

    private GameObject spawnedObject;

    void Start()
    {
        // Default model
        selectedPrefab = heartPrefab;
        Debug.Log("Default = Heart");
    }

    public void SelectHeart()
    {
        selectedPrefab = heartPrefab;
        Debug.Log("Heart Selected");
    }

    public void SelectKidney()
    {
        selectedPrefab = kidneyPrefab;
        Debug.Log("Kidney Selected");
    }

    public void SelectEngine()
    {
        selectedPrefab = enginePrefab;
        Debug.Log("Engine Selected");
    }

    public void SpawnModel(Vector3 position, Quaternion rotation)
    {
        // Destroy previous model
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }

        // Spawn new model
        spawnedObject = Instantiate(selectedPrefab, position, rotation);
    }
}