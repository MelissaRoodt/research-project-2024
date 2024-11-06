using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using System.Net;
using Unity.Netcode.Transports.UTP;
using System;

/// Network Manager for the server interface 
public class NetworkManagerUI : MonoBehaviour
{

    [Header("Server UI")]
    private string ip = "0.0.0.0";
    [SerializeField] private TMP_InputField inputIP;
    [SerializeField] private Button btnServer;

    [Header("Client UI")]
    [SerializeField] private TextMeshProUGUI[] txtClient;

    [Header("Player Movement")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameController gameController; /// see GameController()

    [Header("UI Manager")]
    [SerializeField] private UIController uiController;

    private void Awake()
    {
        btnServer.onClick.AddListener(() => {

            NetworkManager.Singleton.StartServer();
            ip = GetLocalIPAddress();
            inputIP.text = ip;
            gameController.setPlayer(SpawnPlayer());//spawn player and set in game manager
            uiController.setServerActive();
        });
    }

    /// Spawn the player instance 
    /// @returns player gameobject
    private GameObject SpawnPlayer()
    {
        GameObject playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerInstance.GetComponent<NetworkObject>().Spawn();
        return playerInstance;
    }

    /// @returns the local ip address of machine
    string GetLocalIPAddress()
    {
        foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    ///When clients connect Display them in interface
    /// @see OnClientConnected()
    private void OnEnable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    ///When clients disconnects Remove them from the interface
    /// @see OnClientDisconnected()
    private void OnDisable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
    }

    ///Change ui text to display client connected
    private void OnClientConnected(ulong clientId)
    {
        if(clientId <= 3)
        {
            txtClient[clientId -1].text = $"Controller ID: {clientId} Connected";
        }
    }

    ///Change ui text to display client disconnected
    private void OnClientDisconnected(ulong clientId)
    {
        if (clientId > 0)
        {
            txtClient[clientId - 1].text = $"Controller ID: {clientId} Disconnected";
        }
    }
}
