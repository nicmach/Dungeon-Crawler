using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // public static means you can access it from everywhere else in the code
    private void Awake() // Awake() means this function is calles when a Scene loads, or when an previously inactive GameObject is set to active, or after a GameObjhect created with Object.Instantiate is initialized.
    {
        if (GameManager.instance != null) // This prevents there to be two GameManagers at the same time. If, for example the first scene is loaded again (in which there already is a GameManager), it will destroy that on, so that only one GameManager exists at a time.
        {
            Destroy(gameObject);                       
            Destroy(Player.gameObject);                 // Without these lines every time we load into a level again, where a player figure is already placed, we're gonna have multiple Players and FloatingTextManagers 
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(ui);
            return;
        }

        // PlayerPrefs.DeleteAll(); - Can be used to destroy loading state

        instance = this;
        SceneManager.sceneLoaded += Load; // Calls all the functions in load, if the event (i.e. a new scene is loaded) occurs
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    // Resources needed
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> xp;
    public List<int> weaponPrices;

    // References for scripts etc.
    public Player Player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform HpBar;
    public GameObject ui;
    public GameObject hud;
    public Animator DeathWindow;


    // Logic for gold, health, mana etc. 
    public int gold;
    public int experience;

    public int Weapons;

    // Floating text
    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(message, fontSize, color, position, motion, duration); // Allows to call the Show function from everywhere in the code easely with GameManager.instance.ShowText()
    }

    // Weapon Upgrade
    public bool TryUpgradeWeapon()
    {
        if (weaponPrices.Count <= weapon.weaponLevel) // If your weapon is a higher or equal level than/ to the number of weaponPrices (i.e. possible upgrades) you cant buy another upgrade
            return false;
        if (gold >= weaponPrices[weapon.weaponLevel])
        {
            gold -= weaponPrices[weapon.weaponLevel]; // Subtracts the cost of the upgrade
            weapon.UpgradeWeapon(); // Does the upgrade (i.e. increases damage, changes sprite etc.)
            return true;
        }

        return false; // Returns false if the weapon is not maxed out but you do bot have enough money
    }

    // Update Experience

    public int GetCurrentLevel()
    {
        int XpLevel = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xp[XpLevel];
            XpLevel++;

            if (XpLevel == xp.Count)
                return XpLevel;
        }

        return XpLevel;
    }

    public int GetXpToLevel(int level)
    {
        int XpLevel = 0;
        int experience = 0;

        while (XpLevel < level)
        {
            experience += xp[XpLevel];
            XpLevel++;
        }

        return experience;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel(); // Get the current level before adding the xp
        experience += xp;
        if (currLevel < GetCurrentLevel()) // if the level increased call OnLevelUp()
            OnLevelUp(); 
    }

    public void OnLevelUp()
    {
        Player.OnLevelUp();
    }

    public void Respawn()
    {
        DeathWindow.SetTrigger("Hidden");
        UnityEngine.SceneManagement.SceneManager.LoadScene("PlayerBase");
        Player.Respawn();
    }

    // Update HpBar 
    public void OnHitpointChange()
    {
        float ratio = (float)Player.hitpoint / (float)Player.maxHitpoint;
        HpBar.localScale = new Vector3(ratio, 1, 1);
    }

    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        Player.transform.position = GameObject.Find("SpawnPoint").transform.position; // Teleport the player to the set spwanpoint on loading the game
    }
    // Loading and saving the game
    public void Save()
    {
        Debug.Log("Game saved");
        string s = "";

        // Saving the current variables into s 

        s += "Skin" + "|";
        s += gold.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void Load(Scene s, LoadSceneMode mode)
    {
        // If there is no saved state don't try to load one

        SceneManager.sceneLoaded -= Load;

        if (!PlayerPrefs.HasKey("SaveState")) 
            return;

        // Loading the saved variables into data

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        
        gold = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
            Player.SetLevel(GetCurrentLevel());

        weapon.SetWeaponLevel(int.Parse(data[3]));

        Debug.Log("Game loaded");
    }
}