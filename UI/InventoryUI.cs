using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace MFarm.Inventory{
public class InventoryUI : MonoBehaviour
{   [Header("玩家背包UI")]
    [SerializeField] private GameObject bagUI;
    private bool bagOpened;
    [SerializeField] private SlotUI[] playerSlot;

    public Image dragItem;

    public ItemToolKit itemToolKit;

    private void OnEnable() {
        EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
    }
    private void OnDisable() {
        EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;

    }

        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch(location){
                case InventoryLocation.Player:
                for (int i = 0;i < playerSlot.Length; i++ ){
                    if(list[i].itemAmount >0){
                       var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                       playerSlot[i].UpdateSlot(item,list[i].itemAmount);
                    }else{
                        playerSlot[i].UpdateEmptySlot();
                    }
                }
                break;
            }
        }

        private void Start() {
    //给每个格子序号
    for (int i = 0;i < playerSlot.Length;i++){
        playerSlot[i].slotIndex = i;
        }
        bagOpened = bagUI.activeInHierarchy;
   }
   private void Update() {
    if (Input.GetKeyDown(KeyCode.Tab)){
        OpenBagUI();
    }
   }



    public void OpenBagUI(){
        bagOpened = !bagOpened;
        bagUI.SetActive(bagOpened);
    
    }
    /// <summary>
    /// 更新Slot高亮显示
    /// </summary>
    /// <param name="index">序号</param>
    public void UpdateSlotHightlight(int index){
        foreach (var slot in playerSlot){
            if(slot.isSelected && slot.slotIndex == index){
                slot.slotHightLight.gameObject.SetActive(true);
            }else{
                slot.isSelected = false;
                slot.slotHightLight.gameObject.SetActive(false);
            }
        }
    }


 
    }
    
}