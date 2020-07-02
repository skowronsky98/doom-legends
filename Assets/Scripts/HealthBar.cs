using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _hpText;
    [SerializeField] private Card card;
    private int _hp, _maxHp;
    private float x = 1f, currentValue;

    private void Update()
    {
        if (_maxHp == 0)
        {
            _hp = _maxHp = card.Health;
            UpdateHealth();
        }

        if(x != _image.fillAmount || _hp != card.Health)
            UpdateHealth();
    }

    private void UpdateHealth()
    {
        _hp = card.Health;
        x = (float) _hp / _maxHp;

        _image.fillAmount = Mathf.MoveTowards(_image.fillAmount, x, Time.deltaTime * 1.5f);
        
        UpdateText();
    }
    private void UpdateText()
    {
        _hpText.text = _hp + " / " + _maxHp;
    }
}
