using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Cards> _cards;
    private List<Cards> _playerCards;
    private List<SpriteRenderer> _playerSprites;
    private List<Cards> _enemyCards;
    private List<SpriteRenderer> _enemySprites;


    private void StartPrep()
    {
        _enemyCards.Add(_cards[Random.Range(0, _cards.Count - 1)]);
        _enemyCards.Add(_cards[Random.Range(0, _cards.Count - 1)]);
        _enemyCards.Add(_cards[Random.Range(0, _cards.Count - 1)]);
        ClearPlayerCards();
    }

    public void AddPlayerCards(Cards newCard)
    {
        if (_playerCards.Count < 3)
        {
            _playerCards.Add(newCard);
            UpdateCardUI();
        }
    }

    public void ClearPlayerCards()
    {
        _playerCards.Clear();
    }

    private void UpdateCardUI()
    {
        for (int i = 0;i < _enemySprites.Count; i++)
        {
            if (_playerCards[i] != null)
            {
                _enemySprites[i].sprite = _enemyCards[i].sprite;
            }
            else
            {
                _enemySprites[i].sprite = null;
            }
        }

        for (int i = 0;i < _playerSprites.Count; i++)
        {
            if (_playerCards[i] != null)
            {
                _playerSprites[i].sprite = _playerCards[i].sprite;
            }
            else
            {
                _playerSprites[i].sprite= null;
            }
        }
    }
}
