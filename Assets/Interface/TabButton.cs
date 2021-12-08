using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour,IPointerEnterHandler, IPointerClickHandler//, IPointerExitHandler
{
    public TabsGroup tabGroup;

    public Image background;
    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    // Update is called once per frame

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
            Debug.Log(name + " Game Object Right Clicked!");
        }

        //Use this to tell when the user left-clicks on the Button
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log(name + " Game Object Left Clicked!");
        }
    }

    /*public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
        Debug.Log("The cursor exited the selectable UI element.");
    }*/

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
        Debug.Log("The cursor entered the selectable UI element.");
    }
}
