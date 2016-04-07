using UnityEngine;
using System.Collections.Generic;

public class ManaHelper : MonoBehaviour {

    public GameObject manaIcon;
    public List<GameObject> manaIcons = new List<GameObject>();

    public int mana = 1;
    public int miniMana = 0;

    void Start()
    {
        for(int i = 0; i < mana; i++)
        {
            GameObject tempMana = (GameObject)Instantiate(manaIcon);
            tempMana.transform.SetParent(transform);
            manaIcons.Add(tempMana);
        }
    }

    public void AddMiniMana()
    {
        if(miniMana <= 8)
            miniMana++;
        else if(mana < 6)
        {
            mana++;
            miniMana = 0;
            GameObject tempMana = (GameObject)Instantiate(manaIcon);
            tempMana.transform.SetParent(transform);
            manaIcons.Add(tempMana);
        }
    }

    public bool UseMana()
    {
        if(manaIcons.Count > 0)
        {
            Destroy(manaIcons[0]);
            manaIcons.Remove(manaIcons[0]);
            return true;
        }

        return false;
    }
}
