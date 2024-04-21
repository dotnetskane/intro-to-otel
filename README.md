# Introduction to OpenTelemetry in .NET
_By Peter Nylander 2024_

This is the repo for the talk "Introduction to OpenTelemetry in .NET" by Peter Nylander at the .NET Skåne user group event [24/4 2024](https://www.meetup.com/net-skane/events/299895141).

The talk is a simple introduction how to get started with OpenTelemetry in .NET.

The repo is structured like so that each commit represents one step of the presentation

1. [Creating the web api that we are going to use for rest of the talk](https://github.com/penyland/OTEL1/commit/3fbb3a75fbf8091c70dfd52a13250bda80bd6852)
2. [Logging](https://github.com/penyland/OTEL1/tree/3fbb3a75fbf8091c70dfd52a13250bda80bd6852)
3. [Standard metrics](https://github.com/penyland/OTEL1/commit/fa12ab0f9d071eea32d99bfc5e36bb27a3a3dbed)
4. [Custom metrics](https://github.com/penyland/OTEL1/commit/deb07f93754b50fdfdc27020106709cc101b0512)
5. [Tracing](https://github.com/penyland/OTEL1/commit/97cbf78e8ac6d8f515dcdffcd4fde61366e6b464)
6. [Tracing -> Downstream api](https://github.com/penyland/OTEL1/commit/90216f43b87c9e1264ff760f0e2120bbb52a14d8)
7. [Custom tracing](https://github.com/penyland/OTEL1/commit/9a42730098f0d30917997344b2e750133f654dd6)
8. [Custom tracing - add tag to span](https://github.com/penyland/OTEL1/commit/e87dedd06bcc2b0ff438c38015b32ef4d9beb806)
9. [Custom tracing - add events to span](https://github.com/penyland/OTEL1/commit/fe184ddb90d24b50b374428ec9a9557474f245cb)

During the talk I used the [.NET Aspire dashboard](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/dashboard) to easily demonstrate how to view logs, metrics and traces.

Start the dashboard by executing the following command:

`docker run --rm -it -p 18888:18888 -p 4317:18889 -d --name aspire-dashboard -e DOTNET_DASHBOARD_UNSECURED_ALLOW_ANONYMOUS='true' mcr.microsoft.com/dotnet/nightly/aspire-dashboard:8.0.0-preview.5`

You can also run [Grafana LGTM](https://grafana.com/blog/2024/03/13/an-opentelemetry-backend-in-a-docker-image-introducing-grafana/otel-lgtm/) as the OpenTelemetry backend. I also showed this briefly.

Start Grafana by executing:

`docker run -p 3000:3000 -p 4317:4317 -p 4318:4318 --rm -ti grafana/otel-lgtm`
