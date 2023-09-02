using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPakage : MonoBehaviour
{
    public GameObject cardPrefab;
    private CardStore cardStore;
    [SerializeField] private GameObject cardPool;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject magicCircle;
    // Start is called before the first frame update
    void Start()
    {
        cardStore = GetComponent<CardStore>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnclickOpen()
    {
        button.SetActive(false);
        magicCircle.SetActive(true);
        Invoke("LoadCard", 1.5f);
    }

    public void LoadCard()
    {
        magicCircle.SetActive(false);
        GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool.transform);
        newCard.GetComponent<CardDisplay>().card = cardStore.RandomCard();

    }
}
