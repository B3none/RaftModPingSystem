using UnityEngine;

public class RaftModPingSystem : Mod
{
    private bool show = false;
    List<Network_Player> currentPlayers = new List<Network_Player>();

    public void Start()
    {
        Debug.Log("Mod RaftModPingSystem has been loaded!");
    }

    public void Update()
    {   
        if (CanvasHelper.ActiveMenu !== MenuType.Inventory && Input.GetKeyDown(KeyCode.LeftAlt)) {
            if (!show) {
                currentPlayers.Clear();
                
                var players = FindObjectsOfType<Network_Player>();
                var localPlayer = RAPI.GetLocalPlayer();

                foreach (var player in players)
                {
                    if (player != localPlayer) {
                        currentPlayers.Add(player);
                    }
                }
            }

            show = !show;
            
            Network_Player player = RAPI.GetLocalPlayer();

            if (player != null) {
                SortPlayerInventory(player);
            }
        }
    }

    private void PlayerPing(Network_Player player) {
        
    }

    void OnGUI()
    {
        if (!show) {
            return;
        }

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
