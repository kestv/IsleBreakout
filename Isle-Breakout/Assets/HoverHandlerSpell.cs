using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverHandlerSpell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Info panel prefab")]
    [SerializeField] private GameObject spellInfoPanelPrefab;

    private SpellHolder spellHolder;
    private SpellInfo spell;    
    private SpellInfoPanelController panelCtrl;
    private GameObject infoPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        InitPanel(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TerminatePanel();
    }

    public void InitPanel(PointerEventData eventData)
    {
        spellHolder = GetComponent<SpellHolder>();
        spell = spellHolder.getSpell().GetComponent<SpellInfo>();

        if(spell != null)
        {
            infoPanel = Instantiate(spellInfoPanelPrefab, transform.root);
            panelCtrl = infoPanel.GetComponent<SpellInfoPanelController>();
            panelCtrl.InitPanel(spell);
        }

        panelCtrl.transform.position = new Vector2(eventData.position.x, eventData.position.y);
        Destroy(infoPanel, 1.5f);

    }

    public void TerminatePanel()
    {
        if (infoPanel != null)
        {
            Destroy(infoPanel);
        }
    }
}
