using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus;

public class GameInstance : MonoBehaviour
{
    public ColyseusClient Client { get; private set; }
    public ColyseusRoom<MyRoomState> Room { get; private set; }

    public static GameInstance Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        CreateClient();
    }

    public void CreateClient()
    {
        Client = new ColyseusClient("ws://localhost:2567");
    }

    /// <summary>
    /// Call Function from UI
    /// </summary>
    public async void JoinOrCreateRoom()
    {
        try
        {
            Room = await Client.JoinOrCreate<MyRoomState>("my_room");
            Room.OnMessage<SimpleChatFromServer>("simple-chat-from-server", (data) =>
            {
                Debug.Log("Recv " + data.message + " from server");
            });

            Room.OnStateChange += (state, isFirstState) =>
            {
                Debug.Log("OnStateChange " + isFirstState + " " + state.mySynchronizedProperty);
            };

            /*
            Room.State.OnMySynchronizedPropertyChange += (val, isFirstState) =>
            {

            };
            */
        }
        catch (System.Exception ex)
        {
           
            Debug.LogError($"[GameInstance->JoinOrCreateRoom] Cannot Join: {ex.Message}");
            return;
        }
    }

    /// <summary>
    /// Call Function from UI
    /// </summary>
    public async void Send()
    {
        await Room.Send("simple-chat", "#Send# Hellow");
    }
}
