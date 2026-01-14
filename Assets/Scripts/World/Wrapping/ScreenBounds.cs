using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using Zenject;

[RequireComponent (typeof(BoxCollider2D))]
public class ScreenBounds : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    [SerializeField] private float _teleportOffset = 0.995f;

    private BoxCollider2D _boxCollider;

    private UnityEvent<Collider2D> OnTriggerExit;

    private GameSettings _gameSettings;

    private (bool, bool) OutOfBounds(Vector2 position) => (Mathf.Abs(position.x) > Mathf.Abs(_boxCollider.bounds.min.x), Mathf.Abs(position.y) > Mathf.Abs(_boxCollider.bounds.min.y));

    [Inject]
    private void Construct(GameSettings settings)
    {
        _gameSettings = settings;
    }

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.isTrigger = true;

        transform.position = Vector3.zero;

        UpdateBoundsSize();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print(1);

        var position = collision.transform.position;

        var result = OutOfBounds(position);

        if (result.Item1 && result.Item2)
        {
            collision.transform.position = new Vector2(position.x * -_teleportOffset, position.y * -_teleportOffset);
        }
        else if (result.Item1)
        {
            collision.transform.position = new Vector2(position.x * -_teleportOffset, position.y);
        }
        else if (result.Item2)
        {
            collision.transform.position = new Vector2(position.x, position.y * -_teleportOffset);
        }
    }

    private void UpdateBoundsSize()
    {
        _boxCollider.size = new Vector2(_gameSettings.MapSize * _mainCamera.aspect, _gameSettings.MapSize);

        _mainCamera.orthographicSize = _gameSettings.CameraSize;
    }
}
