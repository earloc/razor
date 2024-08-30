﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.LanguageServer.Protocol;

namespace Microsoft.CodeAnalysis.Razor.Formatting;

internal interface IFormattingPass
{
    Task<TextEdit[]> ExecuteAsync(FormattingContext context, TextEdit[] edits, CancellationToken cancellationToken);
}
