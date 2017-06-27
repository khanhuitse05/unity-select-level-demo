using UnityEngine;
public class LevelSelectDemo : MonoBehaviour
{
    public LevelSelectionLogic levelLogic;

    void Awake()
    {
        PlayerPrefsLevel.LoadLevelData();
        levelLogic.init();
    }
    void Start()
    {
        levelLogic.onEnter();
    }
}