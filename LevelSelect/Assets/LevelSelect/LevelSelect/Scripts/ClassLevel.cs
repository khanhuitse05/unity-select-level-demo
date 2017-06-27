using System.Collections.Generic;

public class PlayerPrefsLevel
{
    #region LevelData
    const string prefsKeyLevel = "key_prefsKeyLevel";
    public const int maxLevel = 5;
    static LevelData _levelData;
    public static LevelData levelData { get { return _levelData; } }
    public static LevelData LoadLevelData()
    {
        _levelData = SaveGameManager.loadData<LevelData>(prefsKeyLevel);
        if (_levelData == null)
        {
            _levelData = new LevelData();
            saveLevelData();
        }
        return _levelData;
    }
    public static void saveLevelData()
    {
        SaveGameManager.saveData<LevelData>(prefsKeyLevel, _levelData);
    }
    #endregion
}
public class LevelData
{
    public int CurrentLevel;
    public List<LevelInfo> listLevel;
    public LevelData()
    {
        listLevel = new List<LevelInfo>();
        for (int i = 0; i < PlayerPrefsLevel.maxLevel; i++)
        {
            LevelInfo _info = new LevelInfo(i);
            listLevel.Add(_info);
        }
        listLevel[0].OnUnlock();
    }
    public LevelInfo GetLevelInfo(int _index)
    {
        return listLevel[_index];
    }
    public void FinishLevel(int star)
    {
        if (star > 0)
        {
            listLevel[CurrentLevel].OnFinish(star);
            if (CurrentLevel < (listLevel.Count - 1))
            {
                listLevel[CurrentLevel + 1].OnUnlock();
            }
            PlayerPrefsLevel.saveLevelData();
        }
    }
}
[System.Serializable]
public class LevelInfo
{
    public int LevelIndex;      //Level index, can be found in the build settings;
    public bool isUnlock;       //Is level unlocked? you can unlock it as default;
    public bool isFinished;     //Is level finished?
    public int starsCount;      //Stars count, zero is default, 3 is max in this case;	
    public LevelInfo() { }
    public LevelInfo(int _index)
    {
        LevelIndex = _index;
        isUnlock = false;
        isFinished = false;
        starsCount = 0;
    }
    public LevelInfo Clone()
    {
        LevelInfo _item = new LevelInfo();
        _item.LevelIndex = LevelIndex;
        _item.isUnlock = isUnlock;
        _item.isFinished = isFinished;
        _item.starsCount = starsCount;
        return _item;
    }
    public void OnUnlock()
    {
        if (isUnlock == false)
        {
            starsCount = 0;
            isFinished = false;
            isUnlock = true;
        }
    }
    public void OnFinish(int _star)
    {
        isFinished = true;
        isUnlock = true;
        if (starsCount < _star)
        {
            starsCount = _star;
            // Gain Cash
        }
    }
}