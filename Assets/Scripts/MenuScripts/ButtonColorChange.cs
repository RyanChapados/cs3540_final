using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;
    public Color highlightedColor;
    public Color pressColor;
    public Color originalColor;

    private void Start()
    {
        // Get the TextMeshPro component from the button
        //buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnButtonPress()
    {
        // Change the text color to the desired color
        buttonText.color = pressColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the text color to the highlighted color
        buttonText.color = highlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Revert the text color to the original color
        buttonText.color = originalColor;
    }

    private void OnEnable()
    {
        buttonText.color = originalColor;
    }
}
