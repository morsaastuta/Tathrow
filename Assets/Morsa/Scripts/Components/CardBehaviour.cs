using DG.Tweening;
using System.Collections;
using UnityEngine;
using static Glossary;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] public Card card;
    public bool flipped = false;
    bool selected = false;
    float throwForce = 8f;

    public bool untouchable = false;

    [SerializeField] Rigidbody2D body;
    [SerializeField] SpriteRenderer renderer;

    public SlotBehaviour linkedSlot;

    void Start()
    {
        renderer.sprite = card.image;
    }

    public void Link(SlotBehaviour slot)
    {
        if (linkedSlot != null) linkedSlot.Unlink();
        linkedSlot = slot;
    }

    public IEnumerator Flip()
    {
        Debug.Log("flips");
        untouchable = true;
        flipped = !flipped;
        transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, 180), 0.5f);
        yield return new WaitForSeconds(0.5f);
        untouchable = false;
    }

    public void Throw(Vector2 direction)
    {
        body.linearVelocity = direction.normalized * throwForce;
    }

    public void Slide(Vector2 position)
    {
        body.linearVelocity = Vector2.zero;
        transform.DOMove(position, 1f);
    }

    public void Enlarge()
    {
        transform.DOScale(1.2f, 0.5f);
    }

    public string GetProperties()
    {
        string properties = "";
        if (!flipped) foreach (Property property in card.uprightProperties) properties += GetProperty(property) + "\n";
        else foreach (Property property in card.flippedProperties) properties += GetProperty(property) + "\n";
        return properties;
    }

    public void Select()
    {
        GameManager.instance.Deselect();
        renderer.sortingOrder = 2;
        selected = true;
    }

    public void Deselect()
    {
        selected = false;
        renderer.sortingOrder = 0;
        transform.DOScale(1, 0.5f);
    }

    public void Discard()
    {
        untouchable = true;
        body.linearVelocityY = -4f;
        linkedSlot.Unlink();
        Destroy(gameObject, 1f);
    }
}
