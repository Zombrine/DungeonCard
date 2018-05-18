using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using SocketIO;

public static class Global // важные данные, доступные во всей области проекта. Обычно, здесь хранятся данные пользователя.
{
    public static SocketIOComponent Socket;
    public static string Login;
    public static string Token;
    
}
public class ServerHelper : MonoBehaviour {
    public GameObject login;
    public GameObject password;

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.Find("SocketIO");
        Global.Socket = go.GetComponent<SocketIOComponent>();
        Global.Socket.On("onMessage", onMessage);
	}
	
	public void onMessage(SocketIOEvent e)
    {
        Debug.Log("Message");
    }

    public void LogOn()
    {
        JSONObject data = new JSONObject(JSONObject.Type.OBJECT);
        data.AddField("module", "authorization");
        data.AddField("act", "logOn");
        JSONObject param = new JSONObject(JSONObject.Type.OBJECT);
        param.AddField("login", "Test");
        param.AddField("password", "Test");
        data.AddField("param", param);
        Global.Socket.Emit("onMessage", data);
    }
}
