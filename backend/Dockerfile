FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
EXPOSE 80
COPY bin/Publish/* ./
RUN mkdir -p runtimes; mv unix runtimes/unix
ENTRYPOINT ["dotnet", "LecturerService.dll"]