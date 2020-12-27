using UnityEngine;

public class RaftModPingSystem : Mod
{
    public void Start()
    {
        Debug.Log("Mod RaftModPingSystem has been loaded!");
    }

    public void OnModUnload()
    {
        Debug.Log("Mod RaftModPingSystem has been unloaded!");
    }
}