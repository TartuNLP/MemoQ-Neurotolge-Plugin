# TartuNLP MemoQ Plugin
Plugin for using [TartuNLP machine translation](https://www.neurotolge.ee) engines in MemoQ.
This repository is designed to work with the LTS versions of MemoQ starting from 9.0. Although the plugin may work with non-LTS versions, these have not been tested.

## Installation
- Find the plugin file in the [releases section](https://github.com/TartuNLP/MemoQ-Neurotolge-Plugin/releases).
- To install the plugin, copy the correct TartuNLP.dll file for your MemoQ version from the bin folder of this repository into the Addins folder in the installation directory of your MemoQ client. 
- Create an XML file named ClientDevConfig.xml in the %programdata%/MemoQ folder with the following content:
    ```<?xml version="1.0" encoding="utf-8"?>
     <ClientDevConfig>
       <LoadUnsignedPlugins>true</LoadUnsignedPlugins>
     </ClientDevConfig>
    ```
- Run the MemoQ Client.