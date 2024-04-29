#!/bin/bash
IMAGE_NAME="fterdev/4minator"

CONTAINER_NAME="4-minator"


echo "Stopping existing container..."
sudo  docker stop $CONTAINER_NAME

echo "Removing existing container..."
sudo docker rm $CONTAINER_NAME

echo "Pulling the latest image..."
sudo docker pull $IMAGE_NAME


echo "Starting new container..."
sudo docker run -d -p 1525:8080 --name $CONTAINER_NAME $IMAGE_NAME
