#!/bin/bash

rm TOP_Utils.sln

dotnet new sln
dotnet sln add ./TOP_Utils/TOP_Utils.csproj