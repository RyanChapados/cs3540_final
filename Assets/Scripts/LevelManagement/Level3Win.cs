using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Win : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float zVal = player.position.z;

        if (zVal > 514 || zVal < 477)
        {
            FindObjectOfType<LevelManager>().LevelBeat();
        }
    }
}
