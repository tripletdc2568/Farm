using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening.Plugins;


namespace MFarm.Inventory{
public class SlotUI : MonoBehaviour,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
   
    [Header("组件获取")]
    [SerializeField]private Image slotImage;// 格子中的物品图片
    [SerializeField]private TextMeshProUGUI amountText;// 物品数量文本
    [SerializeField]private Button button;// 与格子绑定的按钮
    [SerializeField] private GameObject itemPrefab; // 物品预制体，用于掉落物品

    [Header("格子类型")] 
    public Image slotHightLight;// 高亮效果
    public SlotType slotType;// 格子类型（例如背包、装备栏）
//物品信息
    public ItemDetails itemDetails;// 当前格子的物品详情
    public int itemAmount;// 当前物品的数量
    public bool isSelected; // 是否被选中
    public int slotIndex;// 当前格子的索引
    public GameObject player;// 玩家对象引用


// 获取 InventoryUI 脚本    
private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();
   private void Start()
        {
            isSelected = false;
            if (itemDetails.itemID == 0)
            {
                UpdateEmptySlot(); // 如果物品ID为0，更新为空格子
            }
        }
        private void Update()
        {
            // 监听 X 键的按下事件
            if (Input.GetKeyDown(KeyCode.X) && isSelected)
            {
                DropSelectedItem(); // 丢弃选中的物品
            }
        }
        /// <summary>
        /// 丢弃当前选中的物品
        /// </summary>
         private void DropSelectedItem()
    {
        // 确认物品详情是否有效，并且物品数量大于0
        if (itemDetails != null && itemDetails.canDropped && itemAmount > 0)
        {
            Vector3 playerPosition = player.transform.position;

            // 随机生成偏移量
            float offsetX = Random.Range(-2f, 2f);
            float offsetY = Random.Range(-2f, 2f);

            // 计算掉落位置
            Vector3 dropPosition = new Vector3(playerPosition.x + offsetX, playerPosition.y + offsetY, playerPosition.z);

            // 实例化物品的Prefab
            //GameObject itemPrefab = new GameObject(itemDetails.itemName); // 假设物品Prefab从物品名创建
            //itemPrefab.transform.position = dropPosition; // 将物品放置在玩家附近随机位置

            Debug.Log($"物品 {itemDetails.itemName} 掉落在: {dropPosition}");

            // 减少物品数量
            itemAmount--;
            
            
            InventoryItem currentItem = InventoryManager.Instance.inventoryBag_SO.itemList[slotIndex];
            // 更新背包，移除物品
            if (itemAmount == 0)
            {
                // 更新空槽
                currentItem.itemID = 0; // 清空这个槽位的itemID
                currentItem.itemAmount = 0;
                UpdateEmptySlot(); // 更新UI，显示为空
            }
            else
            {
                EventHandler.CallInstantiateItemInScene(itemDetails.itemID,dropPosition);
                // 如果还有物品数量，更新UI
                
                currentItem.itemAmount = itemAmount; // 修改获取到的元素的属性
                InventoryManager.Instance.inventoryBag_SO.itemList[slotIndex] = currentItem;
                amountText.text = itemAmount.ToString(); // 更新数量显示
            }
            // 通知UI更新背包
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, InventoryManager.Instance.inventoryBag_SO.itemList);
        //EventHandler.CallInstantiateItemInScene(itemDetails.itemID,dropPosition);
        }
        else
        {
            Debug.LogError("物品不能被丢弃，或者物品数量不足！");
        }
    }




/// <summary>
/// 将Slot更新为空
/// </summary>
    public void UpdateEmptySlot(){
        if(isSelected){
            isSelected = false;
        }
        slotImage.enabled = false;// 隐藏物品图片
        amountText.text = string.Empty;// 清空数量文本
        button.interactable = false;// 禁用按钮
    }
/// <summary>
/// 更新格子UI和信息
/// </summary>
/// <param name="item">ItemDetails</param>
/// <param name="amount">持有数量</param>
    public void UpdateSlot(ItemDetails item,int amount){
        itemDetails = item;
        slotImage.sprite = item.itemIcon;// 更新物品图标
        itemAmount = amount;// 更新数量
        amountText.text = amount.ToString();// 显示数量
        button.interactable = true;// 启用按钮
        slotImage.enabled = true;// 显示图片
    }
/// <summary>
/// 点击格子时触发
/// </summary>        
        public void OnPointerClick(PointerEventData eventData)
        {
            if(itemAmount == 0) return;// 如果格子为空，不执行后续操作
            isSelected = !isSelected;// 切换选中状态
            
            inventoryUI.UpdateSlotHightlight(slotIndex);// 更新高亮状态

            if(slotType == SlotType.Bag){
                //通知物品被选中的状态和信息
                EventHandler.CallItemSelectedEvent(itemDetails,isSelected);
            }
        }

/// <summary>
/// 拖动开始时触发
/// </summary>
        public void OnBeginDrag(PointerEventData eventData)
        {
            if(itemAmount != 0){ 
                // 激活整个拖拽的 GameObject
                inventoryUI.dragItem.gameObject.SetActive(true);// 启用拖动物体的 GameObject
                inventoryUI.dragItem.enabled = true;// 启用拖动物体的 Image 组件
                inventoryUI.dragItem.sprite = slotImage.sprite;// 设置拖动的物品图标
                inventoryUI.dragItem.SetNativeSize();// 设置图标为原始大小
                isSelected = true;
                inventoryUI.UpdateSlotHightlight(slotIndex);// 更新高亮状态
                }
           
        }
        /// <summary>
        /// 拖动结束时触发
        /// </summary>
        public void OnDrag(PointerEventData eventData)
        {
            inventoryUI.dragItem.transform.position = Input.mousePosition;// 拖动时更新物品图标的位置为鼠标位置
        }

        public void OnEndDrag(PointerEventData eventData)
        {
           inventoryUI.dragItem.enabled = false; // 隐藏拖动物品的图标
           //Debug.Log(eventData.pointerCurrentRaycast.gameObject);

           if (eventData.pointerCurrentRaycast.gameObject != null)
           {
            if(eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() == null)
            return;

            var targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
            int targetIndex = targetSlot.slotIndex;

            // 仅在背包中交换物品
            if(slotType == SlotType.Bag && targetSlot.slotType == SlotType.Bag)
            {
                InventoryManager.Instance.SwapItem(slotIndex,targetIndex);
            }
            inventoryUI.UpdateSlotHightlight(-1);// 清除高亮
           }else
           {
            var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, -Camera.main.transform.position.z));

            EventHandler.CallInstantiateItemInScene(itemDetails.itemID,pos);
           }
        }


    }
    }

