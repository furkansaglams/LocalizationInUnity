using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageMenuItem : MonoBehaviour
{
    [HideInInspector] public Image img;
    [HideInInspector] public RectTransform rectTrans;

    LanguageMenu langauageMenu;
    Button button;

    private int index;
    void Awake()
    {
        img = GetComponent<Image>();
        rectTrans = GetComponent<RectTransform>();
        langauageMenu = rectTrans.parent.GetComponent<LanguageMenu>();
        button = GetComponent<Button>();

        index = rectTrans.GetSiblingIndex() - 1;

        button.onClick.AddListener(OnItemClick);


    }

    void OnItemClick()
    {
        langauageMenu.OnItemClick(index, img);
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(OnItemClick);
    }
}
