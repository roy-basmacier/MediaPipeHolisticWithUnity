using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CalibrateButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button _button;

    public void OnClick()
    {
        Debug.Log("calibrating hand");
    }
}
