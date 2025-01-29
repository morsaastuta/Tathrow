using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Static")]
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] Sprite off;
    [SerializeField] Sprite hover;
    [SerializeField] Sprite press;

    [Header("Animated")]
    [SerializeField] Animator animator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (animator) animator.SetBool("hover", true);

        if (renderer) renderer.sprite = hover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator) animator.SetBool("hover", false);

        if (renderer) renderer.sprite = off;
    }
    
    public void Press()
    {
        if (animator) animator.SetBool("click", true);

        if (renderer) renderer.sprite = press;
    }
}
