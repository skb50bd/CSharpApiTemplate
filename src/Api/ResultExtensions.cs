using System.Diagnostics.Contracts;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace Template.Api;

public static class ResultExtensions
{
    [Pure]
    public static async Task<TResult> MatchAsync<TSuccess, TResult>(
        this Task<Result<TSuccess>> inputTask,
        Func<TSuccess, TResult> success,
        Func<Exception, TResult> fail)
    {
        var input = await inputTask;
        return input.Match(success, fail);
    }
    
    [Pure]
    public static async Task<ActionResult<T>> MatchAsync<T>(
        this Task<Result<T>> inputTask,
        Func<T, ActionResult<T>> success,
        Func<Exception, ActionResult<T>> fail)
    {
        var input = await inputTask;
        return input.Match(success, fail);
    }
    
    [Pure]
    public static async Task<ActionResult<TResult>> MatchAsync<TSuccess, TResult>(
        this Task<Result<TSuccess>> inputTask,
        Func<TSuccess, ActionResult<TResult>> success,
        Func<Exception, ActionResult<TResult>> fail)
    {
        var input = await inputTask;
        return input.Match(success, fail);
    }

    [Pure]
    public static async Task<ActionResult<T>> MatchAsync<T>(
        this Task<T?> inputTask,
        Func<T, ActionResult<T>> success,
        Func<ActionResult<T>> fail)
    {
        var input = await inputTask;

        return input is null
            ? fail()
            : success(input);
    }
}