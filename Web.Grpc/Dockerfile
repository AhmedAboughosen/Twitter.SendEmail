FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Web.Grpc/Web.Grpc.csproj", "Web.Grpc/"]
RUN dotnet restore "Web.Grpc/Web.Grpc.csproj"
COPY . .
WORKDIR "/src/Web.Grpc"
RUN dotnet build "Web.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Grpc.dll"]
