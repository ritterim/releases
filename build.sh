#!/usr/bin/env bash

command_exists () {
    type "$1" &> /dev/null ;
}

if ! command_exists dnvm || [ -z "$SKIP_DNX_INSTALL" ]; then
    curl -sSL https://raw.githubusercontent.com/aspnet/Home/dev/dnvminstall.sh | DNX_BRANCH=dev sh && source ~/.dnx/dnvm/dnvm.sh
fi

if ! command_exists dnx || [ -z "$SKIP_DNX_INSTALL" ]; then
    dnvm install 1.0.0-rc1-update1 -runtime coreclr -alias default
    dnvm install 1.0.0-rc1-update1 -runtime mono -alias default
else
    dnvm use default -runtime mono
fi

dnu restore && dnu build src/* && dnx --project tests/RimDev.Releases.Tests test