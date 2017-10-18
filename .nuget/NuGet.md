Commands
------------
nuget setApiKey xxx-xxx-xxxx-xxxx
nuget push .\packages\Logrila.1.0.0.0.nupkg

nuget pack ..\Logrila\Logrila.Logging\Logrila.Logging.csproj -IncludeReferencedProjects -Build -Symbols -Prop Configuration=Release -OutputDirectory ".\packages"
nuget pack ..\Logrila\Logrila.Logging.NLogIntegration\Logrila.Logging.NLogIntegration.csproj -IncludeReferencedProjects -Build -Symbols -Prop Configuration=Release -OutputDirectory ".\packages"
nuget pack ..\Logrila\Logrila.Logging.SerilogIntegration\Logrila.Logging.SerilogIntegration.csproj -IncludeReferencedProjects -Build -Symbols -Prop Configuration=Release -OutputDirectory ".\packages"
nuget pack ..\Logrila\Logrila.Logging.Log4NetIntegration\Logrila.Logging.Log4NetIntegration.csproj -IncludeReferencedProjects -Build -Symbols -Prop Configuration=Release -OutputDirectory ".\packages"
