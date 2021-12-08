using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class TabsGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabButton selectTab;
    public List<GameObject> objectsToSwap;
    public PanelGroup panelGroup;
    public int i;
    public void Subscribe(TabButton button)
    {
        if (tabButtons==null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
    }

    public int GetIndex()
    {
        return i;
    }
    /*public void OnTabEnter(TabButton button)
    {
        ResestTabs();
        if (selectTab == null || button != selectTab)
        {
            button.background.sprite = tabHover;
        }
    }*/

    public void OnTabExit(TabButton button)
    {
        ResestTabs();
    }

    public void OnTabSelected(TabButton button)
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
        foreach (TabButton button in tabButtons)
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (i==0)
            {
                i = 1;
            }
            else
            {
                i--;
            }
            OnTabSelected(tabButtons[i]);       
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (i==1)
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
