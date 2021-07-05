#!/bin/bash
set -e # exit on first error
set -u # exit on using unset variable

DOCKER_REGISTRY=$1
echo "Using docker registry $DOCKER_REGISTRY"
echo "Building htmlconverterwebapi:latest"
docker build -f ./HtmlConverter.WebApi/Dockerfile -t htmlconverterwebapi --rm=true -m 2GB .
echo 'htmlconverterwebapi:latest built'

GITHASH="$(git rev-parse --short HEAD)"

echo 'Tagging latest'
docker tag htmlconverterwebapi $DOCKER_REGISTRY/htmlconverterwebapi:latest
echo "Tagging $GITHASH"
docker tag htmlconverterwebapi $DOCKER_REGISTRY/htmlconverterwebapi:$GITHASH

echo 'Pushing'
docker push $DOCKER_REGISTRY/htmlconverterwebapi:latest
docker push $DOCKER_REGISTRY/htmlconverterwebapi:$GITHASH
