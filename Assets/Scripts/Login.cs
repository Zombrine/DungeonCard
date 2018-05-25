using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

   [SerializeField] GameObject mainLogin;
   [SerializeField] GameObject mainRegister;

   Button btnLogin;
   Button btnRegister;

   void Start()
   {
      GameObject btnLogin1 = GameObject.Find("ButtonLogin");
      btnLogin = btnLogin1.GetComponent<Button>();
      GameObject btnRegister1 = GameObject.Find("ButtonRegister");
      btnRegister = btnRegister1.GetComponent<Button>();
   }

   public void LoginButton()
   {
      //gameObject.SetActive(false);
      if (btnLogin.interactable)
      {
         btnLogin.interactable = false;
         btnRegister.interactable = true;
         mainLogin.SetActive(true);
         mainRegister.SetActive(false);
      }
   }

   public void RegisterButton()
   {
      //gameObject.SetActive(false);
      if (btnRegister.interactable)
      {
         btnLogin.interactable = true;
         btnRegister.interactable = false;
         mainLogin.SetActive(false);
         mainRegister.SetActive(true);
      }
   }
}
