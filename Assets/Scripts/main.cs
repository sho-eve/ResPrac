using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{

    public Dropdown _dLevelValue;
    public static int _iLevelValue;
    public Image[] _iBlockRedMain;
    public Image[] _iBlockBlueMain;

    private float _fTimeLimit = 0.5f;
    private int _iCounter = 0;

    // Start is called before the first frame update
    void Start () {
        ResetBlock();
    }

    // Update is called once per frame
    void Update () {
        _iLevelValue = _dLevelValue.value;
        _fTimeLimit -= Time.deltaTime;

        if (_fTimeLimit <= 0.0) {
            _iBlockRedMain[_iCounter].gameObject.SetActive(true);
            _iBlockBlueMain[_iCounter].gameObject.SetActive(true);
            _iCounter++;
            _fTimeLimit = 0.5f;
        }

        if (_iCounter == 5) {
            ResetBlock();
            _iCounter = 0;
        }
    }

    public void StartButton () {
        SceneManager.LoadScene ("Game Scene");
    }

    private void ResetBlock () {
        for (int i = 0; i < 4; i++) {
            _iBlockRedMain[i].gameObject.SetActive(false);
            _iBlockBlueMain[i].gameObject.SetActive(false);
        }
    }
}
