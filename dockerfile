# Utilisez l'image officielle de .NET Core en tant qu'image de base
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Définir le répertoire de travail dans le conteneur
WORKDIR /app

# Copiez le fichier csproj et restorez les dépendances avant de copier le code source
COPY *.sln ./
COPY **/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
RUN dotnet restore

# Copiez tout le reste et compilez l'application
COPY . ./
RUN dotnet publish -c Release -o out

# Utilisez une image plus légère pour l'exécution
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Définir le répertoire de travail dans le conteneur
WORKDIR /app

# Copiez les fichiers publiés depuis la phase de build précédente
COPY --from=build /app/out ./

# Commande de démarrage de l'application
ENTRYPOINT ["dotnet", "ProbabilityX.dll"]
