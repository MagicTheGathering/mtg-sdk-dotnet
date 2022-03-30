using System.Threading;
using System.Threading.Tasks;

namespace MtgApiManager.Lib.Core
{
    internal interface IRateLimit
    {
        bool IsTurnedOn { get; }

        void AddApiCall();

        Task<int> Delay(int requestsPerHour, CancellationToken cancellationToken);

        void Reset();
    }
}