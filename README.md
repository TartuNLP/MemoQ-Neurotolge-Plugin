# TartuNLP MemoQ Plugin
Plugin for using [TartuNLP machine translation](https://www.neurotolge.ee) engines in MemoQ. This repository is based on the MemoQ SDK for versions 9.3-9.4, but a plugin for versions 9.0-9.2 can also be found in the bin folder.

## Installation
- To install the plugin, copy the correct TartuNLP.dll file for your MemoQ version from the bin folder of this repository into the Addins folder in the installation directory of your MemoQ client. 
- Create an XML file named ClientDevConfig.xml in the %programdata%/MemoQ folder with the following content:
    ```<?xml version="1.0" encoding="utf-8"?>
     <ClientDevConfig>
       <LoadUnsignedPlugins>true</LoadUnsignedPlugins>
     </ClientDevConfig>
    ```
- Run the MemoQ Client.