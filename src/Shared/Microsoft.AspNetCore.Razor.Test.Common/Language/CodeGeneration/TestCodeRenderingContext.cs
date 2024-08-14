﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace Microsoft.AspNetCore.Razor.Language.CodeGeneration;

public static class TestCodeRenderingContext
{
    public static CodeRenderingContext CreateDesignTime(
        string newLineString = null,
        string suppressUniqueIds = "test",
        RazorSourceDocument source = null,
        IntermediateNodeWriter nodeWriter = null)
    {
        var documentNode = new DocumentIntermediateNode();
        var optionsBuilder = RazorCodeGenerationOptions.DesignTimeDefault.ToBuilder();

        if (source == null)
        {
            source = TestRazorSourceDocument.Create();
        }

        var codeDocument = RazorCodeDocument.Create(source);
        if (newLineString != null)
        {
            optionsBuilder.NewLine = newLineString;
        }

        if (suppressUniqueIds != null)
        {
            optionsBuilder.SuppressUniqueIds = suppressUniqueIds;
        }

        if (nodeWriter == null)
        {
            nodeWriter = new DesignTimeNodeWriter();
        }

        var options = optionsBuilder.Build();

        var context = new CodeRenderingContext(nodeWriter, codeDocument, documentNode, options);
        context.Visitor = new RenderChildrenVisitor(context);

        return context;
    }

    public static CodeRenderingContext CreateRuntime(
        string newLineString = null,
        string suppressUniqueIds = "test",
        RazorSourceDocument source = null,
        IntermediateNodeWriter nodeWriter = null)
    {
        var documentNode = new DocumentIntermediateNode();
        var optionsBuilder = RazorCodeGenerationOptions.Default.ToBuilder();

        if (source == null)
        {
            source = TestRazorSourceDocument.Create();
        }

        var codeDocument = RazorCodeDocument.Create(source);
        if (newLineString != null)
        {
            optionsBuilder.NewLine = newLineString;
        }

        if (suppressUniqueIds != null)
        {
            optionsBuilder.SuppressUniqueIds = suppressUniqueIds;
        }

        if (nodeWriter == null)
        {
            nodeWriter = new RuntimeNodeWriter();
        }

        var options = optionsBuilder.Build();

        var context = new CodeRenderingContext(nodeWriter, codeDocument, documentNode, options);
        context.Visitor = new RenderChildrenVisitor(context);

        return context;
    }

    private class RenderChildrenVisitor : IntermediateNodeVisitor
    {
        private readonly CodeRenderingContext _context;
        public RenderChildrenVisitor(CodeRenderingContext context)
        {
            _context = context;
        }

        public override void VisitDefault(IntermediateNode node)
        {
            _context.CodeWriter.WriteLine("Render Children");
        }
    }
}
