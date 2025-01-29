using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Glossary;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    int score = 0;

    public void Score(int s)
    {
        score += s;
    }

    float touchTimer = 0;
    bool enlarged = false;
    Vector2 initTouchPos = new();
    float timeThreshold = 0.16f;
    float throwThreshold = 30f;

    [Header("Descriptor")]
    [SerializeField] SpriteRenderer dScreen;
    [SerializeField] TextMeshPro dName;
    [SerializeField] TextMeshPro dDescription;
    [SerializeField] TextMeshPro dProperties;
    [SerializeField] TextMeshPro dInverted;

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

    [Header("Clients")]
    [SerializeField] Transform initPosition;
    [SerializeField] Transform corePosition;
    [SerializeField] Transform exitPosition;
    [SerializeField] GameObject clientPrefab;
    GameObject currentClient;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void Begin()
    {
        NextRound();
    }

    public void Exit()
    {

    }

    void Update()
    {
        // If the player is touching with ONE finger, project raycast and select card if collided
        if (Input.touchCount >= 1)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    initTouchPos = Input.GetTouch(0).position;
                    Vector3 castPos = Camera.main.ScreenToWorldPoint(initTouchPos);
                    Vector2 touchPos = new(castPos.x, castPos.y);
                    Collider2D hit = Physics2D.OverlapPoint(touchPos);
                    if (hit && hit.GetComponent<CardBehaviour>()) Select(hit.GetComponent<CardBehaviour>());
                    break;

                case TouchPhase.Moved:
                    if (selectedCard != null && !selectedCard.linkedSlot.client && touchTimer > timeThreshold && Vector2.Distance(Input.GetTouch(0).position, initTouchPos) > throwThreshold)
                    {
                        selectedCard.Throw(Input.GetTouch(0).position - initTouchPos);
                        Deselect();
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (selectedCard != null)
                    {
                        if (!selectedCard.linkedSlot.client && !selectedCard.untouchable && touchTimer < timeThreshold) StartCoroutine(selectedCard.Flip());
                        Deselect();
                    }
                    break;
            }
        }

        if (selectedCard != null)
        {
            touchTimer += Time.deltaTime;
            if (!enlarged && touchTimer >= timeThreshold)
            {
                enlarged = true;
                selectedCard.Enlarge();
            }
        }
    }

    public IEnumerator EndRound()
    {
        yield return new WaitForSeconds(1f);

        Deselect();

        foreach (CardBehaviour card in hand)
        {
            deck.Add(card.card);
            card.Discard();
        }

        hand.Clear();

        if (pastCard != null)
        {
            deck.Add(pastCard.card);
            pastCard.Discard();
        }

        if (presentCard != null)
        {
            deck.Add(presentCard.card);
            presentCard.Discard();
        }

        if (futureCard != null)
        {
            deck.Add(futureCard.card);
            futureCard.Discard();
        }

        WalkOut();

        Invoke("NextRound", 1.5f);
    }

    public void AbortRound()
    {

        Deselect();

        foreach (CardBehaviour card in hand)
        {
            deck.Add(card.card);
            card.Discard();
        }

        hand.Clear();

        if (pastCard != null)
        {
            deck.Add(pastCard.card);
            pastCard.Discard();
        }

        if (presentCard != null)
        {
            deck.Add(presentCard.card);
            presentCard.Discard();
        }

        if (futureCard != null)
        {
            deck.Add(futureCard.card);
            futureCard.Discard();
        }

        WalkOut();
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
        }

        foreach (CardBehaviour card in hand)
        {
            handSlots[hand.IndexOf(card)].Link(card);
            if (Random.Range(0f, 1f) > 0.5f) StartCoroutine(card.Flip());
        }

        // Create client
        currentClient = Instantiate(clientPrefab);
        currentClient.transform.position = initPosition.position;
        currentClient.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        WalkIn();
    }

    public void WalkIn()
    {
        currentClient.GetComponent<ClientBehaviour>().SetProperties(pastSlot, presentSlot, futureSlot);
        currentClient.transform.DOMove(corePosition.position, 1.2f);
        currentClient.transform.DOScale(new Vector3(1, 1, 1), 1.2f);
    }

    public void WalkOut()
    {
        currentClient.transform.DOMove(exitPosition.position, 1.2f);
        currentClient.transform.DOScale(new Vector3(0.5f, 0.5f, 1), 1.2f);
        Destroy(currentClient, 1.2f);
    }

    public void Select(CardBehaviour card)
    {
        card.Select();
        selectedCard = card;

        dName.SetText(selectedCard.card.title);
        dDescription.SetText(selectedCard.card.description);
        dProperties.SetText(selectedCard.GetProperties());
        if (!selectedCard.flipped) dInverted.SetText("");
        else dInverted.SetText("Invertida");

        dScreen.DOColor(new Color(0, 0, 0, 0.5f), 0.5f);
        dName.DOColor(new Color(255, 255, 255, 1f), 0.5f);
        dDescription.DOColor(new Color(255, 255, 255, 1f), 0.5f);
        dProperties.DOColor(new Color(255, 255, 255, 1f), 0.5f);
        dInverted.DOColor(new Color(255, 255, 255, 1f), 0.5f);
    }

    public void Deselect()
    {
        enlarged = false;
        touchTimer = 0;
        selectedCard = null;

        foreach (CardBehaviour card in hand) card.Deselect();

        dScreen.DOColor(new Color(0, 0, 0, 0f), 0.5f);
        dName.DOColor(new Color(255, 255, 255, 0f), 0.5f);
        dDescription.DOColor(new Color(255, 255, 255, 0f), 0.5f);
        dProperties.DOColor(new Color(255, 255, 255, 0f), 0.5f);
        dInverted.DOColor(new Color(255, 255, 255, 0f), 0.5f);
    }
}
