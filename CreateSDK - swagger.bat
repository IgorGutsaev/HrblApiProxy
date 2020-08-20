call autorest initialize.md --v3 --static-initializer --add-credentials --input-file=apimatic-swagger.json --csharp --namespace=Filuet.Fusion.SDK --output-folder=./Filuet.Fusion.SDK/Code
dotnet new classlib --name Filuet.Fusion.SDK --force --framework netstandard2.1

del Filuet.Fusion.SDK\Class1.cs
rmdir Filuet.Fusion.SDK\Models /S /Q

cd Filuet.Fusion.SDK
dotnet add package Microsoft.Rest.ClientRuntime -v 2.3.18

cd..
dotnet sln Filuet.Fusion.SDK.sln add Filuet.Fusion.SDK\Filuet.Fusion.SDK.csproj