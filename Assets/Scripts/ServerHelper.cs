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

   //public GameObject login;
   //public GameObject password;

   // Use this for initialization
   void Start()
   {
      GameObject go = GameObject.Find("SocketIO");
      Global.socket = go.GetComponent<SocketIOComponent>();
      Global.socket.On("onMessage", onMessage);
   }

   public void onMessage(SocketIOEvent e)
   {
      switch (e.data["module"].str) {
         case "authorization":
            AuthorizationModule (e.data);
            break;
      }
   }
   
   public void AuthorizationModule (JSONObject data)
   {
      switch (data["act"].str) {
         case "logOn":
            onLogOn (data["param"]);
            break;
      }
   }
   
   public void onLogOn (JSONObject param)
   {
      if (param["error_code"].n == 0)
         Debug.Log ("SUCCESS!!!");
      else
         Debug.Log ("Error in logOn: " + param["error_code"].str);
   }
}
