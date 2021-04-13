FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app


COPY *.csproj ./
RUN dotnet restore


COPY . ./
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app

# ENV ASPNETCORE_URLS=http://+:3101

ENV ASPNETCORE_ELEPHANTSQLURL="postgres://jgekejbi:0X1d1s95628fK2zEtRAP0ADDIhZnLZFU@tuffi.db.elephantsql.com:5432/jgekejbi"

COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "contacts.dll"]