// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Xunit;

public class AssemblyVersionTests
{
    private const string AssemblyUnderTest = "../../../Example.NuGet.Library/Debug/net6.0/Example.NuGet.Library.dll";

    public AssemblyVersionTests()
    {
    }

    [Fact]
    public void AssemblyVersionInformation()
    {
        Assert.True(File.Exists(AssemblyUnderTest));

        var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(AssemblyUnderTest);
        Assert.Equal("42.42.42.42", fvi.FileVersion);
        Assert.Equal("42.42.42.42-beta+cdffde26ae", fvi.ProductVersion);

        string assemblyVersion = System.Reflection.Assembly.LoadFile(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), AssemblyUnderTest)).GetName().Version?.ToString() ?? string.Empty;
        Assert.Equal("0.1.0.0", assemblyVersion);
    }
}
