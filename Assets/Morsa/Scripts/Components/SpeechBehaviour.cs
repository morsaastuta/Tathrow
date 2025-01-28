using TMPro;
using UnityEngine;
using static Glossary;

public class SpeechBehaviour : MonoBehaviour
{
    [SerializeField] GameObject bubble;
    [SerializeField] TextMeshPro speech;



    public void Show(Property p)
    {
        string sp = "";

        switch(p)
        {
            
        }

        speech.SetText(sp);

        bubble.SetActive(true);
    }

    public void Hide()
    {
        bubble.SetActive(false);
    }
}
