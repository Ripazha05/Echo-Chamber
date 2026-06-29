using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float hoverScale = 1.15f;
    [SerializeField] private float animSpeed = 8f;

    private TextMeshProUGUI label;
    private Vector3 originalScale;
    private Vector3 targetScale;

    void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * animSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }
}