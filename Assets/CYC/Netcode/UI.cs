using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] Button btnHost;
    [SerializeField] Button btnServer;
    [SerializeField] Button btnClient;
    private void Awake()
    {
        btnHost.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
        btnServer.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });
        btnClient.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
