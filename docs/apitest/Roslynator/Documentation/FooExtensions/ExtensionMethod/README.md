# FooExtensions\.ExtensionMethod Method

Namespace: [Roslynator.Documentation](../../README.md)

Assembly: Roslynator\.Documentation\.DocumentationTest\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ExtensionMethod\(Foo\) | x |
| ExtensionMethod\<T, T2>\(T\) | |
| ExtensionMethod\<T>\(T\) | |

## ExtensionMethod\(Foo\)

### Summary

x

```csharp
public static void ExtensionMethod(this Foo foo)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| foo | |

#### Returns

[Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)


## ExtensionMethod\<T>\(T\)

```csharp
public static void ExtensionMethod<T>(this T foo) where T : Foo
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| T | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| foo | |

#### Returns

[Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)


## ExtensionMethod\<T, T2>\(T\)

```csharp
public static void ExtensionMethod<T, T2>(this T foo) where T : T2 where T2 : Foo
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| T | |
| T2 | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| foo | |

#### Returns

[Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)


