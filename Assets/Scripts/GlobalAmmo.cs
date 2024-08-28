using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlobalAmmo : MonoBehaviour
{
    public static int CurrentAmmo;
    private int InternalAmmo;
    public TextMeshProUGUI AmmoDisplay;

    public static int LoadedAmmo = 0;
    private int InternalLoaded;
    public TextMeshProUGUI LoadedDisplay;

    private void Update()
    {
        InternalAmmo = CurrentAmmo;
        InternalLoaded = LoadedAmmo;
        AmmoDisplay.text = InternalAmmo.ToString();
        LoadedDisplay.text = InternalLoaded.ToString();
    }
}
