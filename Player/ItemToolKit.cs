using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MFarm.Inventory;
public class ItemToolKit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Text valueText;
    [SerializeField] private GameObject bottomPart;

    public void SetTooltip(ItemDetails itemDetails,SlotType slotType){
        nameText.text = "名字 <color=#F857C6>" + itemDetails.itemName;
        typeText.text = "类型 <color=#F857C6>" + GetitemType(itemDetails.itemType);
        descriptionText.text = itemDetails.itemDescription;

        if(itemDetails.itemType == ItemType.seed  || itemDetails.itemType == ItemType.Commodity || itemDetails.itemType == ItemType.Furniture){
            bottomPart.SetActive(true);

            var price = itemDetails.itemPrice;
            if(slotType == SlotType.Bag){
                price = (int)(price * itemDetails.sellpercentage);
            }

            valueText.text = price.ToString();
        }else{
            bottomPart.SetActive(false);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    private string GetitemType(ItemType itemType){
        return itemType switch
        {
        ItemType.seed => "种子",
        ItemType.Commodity => "商品",
        ItemType.Collections => "商品",
        ItemType.Furniture => "家具",
        ItemType.BreakTool => "工具",
        ItemType.HoeTool => "工具",
        ItemType.ChopTool => "工具",
        _ => "无"
        
        };
    }
}
