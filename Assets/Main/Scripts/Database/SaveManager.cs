using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    private class saveObject
    {
        public Vector3 Position;
        public int Gold;
        public int Experience;
        public int Difficulty;
        public string name;
        public List<int> Itemamounts;

    }
    public PlayerManager pm;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SaveSystem.Init();
    }

    public void SaveGame()
    {
        pm = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>();
        saveObject saveObj = new saveObject
        {
            Position = pm.transform.position,
            Gold = pm.gold,
            Experience = pm.experience,
            Difficulty = PlayerPrefs.GetInt(PrefNames.difficulty),
            name = PlayerPrefs.GetString(PrefNames.playerName),
            Itemamounts = new List<int>(pm.inventory.Count)
        };
        foreach(inventorySlotProxy proxy in pm.inventory)
        {
            saveObj.Itemamounts.Add(proxy.itemAmount);
        }
        string json = JsonUtility.ToJson(saveObj);
        SaveSystem.Save(json);
    }

    public void LoadGame()
    {
        saveObject loadedSave = JsonUtility.FromJson<saveObject>(SaveSystem.Load());
        PlayerPrefs.SetInt(PrefNames.difficulty, loadedSave.Difficulty);
        PlayerPrefs.SetString(PrefNames.playerName, loadedSave.name);
        Debug.Log(loadedSave.Experience);
        StartCoroutine(LoadingValues(loadedSave));
    }

    IEnumerator LoadingValues(saveObject loadedSave)
    {
        while(SceneManager.GetActiveScene().buildIndex !=1)
        {
            Debug.Log("Wait");
            yield return new WaitForSecondsRealtime(0.5f);
        }
        Debug.Log("Load Value");
        pm=GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>();
        pm.gold = loadedSave.Gold;
        pm.experience = loadedSave.Experience;
        pm.gameObject.transform.position = loadedSave.Position;
        for(int i = 0; i < loadedSave.Itemamounts.Count; i++)
        {
            pm.inventory[i].itemAmount = loadedSave.Itemamounts[i];
        }
        pm.UpdateUI();

    }
}
