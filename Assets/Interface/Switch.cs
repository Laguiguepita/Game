using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public GameObject home;
    public GameObject settings;
    public TabsGroup tab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int i =tab.GetIndex();
        if (i==0)
        {
            home.SetActive(true);
            settings.SetActive(false);
        }
        else if (i==1)
        {
            home.SetActive(false);
            settings.SetActive(true);
        }
    }
}
