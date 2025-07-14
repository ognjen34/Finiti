    

Write-Host "🔄 Starting Docker containers..."
docker-compose -f "Finiti\docker-compose.yaml" up -d

Start-Sleep -Seconds 10

Write-Host "🚀 Running .NET backend..."
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd Finiti/Finiti.WEB; dotnet run"

Write-Host "🌐 Running Angular frontend..."
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd client; npm install; ng serve --open"

Write-Host "✅ All services started."
