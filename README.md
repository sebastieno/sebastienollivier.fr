# sebastienollivier.fr

This repo contains the source code of [sebastienollivier.fr](https://sebastienollivier.fr), which is my personal web site and blog.

This web site is based on ASPNET Core 2 and hosted in Microsoft Azure. The following Azure services are used :
- *App Service* : to host the web site
- *SQL Azure* : to store data 
- *Blob Storage* : to store posts' assets, like images or videos
- *Application Insights* : to monitor web site usage
- *Search Service* : to provide advanced search features
- *Azure Function* : to scan database and refresh the *Search Service* index 

Here a visual overview of the solution :

![archi_blog](/archi.png)

[<img src="https://sebastieno.visualstudio.com/_apis/public/build/definitions/36d54933-57d2-4f52-9726-ce3884259970/15/badge"/>](https://sebastieno.visualstudio.com/Blog/_build/index?definitionId=15)

## How to run the source code ?

*If anything is unclear, feels free to add an issue here [https://github.com/sebastieno/sebastienollivier.fr/issues](https://github.com/sebastieno/sebastienollivier.fr/issues) explaining what is unclear, I will update this readme to be more specific*

### Configure
Before running the application, you must provide some configurations in the *Blog.Web/appsettings.json* :
- *Data:BlogConnection:ConnectionString* : Connection string to the sql database. You can use the *Blog.Database* project in order to generate a new empty database

Some other configurations can be provided, to use the search features or to send telemetry :
- *Data:AzureSearch:Name* : Name of the Azure Search resource
- *Data:AzureSearch:Key* : Key of the Azure Search resource
- *Data:AzureSearch:IndexName* : Name of the Azure Search resource index
- *ApplicationInsights:InstrumentationKey* : Instrumentation key of the Application Insights resource

### Run
The project *Blog.Web* must be launched with the profile *iisexpress*, to use HTTPS (in order to be ISO between the dev and the prod environments).

The project *Blog.AzureFunction* can also be launched to force the refresh of the Azure Search resource index (just do not forget to change the local.settings.json value before)

### Compile assets

The project *Blog.Web* uses some static assets (less files, images, etc.), which are managed by a gulp worklow. To install all dependencies, just run the `npm install` command.

After assets modifications, you have to launch the compilation to update output, by launching the `gulp compile-less` command or the `gulp copy-images` command which respectively compile less files and copy images.

To improve the development experience, you can also run the `gulp` command, which will hot reload less files using browser-sync.

## Can I use the source code and can I contribute ?

Yes and Yes :)

The source code is free to use. You can fork it, upgrade it, improve it and do whatever you want.

You can also send me pull request to share your work, I will do my best to consider as fast as possible.