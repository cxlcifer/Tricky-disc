using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
   [SerializeField] private float _minMovingDuration;
   [SerializeField] private float _maxMovingDuration;
   
   private float _minPointX;
   private float _maxPointX;

   private float _delayBetweenMovements;
   
   private SpriteRenderer _enemySprite;

   private Sequence _moveSequence;
   
   private void Move()
   {
      var randomMovementDuration = GetRandomMovementDuration();
      var nextPosition = GetNextRandomPosition();

      _moveSequence = DOTween.Sequence();
      _moveSequence.Append(transform.DOMoveX(nextPosition, randomMovementDuration));
      _moveSequence.AppendInterval(_delayBetweenMovements);
      _moveSequence.OnComplete(Move);
   }
   
   
   private float GetRandomMovementDuration()
   {
      return Random.Range(_minMovingDuration, _maxMovingDuration);
   }

   private float GetNextRandomPosition()
   {
      return Random.Range(_minPointX, _maxPointX);
   }
   
   public void Initialize(float minPointX, float maxPointX, float delayBetweenMovements)
   {
      _enemySprite = GetComponent<SpriteRenderer>();
      var offset = _enemySprite.bounds.size.x / 2;

      _minPointX = minPointX;
      _maxPointX = maxPointX;
      _delayBetweenMovements = delayBetweenMovements;
      Move();
   }
   
   //вызывается по событию уничтожения врага.
   [UsedImplicitly]
   public void Destroy()
   {
      Destroy(gameObject);
   }

   private void OnDestroy()
   {
      _moveSequence.Kill();
   }
}
