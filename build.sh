#!/bin/bash

# Frontend build

# Backend build

(cd backend &&
{
    BEND_OUT_DIR=bin/Publish

    mkdir -p "$BEND_OUT_DIR"
    rm -rf bin/*
    dotnet publish LecturerService.csproj -c Release -o "$BEND_OUT_DIR" &&
    {
        (cd "$BEND_OUT_DIR" &&
        {
            rm appsettings.Development.json LecturerService.pdb
            rm -rf runtimes/win*
        })
        docker build -t lecturer-service . &&
        docker run --publish 4200:80 lecturer-service
    }
})