using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //public Vector3 openPosition;
    //public Vector3 closedPosition;
    public Quaternion openRotation;
    //public Quaternion closedRotation;

    private float elapsedTime = 0f;
    private float duration = 5f;

    public void StartDoorOpen()
    {
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        //Vector3 startingPosition = transform.position;
        Quaternion startingRotation = transform.rotation;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the interpolation factor between 0 and 1.
            float t = Mathf.Clamp01(elapsedTime / duration);

            // Interpolate the position and rotation of the door.
            transform.rotation = Quaternion.Lerp(startingRotation, openRotation, t);

            yield return null;
        }
    }
}
