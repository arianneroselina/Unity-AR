using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject dragonPrefab; // Reference to the dragon prefab
    [SerializeField] private Vector3 prefabOffset; // Offset for the dragon's position

    private GameObject dragon; // Reference to the instantiated dragon
    private ARTrackedImageManager aRTrackedImageManager; // Reference to the ARTrackedImageManager

    private void OnEnable() {
        // Get the ARTrackedImageManager component attached to this GameObject
        aRTrackedImageManager = gameObject.GetComponent<ARTrackedImageManager>();

        // Subscribe to the trackedImagesChanged event
        aRTrackedImageManager.trackedImagesChanged += OnImageChanged;

        // Instantiate the dragon immediately when the game starts
        InstantiateDragon();
    }

    private void InstantiateDragon() {
        // Instantiate the dragon prefab at the position of the parent object with an offset
        dragon = Instantiate(dragonPrefab, transform.position + prefabOffset, transform.rotation);
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs obj) {
        foreach (ARTrackedImage image in obj.added) {
            if (dragon == null) {
                // Instantiate the dragon at the tracked image's position with the specified offset
                dragon = Instantiate(dragonPrefab, image.transform);
                dragon.transform.position += prefabOffset;
            } else {
                // If the dragon is already instantiated, move it to the new tracked image's position
                dragon.transform.position = image.transform.position + prefabOffset;
            }
        }
    }

    private void OnDisable() {
        // Unsubscribe from the trackedImagesChanged event to avoid memory leaks
        aRTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }
}
