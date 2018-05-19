using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogOn : MonoBehaviour {

   public Text login;
   public Text password;
   public Text error;

   public void Push()
   {
      JSONObject data = new JSONObject(JSONObject.Type.OBJECT);
      data.AddField("module", "authorization");
      data.AddField("act", "logOn");
      JSONObject param = new JSONObject(JSONObject.Type.OBJECT);
      param.AddField("login", login.GetComponent<Text>().text);
      param.AddField("password", password.GetComponent<Text>().text);
      data.AddField("param", param);
      if (Global.socket.IsConnected)
      {
         if (login.GetComponent<Text>().text == "Test" && password.GetComponent<Text>().text == "Test")
            Global.socket.Emit("onMessage", data);
         else
         {
            error.gameObject.SetActive(true);
            error.GetComponent<Text>().text = "Проверьте логин или пароль";
         }
      }
      else
      {
         error.GetComponent<Text>().text = "Проверьте подключение к интернету";
      }
   }
}
