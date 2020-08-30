﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place_Prefab : MonoBehaviour
{
    [SerializeField]
    public Transform playerPos;           //pt bridge ca daca e pe pod sa sas puna la nnivelu podului chiar daca e pe insula
    [SerializeField]
    private LayerMask other_Preab_Mask;       // numa bridge ca sa vada daca playeru e pe bridge

    public GameObject Prefab_In_Hand;
    [SerializeField]
    public GameObject Building_Spawn_Position;
    [SerializeField]
    private LayerMask Floor_placeable_Surface_Mask;      //pt pe care poate ffi pusa floor asta nu poate ffi pusa pe alt floor ; masca asta inseamna layerele pe care POATE FI PUS LORU ADICA SA NU AIBA COLIZIUNE CU ASTEA
    [SerializeField]
    public LayerMask Bridge_placeable_Surface_Mask;     //e ca la floor
  
    //[SerializeField]
    //private LayerMask Floor_Mask;      //ala cu layeru de floor ca sa poate ffface snap apte fflooruri sau walluri

    public bool isSnapped;


    //pt colorplacing la bridge ca nu pot atribui pe script
    public LayerMask bridgeMask;     // pt colorplacing la bridge
    [SerializeField]
    public LayerMask groundedBUTnotbridgeMak;   //layerurile pe care daca sta e grounded CU EXCEPTIA podului adica layerul OTHER PREFAB
    //


    [SerializeField]
    private GameObject Item_008;    //wooden floor 
    [SerializeField]
    private GameObject Item_014;

    [SerializeField]
    private bool asdadad;

    void Start()
    {
        
    }

    
    void Update()
    {      

        if (FindObjectOfType<Handing_Item>().handing_placeable == false)      //daca nu e cond asta ai mai multe iteme in mana in ac timp
            PrefabSpawn();

                   

        if (FindObjectOfType<Handing_Item>().handing_placeable == true)  //aici il muta
        {
            SnapDetach();     // e snapeed si te departezi sa nu mai ffie snapped si sa fie din nou dupa tine

            if (isSnapped == false)   // daca e snapped sa nu l mai poti muta(oricum nu puteai pe x,z ca i scoteai parentu dar se schimba pe y
            {
                RaycastHit hit;

                if (FindObjectOfType<Handing_Item>().SelectedItemCode == 8)     //floor wood
                {
                    if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Floor_placeable_Surface_Mask))
                        Prefab_In_Hand.transform.position = new Vector3(Prefab_In_Hand.transform.position.x, hit.point.y, Prefab_In_Hand.transform.position.z);
                }
                else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 14)     //bridge
                {
                    RaycastHit hit2;
                    asdadad = Physics.Raycast(playerPos.position, -transform.up, out hit2, 5f, other_Preab_Mask);
                    if (Physics.Raycast(playerPos.position, -transform.up, out hit2, 5f, other_Preab_Mask))            //e pe pod ; situatia asta e daca e in aer merge si fara da ar trebui ca prima buc din pod sa fie la marginea insulei si daca vrei sa faci design nu prea merge
                    {
                        if(hit2.collider.tag == "Bridge")
                        Prefab_In_Hand.transform.position = new Vector3(Prefab_In_Hand.transform.position.x, Building_Spawn_Position.transform.position.y - 4f, Prefab_In_Hand.transform.position.z);
                    }
                    else if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Bridge_placeable_Surface_Mask))
                        Prefab_In_Hand.transform.position = new Vector3(Prefab_In_Hand.transform.position.x, hit.point.y, Prefab_In_Hand.transform.position.z);
                  //  else //insemna ca e in aer si fiind pod sa se poata prinde de alt pod
                  //      Prefab_In_Hand.transform.position = new Vector3(Prefab_In_Hand.transform.position.x, Building_Spawn_Position.transform.position.y - 4f, Prefab_In_Hand.transform.position.z);
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && Prefab_In_Hand.transform.GetChild(0).GetComponent<ColorPlacingChange>().placeable == true)
            {
                PlacePrefab();
            }

        }
    }



    void PrefabSpawn()     //cand o ai in inventar si selectezi slotu sa apara buildingu si sa l poti muta
    {
        if (FindObjectOfType<Handing_Item>().SelectedItemCode == 8) // place wooden_floor
        {
            RaycastHit hit;
            if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Floor_placeable_Surface_Mask))
            {
                FindObjectOfType<Handing_Item>().handing_placeable = true;
                Prefab_In_Hand = Instantiate(Item_008, hit.point, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }

            Prefab_In_Hand.transform.SetParent(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform);
            Prefab_In_Hand.transform.localRotation = Quaternion.identity;
            Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<Snap_Floor>();

        }
        else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 14) // place bridge
        {
            RaycastHit hit;
            if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Bridge_placeable_Surface_Mask))
            {
                FindObjectOfType<Handing_Item>().handing_placeable = true;
                Prefab_In_Hand = Instantiate(Item_014, hit.point, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }
            else         // nu gaseste cv sub inseamna ca nu e pe insulea da sa poata ace snap cu alt pod in aer
            {
                FindObjectOfType<Handing_Item>().handing_placeable = true;
                Vector3 pos = new Vector3(Building_Spawn_Position.transform.position.x, Building_Spawn_Position.transform.position.y - 4f, Building_Spawn_Position.transform.position.z);
                Prefab_In_Hand = Instantiate(Item_014, pos, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }

            Prefab_In_Hand.transform.SetParent(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform);
            Prefab_In_Hand.transform.localRotation = Quaternion.identity;
            Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<Snap_Bridge>();

        }
    }


    void PlacePrefab()   // ui ai handing si apesi sa ramana pe pozitie
    {
        FindObjectOfType<Handing_Item>().handing_placeable = false;

        if (FindObjectOfType<Handing_Item>().SelectedItemCode == 8)  //wooden floor
            Instantiate(Item_008, Prefab_In_Hand.transform.position, Quaternion.Euler(Prefab_In_Hand.transform.rotation.x, Prefab_In_Hand.transform.eulerAngles.y, Prefab_In_Hand.transform.rotation.z));
        else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 14)  //bridge
        {
            if((Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, 10f, Bridge_placeable_Surface_Mask)) == true || isSnapped == true)       //ori e pe cv insula ori daca nu sa ie snapped de alt pod ca sa nu poti pune in aer
               Instantiate(Item_014, Prefab_In_Hand.transform.position, Quaternion.Euler(Prefab_In_Hand.transform.rotation.x, Prefab_In_Hand.transform.eulerAngles.y, Prefab_In_Hand.transform.rotation.z));
        }



        Destroy(Prefab_In_Hand.gameObject);

        FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15]--;   //ai plasat cladirea o scoate din inventar
        if (FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] == 0)
        {
            FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] = 0;
            FindObjectOfType<Handing_Item>().SelectedItemCode = 0;
        }

    }

    void SnapDetach()      // e snapeed si te departezi sa nu mai ffie snapped si sa fie din nou dupa tine
    {
        if (isSnapped == true && Vector3.Distance(Prefab_In_Hand.transform.position, FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position) > 5) //e snapped de alt floor si playeru s a departat prea mult si tre sa revina sa poate muta playeru
        {
            isSnapped = false;
            if (FindObjectOfType<Handing_Item>().SelectedItemCode == 8)
            {
                RaycastHit hit;
                if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Floor_placeable_Surface_Mask))
                {
                    Prefab_In_Hand.transform.position = hit.point;
                }
            }
            else if(FindObjectOfType<Handing_Item>().SelectedItemCode == 14)
            {
                RaycastHit hit;
                if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Bridge_placeable_Surface_Mask))
                {
                    Prefab_In_Hand.transform.position = hit.point;
                }
                else
                    Prefab_In_Hand.transform.position = new Vector3(Building_Spawn_Position.transform.position.x, Building_Spawn_Position.transform.position.y - 4f, Building_Spawn_Position.transform.position.z);
            }

           Prefab_In_Hand.transform.SetParent(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform);
           Prefab_In_Hand.transform.localRotation = Quaternion.identity;
            
            
       }
    }


    
}
