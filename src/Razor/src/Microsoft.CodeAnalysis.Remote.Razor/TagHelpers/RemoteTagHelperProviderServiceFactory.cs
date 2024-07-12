﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor;
using Microsoft.CodeAnalysis.Razor.Remote;
using Microsoft.VisualStudio.Composition;

namespace Microsoft.CodeAnalysis.Remote.Razor;

internal sealed class RemoteTagHelperProviderServiceFactory : RazorServiceFactoryBase<IRemoteTagHelperProviderService>
{
    // WARNING: We must always have a parameterless constructor in order to be properly handled by ServiceHub.
    public RemoteTagHelperProviderServiceFactory()
        : base(RazorServices.Descriptors)
    {
    }

    protected override IRemoteTagHelperProviderService CreateService(IRazorServiceBroker serviceBroker, ExportProvider exportProvider)
    {
        var tagHelperResolver = exportProvider.GetExportedValue<RemoteTagHelperResolver>().AssumeNotNull();
        var tagHelperDeltaProvider = exportProvider.GetExportedValue<RemoteTagHelperDeltaProvider>().AssumeNotNull();
        return new RemoteTagHelperProviderService(serviceBroker, tagHelperResolver, tagHelperDeltaProvider);
    }
}
