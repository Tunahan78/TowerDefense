using UnityEditor;
using UnityEngine;

public class UI_Enable : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;

    public void SwitchTo(GameObject uiElement)
    {
       foreach(var ui in uiElements)
        {
            ui.SetActive(false);
        }
        
        uiElement.SetActive(true);
    }

    public void QuitButton()
    {
        if(EditorApplication.isPlaying)
          EditorApplication.isPlaying = false;
        else
          Application.Quit();
    }
}
