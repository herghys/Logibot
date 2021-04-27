using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DropdownHandler : MonoBehaviour
{
    private void Start()
    {
        var dropdown = GetComponent<TMP_Dropdown>();
        dropdown.options.Clear();

        DropdownItemSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    private void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
    }
}
