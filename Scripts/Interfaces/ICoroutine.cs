using System.Collections;

namespace Interfaces
{
    public interface ICoroutine
    {
        IEnumerator Coroutine { get; set;}

        IEnumerator RestartLevel(float delay);
        IEnumerator GetNextLevel(float delay);
    }
}
