###### AUTHOR : Clément BARANGER	DATE : 10/01/2020

# Projet POMODORO 

## Technologie

* C#
* WPF
* .NET CORE
* MVVM

## Consigne

* Pouvoir programmer/préparer des sessions de pomodoro 
* Pour chaque pomodoro associer des mots clé/tag, auto-completion
* On lance la session de pomodoro (25/5/25/5/25/5/25/15) 
* Mettre en pause 
* Voir combien de pomodoro fait 
* Filtre par tag/date

## Changer la durée des timers

Dans le fichier `Model/PomodoroClock.cs` ligne 13 changer les 3 valeurs de timer.

## DO

* [X] - Minuteur de type pomodoro (25/5/25/5/25/5/25/15)
* [X] - Changer de timer en double cliquant dans la liste
* [X] - Ajouter des pomodoros dans la liste
* [X] - Supprimer un pomodoro de la liste
* [X] - Filtrer la liste en fonction des tags, on peut faire une recherche multi tag en ajoutant des espaces entre ex : tag2 tag3 finish
* [X] - Filter les pomodoros en fonction de la date, on peut faire une recherche par tag et date en même temps ex tag1 tag3 29/02/2020
* [X] - Filtrer la liste en fonction de la priorité
* [X] - Ajouter des tags aux pomodoros existants, ajoute le tag au pomodo sélectionné
* [X] - Voir à quelle étape du pomodoro on est rendu
* [X] - Visualiser les pomodoros fini, ajout automatique du tag finish
* [X] - Ajouter un son lors de la fin d'un timer
* [X] - Enregistrer les pomodoros dans un fichier dataPomodoro.data
* [X] - Charger les pomodoros depuis le fichier dataPomodoro.data

## TO DO



## Requirement

`dotnet add package System.Windows.Interactivity.WPF --version 2.0.20525`