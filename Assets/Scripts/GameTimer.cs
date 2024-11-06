using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// Calculates the running time of game in minutes if game is running
public class GameTimer : MonoBehaviour
{
    [Header("Game Timer")]
    public TextMeshProUGUI timerText;
    public float realSecondsPerGameHour = 60f;
    public float maxGameTime = 5;
    private float gameTime = 0f;
    private bool isRunning = true;
    private float elapsedTime = 0f;

    [Header("Background Color")]
    [SerializeField] private BackgroundColor background;
    [SerializeField] private BackgroundColor middleground;
    [SerializeField] private BackgroundColor foreground;

    [Header("Effects")]
    [SerializeField] private GameObject starEffect;


    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= 1f)
            {
                gameTime += elapsedTime / realSecondsPerGameHour; //convert seconds to minutes
                elapsedTime = 0f;

                if(gameTime >= 3 && gameTime <= 4)
                {
                    starEffect.SetActive(true);
                }

                if (gameTime >= maxGameTime)
                {
                    gameTime = maxGameTime;
                    isRunning = false;
                }

                UpdateTimerText();
                UpdateBackgroundColors(gameTime);
            }
        }
    }

    /// Update the UI timer text to hours and min format
    void UpdateTimerText()
    {
        int hours = Mathf.FloorToInt(gameTime);
        int minutes = Mathf.FloorToInt((gameTime - hours) * 60f);
        timerText.text = string.Format("{00:00}:{01:00}", hours, minutes);
    }

    /// Update background colors see BackgroundColor() based on current game time
    public void UpdateBackgroundColors(float gameTime) 
    {
        background.UpdateBackgroundColors(gameTime);
        middleground.UpdateBackgroundColors(gameTime);
        foreground.UpdateBackgroundColors(gameTime);
    }

    public void ResetGameTime() {
        gameTime = 0;
        elapsedTime = 0;
        isRunning = true;
    }

    public float getGameTime() 
    {
        return gameTime;
    }
}
