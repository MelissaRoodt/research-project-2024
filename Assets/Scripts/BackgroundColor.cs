using System;
using UnityEngine;

/// Changes the background of the game to morning and night
/// 
/// Changes the color of spriteRenderer by two color values <summary>
/// @param gameTime - duration of gameplay determined by GameTimer() class
public class BackgroundColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;

    [SerializeField] private Color morningColor = Color.white;
    [SerializeField] private Color nightColor = new Color(0.1f, 0.1f, 0.3f);

    public void UpdateBackgroundColors(float gameTime)
    {
        float t = gameTime / 24f;
        Color currentColor = Color.Lerp(morningColor, nightColor, t);

        background.color = currentColor;
    }
}
