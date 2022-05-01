using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text gameOverText;
    public Text finalScoreText;

    public float score;

    // Start is called before the first frame update
    void Start()
    {
        // Reset the score and disable the game over display
        score = 0;
        scoreText.enabled = true;
        scoreText.text = "Score: " + score;
        gameOverText.enabled = false;
        finalScoreText.enabled = false;
    }

    // Increases the score by a given amount
    public void AddScore(float add)
    {
        score += add;
        scoreText.text = "Score: " + (int)score;
    }

    // Displays the game over screen
    public void GameOverScore()
    {
        scoreText.enabled = false;
        gameOverText.enabled = true;
        finalScoreText.enabled = true;
        finalScoreText.text = "Final Score: " + (int)score;
    }
}
