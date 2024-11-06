using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// Shop to buy more dogs using doggy points
public class Shop : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnBuyRetriever;
    [SerializeField] private Button btnEquipRetriever;

    [SerializeField] private Button btnBuyRussel;
    [SerializeField] private Button btnEquipRussel;

    [SerializeField] private Button btnBuyShin;
    [SerializeField] private Button btnEquipShin;

    [Header("Dog")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI txtDoggyPoints;

    [Header("Doggy Points")]
    [SerializeField] private DoggyPoints doggyPoints; /// see DoggyPoints()

    [Header("Sprites")]
    [SerializeField] private Sprite spriteRetriever;
    [SerializeField] private Sprite spriteRussel;
    [SerializeField] private Sprite spriteShin;

    [Header("Animators")]
    [SerializeField] private RuntimeAnimatorController animRetriever;
    [SerializeField] private RuntimeAnimatorController animRussel;
    [SerializeField] private RuntimeAnimatorController animShin;

    [Header("Prices")]
    [SerializeField] private int retrieverPrice;
    [SerializeField] private int russelPrice;
    [SerializeField] private int shinPrice;

    [SerializeField] private AudioSource buySound;


    private bool isRetrieverEquipable = true;
    private bool isRusselEquipable = false;
    private bool isShinEquipable = false;

    private void Awake()
    {
        //retriever
        btnBuyRetriever.onClick.AddListener(() =>
        {
            isRetrieverEquipable = BuyDog(retrieverPrice, btnBuyRetriever);
        });

        btnEquipRetriever.onClick.AddListener(() =>
        {
            EquipDog(isRetrieverEquipable, spriteRetriever, animRetriever);
        });

        //russel
        btnBuyRussel.onClick.AddListener(() =>
        {
            isRusselEquipable = BuyDog(russelPrice, btnBuyRussel);
        });

        btnEquipRussel.onClick.AddListener(() =>
        {
            EquipDog(isRusselEquipable, spriteRussel, animRussel);
        });

        //shin
        btnBuyShin.onClick.AddListener(() =>
        {
            isShinEquipable = BuyDog(shinPrice, btnBuyShin);
        });

        btnEquipShin.onClick.AddListener(() =>
        {
            EquipDog(isShinEquipable, spriteShin, animShin);
        });
    }

    /// Method to buy a dog using doggypoints
    /// @param price            -the price of a dog
    /// @param currentButton    -disables the button pressed
    /// @returns true is successful else false

    public bool BuyDog(int price, Button currentButton) 
    {
        if(doggyPoints.getDoggyPoints() > price)
        {
            doggyPoints.setDoggyTreat(-price);
            ResetShop();
            buySound.Play();
            currentButton.enabled = false;
            return true;
        }

        return false;
    }
    /// Method to equip a dog
    /// @param equipable            -boolean if already purchased
    /// @param sprite               -changes the dog sprite to new one 
    /// @param _anim                -changes the anim to new one
    public void EquipDog(bool equipable, Sprite _sprite, RuntimeAnimatorController _anim)
    {
        if(equipable)
        {
            sprite.sprite = _sprite;
            animator.runtimeAnimatorController = _anim;
            buySound.Play();
            Debug.Log("set dog");
        }
    }

    /// Reset the doggy points ui
    public void ResetShop() {
        txtDoggyPoints.text = "$: " + doggyPoints.getDoggyPoints().ToString();
    }
}
