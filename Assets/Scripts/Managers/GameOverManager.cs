using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void EndGame()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0.0f;

        ScoreManager.Instance.GameOver();
        ScoreManager.Instance.UpdateScoreText();
        
        finalScoreText.text = ScoreManager.Instance.ScoreText.text;
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
