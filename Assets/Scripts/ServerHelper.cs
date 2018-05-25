using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
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
   public GameObject loadingScreen;
   public Text textError;
   // Use this for initialization
   void Start()
   {
      GameObject go = GameObject.Find("SocketIO");
      Global.socket = go.GetComponent<SocketIOComponent>();
      Global.socket.On("onMessage", onMessage);
   }

   public void onMessage(SocketIOEvent e)
   {
      Debug.Log("first data: " + e.data);
      Debug.Log("module: " + e.data["module"].str);

      switch (e.data["module"].str)
      {
         case "authorization":
            AuthorizationModule(e.data);
            break;
      }
   }

   public void AuthorizationModule(JSONObject data)
   {
      Debug.Log("data: " + data);
      Debug.Log(data["act"].str);
      switch (data["act"].str)
      {
         case "logOn":
            onLogOn(data["param"]);
            break;
         case "register":
            onRegisterOn(data["param"]);
            break;
      }
   }

   public void onLogOn(JSONObject param)
   {
      Debug.Log(param);
      Debug.Log(param["error_code"]);
      Debug.Log(param.GetField("error_code"));
      if (param["error_code"].n == 0)
      {
         Debug.Log("SUCCESS!!!");
         loadingScreen.SetActive(true);
         GameObject go = GameObject.Find("MainLogin");
         go.SetActive(false);
         textError.text = "";
      }
      else if (param["error_code"].n == 1)
         textError.text = "Проверьте логин или пароль";
      else
         Debug.Log("Error in logOn: " + param["error_code"].str);
   }

   public void onRegisterOn(JSONObject param)
   {
      Debug.Log(param);
      Debug.Log(param["error_code"]);
      Debug.Log(param.GetField("error_code"));
      if (param["error_code"].n == 0)
      {
         Debug.Log("Register!!!");
         loadingScreen.SetActive(true);
         GameObject go = GameObject.Find("MainLogin");
         go.SetActive(false);
         textError.text = "";
      }
      else if (param["error_code"].n == 1)
         textError.text = "Такой логин уже используется";
      else if (param["error_code"].n == 2)
         textError.text = "Такой никнейм уже используется";
      else
         Debug.Log("Error in logOn: " + param["error_code"].str);
   }
}
