#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 1337
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5001;http://+:1337

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ProtobufTest/ProtobufTest.csproj", "ProtobufTest/"]
RUN dotnet restore "ProtobufTest/ProtobufTest.csproj"
COPY . .
WORKDIR "/src/ProtobufTest"
RUN dotnet build "ProtobufTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProtobufTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app


COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProtobufTest.dll"]