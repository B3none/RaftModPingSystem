using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RaftModPingSystem : Mod
{
    public static List<Network_Player> currentPlayers = new List<Network_Player>();

    public void Start()
    {
        Debug.Log("Mod RaftModPingSystem has been loaded!");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            Debug.Log(CanvasHelper.ActiveMenu);
            Debug.Log("LeftAlt pressed and menu not open");

            currentPlayers.Clear();
            
            var players = FindObjectsOfType<Network_Player>();
            var localPlayer = RAPI.GetLocalPlayer();

            foreach (var player in players)
            {
                if (player != localPlayer) {
                    currentPlayers.Add(player);
                }
            }
            
            Network_Player thisPlayer = RAPI.GetLocalPlayer();

            if (thisPlayer != null) {
                PlayerPing(thisPlayer);
            }
        }
    }

    private void PlayerPing(Network_Player player) {
        OnGUI();
    }

    void OnGUI()
    {
        foreach (var player in currentPlayers)
        {
            if (player == null) {
                continue;
            }

            var delta = player.transform.position - Camera.main.transform.position;
            
            if (Vector3.Dot(Camera.main.transform.forward, delta) < 0) {
                continue;
            }

            var pos = Camera.main.WorldToScreenPoint(player.transform.position);
            string[] names = player.name.Split(',');
            GUI.Box(new Rect(pos.x, Screen.height - pos.y, 150, 20), names[names.Length - 1]);
        }
    }

    public void OnModUnload()
    {
        Debug.Log("Mod RaftModPingSystem has been unloaded!");
    }
}
