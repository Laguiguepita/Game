using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterButton : MonoBehaviour
{
    public Button butt;
    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            butt.onClick.Invoke();
        }
    }
}
