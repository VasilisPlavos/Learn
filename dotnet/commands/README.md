dotnet build
dotnet build -c Release --self-contained true
dotnet build -c Debug

dotnet publish
dotnet publish  --self-contained true --no-build --no-restore --no-dependencies

dotnet list package --vulnerable –include-transitive