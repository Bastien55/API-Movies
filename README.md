# API-Movies

Projet d'API Rest concernant les films.

Pour profiter au mieux de ce projet il faut :

# 1. Build l'application

Il faut se mettre dans le répertoire du projet et faire : docker-compose up
Cela va créer : 
- la base de donnée MariaDB sur le port 3306
- L'interface PhpMyAdmin sur le port 8080
- L'API sur le port 5206

# 2. Tester l'application

Une fois l'environnement build il faut ouvrir le navigateur et allé a cette adresse :
http://localhost:5206/swagger/index.html

Puis vous pouvez tester l'API

# 3. Base de donnée

Un fichier SQL est intégré au projet afin de construire plus facilement les tables
