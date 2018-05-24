using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogOn : MonoBehaviour {

   public Text login;
   public Text password;
   public Text textError;

   public void Push()
   {
      if (Global.socket.socket.IsAlive)
      {
         JSONObject data = new JSONObject(JSONObject.Type.OBJECT);
         data.AddField("module", "authorization");
         data.AddField("act", "logOn");
         JSONObject param = new JSONObject(JSONObject.Type.OBJECT);
         param.AddField("login", login.GetComponent<Text>().text);
         param.AddField("password", password.GetComponent<Text>().text);
         data.AddField("param", param);
         Global.socket.Emit("onMessage", data);
         Debug.Log("Server");
      }
      else
      {
         textError.text = "Проверьте подключение к интернету";
         Debug.Log("No server");
      }
   }
}
