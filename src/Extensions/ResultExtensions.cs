using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Grpc.Core;

namespace FinanceTracker.Extensions
{
    public static class ResultExtensions
    {
        public static TValue UnwrapOrThrow<TValue>(this Result<TValue> result, string errorMessage)
        {
            if (result.IsSuccess)
            {
                return result.Value;
            }

            // All rpc exceptions will have StatusCode.Internal (for now) but this should be altered to be specific.
            throw new RpcException(new Status(StatusCode.Internal, errorMessage));
        }

        public static async Task<TValue> UnwrapOrThrow<TValue>(this Task<Result<TValue>> resultTask, string errorMessage) =>
            (await resultTask.DefaultAwait()).UnwrapOrThrow(errorMessage);

        private static ConfiguredTaskAwaitable<T> DefaultAwait<T>(
            this Task<T> task) => task.ConfigureAwait(Result.DefaultConfigureAwait);
    }
}
