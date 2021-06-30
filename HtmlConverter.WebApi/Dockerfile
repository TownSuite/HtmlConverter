#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM ubuntu:20.04 as base

ARG DEBIAN_FRONTEND=noninteractive

WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt update && apt install wget -y \
    && wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && rm -rf packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y apt-transport-https \
    && apt-get update \
    && apt-get install -y aspnetcore-runtime-5.0

RUN apt-get update \
    # Install needed packagess
    && apt-get install -y --no-install-recommends xvfb unzip xz-utils wkhtmltopdf ca-certificates fontconfig libjpeg-turbo8 xfonts-75dpi xfonts-base xfonts-utils xfonts-encodings  \
    && mkdir -p /tmp && cd /tmp/ \
    # Install a newish wkhtmltox
    && wget -q https://github.com/wkhtmltopdf/wkhtmltopdf/releases/download/0.12.5/wkhtmltox_0.12.5-1.focal_amd64.deb \
    && dpkg -i wkhtmltox_0.12.5-1.focal_amd64.deb \
    && apt-get -f install -y \
    && rm wkhtmltox_0.12.5-1.focal_amd64.deb \
    # Clean
    && rm -rf /tmp/* \
    && apt-get purge -y --auto-remove unzip xz-utils wget \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HtmlConverter.WebApi/HtmlConverter.WebApi.csproj", "HtmlConverter.WebApi/"]
COPY ["HtmlConverter/HtmlConverter.csproj", "HtmlConverter/"]
RUN dotnet restore "HtmlConverter.WebApi/HtmlConverter.WebApi.csproj"
COPY . .
WORKDIR "/src/HtmlConverter.WebApi"
RUN dotnet build "HtmlConverter.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HtmlConverter.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HtmlConverter.WebApi.dll"]