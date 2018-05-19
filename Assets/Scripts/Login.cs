using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour {

   public GameObject canvas;

   public void LoginButton()
   {
      canvas.SetActive(true);
      gameObject.SetActive(false);
   }
}
