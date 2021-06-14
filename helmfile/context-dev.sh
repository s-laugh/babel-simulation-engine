#!/bin/bash
az aks get-credentials --resource-group ESdCDPSBDMK8SDev --name ESdCDPSBDMK8SDev-K8S

# Default to latest
if [ -z "$DOCKER_TAG" ]
then
    export DOCKER_TAG=latest
fi
# App specific secrets
export OUR_VARIABLE_HERE="$(az keyvault secret show --vault-name DTSSecrets --name dts-github-our-variable-here --query value -otsv)"
# Generic secrets
export BASE_DOMAIN_DEV=dev.dts-stn.com
export GITHUB_USER=$(az keyvault secret show --vault-name DTSSecrets --name dts-github-user --query value -otsv)
export GITHUB_TOKEN=$(az keyvault secret show --vault-name DTSSecrets --name dts-github-token --query value -otsv)
