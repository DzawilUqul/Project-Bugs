using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    public static GameAssets i {
        get {
            if(_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    // ASSETS
    public Transform damagePopupPF;
    private Transform _uiCanvas;
    public Transform uiCanvas {
        get {
            if(_uiCanvas == null) _uiCanvas = GameObject.Find("UICanvas").transform;
            return _uiCanvas;
        }
    }
}
