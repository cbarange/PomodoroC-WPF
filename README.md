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

## DO

* [X] - Minuteur de type pomodoro (25/5/25/5/25/5/25/15)
* [X] - Ajouter des pomodoros dans la liste
* [X] - Filtrer la liste en fonction des tags
* [X] - Filtrer la liste en fonction de la priorité
* [X] - Changer de timer en double cliquant dans la liste
* [X] - Ajouter des tags aux pomodoros existants
* [X] - Filter les pomodoros en fonction de la date
* [X] - Enregistrer les pomodoros dans un fichier dataPomodoro.data
* [X] - Charger les pomodoros depuis le fichier dataPomodoro.data
* [X] - Supprimer un pomodoro de la liste
* [X] - Voir à quelle étape du pomodoro on est rendu

## TO DO

* [ ] - Visualiser les pomodoros fini

## Requirement

`dotnet add package System.Windows.Interactivity.WPF --version 2.0.20525`