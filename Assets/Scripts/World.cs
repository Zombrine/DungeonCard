using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;

public class World : MonoBehaviour
{

   [SerializeField] GameObject prefTile;
   // Use this for initialization
   void Start()
   {
      JSONObject data = new JSONObject(JSONObject.Type.OBJECT);
      data.AddField("module", "location");
      data.AddField("act", "getLocation");
      JSONObject param = new JSONObject(JSONObject.Type.OBJECT);
      data.AddField("param", param);

      /*param.AddField("password", hash);
      data.AddField("param", param);*/
      Global.socket.Emit("onMessage", data);
      
      Debug.Log("Server");
   }

   void StructLocation(SocketIOEvent ev) // Построение локации. Вызывается сервером после получение запроса GetLocation
   {
      Debug.Log("StructLocation");
      Global.LocationType = ev.data["Location"].str;

      float Size = ev.data["Size"].n;
      Size = Mathf.Sqrt(Size);
      for (int j = 0; j < Size; j++)
      {
         for (int i = 0; i < Size; i++)
         {
            GameObject Temp = Instantiate(prefTile, new Vector3(0 + 150 * i, 0 + 150 * j, 0f), Quaternion.identity);
            string SectorName = ev.data["x" + i + "y" + j].str;
            Temp.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + SectorName);
            Temp.name = SectorName;
            Temp.transform.parent = (GameObject.Find("CanvasWorld")).transform;
            Temp.transform.localPosition = new Vector3(0 + 150 * i, 0 + 150 * j, 0f);
            Temp.transform.localScale = new Vector3(1, 1, 1);
         }
      }
   }
}
