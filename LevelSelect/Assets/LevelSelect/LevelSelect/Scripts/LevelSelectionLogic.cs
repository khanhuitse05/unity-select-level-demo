using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelSelectionLogic : MonoBehaviour
{

    static LevelSelectionLogic _instance;
    public static LevelSelectionLogic Instance { get { return _instance; } }

    public List<LevelList> levelList = new List<LevelList>(); //List of level class;
    public static LevelList currentLevel;
    public GameObject btnPlay;

    void Awake()
    {
        _instance = this;
    }
    public void init()
    {
        if (levelList.Count != PlayerPrefsLevel.maxLevel)
        {
            Debug.LogWarning("levelList.Count != MAX_LEVEL");
        }
        for (int i = 0; i < levelList.Count; i++)
        {
            levelList[i].SetInfo(PlayerPrefsLevel.levelData.GetLevelInfo(i));
        }
        currentLevel = null;
    }
    public void onEnter()
    {
        if (currentLevel != null)
        {
            currentLevel.Highlight(false);
            currentLevel = null;
        }
    }
    public void OnClickLevel(LevelList _item)
    {
        if (_item.info.isUnlock)
        {
            if (currentLevel == null)
            {
                _item.Highlight(true);
                btnPlay.SetActive(true);
                currentLevel = _item;
            }
            else
            {
                currentLevel.Highlight(false);
                _item.Highlight(true);
                currentLevel = _item;
            }
        }
    }
    public void OnClickPlay()
    {
        if (currentLevel != null)
        {
            int _star = Random.Range(1, 4);
            PlayerPrefsLevel.levelData.CurrentLevel = currentLevel.info.LevelIndex;
            PlayerPrefsLevel.levelData.FinishLevel(_star);
            for (int i = 0; i < levelList.Count; i++)
            {
                levelList[i].SetInfo(PlayerPrefsLevel.levelData.GetLevelInfo(i));
            }
        }
        //SceneControl.LoadLevel(currentLevel.info.LevelIndex);
    }
}


