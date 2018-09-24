using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI text;
    private Color32 baseColor;
    [SerializeField] Color32 hoverColor;
    [SerializeField] float fontSizeAdjust = 10f;
    [SerializeField] AudioClip clickSound;
    [SerializeField] float volume = 0.75f;

    private void Start()
    {
        baseColor = text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hoverColor;
        text.fontSize += fontSizeAdjust; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = baseColor;
        text.fontSize -= fontSizeAdjust;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position, volume);
    }
}