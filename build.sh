#!/bin/bash

source=/usr/src/TimePeriodLibrary
packages=$source/packages
outdir=$source/bin
owneruser=username

routine(){
  dotnet restore --packages $packages
  dotnet build -o $outdir -f $1
  dotnet publish -o $outdir -f $1 -c Release -r linux-x64
  cd ..
}

cd $source/TimePeriod
routine netstandard2.0
cd $source/TimePeriodDemo
routine netcoreapp2.0
cd $source/..
chown -R $owneruser:users *
chmod -R o-rwx *
echo Done Building
