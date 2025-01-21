using DG.Tweening;
using System.Collections;
using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] public Card card;
    bool flipped = false;
    bool selected = false;
    float throwForce = 5f;

    public bool untouchable = false;

    [SerializeField] Rigidbody2D body;
    [SerializeField] SpriteRenderer renderer;

    SlotBehaviour linkedSlot;

    void Start()
    {
        renderer.sprite = card.image;
    }

    void Update()
    {

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
        if (!flipped) body.linearVelocity = direction.normalized * throwForce;
        else body.linearVelocity = -direction.normalized * throwForce;
    }

    public void Slide(Vector2 position)
    {
        body.linearVelocity = Vector2.zero;
        transform.DOMove(position, 1f);
    }

    public void Select()
    {
        GameManager.instance.Deselect();
        selected = true;
    }

    public void Deselect()
    {
        selected = false;
    }
}
