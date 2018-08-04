FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 49279
EXPOSE 44394

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Mpc.AllStack.WebAppMvc/Mpc.AllStack.WebAppMvc.csproj Mpc.AllStack.WebAppMvc/
RUN dotnet restore Mpc.AllStack.WebAppMvc/Mpc.AllStack.WebAppMvc.csproj
COPY . .
WORKDIR /src/Mpc.AllStack.WebAppMvc
RUN dotnet build Mpc.AllStack.WebAppMvc.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Mpc.AllStack.WebAppMvc.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Mpc.AllStack.WebAppMvc.dll"]
