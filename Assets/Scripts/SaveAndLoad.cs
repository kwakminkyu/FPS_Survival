using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPos;
    public Vector3 playerRot;

    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();
}

public class SaveAndLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private PlayerController playerController;
    private Inventory inventory;

    private void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        }

        playerController = FindAnyObjectByType<PlayerController>();
        inventory = FindAnyObjectByType<Inventory>();
    }

    public void SaveData()
    {
        saveData.playerPos = playerController.transform.position;
        saveData.playerRot = playerController.transform.eulerAngles;

        Slot[] slots = inventory.GetSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemNumber.Add(slots[i].itemCount);
            }
        }

        string json = JsonUtility.ToJson(saveData);
        
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("���� �Ϸ�");
        Debug.Log(json);
    }

    public void LoadData()
    {
        if (!File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            Debug.Log("������ �����ϴ�");
            return;
        }

        string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
        saveData = JsonUtility.FromJson<SaveData>(loadJson);

        playerController.transform.position = saveData.playerPos;
        playerController.transform.eulerAngles = saveData.playerRot;

        for (int i = 0; i < saveData.invenItemName.Count; i++)
        {
            inventory.LoadToInven(saveData.invenArrayNumber[i],
                                    saveData.invenItemName[i],
                                    saveData.invenItemNumber[i]);
        }
        Debug.Log("�ε� �Ϸ�");
    }
}
