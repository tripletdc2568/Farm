using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MFarm.Inventory{
public class Item : MonoBehaviour
{
    public int itemID;

    private SpriteRenderer SpriteRenderer;

    public ItemDetails itemDetails;
    private BoxCollider2D coll;

    private void Start() {
        if(itemID != 0){
            Init(itemID);
        }
    }

    private void Awake(){
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }
    public void Init(int ID){
        itemID = ID;

        //Inventory获取当前数据
        itemDetails = InventoryManager.Instance.GetItemDetails(itemID);

        if(itemDetails != null){
            SpriteRenderer.sprite = itemDetails.itemOnWorldSprite !=null? itemDetails.itemOnWorldSprite : itemDetails.itemIcon;
        }
        //修改碰撞体尺寸
        Vector2 newSize = new Vector2(SpriteRenderer.sprite.bounds.size.x,SpriteRenderer.sprite.bounds.size.y);
        coll.size = newSize;
        coll.offset = new Vector2(0,SpriteRenderer.sprite.bounds.center.y);

    }
}
}
