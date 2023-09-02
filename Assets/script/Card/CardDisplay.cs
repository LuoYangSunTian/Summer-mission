using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI describe;
    [SerializeField] private int id;
    [SerializeField] public Card card;
    // Start is called before the first frame update
    void Start()
    {
        ShowCard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowCard()
    {
        skillName.text = card.name;
        describe.text = card.describe;
    }
}
