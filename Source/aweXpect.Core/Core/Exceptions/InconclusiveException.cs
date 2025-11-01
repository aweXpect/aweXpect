using System;

// ReSharper disable once CheckNamespace
namespace aweXpect;

/// <summary>
///     Represents the default inconclusive exception in case no test framework is configured.
/// </summary>
public class InconclusiveException(string message) : Exception(message);
