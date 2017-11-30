#!/bin/bash

source=/usr/src/TimePeriodLibrary
packages=$source/packages
outdir=$source/bin
owneruser=username
ownergrp=users

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
cd $source
chown -R $owneruser:$ownergrp $source/*
chmod -R o-rwx $source/*
echo Done Building
