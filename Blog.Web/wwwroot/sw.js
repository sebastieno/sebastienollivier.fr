let cacheName = "v1";

let urlsToCache = [
  "/images/headshot.png",
  "/scripts/prism.js",
  "/styles/prism.css",
  "/images/bg-pattern.png",
  "/images/search.svg",
  "/offline",
  "https://fonts.googleapis.com/css?family=Open+Sans|Roboto+Condensed:700"
];

this.addEventListener("install", function(event) {
  event.waitUntil(async () => {
    let cache = await caches.open(cacheName);
    return await cache.addAll(urlsToCache);
  });
});

this.addEventListener("fetch", event => {
  event.respondWith(
    (async () => {
      let cache = await caches.open(cacheName);
      let response = await cache.match(event.request);
      if (response) {
        return response;
      }

      try {
        let serverResponse = await fetch(event.request);
        cache.put(event.request, serverResponse.clone());
        return serverResponse;
      } catch (e) {
        return await cache.match("/offline");
      }
    })()
  );
});
