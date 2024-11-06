using System;
using System.Net;
using System.Net.NetworkInformation;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

/// Network controller for the client 
public class NetworkController : MonoBehaviour
{
    private string ip;/// the ip to connect to the server
    private string actionEvent;/// the chosen action method left/right/action determined by button value

    [Header("Client Information")]
    [SerializeField] private TMP_InputField inputIp;
    [SerializeField] private TMP_Dropdown drpButtonType; ///values are left/right/action

    [Header("Controller Buttons")]
    [SerializeField] private Button btnClient;
    [SerializeField] private Button btnAction;
    [SerializeField] private Button btnLeft;
    [SerializeField] private Button btnRight;

    [Header("Error Field")]
    [SerializeField] private TextMeshProUGUI txtMessage;

    public void Awake()
    {
        btnClient.onClick.AddListener(() => {
            ValidateServerConnection();
        });

        btnAction.onClick.AddListener(() =>
        {
            SendActionToServer(actionEvent);
        });

        btnLeft.onClick.AddListener(() =>
        {
            SendActionToServer(actionEvent);
        });

        btnRight.onClick.AddListener(() =>
        {
            SendActionToServer(actionEvent);
        });
    }

    /// Validate the server connection by checking ip
    private void ValidateServerConnection()
    {
        setError("Connection Status ... ");

        if (inputIp.text.Length == 0)
        {
            setError("Please enter IP");
            return;
        }

        ip = inputIp.text;
        Debug.Log(ip + " : " +  NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port);

        if (!IsValidIPAddress(ip))
        {
            setError("Invalid IP address.");
            return;
        }

        var networkManager = NetworkManager.Singleton;
        if (networkManager == null)
        {
            setError("Internal error: NetworkManager is not initialized.");
            return;
        }

        var unityTransport = networkManager.GetComponent<UnityTransport>();
        if (unityTransport == null)
        {
            setError("Internal error: UnityTransport component is missing.");
            return;
        }

        ConnectToServer(networkManager, unityTransport);
    }

    private bool IsValidIPAddress(string ip)
    {
        return IPAddress.TryParse(ip, out _);
    }

    public void setError(string message)
    {
        txtMessage.text = message;
    }

    /// Connect to the server using ip
    /// 
    /// sets the ip to input value and starts a client
    /// @param networkManager   - Network manager provided by Unity for GameObjects package
    /// @param unityTransport   - Unity transport provided by Unity for GameObjects package
    /// @see SetButton

    private void ConnectToServer(NetworkManager networkManager, UnityTransport unityTransport)
    {
        unityTransport.ConnectionData.Address = ip;
        networkManager.StartClient();
        SetButton();
    }

    /// Sets client ui button active based on chose action event 
    private void SetButton()
    {
        actionEvent = drpButtonType.options[drpButtonType.value].text;

        btnAction.gameObject.SetActive(false);
        btnRight.gameObject.SetActive(false);
        btnLeft.gameObject.SetActive(false);

        if(actionEvent == "action") btnAction.gameObject.SetActive(true);
        if(actionEvent == "left") btnLeft.gameObject.SetActive(true);
        if(actionEvent == "right") btnRight.gameObject.SetActive(true);
    }

    /// Sends a action to the server using RPC
    /// see PlayerMovement() DropServerRpc method
    private void SendActionToServer(string action)
    {
        if (NetworkManager.Singleton.IsClient && NetworkManager.Singleton.IsConnectedClient)
        {
            if (action == "action") PlayerMovement.Instance.DropServerRpc();
            if (action == "left") PlayerMovement.Instance.MoveLeftServerRpc();
            if (action == "right") PlayerMovement.Instance.MoveRightServerRpc();
        }
        else
        {
            Debug.LogWarning("Client not connected or not a client.");
        }
    }

}
