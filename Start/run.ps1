Start-Process cmd -Argument "/c ""dotnet watch --project ../Switch/Switch.csproj run"""
Start-Process cmd -Argument "/c ""dotnet watch --project ../Lamp/Lamp.csproj run"""
Start-Process cmd -Argument "/c ""dotnet watch --project ../Orchestrator/Orchestrator.csproj run"""
Start-Process cmd -Argument "/c ""yarn run --cwd=./Front start"""