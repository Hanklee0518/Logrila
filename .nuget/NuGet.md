Commands
------------
nuget setApiKey xxx-xxx-xxxx-xxxx
nuget push .\packages\Logrila.1.0.0.0.nupkg

nuget pack ..\Logrila\Logrila.Logging\Logrila.Logging.csproj -IncludeReferencedProjects -Build -Symbols -Prop Configuration=Release -OutputDirectory ".\packages"
nuget pack ..\Logrila\Logrila.Logging.NLogIntegration\Logrila.Logging.NLogIntegration.csproj -IncludeReferencedProjects -Build -Symbols -Prop Configuration=Release -OutputDirectory ".\packages"
nuget pack ..\Logrila\Logrila.Logging.SerilogIntegration\Logrila.Logging.SerilogIntegration.csproj -IncludeReferencedProjects -Build -Symbols -Prop Configuration=Release -OutputDirectory ".\packages"
nuget pack ..\Logrila\Logrila.Logging.Log4NetIntegration\Logrila.Logging.Log4NetIntegration.csproj -IncludeReferencedProjects -Build -Symbols -Prop Configuration=Release -OutputDirectory ".\packages"

nuget pack ..\Logrila\Logrila.Logging.Log4NetToNLog\Logrila.Logging.Log4NetToNLog.csproj -IncludeReferencedProjects -Build -Symbols -Prop Configuration=Release -OutputDirectory ".\packages"
nuget pack ..\Logrila\Logrila.Logging.Log4NetToLogrila\Logrila.Logging.Log4NetToLogrila.csproj -IncludeReferencedProjects -Build -Symbols -Prop Configuration=Release -OutputDirectory ".\packages"

nuget pack ..\Logrila\Logrila.Logging.SerilogToLogrila\Logrila.Logging.SerilogToLogrila.csproj -IncludeReferencedProjects -Build -Symbols -Prop Configuration=Release -OutputDirectory ".\packages"
nuget pack ..\Logrila\Logrila.Logging.SerilogToNLog\Logrila.Logging.SerilogToNLog.csproj -IncludeReferencedProjects -Build -Symbols -Prop Configuration=Release -OutputDirectory ".\packages"
