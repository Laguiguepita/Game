/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TestGroup : MonoBehaviour
{
    protected List<TabButton> _tabs = new List<TabButton>();

    public GameObject[] GameObjects;

    public PanelGroup pagePanel;

    [SerializeField] protected TabButton activeTab;

    public Action onTabSelectedCallback;
    // Start is called before the first frame update
    void Start()
    {
        StartActiveTab();
    }

    public void SetActive(TabButton tab)
    {
        foreach (TabButton t in _tabs)
        {
            t.Desactivate();
        }

        activeTab = tab;
        activeTab.Activate();
        if (onTabSelectedCallback != null)
        {
            onTabSelectedCallback();
        }

        if (tabSwapsActiveGameObject)
        {
            SwapGameObject();
        }

        if (pagePanel != null)
        {
            pagePanel.SetPageIndex(tab.transform.GetSiblingIndex()); 
        }
    }
    public void SetActive(int siblingIndex)
    {
        foreach (TabButton t in _tabs)
        {
            if (t.transform.GetSiblingIndex() == siblingIndex)
            {
                SetActive(t);
                return;
            }
        }
    }
    void SwapGameObject()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }

        if (activeTab.transform.GetSiblingIndex() < gameObjects.Length)
        {
            gameObjects[activeTab.transform.GetSiblingIndex()].SetActive(true);
        }
    }
    public void DisableTab(int index)
    {
        if (_tabs.Count >index)
        {
            _tabs[index].Disable();
        }
    }

    public void EnableTab(int index)
    {
        if (_tabs.Count > index)
        {
            _tabs[index].Enable();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (activeTab.transform.GetSiblingIndex() > 0)
            {
                SetActive(activeTab.transform.GetSiblingIndex() - 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (activeTab.transform.GetSiblingIndex() < transform.childCount-1)
            {
                SetActive(activeTab.transform.GetSiblingIndex() + 1);
            }
        }
    }
}*/
