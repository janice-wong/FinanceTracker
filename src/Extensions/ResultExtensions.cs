using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult UnwrapOrThrow<TValue>(this Result<TValue> result, string errorMessage)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Value);
            }

            dynamic response = new JObject();
            response.error = errorMessage;
            return new BadRequestObjectResult(response);
        }

        public static async Task<IActionResult> UnwrapOrThrow<TValue>(this Task<Result<TValue>> resultTask, string errorMessage) =>
            (await resultTask.DefaultAwait()).UnwrapOrThrow(errorMessage);

        private static ConfiguredTaskAwaitable<T> DefaultAwait<T>(
            this Task<T> task) => task.ConfigureAwait(Result.DefaultConfigureAwait);
    }
}
