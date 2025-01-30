using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] List<GameObject> hiddenOnStart = new();

    [Header("Panes")]
    List<GameObject> paneHistory = new();
    [SerializeField] GameObject pane_mainSelector;
    [SerializeField] GameObject pane_roundSelector;
    [SerializeField] GameObject pane_beginPrompt;
    [SerializeField] GameObject pane_quitPrompt;

    void Start()
    {
        MainSelector();
    }

    public void Game()
    {
        RoundSelector();

        foreach (GameObject go in hiddenOnStart) go.SetActive(true);

        RoundManager.instance.Begin();
    }

    public void End()
    {
        MainSelector();
        
        RoundManager.instance.AbortRound();

        foreach (GameObject go in hiddenOnStart) go.SetActive(false);
    }

    public void MainSelector()
    {
        RestartHistory();
        AdvancePane(pane_mainSelector);
    }

    public void RoundSelector()
    {
        RestartHistory();
        AdvancePane(pane_roundSelector);
    }

    public void BeginPrompt()
    {
        AdvancePane(pane_beginPrompt);
    }

    public void QuitPrompt()
    {
        RoundManager.instance.BlockHand();
        AdvancePane(pane_quitPrompt);
    }

    public void AdvancePane(GameObject pane)
    {
        if (paneHistory.Count > 0) paneHistory[paneHistory.Count - 1].SetActive(false);
        pane.SetActive(true);
        paneHistory.Add(pane);
    }

    public void ReturnPane()
    {
        RoundManager.instance.ReleaseHand();

        paneHistory[paneHistory.Count - 1].SetActive(false);
        paneHistory[paneHistory.Count - 2].SetActive(true);

        paneHistory.RemoveAt(paneHistory.Count - 1);
    }

    public void RestartHistory()
    {
        for (int i = 0; i < paneHistory.Count; i++)
        {
            paneHistory[0].SetActive(false);
            paneHistory.RemoveAt(0);
        }
    }
}
