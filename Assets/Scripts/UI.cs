using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Text fields
    public Text healthText, levelText, goldText, xpText, upgradeCostText, hpText;

    // Spirtes and XP-Bar
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    public void FixedUpdate()
    {
        hpText.text = GameManager.instance.Player.hitpoint.ToString();
    }

    // Selecting the character sprites (in the menu)
    public void OnArrowClick(bool next) // bool next tells if the next or the previous button was pressed
    {
        if (next)
        {
            currentCharacterSelection++;

            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChange();
        }
        else
        {
            currentCharacterSelection--;

            if (currentCharacterSelection < 0) // Because numbers under 0 are not valid for the array
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1; // Begins at the end of the array

            OnSelectionChange();
        }
    }

    private void OnSelectionChange()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection]; // Transforms the image characterSelectionSprite into a sprite from the playerSprite with the index of currentCharacterSelection
        GameManager.instance.Player.SwapSprite(currentCharacterSelection); // Change the sprite of the character
    }

    // Upgrading the weapon (in the menu)
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateUI();
    }
    
    public void UpdateUI()
    {

        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "Max";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        healthText.text = GameManager.instance.Player.hitpoint.ToString();
        goldText.text = GameManager.instance.gold.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        if (GameManager.instance.GetCurrentLevel() == GameManager.instance.xp.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + "total xp";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(GameManager.instance.GetCurrentLevel() - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(GameManager.instance.GetCurrentLevel());

            int difference = currLevelXp - prevLevelXp;
            int currXpOfLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpOfLevel / (float)difference;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpOfLevel.ToString() + " / " + difference;
        }
    }
}
