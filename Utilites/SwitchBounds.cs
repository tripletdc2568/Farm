using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchBounds : MonoBehaviour
{
    //Todo:�л����������
    private void Start()
    {
        SwitchConfinerShape();
    }
    private void SwitchConfinerShape() {
        GameObject confinerGameObject = GameObject.FindGameObjectsWithTag("BoundsConfiner")[0];
        PolygonCollider2D confinerShape = confinerGameObject.GetComponent<PolygonCollider2D>();

        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();

        confiner.m_BoundingShape2D = confinerShape;

        //�л�����������֮ǰ�Ļ���
        confiner.InvalidatePathCache();

   }
}
