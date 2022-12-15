read -p 'Enter Migration name: ' migname
dotnet ef migrations add $migname -p ../src/database/database.csproj -s ../src/database-migration/database-migration.csproj
echo 'Test update database'
dotnet ef database update -p ../src/database/database.csproj -s ../src/database-migration/database-migration.csproj