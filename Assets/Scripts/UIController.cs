using UnityEngine;
using UnityEngine.UI;

/// Controls the main menu ui
public class UIController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnServer;
    [SerializeField] private Button btnShop;
    [SerializeField] private Button btnDataAnalysis;
    [SerializeField] private Button btnExitFromNetwork;
    [SerializeField] private Button btnExitFromShop;
    [SerializeField] private Button btnExitData;


    [Header("UI")]
    [SerializeField] private GameObject uiNetworkManager;
    [SerializeField] private GameObject uiHUD;
    [SerializeField] private GameObject uiShop;
    [SerializeField] private GameObject uiDataAnalysis;
    [SerializeField] private GameObject uiMainMenu;

    [Header("Dependencies")]
    [SerializeField] private DataController dataController;
    [SerializeField] private Shop shop;
    [SerializeField] private GameTimer gameTimer;


    //get the game controller here to set game active or not

    private void Awake()
    {
        //start menu
        btnPlay.onClick.AddListener(() =>
        {
            dataController.ResetData();
            gameTimer.ResetGameTime();
            resetUI();
            uiHUD.SetActive(true);
        });

        btnServer.onClick.AddListener(() =>
        {
            resetUI();
            uiNetworkManager.SetActive(true);
        });

        btnShop.onClick.AddListener(() =>
        {
            shop.ResetShop();
            resetUI();
            uiShop.SetActive(true);
        });

        btnDataAnalysis.onClick.AddListener(() =>
        {
            resetUI();
            uiDataAnalysis.SetActive(true);
        });

        //network manager ui
        btnExitFromNetwork.onClick.AddListener(() =>
        {
            setMainMenuActive();
        });

        //shop 
        btnExitFromShop.onClick.AddListener(() =>
        {
            setMainMenuActive();
        });

        //data
        btnExitData.onClick.AddListener(() =>
        {
            setMainMenuActive();
        });
    }

    /// Sets all menus active to false
    private void resetUI() 
    {
        uiNetworkManager.SetActive(false);
        uiHUD.SetActive(false);
        uiShop.SetActive(false);
        uiDataAnalysis.SetActive(false);
        uiMainMenu.SetActive(false);
    }

    public void setMainMenuActive() 
    {
        resetUI();
        uiMainMenu.SetActive(true);
    }

    public void setServerActive()
    {
        resetUI();
        btnPlay.gameObject.SetActive(true);
        uiMainMenu.SetActive(true);
    }

    public void setHudActive()
    {
        resetUI();
        uiHUD.SetActive(true);
    }

}
