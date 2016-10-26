#!/usr/bin/env bash

command_exists () {
    type "$1" &> /dev/null ;
}

if ! command_exists dotnet || [ -z "$SKIP_DNX_INSTALL" ]; then
    curl -sSL https://raw.githubusercontent.com/dotnet/cli/rel/1.0.0-preview2/scripts/obtain/dotnet-install.sh | bash -s
fi

dotnet restore && dotnet build **/project.json