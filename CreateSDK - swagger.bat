call autorest initialize.md --v3 --static-initializer --add-credentials --input-file=apimatic-swagger.json --csharp --namespace=Filuet.Fusion.SDK --output-folder=./SDK/Code
dotnet new classlib -o SDK --name Filuet.Hrbl.Ordering.SDK --force --framework net7.0

del SDK\Class1.cs
rmdir SDK\Models /S /Q

cd SDK
dotnet add package Microsoft.Rest.ClientRuntime -v 2.3.24

cd..
dotnet sln Filuet.Hrbl.Ordering.SDK.sln add SDK\Filuet.Hrbl.Ordering.SDK.csproj