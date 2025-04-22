using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance;

    private int score = 0;

    public TextMeshProUGUI scoreText;

    void Awake() {
        Instance = this;
        UpdateScoreUI();
    }

    public void AddScore(int amount) {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI() {
        scoreText.text = score.ToString();
    }
}
