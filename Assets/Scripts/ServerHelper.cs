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
    public static string LocationType;
    public static int HP;
    public static int DMG;
    public static int Level;
    public static int XP;
    public static int XPForLevel;
    public static JSONObject[] Items;
    public static int QuantityItems;
    public static int Money;
    public static string Battle;
    public static string BattleLeader;
    public static string BattleStep;
    public static GameObject HeroCard;

    public static JSONObject PrepareData(JSONObject data)
    {
        data.AddField("login", Login);
        data.AddField("token", Token);
        if (Battle != "") data.AddField("battle", Battle);
        return data;
    }

    public static void ChangeProperty(string Name, int Value)
    {
        switch (Name)
        {
            case "HP":
                HP += Value;
                break;
            case "DMG":
                DMG += Value;
                break;
        }
    }
}
public class ServerHelper : MonoBehaviour {
    public GameObject login;
    public GameObject password;

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.Find("SocketIO");
        Global.Socket = go.GetComponent<SocketIOComponent>();
        Global.Socket.On("Message", onMessage);
	}
	
	public void onMessage(SocketIOEvent e)
    {
        Debug.Log(e.data);
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
