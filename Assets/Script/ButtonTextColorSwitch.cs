using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTextColorSwitch : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private TextMeshProUGUI text;
    private Vector4 originalColor;
    private Vector4 hoverColor = new Vector4(255f, 0f, 0f, 255f);

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        originalColor = text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = originalColor;
    }
}