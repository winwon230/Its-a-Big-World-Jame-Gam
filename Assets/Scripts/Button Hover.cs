using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Vector2 originalScale;
    private float scale = 1.1f;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData evenData)
    {
        rectTransform.localScale = originalScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
