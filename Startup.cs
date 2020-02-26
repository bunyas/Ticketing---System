#region Copyright Syncfusion Inc. 2001-2017.

// Copyright Syncfusion Inc. 2001-2017. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws.

#endregion Copyright Syncfusion Inc. 2001-2017.

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Buses.Startup))]

namespace Buses
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}