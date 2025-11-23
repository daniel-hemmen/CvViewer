# Copilot instructions — CvViewer (frontend/cv-viewer)

This file tells AI coding agents how this repository is organized and how to be productive quickly.

Key facts

- Project: Angular 20 single-page application with optional server-side rendering (SSR).
- Package manager: npm. Key scripts in `package.json`: `start` (`ng serve`), `build` (`ng build`), `watch` (`ng build --watch --configuration development`), `test` (`ng test`), and `serve:ssr:cv-viewer` (runs `node dist/cv-viewer/server/server.mjs`).
- SSR: uses `@angular/ssr` and a small Express server at `src/server.ts`. Server entry and bootstrap are in `src/main.server.ts`.
- Static dataset: `public/data/cv-details.json` — the app reads static JSON from `public` as an example data source.

Architecture overview (what to know)

- Standalone components + signals: components are implemented without NgModules (see `src/app/app.ts`). They import primitives directly (e.g. `RouterOutlet`, `CommonModule`) and use `signal()` from Angular. Keep changes compatible with standalone usage.
- Routing: client routes in `src/app/app.routes.ts` and SSR rendering routes in `src/app/app.routes.server.ts`. When you add a route, update both places as appropriate.
- Application config: `src/app/app.config.ts` (browser config) and `src/app/app.config.server.ts` (server merge via `mergeApplicationConfig`). Do not change only one without considering the other.
- SSR request handling: `src/server.ts` uses `AngularNodeAppEngine` and `createNodeRequestHandler`. Static assets are served from the browser build folder (see the `express.static(...)` call).

Important files to reference

- `package.json` — scripts and dependencies.
- `angular.json` — build/serve configuration and where assets are copied from (`public`).
- `src/main.ts` — client bootstrap.
- `src/main.server.ts` — server bootstrap used for SSR.
- `src/server.ts` — Express SSR server and static asset serving.
- `src/app/app.ts`, `src/app/app.html`, `src/app/app.scss` — root component (shows standalone component pattern and Material usage).
- `src/app/app.routes.ts`, `src/app/app.routes.server.ts` — routing patterns and prerender config.
- `public/data/cv-details.json` — sample data the app consumes.

Build / dev workflows (explicit commands)

- Local dev (fast): `npm run start` or `ng serve` — runs the development dev-server (port 4200 by default).
- Watch build: `npm run watch` (alias: `ng build --watch --configuration development`).
- Production build (browser + server): `npm run build` (runs `ng build` per config in `angular.json`).
- Run SSR server after build: `npm run serve:ssr:cv-viewer` — starts the node server using the built `dist` output. Typical flow for testing SSR: `npm run build` then `npm run serve:ssr:cv-viewer`.
- Tests: `npm run test` (Karma + Jasmine configured).

Project conventions and patterns (concrete examples)

- Styling: SCSS used everywhere; new components should use `.scss` and follow the existing style inclusion (`styleUrl: './component.scss'`).
- Standalone components: follow the pattern in `src/app/app.ts` where `@Component({ imports: [...], templateUrl: ..., styleUrl: ... })` is used. Avoid adding NgModules; prefer standalone components and `provideRouter` for routing.
- HTTP + fetch: `provideHttpClient(withFetch())` is used in `app.config.ts` — the app expects the Fetch-based `HttpClient` in browser context. Safe to call `fetch('/data/cv-details.json')` or use Angular `HttpClient` with the provided fetch adapter.
- Server config merge: `app.config.server.ts` uses `mergeApplicationConfig(appConfig, serverConfig)` — when adding providers for server rendering, place them in `app.config.server.ts` or the `serverConfig` provider list.

Integration points & external dependencies

- Express static serving: `src/server.ts` serves the client build (`dist/<project>/browser`) — if you change asset paths, update `angular.json` assets and `server.ts` accordingly.
- Angular Material & CDK are used (`@angular/material`, `@angular/cdk`) — prefer using existing components/imports in `app.ts` style rather than adding global modules.

Editing guidance for AI agents

- Keep changes small and consistent with standalone components — add import lists in `@Component` `imports` array like `App` does.
- If changing routing, update both `app.routes.ts` and `app.routes.server.ts` (and then `app.config.server.ts` if extra server providers are needed).
- Static data changes: modify files in `public/` and ensure `angular.json` assets continue to include `public`.
- When altering build or server behavior, update `package.json` scripts if and only if you update `angular.json` or `src/server.ts` to match.
- Formatting: a Prettier config is present in `package.json`. Match `printWidth` and `singleQuote` settings.

Examples (copy-paste safe)

- Add a new standalone component (CLI):
  - `ng generate component my-component --standalone --style scss` (project uses standalone components and SCSS)
- Add a client route:
  - edit `src/app/app.routes.ts` and add `{ path: 'new', component: MyComponent }`
  - ensure any SSR prerendering needs are considered: update `src/app/app.routes.server.ts` if you require server prerender rules.

When uncertain

- Search for the pattern in `src/app/` before changing global config files. Look for `provide*` calls in `app.config.ts` and the server merge in `app.config.server.ts`.
- Run `ng serve` locally to verify client changes and `npm run build` + `npm run serve:ssr:cv-viewer` to verify SSR changes.

If anything here is incomplete or you want me to include more examples (e.g., typical component unit tests or a common service pattern), tell me which area to expand and I will iterate.

Never add comments to the code, unless the code is non-obvious or complex, or if explicitly requested by the user.
