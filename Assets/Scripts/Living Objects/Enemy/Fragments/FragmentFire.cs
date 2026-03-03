using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class FragmentFire
{
    private float _duration;

    private float _timer = 0;

    private bool IsTimeElapsed => _timer >= _duration;

    public async UniTask FireTask(CancellationToken token, Transform fragment, float duration)
    {
        _duration = duration;

        Vector3 startScale = fragment.localScale;

        while (!IsTimeElapsed)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);
            Step(startScale, fragment);
        }
    }

    private void Step(Vector3 start, Transform transform)
    {
        transform.localScale = Vector3.Lerp(start, new Vector3(0, 0, 0), _timer / _duration);
        _timer += Time.deltaTime;
    }
}
