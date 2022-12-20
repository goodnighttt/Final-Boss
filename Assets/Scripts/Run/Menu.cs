using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public string _newGameLevel;
    private string levelToLoad;

    [SerializeField] private GameObject nosavedGameDialog = null;
    void Start()
    {
        
    }

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel, LoadSceneMode.Single);
    }

    public void LoadGameDialogYes()
    {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            nosavedGameDialog.SetActive(true);
        }
    }
    public void OnStart()
    {
        Debug.Log("º”‘ÿ÷˜≥°æ∞");
        SceneManager.LoadScene("Scenes/Main", LoadSceneMode.Additive);
    }

    public void OnExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
