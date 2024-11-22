using System;

// ReSharper disable once CheckNamespace
namespace Testably.Expectations;

/// <summary>
///     Represents the default failure exception in case no test framework is configured.
/// </summary>
public class FailException(string message) : Exception(message);
