using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimatorOverride : MonoBehaviour
{
    private Animator[] animators;

    public SpriteRenderer holdItem;

    
    [Header("各部分动画列表")]
    public List<AnimatorType>animatorTypes;
    
    private Dictionary<string,Animator>animatorNameDict = new Dictionary<string, Animator>();

    private void Awake() {
        animators = GetComponentsInChildren<Animator>();

        foreach(var anim in animators){
            animatorNameDict.Add(anim.name,anim);
        }
    }

    private void  OnEnable() {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
    }

   

    private void OnDisable() {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;

    } 
    private void OnItemSelectedEvent(ItemDetails itemDetailsdetails, bool isSelected)
    {
        //WORKFLOW:不同的工具返回不同的动画
        PartType currentType = itemDetailsdetails.itemType switch{
            ItemType.seed => PartType.Carry,
            ItemType.Commodity =>PartType.Carry,
            _ => PartType.None
        };
    }

    private void SwitchAnimator(PartType partType){
        
    }
}
