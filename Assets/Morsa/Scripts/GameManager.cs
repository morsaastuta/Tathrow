using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int touchTimer = 0;
    Vector2 lastTouchPos = new();
    float throwThreshold = 3;

    [Header("Slots")]
    [SerializeField] List<SlotBehaviour> handSlots;
    [SerializeField] SlotBehaviour pastSlot;
    [SerializeField] SlotBehaviour presentSlot;
    [SerializeField] SlotBehaviour futureSlot;

    [Header("Cards")]
    [SerializeField] public List<Card> deck = new();
    [SerializeField] public GameObject cardPrefab;
    List<CardBehaviour> hand = new();
    public CardBehaviour selectedCard;
    public CardBehaviour pastCard;
    public CardBehaviour presentCard;
    public CardBehaviour futureCard;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        NextRound();
    }

    void Update()
    {
        // If the player is touching with ONE finger, project raycast and select card if collided
        if (Input.touchCount == 1)
        {
            lastTouchPos = Input.GetTouch(0).position;

            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    Vector3 castPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    Vector2 touchPos = new(castPos.x, castPos.y);
                    Collider2D hit = Physics2D.OverlapPoint(touchPos);
                    if (hit && hit.GetComponent<CardBehaviour>()) Select(hit.GetComponent<CardBehaviour>());
                    break;

                case TouchPhase.Moved:
                    Debug.Log("moved");
                    if (selectedCard != null && Vector2.Distance(Input.GetTouch(0).position, lastTouchPos) > throwThreshold)
                    {
                        Debug.Log("thrown");
                        selectedCard.Throw(Input.GetTouch(0).position - lastTouchPos);
                        Deselect();
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    Debug.Log("untouched");
                    if (selectedCard != null) Debug.Log("deselected");
                    Deselect();
                    break;
            }
        }

        if (selectedCard != null)
        {
            touchTimer++;
            if (!selectedCard.untouchable && Input.GetTouch(0).tapCount == 1) StartCoroutine(selectedCard.Flip());
            lastTouchPos = Input.GetTouch(0).position;
        }
    }

    public void EndRound()
    {
        Deselect();

        foreach (CardBehaviour card in hand) deck.Add(card.card);

        if (pastCard != null) deck.Add(pastCard.card);
        if (presentCard != null) deck.Add(presentCard.card);
        if (futureCard != null) deck.Add(futureCard.card);
    }

    public void NextRound()
    {
        // Until the hand has 6 cards...
        while (hand.Count < 6)
        {
            // Draw random card from DECK
            int draw = Random.Range(0, deck.Count);

            // Instantiate drawn card
            GameObject card = Instantiate(cardPrefab);
            card.GetComponent<CardBehaviour>().card = deck[draw];
            card.transform.position = handSlots[hand.Count].transform.position;

            // Add drawn card to HAND
            hand.Add(card.GetComponent<CardBehaviour>());

            // Remove drawn card from DECK
            deck.RemoveAt(draw);

            Debug.Log(card);
        }

        foreach (CardBehaviour card in hand) handSlots[hand.IndexOf(card)].Link(card);
    }

    public void Select(CardBehaviour card)
    {
        card.Select();
        selectedCard = card;
    }

    public void Deselect()
    {
        touchTimer = 0;
        selectedCard = null;
        foreach (CardBehaviour card in hand) card.Deselect();
    }
}
