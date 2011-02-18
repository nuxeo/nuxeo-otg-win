# Nuxeo on the Go - Windows implementation

Simple Windows Shell extension implementation to control Nuxeo on the Go


## Building

First, you need to make sure you have:

- Visual Studio 2010 
- .NET 4

Open, and build the solution.


## Testing

Open the build folder with Visual Studio Command Promt (2010), and type:

	regasm Nuxeo-otg-win.dll /codebase

Open a window explorer, and you can see the Nuxeo on the Go contextual menu when right clicking a file


To remove the dll from explorer, type:

	regasm Nuxeo-otg-win.dll /unregister

You may need to restart/kill explorer.exe to release the dll. 


## About Nuxeo

Nuxeo provides a modular, extensible Java-based [open source software platform for enterprise content management] [1] and packaged applications for [document management] [2], [digital asset management] [3] and [case management] [4]. Designed by developers for developers, the Nuxeo platform offers a modern architecture, a powerful plug-in model and extensive packaging capabilities for building content applications. 

[1]: http://www.nuxeo.com/en/products/ep
[2]: http://www.nuxeo.com/en/products/document-management
[3]: http://www.nuxeo.com/en/products/dam
[4]: http://www.nuxeo.com/en/products/case-management

More information on: <http://www.nuxeo.com/>
