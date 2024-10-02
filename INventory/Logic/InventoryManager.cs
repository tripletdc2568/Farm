using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFarm.Inventory
{
   public class InventoryManager : Singleton<InventoryManager>
{
   [Header("物品数据")]
   public ItemDataList_SO itemDataList_SO;
   [Header("背包数据")]
   public InventoryBag_SO inventoryBag_SO;
   private void Start() {
      EventHandler.CallUpdateInventoryUI(InventoryLocation.Player,inventoryBag_SO.itemList);
   }
   public ItemDetails GetItemDetails(int ID)
   {
      return itemDataList_SO.itemDataList.Find(i => i.itemID == ID);
   }
   /// <summary>
   /// 添加物品到Player背包里
   /// </summary> <summary>
   /// 
   /// </summary>
   public void AddItem(Item item,bool toDestory)
   {  //是否已经有该物品
      var index = GetItemIndexInBag(item.itemID);
      
      
      //背包是否有空位
         // if(!CheckBagCapacity())
         // {
         //    return;
         // }
      AddItemAtIndex(item.itemID,index,1);
      
      // InventoryItem newItem= new InventoryItem();
      // newItem.itemID = item.itemID;
      // newItem.itemAmount = 1;
      // inventoryBag_SO.itemList[0] = newItem;
      Debug.Log(GetItemDetails(item.itemID).itemID + "Name:" + GetItemDetails(item.itemID).itemName);
      if(toDestory)
      {
         Destroy(item.gameObject);
      }
      //更新UI
      EventHandler.CallUpdateInventoryUI(InventoryLocation.Player,inventoryBag_SO.itemList);
   }
   /// <summary>
   /// 检查背包是否有空位
   /// </summary>
   /// <returns></returns>
   private bool CheckBagCapacity(){
         for (int i = 0; i < inventoryBag_SO.itemList.Count; i++){
            if(inventoryBag_SO.itemList[i].itemID == 0)
            return true;
            }
            return false;
   }
   /// <summary>
   /// 通过物品ID找到背包已有的物品位置
   /// </summary>
   /// <param name="ID">物品ID</param>
   /// <returns>-1则没有这个物品否则返回序列号</returns>
   private int GetItemIndexInBag(int ID){
       for (int i = 0; i < inventoryBag_SO.itemList.Count; i++){
            if(inventoryBag_SO.itemList[i].itemID == ID)
            return i;
            }
            return -1;
   }
   /// <summary>
   /// 在指定背包序号位置添加物品
   /// </summary>
   /// <param name="ID">物品ID</param>
   /// <param name="index">序号</param>
   /// <param name="amount">数量</param>
   private void AddItemAtIndex(int ID,int index,int amount)
   {
      if (index == -1 && CheckBagCapacity())  //背包没有这个物品 同时背包有空位
      {
         var item = new InventoryItem {itemID = ID,itemAmount = amount};
         for (int i = 0; i < inventoryBag_SO.itemList.Count; i++){
            if(inventoryBag_SO.itemList[i].itemID == 0){
               inventoryBag_SO.itemList[i] = item;
               break;
            }
         }
            
      }
      else  //背包有这个物品
      {
         int currentAmount = inventoryBag_SO.itemList[index].itemAmount + amount;
         var item = new InventoryItem {itemID = ID,itemAmount = currentAmount};

         inventoryBag_SO.itemList[index] = item;         
      }
   }
   /// <summary>
///Player背包范围内交换物品
/// </summary>
/// <param name="fromIndex">起始序号</param>
/// <param name="targetIndex">目标数据序号</param>
    public void SwapItem(int fromIndex,int targetIndex){
        InventoryItem currentItem = inventoryBag_SO.itemList[fromIndex];

        InventoryItem targetItem = inventoryBag_SO.itemList[targetIndex];

        if(targetItem.itemID != 0){
            inventoryBag_SO.itemList[fromIndex] = targetItem;
            inventoryBag_SO.itemList[targetIndex] = currentItem;
        }else{
        
            inventoryBag_SO.itemList[targetIndex] = currentItem;
            inventoryBag_SO.itemList[fromIndex] = new InventoryItem();
        }
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Player,inventoryBag_SO.itemList);
    }
}

}


