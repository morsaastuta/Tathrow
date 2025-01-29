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
    [SerializeField] GameObject pane_exitPrompt;

    public void Game()
    {
        foreach (GameObject go in hiddenOnStart) go.SetActive(true);

        RoundManager.instance.NextRound();
    }

    public void End()
    {
        RoundManager.instance.AbortRound();

        foreach (GameObject go in hiddenOnStart) go.SetActive(false);
    }

    public void MainSelector()
    {
        ClearHistory();
        AdvancePane(pane_mainSelector);
    }

    public void RoundSelector()
    {
        ClearHistory();
        AdvancePane(pane_roundSelector);
    }

    public void BeginPrompt()
    {
        AdvancePane(pane_beginPrompt);
    }

    public void QuitPrompt()
    {
        AdvancePane(pane_quitPrompt);
    }

    public void ExitPrompt()
    {
        AdvancePane(pane_exitPrompt);
    }

    public void AdvancePane(GameObject pane)
    {
        paneHistory[paneHistory.Count - 1].SetActive(false);
        pane.SetActive(true);
        paneHistory.Add(pane);
    }

    public void ReturnPane()
    {
        paneHistory[paneHistory.Count - 1].SetActive(false);
        paneHistory[paneHistory.Count - 2].SetActive(true);

        paneHistory.RemoveAt(paneHistory.Count - 1);
    }

    public void ClearHistory()
    {
        for (int i = 0; i < paneHistory.Count; i++)
        {
            paneHistory[0].SetActive(false);
            paneHistory.RemoveAt(0);
        }
    }
}
