using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSettings : MonoBehaviour
{
    [Header("Sensitivity")]
    public float defaultSensitivity = 130;
    public Slider sensitivitySlider;

    // Start is called before the first frame update
    void Start()
    {
        // Sets up sensitivity
        if (PlayerPrefs.HasKey("sensitivity"))
            sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
        else
        {
            PlayerPrefs.SetFloat("sensitivity", defaultSensitivity);
            sensitivitySlider.value = defaultSensitivity;
        }
    }

    public void UpdateSensitivity()
    {
        PlayerPrefs.SetFloat("sensitivity", sensitivitySlider.value);
    }
}
