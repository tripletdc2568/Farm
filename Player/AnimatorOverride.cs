using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AnimatorOverride 类用于根据所选物品动态更改角色的动画控制器
/// </summary>
public class AnimatorOverride : MonoBehaviour
{
    // 存储角色身上所有的 Animator 组件
    private Animator[] animators;
    // 用于显示当前拿在手中的物品的精灵（例如工具或物品图标）
    public SpriteRenderer holdItem;

    
    [Header("各部分动画列表")]
    // 存储不同部分动画的类型列表    
    public List<AnimatorType>animatorTypes;
    // 用于根据动画名称快速查找相应的 Animator    
    private Dictionary<string,Animator>animatorNameDict = new Dictionary<string, Animator>();

    /// <summary>
    /// 在对象被唤醒时（对象加载到场景中时）调用。 
    /// 获取角色身上的所有 Animator，并将它们存入字典中，便于后续查找。
    /// </summary>
    private void Awake() {
        // 获取该物体及其子物体上的所有 Animator 组件
        animators = GetComponentsInChildren<Animator>();

        // 将 Animator 的名称作为键，Animator 组件作为值存入字典，方便通过名称查找
        foreach(var anim in animators){
            animatorNameDict.Add(anim.name,anim);
        }
    }
    /// <summary>
    /// 当对象启用时调用，订阅 ItemSelectedEvent 事件。
    /// </summary>
    private void  OnEnable() {
        // 当选中物品时触发事件处理方法
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
    }
    /// <summary>
    /// 当对象禁用时调用，取消订阅 ItemSelectedEvent 事件。
    /// </summary>
    private void OnDisable() {
        // 取消订阅以避免内存泄漏
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;

    } 
    /// <summary>
    /// 当有物品被选中时触发的事件处理方法。根据选中的物品类型切换相应的动画。
    /// </summary>
    /// <param name="itemDetails">选中的物品的详细信息</param>
    /// <param name="isSelected">是否选中状态</param>
    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        //WORKFLOW:不同的工具返回不同的动画
        PartType currentType = itemDetails.itemType switch{
            ItemType.seed => PartType.Carry,// 种子类型物品显示"拿东西"的动画
            ItemType.Commodity =>PartType.Carry,// 商品类型物品也显示"拿东西"的动画
            _ => PartType.None// 其他类型物品不播放任何动画
        };

        // 如果物品未被选中，清空动画并隐藏手中的物品
        if(isSelected == false){
            currentType = PartType.None;// 不播放任何动画
            holdItem.enabled = false;// 隐藏手中的物品
        }else {
            // 如果选中的物品类型是需要手持的物品，更新 holdItem 的精灵图像
            if(currentType == PartType.Carry){
                holdItem.sprite = itemDetails.itemOnWorldSprite; // 将精灵图设置为物品对应的图像
                holdItem.enabled = true;// 显示手中的物品
            }
            
        }
        // 切换到相应的动画
        SwitchAnimator(currentType);
    }
    /// <summary>
    /// 切换角色的动画控制器以适应当前选中的物品
    /// </summary>
    /// <param name="partType">物品对应的身体部分动画类型</param>
    private void SwitchAnimator(PartType partType){
        // 遍历所有动画类型，根据身体部分类型切换动画
        foreach(var item in animatorTypes){
            if(item.partType == partType){
                // 根据部分名称找到对应的 Animator，并切换为新的动画控制器
                animatorNameDict[item.partName.ToString()].runtimeAnimatorController = item.animatorOverrideController;
            }
        }
    }
}
