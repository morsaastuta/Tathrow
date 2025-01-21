using DG.Tweening;
using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] public Card card;
    bool flipped = false;
    bool selected = false;

    //

    Rigidbody2D body;
    SpriteRenderer renderer;

    Vector2 startPos;
    Vector2 endPos;
    Vector2 direction;

    float throwForceX = 2;
    float throwForceY = 2;

    float touchTimeStart;
    float touchTimeEnd;
    float timeInterval;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = card.image;
    }

    void Update()
    {
    }

    public void Rotate()
    {
        flipped = true;
        transform.DORotate(transform.rotation.eulerAngles + new Vector3(180, 0, 0), 0.5f);
    }

    public void Throw()
    {
        if (true)
        {

        }
        transform.DOMove(transform.rotation.eulerAngles + new Vector3(180, 0, 0), 0.5f);
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
