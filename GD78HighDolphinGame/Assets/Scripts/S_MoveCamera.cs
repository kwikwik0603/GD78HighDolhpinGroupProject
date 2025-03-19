/*
This script is to move the camera along with the player.
*/

using UnityEngine;

public class S_MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
