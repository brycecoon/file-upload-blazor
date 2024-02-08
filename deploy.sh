#!/bin/bash

cd /home/alex/deployment/file-upload-blazor
echo "cleaning up old projects "

rm -rf bin
rm -rf obj
git pull 

echo "building and publishing dll"
dotnet publish

echo "doing systemd stuff"
sudo cp alex-upload.service /etc/systemd/system/alex-upload.service

sudo chmod 700 /etc/systemd/system/alex-upload.service
sudo systemctl daemon-reload
sudo systemctl restart alex-upload

echo "done doing systemd stuff"
systemctl status alex-upload


