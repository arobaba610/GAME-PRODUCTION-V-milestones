using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float levelTime = 120f; // 2-minute timer
    public TextMeshProUGUI timerText;

    private void Update()
    {
        if (levelTime > 0f)
        {
            levelTime -= Time.deltaTime;

            if (timerText != null)
            {
                int minutes = Mathf.FloorToInt(levelTime / 60f);
                int seconds = Mathf.FloorToInt(levelTime % 60f);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
        else
        {
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        Debug.Log("Time's up! Restarting level...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
