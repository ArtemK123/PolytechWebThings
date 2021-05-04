:: Run in the folder with Infrastructure.csproj
dotnet ef migrations add MigrationName --verbose -s "../Web/Web.csproj" -o "./Database/Migrations"