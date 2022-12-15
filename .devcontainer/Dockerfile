FROM mcr.microsoft.com/devcontainers/dotnet:0-7.0

# [Optional] Uncomment this section to install additional OS packages.
# RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
#     && apt-get -y install --no-install-recommends <your-package-list-here>

# [Optional] Uncomment this line to install global node packages.
# RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g <your-package-here>" 2>&1

RUN dotnet tool install --global dotnet-ef
#RUN export PATH="$PATH:/home/vscode/.dotnet/tools"
RUN echo 'export PATH="$PATH:/home/vscode/.dotnet/tools"' >> ~/.zshrc