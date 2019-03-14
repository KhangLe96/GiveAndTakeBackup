#!/usr/bin/env bash
DEPLOY_ENVIRONMENT=dev
DEPLOY_API_BUILD=Release
CURRENT_PATH=$(pwd)
TARGET_PATH="../../../../Giveaway.API/Giveaway/API/Giveaway.API"
OUTPUT_PATH="${CURRENT_PATH}/build_output/api/app"
REMOTE_PATH=/home/chovanhan/projects/giveandtake/Trunk/deployment/deployment/env/${DEPLOY_ENVIRONMENT}/output/api
DEPLOYMENT_REMOTE_PATH=/home/chovanhan/projects/giveandtake/Trunk/deployment/deployment/env/${DEPLOY_ENVIRONMENT}
REMOTE_ID=chovanhan@13.76.45.56
echo "THIS WILL DEPLOY ON: ${DEPLOY_ENVIRONMENT}"
if [ -d "${TARGET_PATH}" ]; then
	cd "${TARGET_PATH}"
else
	echo "target path not found"
	exit -1
fi

# echo "CHECKOUT master branch"
# git checkout develop
# git pull origin develop

export API_BUILD_NUMBER="$(git rev-parse --abbrev-ref HEAD).$(git rev-parse --short HEAD).$(date +%d%m%Y%H%M%S)"

echo "CLEANING ..."
rm -rf "${OUTPUT_PATH}"

mkdir -p "${OUTPUT_PATH}"

echo "BUILDING API PACKAGE ..."
dotnet publish Giveaway.API.csproj -o "${OUTPUT_PATH}"

echo "REPLACING CONFIG FOR: ${DEPLOY_ENVIRONMENT}"
cp -f "${CURRENT_PATH}/output/api/appsettings.json" "${OUTPUT_PATH}"

cd "${CURRENT_PATH}"

read -r -p "Sending build to server and restart service now? [y/N] " response
case "$response" in
    [yY][eE][sS]|[yY])
        echo "SENDING PACKAGE ..."
        scp -C -i ./key/id_rsa -P 22 -r build_output/api/app ${REMOTE_ID}:${REMOTE_PATH}

		echo "RESTART DOCKER ..."
		export REMOTE_DOCKER_COMMAND="cd ${DEPLOYMENT_REMOTE_PATH};docker-compose up -d --force-recreate giveandtake-api-dev;"
		ssh -i ./key/id_rsa -p 22 ${REMOTE_ID} ${REMOTE_DOCKER_COMMAND}
        ;;
    *)
        echo "not sending data, service not restarted. Exiting ..."
        ;;
esac

