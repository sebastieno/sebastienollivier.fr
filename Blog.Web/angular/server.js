const renderModuleFactory = require('@angular/platform-server').renderModuleFactory;
const appServer = require('./dist-server/main.bundle');
const readFileSync = require('fs').readFileSync;
const file = readFileSync('./wwwroot/index.html').toString();
const createServerRenderer = require('aspnet-prerendering').createServerRenderer;
const createTransferScript = require('@nguniversal/aspnetcore-engine').createTransferScript;
const { provideModuleMap } = require('@nguniversal/module-map-ngfactory-loader');
const ngAspnetCoreEngine = require('@nguniversal/aspnetcore-engine').ngAspnetCoreEngine;
require('zone.js');


module.exports = createServerRenderer(params => {

  /*
   * How can we access data we passed from .NET ?
   * you'd access it directly from `params.data` under the name you passed it
   * ie: params.data.WHATEVER_YOU_PASSED
   * -------
   * We'll show in the next section WHERE you pass this Data in on the .NET side
   */

  // Platform-server provider configuration
  const setupOptions = {
    appSelector: '<app-root></app-root>',
    ngModule: appServer.AppModuleNgFactory,
    request: params,
    providers: [
      provideModuleMap(appServer.LAZY_MODULE_MAP)
    ]
  };

  // ***** Pass in those Providers & your Server NgModule, and that's it!
  return ngAspnetCoreEngine(setupOptions).then(response => {

    // Want to transfer data from Server -> Client?

    // Add transferData to the response.globals Object, and call createTransferScript({}) passing in the Object key/values of data
    // createTransferScript() will JSON Stringify it and return it as a <script> window.TRANSFER_CACHE={}</script>
    // That your browser can pluck and grab the data from
    response.globals.transferData = createTransferScript({
      someData: 'Transfer this to the client on the window.TRANSFER_CACHE {} object',
      fromDotnet: params.data.thisCameFromDotNET // example of data coming from dotnet, in HomeController
    });

    return ({
      html: response.html,
      globals: response.globals
    });

  });
});

// module.exports = function (callback, path) {

//   const setupOptions = {
//     appSelector: '<app-root></app-root>',
//     ngModule: appServer.AppModuleNgFactory,
//     request: path,
//     providers: [
//       // Optional - Any other Server providers you want to pass
//       // (remember you'll have to provide them for the Browser as well)
//     ]
//   };

//   ngAspnetCoreEngine(setupOptions).then(response => {
//     callback(null, ({
//       html: response.html,
//       globals: response.globals
//     }));
//   }).catch(error => callback(error));;

//   // renderModuleFactory(appServer.AppModuleNgFactory, {
//   //   document: file,
//   //   url: path
//   // }).then(body => {
//   //   callback(null, body);
//   // }).catch(error => callback(error));
// }


// export default createServerRenderer((params) => {

//   // Platform-server provider configuration


//   return ngAspnetCoreEngine(setupOptions).then(response => {

//     // Apply your transferData to response.globals
//     response.globals.transferData = createTransferScript({
//       someData: 'Transfer this to the client on the window.TRANSFER_CACHE {} object',
//       fromDotnet: params.data.thisCameFromDotNET // example of data coming from dotnet, in HomeController
//     });

//     return ({
//       html: response.html, // our <app-root> serialized
//       globals: response.globals // all of our styles/scripts/meta-tags/link-tags for aspnet to serve up
//     });
//   });
// });
