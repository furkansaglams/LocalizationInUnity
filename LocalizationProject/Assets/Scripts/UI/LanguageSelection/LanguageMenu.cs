using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;



public class LanguageMenu : MonoBehaviour
{
    [Header("space between menu items")]
    [SerializeField] Vector2 spacing;

    [Space]
    [Header("Main button rotation")]
    [SerializeField] float rotationDuration;
    [SerializeField] Ease rotationEase;

    [Space]
    [Header("Animation")]
    [SerializeField] float expandDuration;
    [SerializeField] float collapseDuration;
    [SerializeField] Ease expandEase;
    [SerializeField] Ease collapseEase;

    [Space]
    [Header("Fading")]
    [SerializeField] float expandFadeDuration;
    [SerializeField] float collapseFadeDuration;

    Button mainButton;
    LanguageMenuItem[] langauageItems;
    bool isExpanded = false;
    Vector2 mainButtonPosition;
    private int itemsCount;
    private int langId;
    void Start()
    {

        itemsCount = transform.childCount - 1;

        langauageItems = new LanguageMenuItem[itemsCount];

        for (int i = 0; i < itemsCount; i++)
        {
            langauageItems[i] = transform.GetChild(i + 1).GetComponent<LanguageMenuItem>();
        }

        mainButton = transform.GetChild(0).GetComponent<Button>();
        mainButton.onClick.AddListener(ToggleMenu);
        mainButton.transform.SetAsLastSibling();
        
        mainButtonPosition = mainButton.GetComponent<RectTransform>().anchoredPosition;
        langId = PlayerPrefs.GetInt("langId");
        ResetPositions();
    }
    void ResetPositions()
    {
        for (int i = 0; i < itemsCount; i++)
        {
            langauageItems[i].rectTrans.anchoredPosition = mainButtonPosition;
        }
    }
    void ToggleMenu()
    {
        isExpanded = !isExpanded;

        if (isExpanded)
        {

            for (int i = 0; i < itemsCount; i++)
            {
                langauageItems[i].rectTrans.DOAnchorPos(mainButtonPosition + spacing * (i + 1), expandDuration).SetEase(expandEase);

                langauageItems[i].img.DOFade(1f, expandFadeDuration).From(0f);
            }
            mainButton.transform
                          .DORotate(Vector3.forward * 360f, rotationDuration, RotateMode.FastBeyond360)
                          .SetEase(rotationEase);

        }
        else
        {

            for (int i = 0; i < itemsCount; i++)
            {
                langauageItems[i].rectTrans.DOAnchorPos(mainButtonPosition, collapseDuration).SetEase(collapseEase);

                langauageItems[i].img.DOFade(0f, collapseFadeDuration);
            }

            mainButton.transform
                          .DORotate(Vector3.forward * (-360f), rotationDuration, RotateMode.FastBeyond360)
                          .SetEase(rotationEase);

        }


    }
    public void OnItemClick(int index, Image btnImage)
    {
        mainButton.image.sprite = btnImage.sprite;
        langId = index;
        ToggleMenu();
    }
    
   
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("langId", langId);
    }




}
