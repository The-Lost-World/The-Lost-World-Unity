﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Show_Item_Cooking_Item_035 : MonoBehaviour
{
    //SCRIPTU ASTA E PUS PE Cooking_SHOW_ITEM (ala de sub casutele de cookibg) SI SE APLICA LA CASUTELE ALEA 4 LA CARE ARATA CE ITIT TREBUIE PT COOKING
    [SerializeField]
    private GameObject[] requiredItemSlot;     //sloturile alea mici
    private float thisItemQuantity;
    [SerializeField]
    private Color enoughtResourceColor;
    [SerializeField]
    private Color NotenoughtResourceColor;

    void Start()
    {
        transform.Find("Item_Image").gameObject.SetActive(false);
        for (int i = 1; i < requiredItemSlot.Length; i++)
            requiredItemSlot[i].transform.Find("Item_Image").gameObject.SetActive(false);
    }


    void Update()
    {
        if (FindObjectOfType<Inventory>().itemCodeHovered > 0)
        {
            ////////////////am bagat asta si aci asta de la else ca aparea un bug de aparea la nail in loc de 1 slot cu required aparea si al doilea cu al doilea required de la ultimu de am dat hover nush dc facea asa

            transform.Find("Item_Image").gameObject.SetActive(false);
            transform.Find("Item_Name").gameObject.SetActive(false);
            transform.Find("Item_Description").gameObject.SetActive(false);

            for (int i = 1; i < requiredItemSlot.Length; i++)
            {
                requiredItemSlot[i].transform.Find("Item_Image").gameObject.SetActive(false);

                requiredItemSlot[i].transform.Find("Quantity_Required").gameObject.SetActive(false);
                requiredItemSlot[i].transform.Find("Bar").gameObject.SetActive(false);
                requiredItemSlot[i].transform.Find("Quantity_Have").gameObject.SetActive(false);
            }

            /////// 

            transform.Find("Item_Image").gameObject.SetActive(true);
            transform.Find("Item_Image").GetComponent<Image>().sprite = FindObjectOfType<List_Of_Items>().Inventory_Sprite[FindObjectOfType<Inventory>().itemCodeHovered];

            transform.Find("Item_Name").gameObject.SetActive(true);
            transform.Find("Item_Name").GetComponent<TextMeshProUGUI>().text = FindObjectOfType<List_Of_Items>().Item_Name[FindObjectOfType<Inventory>().itemCodeHovered];

            transform.Find("Item_Description").gameObject.SetActive(true);
            transform.Find("Item_Description").GetComponent<TextMeshProUGUI>().text = FindObjectOfType<List_Of_Items>().Item_Description[FindObjectOfType<Inventory>().itemCodeHovered];

            for (int i = 1; i < FindObjectOfType<Inventory>().craftingSlotHovered.GetComponent<Item_035_Cooking_Slots>().itemCode.Length; i++)
            {
                requiredItemSlot[i].transform.Find("Item_Image").GetComponent<Image>().sprite = FindObjectOfType<List_Of_Items>().Inventory_Sprite[FindObjectOfType<Inventory>().craftingSlotHovered.GetComponent<Item_035_Cooking_Slots>().itemCode[i]];
                requiredItemSlot[i].transform.Find("Item_Image").gameObject.SetActive(true);

                thisItemQuantity = 0;
                for (int j = 1; j <= 5; j++)
                    if (FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_code[j] == FindObjectOfType<Inventory>().craftingSlotHovered.GetComponent<Item_035_Cooking_Slots>().itemCode[i])
                        thisItemQuantity += FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j];
                

                requiredItemSlot[i].transform.Find("Quantity_Required").gameObject.SetActive(true);
                requiredItemSlot[i].transform.Find("Bar").gameObject.SetActive(true);
                requiredItemSlot[i].transform.Find("Quantity_Have").gameObject.SetActive(true);

                requiredItemSlot[i].transform.Find("Quantity_Required").GetComponent<TextMeshProUGUI>().text = FindObjectOfType<Inventory>().craftingSlotHovered.GetComponent<Item_035_Cooking_Slots>().itemQuantity[i].ToString();
                requiredItemSlot[i].transform.Find("Quantity_Have").GetComponent<TextMeshProUGUI>().text = thisItemQuantity.ToString();
                
                if (thisItemQuantity >= FindObjectOfType<Inventory>().craftingSlotHovered.GetComponent<Item_035_Cooking_Slots>().itemQuantity[i])
                    requiredItemSlot[i].transform.Find("Quantity_Have").GetComponent<TextMeshProUGUI>().color = enoughtResourceColor;
                else
                    requiredItemSlot[i].transform.Find("Quantity_Have").GetComponent<TextMeshProUGUI>().color = NotenoughtResourceColor;
            }
        }
        else
        {
            transform.Find("Item_Image").gameObject.SetActive(false);
            transform.Find("Item_Name").gameObject.SetActive(false);
            transform.Find("Item_Description").gameObject.SetActive(false);

            for (int i = 1; i < requiredItemSlot.Length; i++)
            {
                requiredItemSlot[i].transform.Find("Item_Image").gameObject.SetActive(false);

                requiredItemSlot[i].transform.Find("Quantity_Required").gameObject.SetActive(false);
                requiredItemSlot[i].transform.Find("Bar").gameObject.SetActive(false);
                requiredItemSlot[i].transform.Find("Quantity_Have").gameObject.SetActive(false);
            }
        }
    }



    void ItemQuantityInInventory()
    {

    }
}
