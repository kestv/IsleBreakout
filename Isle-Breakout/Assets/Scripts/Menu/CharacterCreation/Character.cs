using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField]int id;
    [SerializeField]Text name;
    [SerializeField]Text xp;
    [SerializeField]Text level;
    [SerializeField]Text time;
    [SerializeField]Text games;
    [SerializeField]Text newCharacter;
    [SerializeField]Button deleteButton;

    [SerializeField]GameObject list;
    [SerializeField]GameObject nameEnter;
    [SerializeField]PlayerData player;
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
            TimeSpan timeSpan = TimeSpan.FromSeconds(player.totalTimeSpent);
            time.text = string.Format("{0} h {1} min", timeSpan.Hours, timeSpan.Minutes - timeSpan.Hours * 60);
            games.text = player.totalGamesPlayed.ToString() + " games";
            deleteButton.gameObject.SetActive(true);
        }
        else
        {
            deleteButton.gameObject.SetActive(false);
            newCharacter.enabled = true;
            name.enabled = false;
            xp.enabled = false;
            time.enabled = false;
            games.enabled = false;
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
            player.totalTimeSpent = 0;
            player.totalGamesPlayed = 0;
            this.player = player;
            list.SetActive(false);
            nameEnter.SetActive(true);
            nameEnter.GetComponent<NameEnterController>().SetId(id);
        }
        else
        {
            player = DataSystem.Load(id);
            Player.id = player.id;
            Player.name = player.name;
            Player.level = player.level;
            Player.xp = player.xp;
            Player.totalGamesPlayed = player.totalGamesPlayed;
            Player.totalTimeSpent = player.totalTimeSpent;
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
            time.enabled = true;
            games.enabled = true;
            name.text = player.name;
            xp.text = player.xp + " xp";
            level.text = player.level + " lvl";
            TimeSpan timeSpan = TimeSpan.FromSeconds(player.totalTimeSpent);
            time.text = string.Format("{0} h {1} min", timeSpan.Hours, timeSpan.Minutes - timeSpan.Hours * 60);
            games.text = player.totalGamesPlayed.ToString() + " games";
        }
        else
        {
            deleteButton.gameObject.SetActive(false);
            newCharacter.enabled = true;
            name.enabled = false;
            xp.enabled = false;
            level.enabled = false;
            time.enabled = false;
            games.enabled = false;
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
