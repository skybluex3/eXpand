﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using SecuritySystemExample.Module;
using Xpand.Persistent.Base.MiddleTier;

namespace ConsoleApplicationServer {
    public class ConsoleApplicationServerServerApplication : XpandServerApplication {
        public ConsoleApplicationServerServerApplication(ISecurityStrategyBase securityStrategyBase)
            : base(securityStrategyBase) {
            ApplicationName = @"SecuritySystemExample";
            Modules.Add(new SecuritySystemExampleModule());
        }

        protected override void OnDatabaseVersionMismatch(DatabaseVersionMismatchEventArgs args) {
            args.Handled = true;
            args.Updater.Update();
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(args.ConnectionString, args.Connection);
        }

    }
}