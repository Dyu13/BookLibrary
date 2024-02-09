# Book Library

API having a Clean Architecture with inversion of control fro infrastructure dependencies

CQRS on the way, folders and mediator pattern prepared for it

# Tests
- Unit Tests for the Application core functionalities - using the InMemory database
- Integration Tests for the API - run them using docker compose for the .net app with the database and then run tests against localhost
- Unit Tests for Angular app - using Karma
- E2E Tests for the Angular app - to be implemented with Cypress

# Local Env
- API port: 15100
- DB port: 1433
- Docker-Compose: same port for API but mapping 1434 for DB 
- Angular app: 4200
- Logging only to console for now

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).
