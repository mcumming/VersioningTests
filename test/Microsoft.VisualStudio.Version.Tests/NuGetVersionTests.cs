// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO.Compression;
using System.Xml.Linq;
using Xunit;

public class NuGetVersionTests
{
    private const string PackageUnderTest = "../../../Packages/Debug/Example.NuGet.Library.42.42.42-beta.nupkg";

    public NuGetVersionTests()
    {
    }

    [Fact]
    public void NuGetPackageFilenameContainsVersion()
    {
        Assert.True(File.Exists(PackageUnderTest));
    }

    [Fact]
    public void NuGetPackageSpecContainsVersion()
    {
        string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(tempDirectory);

        ZipFile.ExtractToDirectory(PackageUnderTest, tempDirectory);

        var root = XElement.Load(Path.Combine(tempDirectory, @"Example.NuGet.Library.nuspec"));
        var version = root.Descendants()
              .Where(x => x.Name.LocalName == "version")
              .First();
        Assert.Equal("42.42.42-beta+cdffde26ae", version.Value);

        Directory.Delete(tempDirectory, true);
    }
}
