using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour, ICoinCollector
{
    [SerializeField] private GameHandler _gameHandler;
    [SerializeField] private CoinCollectorData _collectorData;
    [SerializeField] private int _fullInventory = 50;
    
    private PlayerMovement _playerMovement;
    private PlayerInputActions _inputActions;
    private int _coinInventory;
    private int _excessInventory;

    // TODO: Create class to handle UI.
    [SerializeField] private TextMeshProUGUI _coinAmountText;

    public bool IsFacingRight => _playerMovement.IsFacingRight;

    public int CoinInventory { get { return _coinInventory; } private set { _coinInventory = Mathf.Max(0, value); } }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();

        _inputActions = new PlayerInputActions();
        _inputActions.PlayerControls.Enable();
    }

    private void Update()
    {
        Vector2 movementInput = _inputActions.PlayerControls.Movement.ReadValue<Vector2>();
        bool runInput = _inputActions.PlayerControls.Run.IsPressed();

        _playerMovement.ProcessMovementInput(in movementInput, in runInput);

        if (movementInput.x != 0f)
            _playerMovement.PreventPlayerRun(_inputActions.PlayerControls.Run.WasPressedThisFrame());

        if (_inputActions.PlayerControls.Action.WasPressedThisFrame())
        {
            if (CoinInventory > 0)
            {
                CoinInventory--;
                Coin coin = _gameHandler.GetFromCoinPool(_collectorData.CoinCollector.position, Quaternion.identity);
                coin.ApllyVelocityAfterEjection(ejectionForce: 3.0f);
            }
        }

        if (_excessInventory > 0)
        {
            _excessInventory--;
            Coin coin = _gameHandler.GetFromCoinPool(_collectorData.CoinCollector.position, Quaternion.identity);
            coin.ApllyVelocityAfterEjection(ejectionForce: 5.0f);
        }
    }

    private void LateUpdate()
    {
        _coinAmountText.text = $"{CoinInventory}/{_fullInventory}";
    }

    public void ReactToCollisionEnter()
    {
        if (CoinInventory < _fullInventory)
        {
            CoinInventory++;
        }
        else
        {
            _excessInventory++;
        }
    }

    public CoinCollectorData ReactToCollisionStay()
    {
        return _collectorData;
    }
}
