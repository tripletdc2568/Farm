using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace MFarm.Inventory{
    [RequireComponent (typeof(SlotUI))]
    public class ShowItemTooltip : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler{
    private SlotUI  slotUI;
    private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

        private void Awake(){
        slotUI = GetComponent<SlotUI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
        {
            if(slotUI.itemAmount != 0){
                inventoryUI.itemToolKit.gameObject.SetActive(true);
                inventoryUI.itemToolKit.SetTooltip(slotUI.itemDetails,slotUI.slotType);

                inventoryUI.itemToolKit.GetComponent<RectTransform>().pivot = new Vector2(0.5f,0);
                inventoryUI.itemToolKit.transform.position = transform.position + Vector3.up * 60;
            }else{
                inventoryUI.itemToolKit.gameObject.SetActive(false);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            inventoryUI.itemToolKit.gameObject.SetActive(false);
        }

    }
}
