using TMPro;
using UnityEngine;

public abstract class InteractionObject : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private int _price;

    private int _amountPaid;

    private void LateUpdate()
    {
        _priceText.text = $"{_amountPaid}/{_price}";
    }

    public void TryStartBehavior(in CoinInventory coinInventory)
    {
        if (coinInventory == null) return;

        if (BehaviorStarted() || coinInventory.CoinAmount < _price) return;

        coinInventory.Purchase(_price);
        _amountPaid = _price;

        PerformBehavior();
    }

    protected abstract bool BehaviorStarted();

    protected abstract void PerformBehavior();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            _canvas.gameObject.SetActive(true);
            player.GetInteractionObject(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            _canvas.gameObject.SetActive(false);
            player.GetInteractionObject(null);
        }
    }
}
