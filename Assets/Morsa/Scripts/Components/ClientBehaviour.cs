using System.Linq;
using UnityEngine;
using static Glossary;

public class ClientBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer bodyRenderer;
    [SerializeField] SpeechBehaviour pastSpeech;
    [SerializeField] SpeechBehaviour presentSpeech;
    [SerializeField] SpeechBehaviour futureSpeech;

    Property[] past;
    Property[] present;
    Property[] future;

    SlotBehaviour pastSlot;
    SlotBehaviour presentSlot;
    SlotBehaviour futureSlot;

    void Start()
    {
        bodyRenderer.color = new Color(Random.Range(0f,255f), Random.Range(0f,255f), Random.Range(0f,255f), 1);
    }

    void FixedUpdate()
    {
        if (pastSlot.card != null && presentSlot.card != null && futureSlot.card != null)
        {
            Score();
            GameManager.instance.EndRound();
        }
    }

    public void SetProperties(SlotBehaviour _pastSlot, SlotBehaviour _presentSlot, SlotBehaviour _futureSlot)
    {
        // Set slots
        pastSlot = _pastSlot;
        presentSlot = _presentSlot;
        futureSlot = _futureSlot;

        // Past
        past.Append(GetRandomProperty());

        // Present
        present.Append(GetRandomProperty());

        // Future
        future.Append(GetRandomProperty());
    }

    void Score()
    {
        int score = 3;
        foreach (Property p in past)
        {
            if (pastSlot.card.GetProperties().Contains(GetProperty(p))) GameManager.instance.Score(score);
            else score--;
        }

        score = 3;
        foreach (Property p in present)
        {
            if (presentSlot.card.GetProperties().Contains(GetProperty(p))) GameManager.instance.Score(score);
            else score--;
        }

        score = 3;
        foreach (Property p in future)
        {
            if (futureSlot.card.GetProperties().Contains(GetProperty(p))) GameManager.instance.Score(score);
            else score--;
        }
    }
}
