using TMPro;
using UnityEngine;

/// Controls the doggy points.
/// 
/// Add doggy points when successfully provided dog a treat. 
/// Add doggy points when successfully finished game before timer ends. 

/// Event triggered by DogController() 
/// depends on GameTimer()
public class DoggyPoints : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtDoggyTreat;
    private int points = 0;

    [Header("Dog AI")]
    [SerializeField] private DogController dog; /// see DogController()
    [SerializeField] private GameTimer gameTimer;/// see GameTimer()

    private void Start()
    {
        dog.OnAddDoggyPoints += Dog_OnAddDoggyPoints;
        dog.onReachEnd += Dog_onReachEnd;
    }

    private void Dog_onReachEnd(object sender, System.EventArgs e)
    {
        if(gameTimer.getGameTime() < gameTimer.maxGameTime)
        {
            setDoggyTreat(10);

            if(gameTimer.getGameTime() < (gameTimer.maxGameTime / 2))
            {
                setDoggyTreat(50);
            }
        }
    }

    private void Dog_OnAddDoggyPoints(object sender, System.EventArgs e) => setDoggyTreat(5);

    public void setDoggyTreat(int value)
    {
        points += value;
        txtDoggyTreat.text = points.ToString("000");
    }

    public int getDoggyPoints()
    { 
        return points;
    }
}
