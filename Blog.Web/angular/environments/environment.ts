export const environment = {
  production: false,
  apiUrl: 'http://localhost:5500/api',
  msalConfig: {
    clientID: 'c23725a0-95b8-45a4-b021-4989fa2cbb3d',
    redirectUri: 'http://localhost:5500/back',
    graphScopes: ['user.read']
  }
};
