using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private int m_Selection;
    public List<Item> m_Items;
    public List<Item> m_Inventory;
    [SerializeField] private List<GameObject> m_PlaceHolders;
    [SerializeField] private List<GameObject> m_Highlighters;
    [SerializeField] private GameObject m_ItemImage;

    // Start is called before the first frame update
    void Start()
    {
        m_Inventory = new List<Item>();
        m_Selection = -1;
        foreach (var img in m_Highlighters)
        {
            img.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (Input.mouseScrollDelta.y < 0)
            {
                m_Selection--;
            }
            else
            {
                m_Selection++;
            }

            if (m_Selection > 4)
            {
                m_Selection = 0;
            }
            else if (m_Selection < 0)
            {
                m_Selection = 4;
            }

            UpdatePlaceHolder();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (m_ItemImage.activeSelf)
            {
                m_ItemImage.GetComponent<Image>().sprite = null;
                m_ItemImage.SetActive(false);
            }
            else
            {
                if (GetItemInHand() != "")
                {
                    m_ItemImage.GetComponent<Image>().sprite = m_Inventory[m_Selection].image;
                    m_ItemImage.SetActive(true);
                }
            }
        }
    }

    public void AddToInventory(int _index)
    {
        m_Inventory.Add(m_Items[_index]);
        m_PlaceHolders[m_Inventory.Count - 1].GetComponent<Image>().color = Color.white;
        m_PlaceHolders[m_Inventory.Count - 1].GetComponent<Image>().sprite = m_Items[_index].image;
        m_Selection = m_Inventory.Count - 1;
        UpdatePlaceHolder();
    }

    public void UpdatePlaceHolder()
    {
        for (int i = 0; i < m_PlaceHolders.Count; i++)
        {
            if (i == m_Selection)
            {
                m_Highlighters[i].SetActive(true);
            }
            else
            {
                m_Highlighters[i].SetActive(false);
            }
        }
    }

    public string GetItemInHand()
    {
        if (m_Inventory.Count > m_Selection && m_Selection != -1)
        {
            return m_Inventory[m_Selection].name;
        }

        return "";
    }
}