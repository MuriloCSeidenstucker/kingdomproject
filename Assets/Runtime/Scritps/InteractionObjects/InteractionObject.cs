using TMPro;
using UnityEngine;
using UnityEngine.Profiling;

public abstract class InteractionObject : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private int _price;

    private int _amountPaid;

    protected abstract bool BehaviorEnded { get; }

    private void Start()
    {
        Vector3 newPos = _canvas.transform.position;
        newPos.y = 0;
        _canvas.transform.position = newPos;
    }

    private void LateUpdate()
    {
        _priceText.text = $"{_amountPaid}/{_price}";
    }

    private void TryStartBehavior(CoinInventory coinInventory)
    {
        if (coinInventory == null) return;

        if (BehaviorEnded || coinInventory.CoinAmount < _price) return;

        coinInventory.Purchase(_price);
        _amountPaid = _price;

        PerformBehavior();
    }

    protected abstract void PerformBehavior();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            if (BehaviorEnded) return;

            _canvas.gameObject.SetActive(true);
            player.TryInteract += TryStartBehavior;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            _canvas.gameObject.SetActive(false);
            player.TryInteract -= TryStartBehavior;
        }
    }
}
