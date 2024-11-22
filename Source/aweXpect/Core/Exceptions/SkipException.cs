using System;

// ReSharper disable once CheckNamespace
namespace Testably.Expectations;

/// <summary>
///     Represents the default skip exception in case no test framework is configured.
/// </summary>
public class SkipException(string message) : Exception(message);
