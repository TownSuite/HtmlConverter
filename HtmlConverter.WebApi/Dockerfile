#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy as base

ARG DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    # Install needed packagess
    && apt-get install -y --no-install-recommends wget xvfb unzip xz-utils wkhtmltopdf ca-certificates fontconfig libjpeg-turbo8 xfonts-75dpi xfonts-base xfonts-utils xfonts-encodings  \
    && mkdir -p /tmp && cd /tmp/ \
    # Install a newish wkhtmltox \
    && wget -q http://archive.ubuntu.com/ubuntu/pool/main/o/openssl/libssl1.1_1.1.1f-1ubuntu2_amd64.deb \
    && dpkg -i libssl1.1_1.1.1f-1ubuntu2_amd64.deb \
    && wget -q https://github.com/wkhtmltopdf/wkhtmltopdf/releases/download/0.12.5/wkhtmltox_0.12.5-1.focal_amd64.deb \
    && dpkg -i wkhtmltox_0.12.5-1.focal_amd64.deb \
    && apt-get -f install -y \
    && rm wkhtmltox_0.12.5-1.focal_amd64.deb \
    # Clean
    && rm -rf /tmp/* \
    && apt-get purge -y --auto-remove unzip xz-utils wget \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HtmlConverter.WebApi/HtmlConverter.WebApi.csproj", "HtmlConverter.WebApi/"]
RUN dotnet restore "HtmlConverter.WebApi/HtmlConverter.WebApi.csproj"
COPY . .
WORKDIR "/src/HtmlConverter.WebApi"
RUN dotnet build "HtmlConverter.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HtmlConverter.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8443
COPY --from=publish /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "HtmlConverter.WebApi.dll"]