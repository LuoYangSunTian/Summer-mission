using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStore : MonoBehaviour
{
    public TextAsset cardData;
    public List<Card> cardList = new List<Card>();
    [SerializeField] private GameObject cardUI;
    // Start is called before the first frame update
    void Start()
    {
        //weapon = GameObject.FindGameObjectWithTag("Weapon");

        LoadCardData();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadCardData()
    {
        string[] dataRow = cardData.text.Split('\n');
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
                continue;
            else if (rowArray[0] == "技能")
            {
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                string describe = rowArray[3];
                Card card = new Card(id, name, describe);
                cardList.Add(card);
            }
        }

    }
    public Card RandomCard()
    {
        Card card = cardList[Random.Range(0, cardList.Count)];
        return card;
    }

    public void Close()
    {
        TouchOpenUI open = GetComponentInParent<TouchOpenUI>();
        open.play.canMove = true;
        cardUI.SetActive(false);
        if (open.weapon != null)
            open.weapon.SetActive(true);
    }
}
