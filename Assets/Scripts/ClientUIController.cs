using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClientUIController : MonoBehaviour
{
    
    [SerializeField] private Button UpArrow; 
    [SerializeField] private Button DownArrow; 
    [SerializeField] private Button RightArrow; 
    [SerializeField] private Button LeftArrow;

    private float horizontalInput;
    private float vertiaclInput;

    private void Awake()
    {
        //gameObject.SetActive(Net.IsClient);

        UpArrow.onClick.AddListener(() => IncreaseValue(ref vertiaclInput));
        DownArrow.onClick.AddListener(() => DecreaseValue(ref vertiaclInput));
        RightArrow.onClick.AddListener(() => IncreaseValue(ref horizontalInput));
        LeftArrow.onClick.AddListener(() => DecreaseValue(ref horizontalInput));
    }

    private void IncreaseValue(ref float value)
    {
        value += 0.1f;
    }

    private void DecreaseValue(ref float value)
    {
        value -= 0.1f;
    }

    private void Update()
    {
        Debug.Log($"Horizontal: {horizontalInput}, Vertical: {vertiaclInput}");
    }
}
