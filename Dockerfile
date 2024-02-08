FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

WORKDIR /app/out
COPY . .
RUN dotnet publish -c Release -o out

CMD ["dotnet", "out/web.dll"]