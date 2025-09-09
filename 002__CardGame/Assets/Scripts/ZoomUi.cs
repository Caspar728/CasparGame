using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public float zoomSize;

    public void OnPointerEnter(PointerEventData pointerEventData)//�̶���ʽ�����������Ч����������
    {
        transform.localScale = new Vector3(zoomSize, zoomSize, 1.0f);//���Ʊ����룺x�ᣬy�ᣬz��
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.localScale = Vector3.one;
    }
}