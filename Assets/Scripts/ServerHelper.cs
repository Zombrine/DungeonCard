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
   public static string LocationType;
}

public class ServerHelper : MonoBehaviour
{

   [SerializeField] GameObject prefTile;
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
         case "location":
            LocationModule(e.data);
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
            onRegister(data["param"]);
            break;
         
      }
   }

   public void LocationModule(JSONObject data)
   {
      Debug.Log("data: " + data);
      Debug.Log(data["act"].str);
      switch (data["act"].str)
      {
         case "getLocation":
            onGetLocation(data["param"]);
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
         GameObject go = GameObject.Find("CanvasMain");
         go.SetActive(false);
      }
      else if (param["error_code"].n == 1)
         textError.text = "Проверьте логин или пароль";
      else
         Debug.Log("Error in logOn: " + param["error_code"].str);
   }

   public void onRegister(JSONObject param)
   {
      Debug.Log(param);
      Debug.Log(param["error_code"]);
      Debug.Log(param.GetField("error_code"));
      if (param["error_code"].n == 0)
      {
         Debug.Log("Register!!!");
         loadingScreen.SetActive(true);
         GameObject go = GameObject.Find("CanvasMain");
         go.SetActive(false);
      }
      else if (param["error_code"].n == 1)
         textError.text = "Такой логин уже используется";
      else if (param["error_code"].n == 2)
         textError.text = "Такой никнейм уже используется";
      else
         Debug.Log("Error in logOn: " + param["error_code"].str);
   }

   public void onGetLocation(JSONObject param)
   {
      Debug.Log(param);
      Debug.Log(param["error_code"]);
      Debug.Log(param.GetField("error_code"));
      if (param["error_code"].n == 0)
      {
         Debug.Log("WORLD!!!");
         Global.socket.On("StructLocation", StructLocation);
         //wo.CreateWorld();
         //GameObject go = GameObject.Find("CanvasMain");
         //go.SetActive(false);
      }
      else
         Debug.Log("Error in logOn: " + param["error_code"].str);
   }

   void StructLocation(SocketIOEvent ev)
   {
      Debug.Log("StructLocation");
      for (int j = 0; j < 6; j++)
      {
         for (int i = 0; i < 6; i++)
         {
            GameObject Temp = Instantiate(prefTile, new Vector3(-49 + (i * 2), 49 - (j * 2), 0f), Quaternion.identity);
            string SectorName = ev.data["x" + i + "y" + j].str;
            Temp.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + SectorName);
            Temp.name = SectorName;
            Temp.transform.parent = (GameObject.Find("CanvasWorld")).transform;
            Temp.transform.localPosition = new Vector3(-600 * 150 * i, -600 + 150 * j, 0f);
            Temp.transform.localScale = new Vector3(1, 1, 1);
         }
      }
   }
}
