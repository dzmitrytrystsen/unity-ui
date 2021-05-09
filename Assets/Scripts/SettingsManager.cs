using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public Button backButton;
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI headerHint;
    public GameObject settingsButtonsObject;
    public Button[] settingsButtons;
    public GameObject allMenusObject;
    public GameObject buttonsMenuObject;
    public GameObject toggleMenuObject;
    public GameObject dropsMenuObject;
    public GameObject inputMenuObject;
    public GameObject scrollViewMenuObject;

    private GameObject currentMenu;


    void Start()
    {
        AddListenersForAllSettingsButtons();
        AddListenersToButtonsMenu();
        AddListenersToTogglesMenu();
        AddListenersToDropsMenu();
        AddListenersToInputMenu();
    }

    private void DisableSettingsMenu(string buttonText)
    {
        backButton.gameObject.SetActive(true);
        headerText.text = buttonText;
        headerHint.text = buttonText;

        settingsButtonsObject.SetActive(false);
    }

    private void AddListenersForAllSettingsButtons()
    {

        for (int buttonIndex = 0; buttonIndex < settingsButtons.Length; buttonIndex++)
        {
            Button currentButton = settingsButtons[buttonIndex];
            string currentButtonText = currentButton.GetComponentInChildren<TextMeshProUGUI>().text;

            if (currentButtonText == "Buttons")
            {
                currentButton.onClick.AddListener(() => {
                    DisableSettingsMenu(currentButtonText);
                    buttonsMenuObject.SetActive(true);
                    currentMenu = buttonsMenuObject;
                });
            }
            else if (currentButtonText == "Toggles")
            {
                currentButton.onClick.AddListener(() => {
                    DisableSettingsMenu(currentButtonText);
                    toggleMenuObject.SetActive(true);
                    currentMenu = toggleMenuObject;
                });
            }
            else if (currentButtonText == "Drops")
            {
                currentButton.onClick.AddListener(() => {
                    DisableSettingsMenu(currentButtonText);
                    dropsMenuObject.SetActive(true);
                    currentMenu = dropsMenuObject;
                });
            }
            else if (currentButtonText == "Input")
            {
                currentButton.onClick.AddListener(() => {
                    DisableSettingsMenu(currentButtonText);
                    inputMenuObject.SetActive(true);
                    currentMenu = inputMenuObject;
                });
            }
            else if (currentButtonText == "Scroll View")
            {
                currentButton.onClick.AddListener(() => {
                    DisableSettingsMenu(currentButtonText);
                    scrollViewMenuObject.SetActive(true);
                    currentMenu = scrollViewMenuObject;
                });
            }
        }

        backButton.onClick.AddListener(() =>
        {
            backButton.gameObject.SetActive(false);
            headerText.text = "Settings";
            headerHint.text = "";

            currentMenu.SetActive(false);
            currentMenu = null;
            settingsButtonsObject.SetActive(true);
        });
    }

    private void AddListenersToButtonsMenu()
    {
        foreach (Button button in buttonsMenuObject.GetComponentsInChildren<Button>())
        {
            string buttonName = button.GetComponentInChildren<TextMeshProUGUI>().text;

            if (buttonName == "One" || buttonName == "Two")
                button.onClick.AddListener(() => headerHint.text = buttonName + " button was clicked");

            else if (buttonName == "Disable")
            {
                button.onClick.AddListener(() =>
                {
                    foreach (Button btn in buttonsMenuObject.GetComponentsInChildren<Button>())
                    {
                        if (btn.GetComponentInChildren<TextMeshProUGUI>().text == "Disable")
                            return;
                        else
                            btn.interactable = !btn.interactable;
                    }
                });
            }
        }
    }

    private void AddListenersToTogglesMenu()
    {
        foreach (Toggle toggle in toggleMenuObject.GetComponentsInChildren<Toggle>())
        {
            string toggleName = toggle.GetComponentInChildren<Text>().text;

            toggle.onValueChanged.AddListener(delegate
            {
                headerHint.text = toggleName + " toggle was selected";
            });
        }
    }

    private void AddListenersToDropsMenu()
    {
        foreach (TMP_Dropdown dropdown in dropsMenuObject.GetComponentsInChildren<TMP_Dropdown>())
        {
            dropdown.onValueChanged.AddListener(delegate
            {
                headerHint.text = dropdown.options[dropdown.value].text + " is selected";
            });
        }
    }

    private void AddListenersToInputMenu()
    {
        foreach (TMP_InputField input in inputMenuObject.GetComponentsInChildren<TMP_InputField>())
        {
            if(input.name == "TitleInput")
            {
                input.onValueChanged.AddListener(delegate
                {
                    headerHint.text = "Title: " + input.text + "\n" + "Text: " + input.transform.parent.GetChild(1).GetComponent<TMP_InputField>().text;
                });
            }
            else
            {
                input.onValueChanged.AddListener(delegate
                {
                    headerHint.text = "Title: " + input.transform.parent.GetChild(0).GetComponent<TMP_InputField>().text + "\n" + "Text: " + input.text;
                });
            }
        }
    }
}
