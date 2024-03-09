using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField] private Transform[] puzzles;
    [SerializeField] private Text timerText;
    [SerializeField] private string nextSceneName;
    [SerializeField] private float initialTimer = 5f;
    [SerializeField] private string finishSceneName = "Finish1";
    [SerializeField] private string restartSceneName = "Restart1";

    private float timer;
    private int currentPuzzleIndex = 0;

    void Start()
    {
        timer = initialTimer;
        UpdateTimerDisplay();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        UpdateTimerDisplay();

        if (timer <= 0)
        {
            SceneManager.LoadScene(restartSceneName);
        }

        if (IsPuzzleSolved())
        {
            currentPuzzleIndex++;
            if (currentPuzzleIndex >= puzzles.Length)
            {
                SceneManager.LoadScene(finishSceneName);
            }
            else
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        int seconds = Mathf.CeilToInt(timer);
        timerText.text = seconds.ToString();
    }

    private bool IsPuzzleSolved()
    {
        foreach (Transform puzzle in puzzles)
        {
            if (!Mathf.Approximately(puzzle.rotation.z, 0))
            {
                return false;
            }
        }
        return true;
    }
}