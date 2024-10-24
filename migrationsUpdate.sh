#!/bin/bash

# Remove existing Migrations
echo "Removing all migrations..."
rm -rf "Migrations"

# Build the project to make sure everything is clean
echo "Cleaning the project..."
dotnet clean

# Rebuild migrations
echo "Rebuilding migrations..."
dotnet ef migrations add InitialMigration

# Update the database
echo "Updating the database..."
dotnet ef database update

echo "Migrations recreated and database updated."
