#!/usr/bin/env bash
DEPLOY_ENVIRONMENT=pro
CURRENT_PATH=$(pwd)
OUTPUT_PATH="${CURRENT_PATH}/build_output/cms/"
REMOTE_PATH=/home/chovanhan/projects/giveandtake/Trunk/deployment/deployment/env/${DEPLOY_ENVIRONMENT}
REMOTE_PROJECT_PATH=/home/chovanhan/projects/giveandtake/Trunk/deployment/deployment/env/${DEPLOY_ENVIRONMENT}/output/cms
REMOTE_ID=chovanhan@13.76.45.56
REMOTE_PORT=22
PROJECT_PATH="../../../../Giveaway.CMS"
DEPLOY_KEY=./key/id_rsa

echo "THIS WILL DEPLOY ON: ${DEPLOY_ENVIRONMENT}"

echo "MOVING to project path"
cd ${PROJECT_PATH}

echo "CHECKOUT master"
git checkout develop
git pull origin develop

echo "BUILDING"
npm install && npm run build

mkdir ${OUTPUT_PATH}
echo "COPY TO OUTPUT $(pwd) to ${OUTPUT_PATH}"
cp -r dist/* ${OUTPUT_PATH}

cd "${CURRENT_PATH}"

read -r -p "Sending build to server and restart service now? [y/N] " response
case "$response" in
    [yY][eE][sS]|[yY])
		scp -C -i ${DEPLOY_KEY} -P ${REMOTE_PORT} -r build_output/cms/* ${REMOTE_ID}:${REMOTE_PROJECT_PATH}
		ssh -i ${DEPLOY_KEY} -p ${REMOTE_PORT} ${REMOTE_ID} "cd ${REMOTE_PATH};docker-compose up -d giveandtake-cms"
		;;
    *)
        echo "not sending data, service not restarted. Exiting ..."
    ;;
esac

echo "========================== Deploy successfully ========================="
