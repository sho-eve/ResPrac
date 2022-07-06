using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    GameObject mainCamera;

    public void OnClick() {
        mainCamera = Camera.main.gameObject;
        mainCamera.GetComponent<GameScene>().AnswerButton();
    }
}
