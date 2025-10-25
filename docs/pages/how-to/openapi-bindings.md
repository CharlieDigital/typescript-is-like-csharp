# How to Interact with the Backend API

If you're building a **Serious Backend™**, chances are that you're going to need an OpenAPI specification as an output for a number of reasons.

First is typically to generate the client TypeScript bindings.  Yes, even if you're using TypeScript on the backend, this makes sense because the frontend model is typically only a subset of the backend model and the OpenAPI interface is the boundary between front- and back-ends.

Second is that many platforms like [AWS API Gateway](https://docs.aws.amazon.com/apigateway/latest/developerguide/http-api-open-api.html) and [Google Cloud API Gateway](https://cloud.google.com/api-gateway/docs/openapi-overview) consume OpenAPI specs.  If you are building an enterprise API or any sort of API where you'll want to implement an API gateway, you'll want OpenAPI.  So even if your frontend is a TypeScript React, Vue, or Svelte application, it makes sense to use the OpenAPI bindings to generate the clients if an API gateway is in the system design.

Third is that OpenAPI can be the foundation of your externally facing APIs and documentation generation using tools like [Redocly](https://redocly.com/) and [Mintlify](https://mintlify.com/docs/api-playground/openapi/setup).  If you're just building a simple, self-contained web app, then it isn't relevant.  But if you're building a serious product (probably why you're using Nest.js and curious about .NET), then OpenAPI tooling is key.

::: info .NET OpenAPI support
Depending on which style of API is used (Minimal, FastEndpoints, or Controllers), the approach to annotating files changes with C# and .NET.  In this guide, we'll walk through the setup of controller APIs as these are the closest match for Nest.js.

Additionally, with .NET 9, Microsoft now offers first party tooling to support exporting OpenAPI specifications though the support for controllers has some gaps around the XML code comments at the moment.
:::

## Authoring

🚧 WIP

## Generating Spec Files

🚧 WIP

## Generating Client Bindings

🚧 WIP
