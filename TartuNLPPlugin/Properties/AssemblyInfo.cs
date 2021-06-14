using System.Reflection;
using System.Runtime.InteropServices;
using MemoQ.Addins.Common.Framework;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("TartuNLP MemoQ Plugin")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("University of Tartu")]
[assembly: AssemblyProduct("TartuNLP MemoQ Plugin")]
[assembly: AssemblyCopyright("Copyright © University of Tartu 2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e80fb60f-85ed-4f18-bb5e-34d8865a4ce4")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
// TODO update version upon new release
[assembly: AssemblyVersion("2.0.1.*")]

[assembly: Module(ModuleName = "TartuNLP", ClassName = "TartuNLP.TartuNLPPluginDirector")]
