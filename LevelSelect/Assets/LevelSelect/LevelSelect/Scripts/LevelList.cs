using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelList : MonoBehaviour
{
    public Image LevelObject;           //Level object renderer in the scene (assign from hierarchy panel);
    public Text levelText;
    public Image Stats;             //Level stats renderer in the scene (assign from hierarchy panel);
    public LevelInfo info;

    public LevelSettings levelSettings = new LevelSettings(); //Level settings class, contain sprites to replace the default ones;

    public void SetInfo(LevelInfo _info)
    {
        info = _info.Clone();
        levelText.gameObject.SetActive(levelSettings.showTextLV);
        levelText.text = "" + (info.LevelIndex + 1);
        if (info.isFinished)
        {
            LevelObject.sprite = levelSettings.iconLevelFinish;
            Stats.gameObject.SetActive(true);
            if (info.starsCount == 1) Stats.sprite = levelSettings.iconOneStar;
            if (info.starsCount == 2) Stats.sprite = levelSettings.iconTwoStars;
            if (info.starsCount == 3) Stats.sprite = levelSettings.iconThreeStars;
        }
        else if (info.isUnlock)
        {
            LevelObject.sprite = levelSettings.iconLevelUnlocked;
            Stats.gameObject.SetActive(false);
        }
        else
        {
            LevelObject.sprite = levelSettings.iconLevelLocked;
            Stats.gameObject.SetActive(false);
            levelText.gameObject.SetActive(false);
        }
    }

    public void OnClickButton()
    {
        LevelSelectionLogic.Instance.OnClickLevel(this);
    }

    public void Highlight(bool _value)
    {
        if (_value)
        {
            gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        else
        {
            gameObject.transform.localScale = Vector3.one;
        }
    }
}

//NOTE: assign this sprites from the project panel;
[System.Serializable]
public class LevelSettings
{
    public bool showTextLV;
    public Sprite iconLevelFinish;              //Unlocked level sprite;
    public Sprite iconLevelUnlocked;                //Unlocked level sprite;
    public Sprite iconLevelLocked;                  //Locked level sprite;
    public Sprite iconOneStar;
    public Sprite iconTwoStars;
    public Sprite iconThreeStars;
}