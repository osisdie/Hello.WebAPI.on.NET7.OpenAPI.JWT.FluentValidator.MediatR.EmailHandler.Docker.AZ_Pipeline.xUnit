FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /build
COPY . .

# --------------------------
# COPY Dependency Files
# --------------------------
COPY data/certs/ src/Endpoint/HelloMediatR/App_Data/

# --------------------------
# Build & Publish
# --------------------------
RUN dotnet restore "src/Endpoint/HelloMediatR/Hello.MediatR.Endpoint.csproj" 
RUN dotnet publish "src/Endpoint/HelloMediatR/Hello.MediatR.Endpoint.csproj" -c Release -o /app --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Hello.MediatR.Endpoint.dll"]

