using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;

public class LogOn : MonoBehaviour {

   public Text login;
   public Text password;
   public Text password2;
   public Text nickname;
   public Text textError;

   public void PushLogin()
   {
      if (Global.socket.socket.IsAlive)
      {
         JSONObject data = new JSONObject(JSONObject.Type.OBJECT);
         data.AddField("module", "authorization");
         data.AddField("act", "logOn");
         JSONObject param = new JSONObject(JSONObject.Type.OBJECT);
         param.AddField("login", login.GetComponent<Text>().text);
         string hash;
         using (MD5 md5Hash = MD5.Create())
         {
            hash = GetMd5Hash(md5Hash, (string)password.text);
         }
         param.AddField("password", hash);
         data.AddField("param", param);
         Global.socket.Emit("onMessage", data);
         Debug.Log("Server");
         deactivateButtons();
      }
      else
      {
         textError.text = "Проверьте подключение к интернету";
         Debug.Log("No server");
      }
   }

   public void PushRegister()
   {
      if (Global.socket.socket.IsAlive)
      {
         if (password.text == password2.text)
         {
            JSONObject data = new JSONObject(JSONObject.Type.OBJECT);
            data.AddField("module", "authorization");
            data.AddField("act", "register");
            JSONObject param = new JSONObject(JSONObject.Type.OBJECT);
            param.AddField("login", login.GetComponent<Text>().text);
            string hash;
            using (MD5 md5Hash = MD5.Create())
            {
               hash = GetMd5Hash(md5Hash, (string)password.text);
            }
            param.AddField("password", hash);

            param.AddField("nickname", nickname.GetComponent<Text>().text);
            data.AddField("param", param);
            Global.socket.Emit("onMessage", data);
            Debug.Log("Server");
            deactivateButtons();
         }
      }
      else
      {
         textError.text = "Проверьте подключение к интернету";
         Debug.Log("No server");
      }
   }

   static string GetMd5Hash(MD5 md5Hash, string input)
   {

      // Convert the input string to a byte array and compute the hash.
      byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

      // Create a new Stringbuilder to collect the bytes
      // and create a string.
      StringBuilder sBuilder = new StringBuilder();

      // Loop through each byte of the hashed data 
      // and format each one as a hexadecimal string.
      for (int i = 0; i < data.Length; i++)
      {
         sBuilder.Append(data[i].ToString("x2"));
      }

      // Return the hexadecimal string.
      return sBuilder.ToString();
   }

   void deactivateButtons()
   {
      for (int i = 0; i <= 1; i++)
      {
         GameObject btn = GameObject.FindGameObjectWithTag("buttons");
         btn.SetActive(false);
      }
   }
}
