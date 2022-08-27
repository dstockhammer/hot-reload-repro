# hot-reload-repro

This repository demonstrates the issue reported in https://github.com/dotnet/aspnetcore/issues/43593:
using **any** custom tag helper breaks dotnet watch hot reload in Razor Pages with `System.TypeLoadException`.

## Steps To Reproduce

The issue is very easy to reproduce:

1. create a fresh Razor Pages project with `dotnet new razor` -> [commit#3754a57](https://github.com/dstockhammer/hot-reload-repro/commit/3754a570d99b6f1ed199c10e780794acd9656229)
2. create a custom tag helper and use in on the Index page (or any other page) -> [commit#912f252](https://github.com/dstockhammer/hot-reload-repro/commit/912f252994e3cd9c0fecdddd80d2a19c76f61aca)
3. run `dotnet watch`
4. making any change that triggers a hot reload -> boom 💥

## Console Output

```
> dotnet watch
dotnet watch 🔥 Hot reload enabled. For a list of supported edits, see https://aka.ms/dotnet/hot-reload.
  💡 Press "Ctrl + R" to restart.
dotnet watch 🔧 Building...
  Determining projects to restore...
  All projects are up-to-date for restore.
  HotReloadRepro -> D:\workspace\hot-reload-repro\bin\Debug\net6.0\HotReloadRepro.dll
dotnet watch 🚀 Started
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7179
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5016
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: D:\workspace\hot-reload-repro\
dotnet watch ⌚ File changed: .\Pages\Index.cshtml.
dotnet watch 🔥 Hot reload of changes succeeded.
fail: Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware[1]
      An unhandled exception has occurred while executing the request.
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HMK832AIMGVR", Request id "0HMK832AIMGVR:00000013": An unhandled exception was thrown by the application.
      System.AggregateException: An error occurred while writing to logger(s). (Could not load type 'HotReloadRepro.Pages.Pages_Index+<ExecuteAsync>d__8#1' from assembly 'HotReloadRepro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.) (Could not load type 'HotReloadRepro.Pages.Pages_Index+<ExecuteAsync>d__8#1' from assembly 'HotReloadRepro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.)
       ---> System.TypeLoadException: Could not load type 'HotReloadRepro.Pages.Pages_Index+<ExecuteAsync>d__8#1' from assembly 'HotReloadRepro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.
         at System.Reflection.CustomAttribute._CreateCaObject(RuntimeModule pModule, RuntimeType type, IRuntimeMethodInfo pCtor, Byte** ppBlob, Byte* pEndBlob, Int32* pcNamedArgs)
         at System.Reflection.CustomAttribute.AddCustomAttributes(ListBuilder`1& attributes, RuntimeModule decoratedModule, Int32 decoratedMetadataToken, RuntimeType attributeFilterType, Boolean mustBeInheritable, ListBuilder`1 derivedAttributes)
         at System.Reflection.CustomAttribute.GetCustomAttributes(RuntimeModule decoratedModule, Int32 decoratedMetadataToken, Int32 pcaCount, RuntimeType attributeFilterType)
         at System.Reflection.CustomAttribute.GetCustomAttributes(RuntimeMethodInfo method, RuntimeType caType, Boolean inherit)
         at System.Reflection.RuntimeMethodInfo.GetCustomAttributes(Type attributeType, Boolean inherit)
         at System.Attribute.GetCustomAttributes(MemberInfo element, Type attributeType, Boolean inherit)
         at System.Reflection.CustomAttributeExtensions.GetCustomAttributes[T](MemberInfo element, Boolean inherit)
         at System.Diagnostics.StackTrace.TryResolveStateMachineMethod(MethodBase& method, Type& declaringType)
         at System.Diagnostics.StackTrace.ToString(TraceFormat traceFormat, StringBuilder sb)
         at System.Diagnostics.StackTrace.ToString(TraceFormat traceFormat)
         at System.Exception.get_StackTrace()
         at System.Exception.ToString()
         at Microsoft.Extensions.Logging.Console.SimpleConsoleFormatter.CreateDefaultLogMessage[TState](TextWriter textWriter, LogEntry`1& logEntry, String message, IExternalScopeProvider scopeProvider)
         at Microsoft.Extensions.Logging.Console.SimpleConsoleFormatter.Write[TState](LogEntry`1& logEntry, IExternalScopeProvider scopeProvider, TextWriter textWriter)
         at Microsoft.Extensions.Logging.Console.ConsoleLogger.Log[TState](LogLevel logLevel, EventId eventId, TState state, Exception exception, Func`3 formatter)
         at Microsoft.Extensions.Logging.Logger.<Log>g__LoggerLog|12_0[TState](LogLevel logLevel, EventId eventId, ILogger logger, Exception exception, Func`3 formatter, List`1& exceptions, TState& state)
         --- End of inner exception stack trace ---
         at Microsoft.Extensions.Logging.Logger.ThrowLoggingError(List`1 exceptions)
         at Microsoft.Extensions.Logging.Logger.Log[TState](LogLevel logLevel, EventId eventId, TState state, Exception exception, Func`3 formatter)
         at Microsoft.Extensions.Logging.Logger`1.Microsoft.Extensions.Logging.ILogger.Log[TState](LogLevel logLevel, EventId eventId, TState state, Exception exception, Func`3 formatter)
         at Microsoft.Extensions.Logging.LoggerMessage.<>c__DisplayClass8_0.<Define>g__Log|0(ILogger logger, Exception exception)
         at Microsoft.AspNetCore.Diagnostics.DiagnosticsLoggerExtensions.UnhandledException(ILogger logger, Exception exception)
         at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.Invoke(HttpContext context)
         at Microsoft.AspNetCore.Watch.BrowserRefresh.BrowserRefreshMiddleware.InvokeAsync(HttpContext context)
         at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpProtocol.ProcessRequests[TContext](IHttpApplication`1 application)
       ---> (Inner Exception #1) System.TypeLoadException: Could not load type 'HotReloadRepro.Pages.Pages_Index+<ExecuteAsync>d__8#1' from assembly 'HotReloadRepro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.
         at System.Reflection.CustomAttribute._CreateCaObject(RuntimeModule pModule, RuntimeType type, IRuntimeMethodInfo pCtor, Byte** ppBlob, Byte* pEndBlob, Int32* pcNamedArgs)
         at System.Reflection.CustomAttribute.AddCustomAttributes(ListBuilder`1& attributes, RuntimeModule decoratedModule, Int32 decoratedMetadataToken, RuntimeType attributeFilterType, Boolean mustBeInheritable, ListBuilder`1 derivedAttributes)
         at System.Reflection.CustomAttribute.GetCustomAttributes(RuntimeModule decoratedModule, Int32 decoratedMetadataToken, Int32 pcaCount, RuntimeType attributeFilterType)
         at System.Reflection.CustomAttribute.GetCustomAttributes(RuntimeMethodInfo method, RuntimeType caType, Boolean inherit)
         at System.Reflection.RuntimeMethodInfo.GetCustomAttributes(Type attributeType, Boolean inherit)
         at System.Attribute.GetCustomAttributes(MemberInfo element, Type attributeType, Boolean inherit)
         at System.Reflection.CustomAttributeExtensions.GetCustomAttributes[T](MemberInfo element, Boolean inherit)
         at System.Diagnostics.StackTrace.TryResolveStateMachineMethod(MethodBase& method, Type& declaringType)
         at System.Diagnostics.StackTrace.ToString(TraceFormat traceFormat, StringBuilder sb)
         at System.Diagnostics.StackTrace.ToString(TraceFormat traceFormat)
         at System.Exception.get_StackTrace()
         at System.Exception.ToString()
         at System.Text.StringBuilder.Append(Object value)
         at Microsoft.Extensions.Logging.EventLog.EventLogLogger.Log[TState](LogLevel logLevel, EventId eventId, TState state, Exception exception, Func`3 formatter)
         at Microsoft.Extensions.Logging.Logger.<Log>g__LoggerLog|12_0[TState](LogLevel logLevel, EventId eventId, ILogger logger, Exception exception, Func`3 formatter, List`1& exceptions, TState& state)<---
```