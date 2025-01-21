using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int touchTimer;

    public static CardBehaviour selectedCard;

    [Header("Sections")]
    [SerializeField] public GameObject handSection;
    [SerializeField] public GameObject deckSection;

    [SerializeField] public List<Card> deck = new();
    [SerializeField] public GameObject cardPrefab;
    public List<CardBehaviour> hand = new();

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
        // Until the hand has 6 cards...
        while (hand.Count < 6)
        {
            // Draw random card from DECK
            int draw = Random.Range(0, deck.Count);

            // Instantiate drawn card
            GameObject card = Instantiate(cardPrefab, handSection.transform);
            card.GetComponent<CardBehaviour>().card = deck[draw];

            // Add drawn card to HAND
            hand.Add(card.GetComponent<CardBehaviour>());

            // Remove drawn card from DECK
            deck.RemoveAt(draw);
        }
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit))
            {

            }
        }
    }

    public void Deselect()
    {
        foreach (CardBehaviour card in hand)
        {
            card.Deselect();
        }
    }
}
