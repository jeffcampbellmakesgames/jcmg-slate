[![Continous Deployment](https://github.com/jeffcampbellmakesgames/jcmg-slate/actions/workflows/publish_release.yml/badge.svg)](https://github.com/jeffcampbellmakesgames/jcmg-slate/actions/workflows/publish_release.yml)
[![openupm](https://img.shields.io/npm/v/com.jeffcampbellmakesgames.slate?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.jeffcampbellmakesgames.slate/)
![GitHub issues](https://img.shields.io/github/issues/jeffcampbellmakesgames/jcmg-slate)
[![Twitter Follow](https://img.shields.io/badge/twitter-%40stampyturtle-blue.svg?style=flat&label=Follow)](https://twitter.com/stampyturtle)

# JCMG Slate
## About
JCMG Slate is a lightweight UI framework library for Unity. It contains a simple paradigm for defining distinct UIScreens and offers an easy way to look them up and hook up animations for them.

## Minimum Requirements
* **Unity Version:** 2021.3.X or Higher
* **Scripting Runtime**: .Net 4.X

## Installing JCMG Slate
Using this library in your project can be done in three ways:

### Install via OpenUPM
The package is available on the [openupm registry](https://openupm.com/). It's recommended to install it via [openupm-cli](https://github.com/openupm/openupm-cli).

```
openupm add com.jeffcampbellmakesgames.jcmg-slate
```

### Install via GIT URL
Using the native Unity Package Manager introduced in 2017.2, you can add this library as a package by modifying your `manifest.json` file found at `/ProjectName/Packages/manifest.json` to include it as a dependency. See the example below on how to reference it.

```
{
	"dependencies": {
		...
		"com.jeffcampbellmakesgames.jcmg-slate" : "https://github.com/jeffcampbellmakesgames/jcmg-slate.git#release/stable",
		...
	}
}
```


You will need to have Git installed and available in your system's PATH.

## Support
If this is useful to you and/or you’d like to see future development and more tools in the future, please consider supporting it either by contributing to the Github projects (submitting bug reports or features and/or creating pull requests) or by buying me coffee using any of the links below. Every little bit helps!

[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/I3I2W7GX)

## Contributing

For information on how to contribute and code style guidelines, please visit [here](CONTRIBUTING.md).
