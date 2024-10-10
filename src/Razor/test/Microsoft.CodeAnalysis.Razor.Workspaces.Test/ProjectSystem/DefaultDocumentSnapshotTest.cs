﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.ProjectSystem;
using Microsoft.AspNetCore.Razor.Test.Common;
using Microsoft.AspNetCore.Razor.Test.Common.Workspaces;
using Microsoft.CodeAnalysis.Text;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.CodeAnalysis.Razor.ProjectSystem;

public class DefaultDocumentSnapshotTest : WorkspaceTestBase
{
    private static readonly HostDocument s_componentHostDocument = TestProjectData.SomeProjectComponentFile1;
    private static readonly HostDocument s_componentCshtmlHostDocument = TestProjectData.SomeProjectCshtmlComponentFile5;
    private static readonly HostDocument s_legacyHostDocument = TestProjectData.SomeProjectFile1;
    private static readonly HostDocument s_nestedComponentHostDocument = TestProjectData.SomeProjectNestedComponentFile3;

    private readonly SourceText _sourceText;
    private readonly VersionStamp _version;
    private readonly DocumentSnapshot _componentDocument;
    private readonly DocumentSnapshot _componentCshtmlDocument;
    private readonly DocumentSnapshot _legacyDocument;
    private readonly DocumentSnapshot _nestedComponentDocument;

    public DefaultDocumentSnapshotTest(ITestOutputHelper testOutput)
        : base(testOutput)
    {
        _sourceText = SourceText.From("<p>Hello World</p>");
        _version = VersionStamp.Create();

        var projectState = ProjectState.Create(ProjectEngineFactoryProvider, TestProjectData.SomeProject, ProjectWorkspaceState.Default);
        var project = new ProjectSnapshot(projectState);

        var textLoader = TestMocks.CreateTextLoader(_sourceText, _version);

        var documentState = DocumentState.Create(s_legacyHostDocument, textLoader);
        _legacyDocument = new DocumentSnapshot(project, documentState);

        documentState = DocumentState.Create(s_componentHostDocument, textLoader);
        _componentDocument = new DocumentSnapshot(project, documentState);

        documentState = DocumentState.Create(s_componentCshtmlHostDocument, textLoader);
        _componentCshtmlDocument = new DocumentSnapshot(project, documentState);

        documentState = DocumentState.Create(s_nestedComponentHostDocument, textLoader);
        _nestedComponentDocument = new DocumentSnapshot(project, documentState);
    }

    [Fact]
    public async Task GCCollect_OutputIsNoLongerCached()
    {
        // Arrange
        await Task.Run(async () => { await _legacyDocument.GetGeneratedOutputAsync(); });

        // Act

        // Forces collection of the cached document output
        GC.Collect();

        // Assert
        Assert.False(_legacyDocument.TryGetGeneratedOutput(out _));
    }

    [Fact]
    public async Task RegeneratingWithReference_CachesOutput()
    {
        // Arrange
        var output = await _legacyDocument.GetGeneratedOutputAsync();

        // Mostly doing this to ensure "var output" doesn't get optimized out
        Assert.NotNull(output);

        // Act & Assert
        Assert.True(_legacyDocument.TryGetGeneratedOutput(out _));
    }

    // This is a sanity test that we invoke component codegen for components.It's a little fragile but
    // necessary.

    [Fact]
    public async Task GetGeneratedOutputAsync_CshtmlComponent_ContainsComponentImports()
    {
        // Act
        var codeDocument = await _componentCshtmlDocument.GetGeneratedOutputAsync();

        // Assert
        Assert.Contains("using global::Microsoft.AspNetCore.Components", codeDocument.GetCSharpSourceText().ToString(), StringComparison.Ordinal);
    }

    [Fact]
    public async Task GetGeneratedOutputAsync_Component()
    {
        // Act
        var codeDocument = await _componentDocument.GetGeneratedOutputAsync();

        // Assert
        Assert.Contains("ComponentBase", codeDocument.GetCSharpSourceText().ToString(), StringComparison.Ordinal);
    }

    [Fact]
    public async Task GetGeneratedOutputAsync_NestedComponentDocument_SetsCorrectNamespaceAndClassName()
    {
        // Act
        var codeDocument = await _nestedComponentDocument.GetGeneratedOutputAsync();

        // Assert
        Assert.Contains("ComponentBase", codeDocument.GetCSharpSourceText().ToString(), StringComparison.Ordinal);
        Assert.Contains("namespace SomeProject.Nested", codeDocument.GetCSharpSourceText().ToString(), StringComparison.Ordinal);
        Assert.Contains("class File3", codeDocument.GetCSharpSourceText().ToString(), StringComparison.Ordinal);
    }

    // This is a sanity test that we invoke legacy codegen for .cshtml files. It's a little fragile but
    // necessary.
    [Fact]
    public async Task GetGeneratedOutputAsync_Legacy()
    {
        // Act
        var codeDocument = await _legacyDocument.GetGeneratedOutputAsync();

        // Assert
        Assert.Contains("Template", codeDocument.GetCSharpSourceText().ToString(), StringComparison.Ordinal);
    }
}
