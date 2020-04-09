using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int id;
    public Text name;
    public Text xp;
    public Text level;
    public Text newCharacter;
    public Button deleteButton;

    public GameObject list;
    public GameObject nameEnter;
    PlayerData player;
    private void Awake()
    {
        
    }

    void Start()
    {
        player = DataSystem.Load(id);
        if(player != null)
        {
            newCharacter.enabled = false;
            name.text = player.name;
            xp.text = player.xp + " xp";
            level.text = player.level + " lvl";
            deleteButton.gameObject.SetActive(true);
        }
        else
        {
            deleteButton.gameObject.SetActive(false);
            newCharacter.enabled = true;
            name.enabled = false;
            xp.enabled = false;
            level.enabled = false;
        }
        CharacterCreationHandler.Instance.onNameEntered += SetName;
    }

    public void SelectCharacter()
    {
        if(player == null)
        {
            PlayerData player = new PlayerData();
            player.id = id;
            player.xp = 0;
            player.level = 1;
            this.player = player;
            list.SetActive(false);
            nameEnter.SetActive(true);
            nameEnter.GetComponent<ButtonController>().id = id;
        }
        else
        {
            player = DataSystem.Load(id);
            Player.id = player.id;
            Player.name = player.name;
            Player.level = player.level;
            Player.xp = player.xp;
        }
        DataSystem.Save(player);
        RefreshUI();
    }

    void RefreshUI()
    {
        player = DataSystem.Load(id);
        if (player != null)
        {
            deleteButton.gameObject.SetActive(true);
            newCharacter.enabled = false;
            name.enabled = true;
            xp.enabled = true;
            level.enabled = true;
            name.text = player.name;
            xp.text = player.xp + " xp";
            level.text = player.level + " lvl";
        }
        else
        {
            deleteButton.gameObject.SetActive(false);
            newCharacter.enabled = true;
            name.enabled = false;
            xp.enabled = false;
            level.enabled = false;
        }
    }

    public void DeleteCharacter()
    {
        Debug.Log(id);
        DataSystem.DeleteSave(id);
        
        RefreshUI();
    }

    public void SetName(string name, int id)
    {
        Debug.Log(name + " " + id);
        if (id == this.id)
        {
            player.name = name;
            DataSystem.Save(player);
            RefreshUI();
        }
    }
}
