using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] string text;

    [Header("Image")]
    [SerializeField] Image renderer;
    [SerializeField] Sprite exit;
    [SerializeField] Sprite hover;
    [SerializeField] Sprite press;

    void Start()
    {
        textBox.SetText(text);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        renderer.sprite = hover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        renderer.sprite = exit;
    }
    
    public void Press()
    {
        GetComponentInParent<AudioSource>().Play();
        Debug.Log("pressed");
        renderer.sprite = press;
    }

    void OnDisable()
    {
        renderer.sprite = exit;
    }

    void OnEnable()
    {
        renderer.sprite = exit;
    }
}
