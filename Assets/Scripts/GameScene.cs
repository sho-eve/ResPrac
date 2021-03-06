using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour {

    public Image[] _iBlockRed;
    public Image[] _iBlockBlue;
    public InputField _iQuestionField;
    public InputField _iAnswerField;
    public Text _tAnswerCounterText,_tMakeCountTimerText;
    public Text[] _tSaveValues;

    private int _iCheckNumberBlockRed = 1, _iCheckNumberBlockBlue = 1;
    private float _fWorldTime, _fTimeLimit = 0;
    private float _fResultNumber, _fModValue, _fBaseNumber, _fMakeTimeValue;
    private int _iSaveCount;
    private float _fMakeCountTimer = 0;

    // Use this for initialization
    void Start () {
        _fBaseNumber = Mathf.Round(Random.Range(0f, 10f));
        _iSaveCount = 0;

        if (main._iLevelValue == 0) { _fWorldTime = 15;}
        else if (main._iLevelValue == 1) { _fWorldTime = 7;}
        else if (main._iLevelValue == 2) { _fWorldTime = 1;}
        else { _fWorldTime = 100;}

        _fTimeLimit = _fWorldTime;

        for (int i = 1;i < 6; i++){
            _iBlockRed[i].gameObject.SetActive(false);
            _iBlockBlue[i].gameObject.SetActive(false);
        }

        ResetAnswerField();
        MakeQuestion();
    }
	
	// Update is called once per frame
	void Update () {
        _fMakeCountTimer += Time.deltaTime;

        if (_fWorldTime != 100) {
            _fTimeLimit -= Time.deltaTime;

            if (_fTimeLimit <= 0.0) {
                _iBlockRed[_iCheckNumberBlockRed].gameObject.SetActive(true);
                _iCheckNumberBlockRed++;
                _fTimeLimit = _fWorldTime;
            }
        }

        if (_fModValue == _fResultNumber){
            MakeQuestion();
        }

        _tAnswerCounterText.text = "求める値:" + _fResultNumber;
        _tMakeCountTimerText.text = "生成時間:" + Mathf.Round(_fMakeCountTimer);

        if (_iCheckNumberBlockRed == 6){SceneManager.LoadScene("Lose Scene");}
        else if (_iCheckNumberBlockBlue == 6){SceneManager.LoadScene("Win Scene");}
    }

    private void MakeQuestion()
    {
        _fModValue = Mathf.Round(Random.Range(1f, 10f));
        _fResultNumber = Mathf.Round(Random.Range(1f, 10f));

        while (_fModValue  < _fResultNumber){
            _fResultNumber = Mathf.Round(Random.Range(1f, 10f));
        }

        _iQuestionField.text = "("+ _fBaseNumber + "+?" + "," + _fModValue + ")";
    }

    private void ResetAnswerField()
    {
        _iAnswerField.text = "";
    }

    public void AnswerButton()
    {
        if (_fResultNumber == Hash1(_fBaseNumber + float.Parse(_iAnswerField.text), _fModValue)){
            _fMakeTimeValue = Mathf.Round(_fMakeCountTimer);
            _fMakeCountTimer = 0;

            _iBlockBlue[_iCheckNumberBlockBlue].gameObject.SetActive(true);
            _iCheckNumberBlockBlue++;

            _tSaveValues[_iSaveCount++].text = _fBaseNumber.ToString();
            _tSaveValues[_iSaveCount++].text = _iAnswerField.text;
            _tSaveValues[_iSaveCount++].text = _fMakeTimeValue.ToString();
            _tSaveValues[_iSaveCount++].text = _fModValue.ToString();

            _fBaseNumber = Hash2(_fBaseNumber, _fMakeTimeValue, float.Parse(_iAnswerField.text), _fModValue);

            ResetAnswerField();
            MakeQuestion();
        }

        ResetAnswerField();
    }

    private float Hash1(float BANumber, float ModValue){
        return BANumber % ModValue;
    }

    private float Hash2(float BaseNumber, float MakeTimeValue, float AnswerNumber, float ModValue)
    {
        return (BaseNumber + AnswerNumber + MakeTimeValue) % ModValue;
    }
}