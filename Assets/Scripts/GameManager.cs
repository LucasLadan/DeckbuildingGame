using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Cards> _cards;
    [SerializeField] private List<Cards> _playerCards;
    [SerializeField] private List<SpriteRenderer> _playerSprites;
    [SerializeField] private List<TextMeshPro> _playerHealth;
    [SerializeField] private List<TextMeshPro> _playerDamage;

    [SerializeField] private List<Cards> _enemyCards;
    [SerializeField] private List<SpriteRenderer> _enemySprites;
    [SerializeField] private List<TextMeshPro> _enemyHealth;
    [SerializeField] private List<TextMeshPro> _enemyDamage;

    private List<int> _enemyHP = new List<int>();
    private List<int> _enemyDMG = new List<int>();
    private List<int> _playerHP = new List<int>();
    private List<int> _playerDMG = new List<int>();

    [SerializeField] private TextMeshProUGUI _victoryScreen;
    [SerializeField] private GameObject _button;


    private void Start()
    {
        StartPrep();
    }

    private void StartPrep()
    {
        _enemyCards.Clear();
        _enemyCards.Add(_cards[UnityEngine.Random.Range(0, _cards.Count - 1)]);
        _enemyCards.Add(_cards[UnityEngine.Random.Range(0, _cards.Count - 1)]);
        _enemyCards.Add(_cards[UnityEngine.Random.Range(0, _cards.Count - 1)]);
        for (int i = 0; i < _enemyCards.Count; i++)
        {
            Debug.Log(_enemyCards[i].health);
            _enemyHP.Add(_enemyCards[i].health);
            _enemyDMG.Add(_enemyCards[i].damage);
        }
        ClearPlayerCards();
        UpdateCardUI();
    }

    public void AddPlayerCards(Cards newCard)
    {
        if (_playerCards.Count < 3)
        {
            _playerCards.Add(newCard);
            _playerHP.Add(_playerCards[_playerCards.Count-1].health);
            _playerDMG.Add(_playerCards[_playerCards.Count - 1].damage);
            UpdateCardUI();
        }
    }

    public void ClearPlayerCards()
    {
        
        _playerCards.Clear();
    }

    public void StartCardGame()
    {
        for (int i = 0; i < _playerCards.Count; i++)
        {
            if (_playerCards[i].special == Cards.Special.buff)
            {
                for (int q = 0; q < _playerCards.Count; q++)
                {
                    if (_playerCards[q].buff == Cards.PreferedBuff.health)
                    {
                        _playerHP[q] += 4;
                    }
                    else
                    {
                        _playerDMG[q] += 1;
                    }
                }
            }
        }

        for (int i = 0; i < _enemyCards.Count; i++)
        {
            if (_enemyCards[i].special == Cards.Special.buff)
            {
                for (int q = 0; q < _enemyCards.Count; q++)
                {
                    if (_enemyCards[q].buff == Cards.PreferedBuff.health)
                    {
                        _enemyHP[q] += 4;
                    }
                    else
                    {
                        _enemyDMG[q] += 1;
                    }
                }
            }
        }

        UpdateCardUI();

        StartCoroutine(DoGameLogic());
    }

    IEnumerator DoGameLogic()
    {
        _button.SetActive(false);
        while (_playerCards.Count > 0 && _enemyCards.Count > 0)//Repeats until the player or enemy loses all their cards
        {
            yield return new WaitForSeconds(1f);
            _enemyHP[0] -= _playerDMG[0];//These 2 lines attack the cards in the front
            _playerHP[0] -= _enemyDMG[0];
            if (_playerHP[0] < 1)
            {
                _playerCards.RemoveAt(0);
                _playerDMG.RemoveAt(0);
                _playerHP.RemoveAt(0);
            }
            if (_enemyHP[0] < 1)
            {
                _enemyCards.RemoveAt(0);
                _enemyHP.RemoveAt(0);
                _enemyDMG.RemoveAt(0);
            }
            UpdateCardUI();
        } 

        _victoryScreen.text = "You win";
        if (_playerCards.Count < 1)//If the player doesn't have any cards remaining (ties result in a lose
        {
            _victoryScreen.text = "You lose";
        }
    }

    private void UpdateCardUI()
    {
        for (int i = 0;i < _enemySprites.Count; i++)
        {
            if (i < _enemyCards.Count)
            {
                _enemySprites[i].sprite = _enemyCards[i].sprite;
                _enemyHealth[i].text = _enemyHP[i].ToString();
                _enemyDamage[i].text = _enemyCards[i].damage.ToString();
            }
            else
            {
                _enemySprites[i].sprite = null;
                _enemyHealth[i].text = "";
                _enemyDamage[i].text = "";
            }
        }

        for (int i = 0;i < _playerSprites.Count; i++)
        {

            if (i < _playerCards.Count)
            {
                _playerSprites[i].sprite = _playerCards[i].sprite;
                _playerHealth[i].text = _playerHP[i].ToString();
                _playerDamage[i].text = _playerCards[i].damage.ToString();
            }
            else
            {
                _playerSprites[i].sprite= null;
                _playerHealth[i].text = "";
                _playerDamage[i].text = "";
            }
        }
    }
}
