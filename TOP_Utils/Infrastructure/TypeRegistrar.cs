using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace TOP_Utils.Infrastructure;

[ExcludeFromCodeCoverage]
public class TypeRegistrar : ITypeRegistrar
{
    private readonly IServiceCollection _builder;
    public IServiceCollection Services => _builder;

    public TypeRegistrar()
    {
        _builder = new ServiceCollection();
    }

    public ITypeResolver Build()
    {
        return new TypeResolver(_builder.BuildServiceProvider());
    }

    public void Register(Type service, Type implementation)
    {
        _builder.AddSingleton(service, implementation);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        _builder.AddSingleton(service, implementation);
    }

    public void RegisterLazy(Type service, Func<object> func)
    {
        if (func is null)
        {
            throw new ArgumentNullException(nameof(func));
        }

        _builder.AddSingleton(service, (provider) => func());
    }

    public CommandApp BuildApp()
    {
        return new CommandApp(this);
    }
}