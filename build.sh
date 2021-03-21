#!/bin/bash

# Frontend build

# Backend build
(cd backend/LecturerService || exit 1
    BEND_OUT_DIR=bin/Publish

    mkdir -p "$BEND_OUT_DIR"
    rm -rf bin/*
    dotnet publish LecturerService.csproj -c Release -o "$BEND_OUT_DIR" || exit 1
    (cd "$BEND_OUT_DIR" &&
    {
        rm appsettings.Development.json LecturerService.pdb
        rm -rf runtimes/win*
    })
)

# Final build
docker-compose up --build