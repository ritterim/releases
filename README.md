# releases [![Build Status](https://travis-ci.org/ritterim/releases.svg?branch=master)](https://travis-ci.org/ritterim/releases)

Pulls and displays releases from multiple GitHub repositories.

## Getting Started

  - `git clone releases`
  - `cd releases`
  - `dnu restore`
  - `dnx --project src/Site web aspnet_env=Development`

## Development Workflow

### appsettings.json

The app. contains an `appsettings.json` file. This should be treated as a template **only** where custom-settings go in a separate environment-specific (i.e.
`appsettings.Development.json`).

```text
{
    "appSettings" :{
        "accessToken": "%your-personal-access-token%",
        "company": "%your-company-name%",
        "showCompanyInHeader: "(true/false)",
        "showLogoInHeader" : "(true/false)",
        "email" : "%contact-email%",
        "logo" : "",
        "repositories" : {
            "ritterim/releases": "Releases"
        }
    }
}
```

`accessToken`: generating a token is done via GitHub's `Personal settings` -> `Personal access token`. When creating the token, the only scope required is `repo` and only if 
you want to access ***your*** private repos. **Remember, tokens are one-time visible (i.e. you cannot retrieve a token once it has been generated)**.

`company`: this is displayed on all page headers

`logo`: this is displayed on all page headers

`repositories`: collection of key-value pairs where the former follows a `:owner/:repo` format and the latter is a human-readable version displayed in the app. UI.

## Publishing Workflow

  - `dnu publish --configuration Release --out %output-path% --runtime active`

  This will use the currently-selected runtime to pre-configure runtime-scripts.

  ### IIS

  For additional setup requirements, see [Publishing to IIS](http://docs.asp.net/en/latest/publishing/iis.html).
