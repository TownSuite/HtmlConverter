#!/usr/bin/env pwsh
$ErrorActionPreference = "Stop"
$CURRENTPATH=$pwd.Path

# Must install powershell:  https://learn.microsoft.com/en-us/powershell/scripting/install/install-ubuntu?view=powershell-7.2


rm -rf build
mkdir -p build


$VERSION = cat Directory.Build.props | grep "<Version>"  | sed 's/[^0-9.]*//g' 
rm -rf build
mkdir -p build
$GITHASH = "$(git rev-parse --short HEAD)"
$GITHASH_FULL = "$(git rev-parse HEAD)"
Add-Content "$CURRENTPATH/build/parameterproperties.txt" "VERSION=$version"
Add-Content "$CURRENTPATH/build/parameterproperties.txt" "GITHASH=$GITHASH"
Add-Content "$CURRENTPATH/build/parameterproperties.txt" "GITHASH_FULL=$GITHASH_FULL"

$builderName = "townsuite-builder"
docker buildx inspect $builderName 2>$null
if ($LASTEXITCODE -eq 0) {
    docker buildx use $builderName
} else {
    docker buildx create --name $builderName --driver docker-container --use | Out-Null
}
docker buildx inspect $builderName --bootstrap
mkdir -p $CURRENTPATH/build/oci

Write-Host "Building townsuite/htmlconverterwebapi:latest" -ForegroundColor Green
docker buildx build -f $CURRENTPATH/HtmlConverter.WebApi/Dockerfile --progress plain --provenance=true --sbom=true --output "type=oci,name=townsuite/htmlconverterwebapi,oci-mediatypes=true,compression=zstd,force-compression=true,tar=false,dest=$CURRENTPATH/build/oci/htmlconverterwebapi" .

tar -cvf $CURRENTPATH/build/oci/htmlconverterwebapi.oci.tar -C $CURRENTPATH/build/oci/htmlconverterwebapi --transform='s|^\./||' .
rm -rf $CURRENTPATH/build/oci/htmlconverterwebapi
Write-Host "Finished htmlconverterwebapi.oci.tar" -ForegroundColor Green
