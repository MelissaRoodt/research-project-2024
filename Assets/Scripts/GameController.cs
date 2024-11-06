using UnityEngine;

/// Controls the game state
/// Depends on DogController() onReachedEnd event to trigger main menu active and reset dog and player position
/// Depends on UIController() to set the ui menus active
public class GameController : MonoBehaviour
{
    [SerializeField] private DogController dogController;

    [Header("UI")]
    [SerializeField] private UIController uiController;

    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerInitialPosition;

    [Header("Dog")]
    [SerializeField] private GameObject dog;
    [SerializeField] private Transform dogInitialPosition;

    private void Start()
    {
        dogController.onReachEnd += DogController_onReachEnd;
    }

    private void DogController_onReachEnd(object sender, System.EventArgs e)
    {
        ///go to main menu
        uiController.setMainMenuActive();

        ///reset position
        ResetPositions();
    }

    /// Reset the player and dog game objects position 
    private void ResetPositions()
    {
        if (player != null)
        {
            player.transform.position = playerInitialPosition.position;
        }
        else Debug.Log("Player not set");
        dog.transform.position = dogInitialPosition.position;
        dog.GetComponent<DogController>().InitialState();
    }

    /// Reset the player game object position 
    /// @param _player takes in a player game object
    /// @see ResetPositions()
    public void setPlayer(GameObject _player)
    {
        player = _player;
        ResetPositions();
    }
}
