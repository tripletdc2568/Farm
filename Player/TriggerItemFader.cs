using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class TriggerItemFader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemFader1[] faders = other.GetComponentsInChildren<ItemFader1>();
        if(faders.Length > 0){
            foreach (var item in faders)
            {
                item.FadeOut();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        ItemFader1[] faders = other.GetComponentsInChildren<ItemFader1>();
        if(faders.Length > 0){
            foreach (var item in faders)
            {
                item.FadeIN();
            }
        }
    }
}
