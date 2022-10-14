FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build-env
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:6.0-focal
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "5Words.dll"]