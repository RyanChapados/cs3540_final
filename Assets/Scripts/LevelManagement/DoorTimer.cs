using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTimer : MonoBehaviour
{
    public float gameTime = 10f;
    public AudioClip doorOpen;
    public Text gameText;

    private GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");

        StartCoroutine(WaitThenOpenDoors());
    }

    IEnumerator WaitThenOpenDoors()
    {
        yield return new WaitForSeconds(gameTime);

        AudioSource.PlayClipAtPoint(doorOpen, GameObject.FindGameObjectWithTag("Player").transform.position);
        gameText.text = "Escape!";

        foreach (GameObject door in doors)
        {
            door.transform.GetComponent<DoorController>().StartDoorOpen();
        }
    }
}
