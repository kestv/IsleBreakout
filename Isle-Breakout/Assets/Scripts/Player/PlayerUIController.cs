using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerUIController : MonoBehaviourPun
{
    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    public GameObject PlayerUIPrefab;

    public CanvasController script;

    private void Start()
    {
        script = PlayerUIPrefab.GetComponent(typeof(CanvasController)) as CanvasController;

        if (PlayerUIPrefab != null)
        {
            GameObject _uiGo = Instantiate(PlayerUIPrefab);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }
    }

    [PunRPC]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            script.ToggleMessagePanel();
        }
    }
}
