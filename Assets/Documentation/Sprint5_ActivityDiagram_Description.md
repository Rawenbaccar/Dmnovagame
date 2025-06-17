# Diagramme d'Activité du Sprint 5 : Description

## Aperçu
Ce diagramme d'activité illustre le flux utilisateur pour les fonctionnalités de connexion, d'inscription, de boutique et de classement implémentées dans le Sprint 5 du projet de jeu Boundless Night. Le diagramme visualise comment les utilisateurs interagissent avec ces systèmes et les points de décision qu'ils rencontrent.

## Description du Flux

### Flux d'Authentification
1. L'utilisateur commence à l'écran d'accueil.
2. L'utilisateur décide s'il possède un compte :
   - **Si Oui** : Le formulaire de connexion est affiché.
     - L'utilisateur saisit son nom d'utilisateur et son mot de passe.
     - Le système valide le formulaire.
     - Si la validation réussit, le système tente l'authentification.
     - Si l'authentification réussit, le système stocke le jeton d'authentification et affiche le menu principal.
     - Si l'authentification échoue, une erreur est affichée et l'utilisateur retourne au formulaire de connexion.
   - **Si Non** : Le formulaire d'inscription est affiché.
     - L'utilisateur saisit son nom d'utilisateur, son email et son mot de passe.
     - Le système valide le formulaire.
     - Si la validation réussit, le système tente de créer le compte.
     - Si la création du compte réussit, un message de succès est affiché et l'utilisateur est dirigé vers le formulaire de connexion.
     - Si la création du compte échoue, une erreur est affichée et l'utilisateur retourne au formulaire d'inscription.

### Navigation dans le Menu Principal
Depuis le menu principal, l'utilisateur peut choisir entre quatre options :
1. **Commencer le Jeu** : Démarre une session de jeu, qui se termine par un game over et la soumission du score.
2. **Boutique** : Permet à l'utilisateur de parcourir et d'acheter des objets.
3. **Classement** : Affiche le classement des joueurs et le rang de l'utilisateur actuel.
4. **Quitter** : Déconnecte l'utilisateur et termine la session.

### Flux de la Boutique
1. L'utilisateur entre dans l'interface de la boutique.
2. L'utilisateur parcourt les objets disponibles.
3. L'utilisateur sélectionne un objet à acheter.
4. Le système vérifie si l'utilisateur dispose de suffisamment de monnaie :
   - **Si Oui** : Le système traite l'achat et met à jour l'inventaire de l'utilisateur.
   - **Si Non** : Une erreur est affichée.
5. L'utilisateur peut retourner à la boutique ou au menu principal.

### Flux du Classement
1. L'utilisateur sélectionne l'option de classement.
2. Le système affiche le classement avec les rangs des joueurs.
3. Le système met en évidence le rang de l'utilisateur actuel.
4. L'utilisateur peut retourner au menu principal.

## Détails d'Implémentation
- Le système d'authentification utilise une approche basée sur des jetons, avec des jetons stockés après une connexion réussie.
- La validation des formulaires s'effectue à la fois côté client et côté serveur.
- Le système de boutique vérifie la disponibilité de la monnaie avant de traiter les achats.
- Le classement affiche les rangs basés sur les scores des joueurs provenant des sessions de jeu terminées.

## Conclusion
Ce diagramme d'activité représente le parcours complet de l'utilisateur à travers les systèmes d'authentification, de menu principal, de boutique et de classement. Il sert de guide visuel pour comprendre comment ces composants interagissent et comment les utilisateurs naviguent entre eux. 