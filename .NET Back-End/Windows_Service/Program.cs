using Topshelf;
using Windows_Service.Operations;

var hostRun = HostFactory.Run(
   x =>
   {
       x.Service<OMGWindowsService>(
            service =>
            {
                service.ConstructUsing(() => new OMGWindowsService());
                service.WhenStarted(s => s.OnStart());
                service.WhenStopped(s => s.OnStop());
                service.WhenPaused(s => s.OnPause());
                service.WhenContinued(s => s.OnContinue());
            });

       x.RunAsLocalSystem();
       x.StartAutomatically();
       x.SetServiceName("OMGWindowsService");
       x.SetDisplayName("OMGWindowsService");
       x.SetDescription(@"OMG Windows Service");
   });

var exitCode = (int)Convert.ChangeType(hostRun, hostRun.GetTypeCode());
Environment.ExitCode = exitCode;
