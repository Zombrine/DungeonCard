using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using SocketIO;

public static class Global // важные данные, доступные во всей области проекта. Обычно, здесь хранятся данные пользователя.
{
   public static SocketIOComponent socket;
   public static string login;
   public static string token;
}

public class ServerHelper : MonoBehaviour
{

   public GameObject login;
   public GameObject password;

   // Use this for initialization
   void Start()
   {
      GameObject go = GameObject.Find("SocketIO");
      Global.socket = go.GetComponent<SocketIOComponent>();
      Global.socket.On("onMessage", onMessage);
   }

   public void onMessage(SocketIOEvent e)
   {
      Debug.Log(e.name + " " + e.data);
   }
}
