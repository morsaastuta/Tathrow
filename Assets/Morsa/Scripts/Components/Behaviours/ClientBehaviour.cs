using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using static Glossary;

public class ClientBehaviour : MonoBehaviour
{
    [Header("Projection")]
    [SerializeField] SpriteRenderer bodyRenderer;
    [SerializeField] Animator bodyAnimator;
    [SerializeField] Animator eyesAnimator;
    float animSpeed = 0.5f;

    [Header("Speeches")]
    [SerializeField] SpeechBehaviour pastSpeech;
    [SerializeField] SpeechBehaviour presentSpeech;
    [SerializeField] SpeechBehaviour futureSpeech;

    List<Property> past = new();
    List<Property> present = new();
    List<Property> future = new();

    SlotBehaviour pastSlot;
    SlotBehaviour presentSlot;
    SlotBehaviour futureSlot;

    bool scored = false;

    void FixedUpdate()
    {
        bodyAnimator.SetFloat("speed", animSpeed);
        eyesAnimator.SetFloat("speed", animSpeed);

        if (!scored && pastSlot.card != null && presentSlot.card != null && futureSlot.card != null)
        {
            scored = true;
            Score();
            StartCoroutine(RoundManager.instance.EndRound());
        }
    }

    public void SetProperties(SlotBehaviour _pastSlot, SlotBehaviour _presentSlot, SlotBehaviour _futureSlot)
    {
        animSpeed = 0.5f;

        bodyRenderer.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        // Set slots
        pastSlot = _pastSlot;
        presentSlot = _presentSlot;
        futureSlot = _futureSlot;

        // Past
        past.Add(GetRandomProperty());

        // Present
        present.Add(GetRandomProperty());

        // Future
        future.Add(GetRandomProperty());

        Invoke("Speak", 1f);
    }

    public void Speak()
    {
        animSpeed = 0.2f;

        pastSpeech.Show(past[0]);
        presentSpeech.Show(present[0]);
        futureSpeech.Show(future[0]);
    }

    void Score()
    {
        animSpeed = 0.5f;

        pastSpeech.Hide();
        presentSpeech.Hide();
        futureSpeech.Hide();

        int totalScore = 0;

        int score = 3;
        foreach (Property p in past)
        {
            if (pastSlot.card.GetProperties().Contains(GetProperty(p))) totalScore += score;
            else score--;
        }

        score = 3;
        foreach (Property p in present)
        {
            if (presentSlot.card.GetProperties().Contains(GetProperty(p))) totalScore += score;
            else score--;
        }

        score = 3;
        foreach (Property p in future)
        {
            if (futureSlot.card.GetProperties().Contains(GetProperty(p))) totalScore += score;
            else score--;
        }

        RoundManager.instance.Score(totalScore);
        Debug.Log(totalScore);
    }
}
