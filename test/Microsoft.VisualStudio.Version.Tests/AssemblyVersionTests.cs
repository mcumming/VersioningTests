// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
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
        Assert.StartsWith("42.42.42.42-beta", fvi.ProductVersion);

        var executionFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        if (executionFolder != null)
        {
            var fullPathToAssembly = Path.Combine(executionFolder, AssemblyUnderTest);
            string assemblyVersion = System.Reflection.Assembly.LoadFile(fullPathToAssembly).GetName().Version?.ToString() ?? string.Empty;
            Assert.Equal("0.1.0.0", assemblyVersion);
        }
        else
        {
        Assert.Fail("Could not locate execution folder.");
        }
    }
}
