using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ShapeSpawner : MonoBehaviour
{
    public string shapeAddress; // Address of the shape prefab in the Addressable Assets system

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Check if spacebar is pressed
        {
            SpawnShape();
        }
    }

    async void SpawnShape()
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(shapeAddress);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject shapePrefab = handle.Result;
            if (shapePrefab != null)
            {
                Instantiate(shapePrefab, Random.insideUnitSphere * 5, Quaternion.identity); // Spawn shape at a random position
            }
            else
            {
                Debug.LogError("Failed to instantiate shape prefab: " + shapeAddress);
            }
        }
        else
        {
            Debug.LogError("Failed to load addressable asset: " + shapeAddress);
        }

        Addressables.Release(handle);
    }
}
