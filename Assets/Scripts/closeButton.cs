using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        CardInfoManager.Instance.HideInfo();
    }
}
