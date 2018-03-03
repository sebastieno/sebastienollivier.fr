const appServer = require('./dist-server/main.bundle');
const readFileSync = require('fs').readFileSync;
const file = readFileSync('./wwwroot/index.html').toString();
const createServerRenderer = appServer.createServerRenderer;
const createTransferScript = appServer.createTransferScript;
const provideModuleMap = appServer.provideModuleMap;
const ngAspnetCoreEngine = appServer.ngAspnetCoreEngine;
const renderModuleFactory = appServer.renderModuleFactory;


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
