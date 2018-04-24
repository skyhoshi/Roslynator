// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Roslynator.CSharp;
using Roslynator.CSharp.CodeFixes;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpCompilerCodeFixVerifier;

namespace Roslynator.CodeFixes.Tests
{
    public static class CS1983ReturnTypeOfAsyncMethodMustBeVoidOrTaskOrTaskOfTTests
    {
        private const string DiagnosticId = CompilerDiagnosticIdentifiers.ReturnTypeOfAsyncMethodMustBeVoidOrTaskOrTaskOfT;

        [Fact]
        public static void TestCodeFix_Task()
        {
            VerifyFix(
@"
using System.Threading.Tasks;

public class Foo
{
    public async object Bar()
    {
        await DoAsync().ConfigureAwait(false);

        return await GetAsync().ConfigureAwait(false);
    }

    public async object Bar2()
    {
        await DoAsync().ConfigureAwait(false);
    }

    public void Bar3()
    {
        async object LocalBar()
        {
            await DoAsync().ConfigureAwait(false);

            return await GetAsync().ConfigureAwait(false);
        }

        async object LocalBar2()
        {
            await DoAsync().ConfigureAwait(false);
        }
    }

    public Task<object> GetAsync()
    {
        return Task.FromResult(default(object));
    }

    public Task DoAsync()
    {
        return Task.CompletedTask;
    }
}
",
@"
using System.Threading.Tasks;

public class Foo
{
    public async Task Bar()
    {
        await DoAsync().ConfigureAwait(false);

        return await GetAsync().ConfigureAwait(false);
    }

    public async Task Bar2()
    {
        await DoAsync().ConfigureAwait(false);
    }

    public void Bar3()
    {
        async Task LocalBar()
        {
            await DoAsync().ConfigureAwait(false);

            return await GetAsync().ConfigureAwait(false);
        }

        async Task LocalBar2()
        {
            await DoAsync().ConfigureAwait(false);
        }
    }

    public Task<object> GetAsync()
    {
        return Task.FromResult(default(object));
    }

    public Task DoAsync()
    {
        return Task.CompletedTask;
    }
}
",
                diagnosticId: DiagnosticId,
                fixProvider: new ReturnTypeOfAsyncMethodMustBeVoidOrTaskOrTaskOfTCodeFixProvider(),
                equivalenceKey: EquivalenceKey.Create(DiagnosticId, "Task"));
        }

        [Fact]
        public static void TestCodeFix_TaskOfT()
        {
            VerifyFix(
@"
using System.Threading.Tasks;

public class Foo
{
    public async object Bar()
    {
        object x = await GetAsync().ConfigureAwait(false);

        return await GetAsync().ConfigureAwait(false);
    }

    public async object Bar2()
    {
        await DoAsync().ConfigureAwait(false);

        return await GetAsync().ConfigureAwait(false);
    }

    public async object Bar3() => await GetAsync().ConfigureAwait(false);

    public void Bar4()
    {
        async object LocalBar()
        {
            object x = await GetAsync().ConfigureAwait(false);

            return await GetAsync().ConfigureAwait(false);
        }

        async object LocalBar2()
        {
            await DoAsync().ConfigureAwait(false);

            return await GetAsync().ConfigureAwait(false);
        }

        async object LocalBar3() => await GetAsync().ConfigureAwait(false);
    }

    public Task<object> GetAsync()
    {
        return Task.FromResult(default(object));
    }

    public Task DoAsync()
    {
        return Task.CompletedTask;
    }
}
",
@"
using System.Threading.Tasks;

public class Foo
{
    public async Task<object> Bar()
    {
        object x = await GetAsync().ConfigureAwait(false);

        return await GetAsync().ConfigureAwait(false);
    }

    public async Task<object> Bar2()
    {
        await DoAsync().ConfigureAwait(false);

        return await GetAsync().ConfigureAwait(false);
    }

    public async Task<object> Bar3() => await GetAsync().ConfigureAwait(false);

    public void Bar4()
    {
        async Task<object> LocalBar()
        {
            object x = await GetAsync().ConfigureAwait(false);

            return await GetAsync().ConfigureAwait(false);
        }

        async Task<object> LocalBar2()
        {
            await DoAsync().ConfigureAwait(false);

            return await GetAsync().ConfigureAwait(false);
        }

        async Task<object> LocalBar3() => await GetAsync().ConfigureAwait(false);
    }

    public Task<object> GetAsync()
    {
        return Task.FromResult(default(object));
    }

    public Task DoAsync()
    {
        return Task.CompletedTask;
    }
}
",
                diagnosticId: DiagnosticId,
                fixProvider: new ReturnTypeOfAsyncMethodMustBeVoidOrTaskOrTaskOfTCodeFixProvider(),
                equivalenceKey: EquivalenceKey.Create(DiagnosticId, "TaskOfT"));
        }

        [Fact]
        public static void TestNoCodeFix()
        {
            const string source = @"
using System.Threading.Tasks;

public class Foo
{
    public async X Bar()
    {
        await DoAsync().ConfigureAwait(false);

        return await GetAsync().ConfigureAwait(false);
    }

    public async X Bar2() => await GetAsync().ConfigureAwait(false);

    public void LocalFunction()
    {
        async X LocalBar()
        {
            await DoAsync().ConfigureAwait(false);

            return await GetAsync().ConfigureAwait(false);
        }

        async X LocalBar2() => await GetAsync().ConfigureAwait(false);
    }

    public Task<object> GetAsync()
    {
        return Task.FromResult(default(object));
    }

    public Task DoAsync()
    {
        return Task.CompletedTask;
    }
}
";
            VerifyNoFix(
                source,
                fixProvider: new ReturnTypeOfAsyncMethodMustBeVoidOrTaskOrTaskOfTCodeFixProvider(),
                equivalenceKey: EquivalenceKey.Create(DiagnosticId, "Task"));

            VerifyNoFix(
                source,
                fixProvider: new ReturnTypeOfAsyncMethodMustBeVoidOrTaskOrTaskOfTCodeFixProvider(),
                equivalenceKey: EquivalenceKey.Create(DiagnosticId, "TaskOfT"));
        }
    }
}
