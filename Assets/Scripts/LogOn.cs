using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOn : MonoBehaviour {
    public GameObject obj;
    public void Push()
    {
        obj.GetComponent<ServerHelper>().LogOn();
    }
}
