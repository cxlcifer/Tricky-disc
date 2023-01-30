using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreLabel;
    [SerializeField] private int _rewardForEnemy;
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _scaleFactor;

    private int _score;

    private void Awake()
    {
        _scoreLabel.text = "0";
    }

    //Чтобы установить награду, которая дается за одного врага добавим сериализованное приватное поле _rewardForEnem
    [UsedImplicitly]
    public void AddScore()
    {
        _score += _rewardForEnemy;
        _scoreLabel.text = _score.ToString();

        _scoreLabel.transform.DOPunchScale(Vector3.one * _scaleFactor, _animationDuration, 0)
            .OnComplete(() => _scoreLabel.transform.localScale = Vector3.one);

    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(GlobalConstants.SCORE_PREFS_KEY, _score);
        PlayerPrefs.Save();
    }
}