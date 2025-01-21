using UnityEngine;

public class SlotBehaviour : MonoBehaviour
{
    public CardBehaviour card;
    float maxDistance = 100f;

    void Update()
    {
        if (card != null && Vector2.Distance(transform.position, card.transform.position) > maxDistance) card.Slide(transform.position);
    }

    public void Unlink()
    {
        card = null;
    }

    public void Link(CardBehaviour newCard)
    {
        card = newCard;
        card.Slide(transform.position);
        card.Link(this);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (card == null && collider.GetComponent<CardBehaviour>()) Link(collider.GetComponent<CardBehaviour>());
    }
}