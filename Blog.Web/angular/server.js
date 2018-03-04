const appServer = require('./dist-server/main.bundle');
const readFileSync = require('fs').readFileSync;
const file = readFileSync('./wwwroot/index.html').toString();
const createServerRenderer = require('aspnet-prerendering').createServerRenderer;
const createTransferScript = require('@nguniversal/aspnetcore-engine').createTransferScript;
const { provideModuleMap } = require('@nguniversal/module-map-ngfactory-loader');
const renderModuleFactory = require('@angular/platform-server').renderModuleFactory;
const ngAspnetCoreEngine = require('@nguniversal/aspnetcore-engine').ngAspnetCoreEngine;
require('zone.js');


module.exports = createServerRenderer(params => {

  const setupOptions = {
    appSelector: '<app-root></app-root>',
    ngModule: appServer.AppModuleNgFactory,
    request: params,
    providers: [
      provideModuleMap(appServer.LAZY_MODULE_MAP)
    ]
  };

  return ngAspnetCoreEngine(setupOptions).then(response => {

    response.globals.transferData = createTransferScript({
      fromDotnet: params.data.thisCameFromDotNET
    });

    return ({
      html: response.html,
      globals: response.globals
    });

  });
});
