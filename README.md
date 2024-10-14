# Shopping Fraud Detection
L’objectif de ce projet est de rechercher les heuristiques les plus efficaces possibles dans le cadre d’un programme destiné à trouver le chemin le plus court entre deux points. 
Le programme en question, ainsi que l’affichage console qui l’accompagne et l’illustre, sont déjà fournis en amont.
Ce qui nous intéresse plus particulièrement est l'arbre d’exploration généré par la recherche du bon chemin, puisque nous essayons de le réduire au maximum. Pour se faire, nous devons appliquer des heuristiques pertinentes à l’algorithme. 
De plus, il est à préciser que l’on utilise un algorithme A*, ce qui signifie que l’on tri les ouverts en ajoutant une heuristique h(N) au coût du chemin calculé comme pour une approche par Dijkstra. 
Ainsi l’algorithme, dans sa forme initiale avant implémentation d’une quelconque heuristique, se contente d’appliquer Dijkstra, c’est-à-dire renvoyer un h(n) égal à 0. 
Nous allons donc passer en revue les heuristiques choisies pour chacun des trois environnements de recherche proposés, ainsi que les résultats associés et les éventuelles divergences qu’il est possible d’obtenir en faisant varier les bons paramètres. 

Projet réalisé par les étudiants de l'ENSC [HUBACHER Lorinda](https://github.com/lololezigoto) , [PERRET Quentin](https://github.com/QuentinPerret) et [TAVARD Hugo](https://github.com/Croquignoles)
