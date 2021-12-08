using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class PanelGroup : MonoBehaviour
{
    public List<PanelButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public PanelButton selectTab;
    public List<GameObject> objectsToSwap;
    public PanelGroup panelGroup;
    public int i;
    public List<TabButton> haut;
    
    
    public void Subscribe(PanelButton button)
    {
        if (tabButtons==null)
        {
            tabButtons = new List<PanelButton>();
        }
        tabButtons.Add(button);
    }

    /*public void OnTabEnter(TabButton button)
    {
        ResestTabs();
        if (selectTab == null || button != selectTab)
        {
            button.background.sprite = tabHover;
        }
    }*/

    public void OnTabExit(PanelButton button)
    {
        ResestTabs();
    }

    public void OnTabSelected(PanelButton button)
    {
        selectTab = button;
        ResestTabs();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i==index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResestTabs()
    {
        foreach (PanelButton button in tabButtons)
        {
            if (selectTab != null && button == selectTab)
            {
                continue;
            }
            button.background.sprite = tabIdle;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (i==0)
            {
                i = 3;
            }
            else
            {
                i--;
            }
            OnTabSelected(tabButtons[i]);       
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (i==3)
            {
                i = 0;
            }
            else
            {
                i++;
            }
            OnTabSelected(tabButtons[i]);       
        }

    }
}
