using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody2D pivot;

    Rigidbody2D body;

    bool held = false;
    float throwSpeed = 1.0f;

    Vector2 drag = new(0,0);

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(held);

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (Vector3.Distance(Camera.main.WorldToScreenPoint(Touchscreen.current.primaryTouch.position.ReadValue()), Camera.main.WorldToScreenPoint(transform.position)) < 10)
            {
                drag = Touchscreen.current.primaryTouch.position.ReadValue() - drag;
                held = true;
            }
            else if (held) Throw();
        }
        else held = false;

        if (held)
        {
            transform.position += (Vector3)drag;
        }
    }

    void Throw()
    {
        held = false;

        // Set rotation of card via the finger's drag
        transform.rotation = new (transform.rotation.x, Quaternion.LookRotation(drag).z, transform.rotation.y, transform.rotation.w);

        // Set linear velocity of card (towards its head, Y axis)
        body.linearVelocity = new(0, throwSpeed);
    }
}
