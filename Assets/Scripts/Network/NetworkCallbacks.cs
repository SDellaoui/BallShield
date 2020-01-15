using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    List<string> logMessages = new List<string>();
    public override void SceneLoadLocalDone(string map)
    {
        // randomize a position
        var spawnPosition = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);
        
        // instantiate player
        BoltNetwork.Instantiate(BoltPrefabs.Player, spawnPosition, Quaternion.identity);
        // instantiate ball
        if(BoltNetwork.IsServer)
            BoltNetwork.Instantiate(BoltPrefabs.Ball, Vector3.zero, Quaternion.identity);
    }
    public override void SessionConnectFailed(UdpKit.UdpSession session, Bolt.IProtocolToken token)
    {
        Debug.Log("Deconnexionnnnnnnnnnnnnn");
    }
    public override void OnEvent(LogEvent evnt)
    {
        logMessages.Insert(0,evnt.Message);
    }
    void OnGUI()
    {
        // only display max the 5 latest log messages
        int maxMessages = Mathf.Min(5, logMessages.Count);
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);

        for (int i = 0; i < maxMessages; ++i)
        {
            GUILayout.Label(logMessages[i]);
        }
        GUILayout.EndArea();
    }
}
