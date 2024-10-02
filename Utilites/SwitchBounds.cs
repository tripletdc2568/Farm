using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchBounds : MonoBehaviour
{
    //Todo:切换场景后更改
    private void Start()
    {
        SwitchConfinerShape();
    }
    private void SwitchConfinerShape() {
        GameObject confinerGameObject = GameObject.FindGameObjectsWithTag("BoundsConfiner")[0];
        PolygonCollider2D confinerShape = confinerGameObject.GetComponent<PolygonCollider2D>();

        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();

        confiner.m_BoundingShape2D = confinerShape;

        //切换场景后，清理之前的缓存
        confiner.InvalidatePathCache();

   }
}
