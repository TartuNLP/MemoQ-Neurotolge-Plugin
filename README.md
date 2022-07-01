# TartuNLP MemoQ Plugin

Plugin for using [TartuNLP machine translation](https://www.neurotolge.ee) engines in MemoQ. The plugin itself and
information about compatibility with different MemoQ versions can be found in
the [releases section](https://github.com/TartuNLP/MemoQ-Neurotolge-Plugin/releases) of this repository.

The plugin is designed to be compatible with the
current [LTS](https://helpcenter.memoq.com/hc/en-us/articles/360010268660-Frequent-release-policy)
version of MemoQ, however, as MemoQ is being constantly updated, feel free to let us know about any compatibility
problems by opening a new issue under this repository.

## Installation

- Find the plugin file in the [releases section](https://github.com/TartuNLP/MemoQ-Neurotolge-Plugin/releases).
- To install the plugin, copy the `TartuNLP.dll` or similarly named file into the `Addins` folder in the installation
  directory of your MemoQ client.
- Create an XML file named ClientDevConfig.xml in the %programdata%/MemoQ folder with the following content:
  ```xml
  <?xml version="1.0" encoding="utf-8"?>
  <ClientDevConfig>
   <LoadUnsignedPlugins>true</LoadUnsignedPlugins>
  </ClientDevConfig>
  ```
- Run the MemoQ Client.