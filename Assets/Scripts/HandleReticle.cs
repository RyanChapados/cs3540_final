using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleReticle : MonoBehaviour
{
    public Sprite reticleImage;

    Color originalReticle;

    // Start is called before the first frame update
    void Start()
    {
        originalReticle = reticleImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        ReticleEffect();
    }

    // Allows the reticle to change if it is over a dementor
    void ReticleEffect()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity) && hit.collider.CompareTag("Enemy"))
        {
            // Sets the reticle to a certain color when it is over an enemy
            reticleImage.color = Color.red;

            // Zooms in the reticle when it is over an enemy
            reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(0.7f, 0.7f, 1), Time.deltaTime * 2);
        }
        else
        {
            // Resets reticle color if it is not over an enemy
            reticleImage.color = originalReticle;

            // Resets reticle size if not over an enemy
            reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, Vector3.one, Time.deltaTime * 2);
        }
    }
}
