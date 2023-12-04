-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : db
-- Généré le : sam. 28 oct. 2023 à 22:03
-- Version du serveur : 11.1.2-MariaDB-1:11.1.2+maria~ubu2204
-- Version de PHP : 8.2.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `API-Movie`
--

-- --------------------------------------------------------

--
-- Structure de la table `acteurs`
--

CREATE TABLE `acteurs` (
  `acteur_id` int(11) NOT NULL,
  `personne_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `acteurs`
--

INSERT INTO `acteurs` (`acteur_id`, `personne_id`) VALUES
(1, 1),
(2, 2),
(3, 3),
(9, 6),
(5, 8),
(6, 10),
(7, 12),
(8, 13);

-- --------------------------------------------------------

--
-- Structure de la table `cast`
--

CREATE TABLE `cast` (
  `cast_id` int(11) NOT NULL,
  `acteur_id` int(11) DEFAULT NULL,
  `realisateur_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `cast`
--

INSERT INTO `cast` (`cast_id`, `acteur_id`, `realisateur_id`) VALUES
(2, 1, NULL),
(3, 2, 1),
(4, 5, 2),
(5, 6, 3),
(6, 7, 2);

-- --------------------------------------------------------

--
-- Structure de la table `film`
--

CREATE TABLE `film` (
  `film_id` int(11) NOT NULL,
  `Nom` varchar(128) DEFAULT NULL,
  `Description` varchar(2048) NOT NULL,
  `Date_de_parution` date NOT NULL DEFAULT current_timestamp(),
  `cast_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `film`
--

INSERT INTO `film` (`film_id`, `Nom`, `Description`, `Date_de_parution`, `cast_id`) VALUES
(1, 'Star Wars III', 'An interstellar war with cool knights', '1985-07-23', 2),
(2, '3 jours max', 'trois jours max', '2023-10-25', 3),
(3, 'Inception', 'Un thriller de science-fiction sur les rêves.', '2010-07-16', 4),
(4, 'Fight Club', 'Un film culte sur la lutte contre la société de consommation.', '1999-10-15', 5),
(5, 'Interstellar', 'Un voyage spatial épique pour sauver l\'humanité.', '2014-11-05', 6);

-- --------------------------------------------------------

--
-- Structure de la table `personnes`
--

CREATE TABLE `personnes` (
  `personne_id` int(11) NOT NULL,
  `nom` varchar(30) DEFAULT NULL,
  `prenom` varchar(30) DEFAULT NULL,
  `date_naissance` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `personnes`
--

INSERT INTO `personnes` (`personne_id`, `nom`, `prenom`, `date_naissance`) VALUES
(1, 'MCGregor', 'Ewan', '1971-03-31'),
(2, 'Boudhali', 'Tarek', '1979-11-05'),
(3, 'BUREAU', 'Bastien', '2000-04-05'),
(4, 'Johny', 'John', '2000-02-14'),
(5, 'Johny', 'Johny', '2000-12-25'),
(6, 'bibi', 'jiji', '1999-01-01'),
(8, 'DiCaprio', 'Leonardo', '1974-11-11'),
(9, 'Nolan', 'Christopher', '1970-07-30'),
(10, 'Pitt', 'Brad', '1963-12-18'),
(11, 'Fincher', 'David', '1962-08-28'),
(12, 'McConaughey', 'Matthew', '1969-11-04'),
(13, 'Johansson', 'Scarlett', '1984-11-22'),
(14, 'Clooney', 'George', '1961-05-06');

-- --------------------------------------------------------

--
-- Structure de la table `realisateur`
--

CREATE TABLE `realisateur` (
  `realisateur_id` int(11) NOT NULL,
  `personne_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `realisateur`
--

INSERT INTO `realisateur` (`realisateur_id`, `personne_id`) VALUES
(1, 2),
(2, 9),
(3, 11),
(5, 14);

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `acteurs`
--
ALTER TABLE `acteurs`
  ADD PRIMARY KEY (`acteur_id`),
  ADD KEY `personne_id` (`personne_id`);

--
-- Index pour la table `cast`
--
ALTER TABLE `cast`
  ADD PRIMARY KEY (`cast_id`),
  ADD KEY `acteur_id` (`acteur_id`),
  ADD KEY `realisateur_id` (`realisateur_id`);

--
-- Index pour la table `film`
--
ALTER TABLE `film`
  ADD PRIMARY KEY (`film_id`),
  ADD KEY `FK_FilmCast` (`cast_id`);

--
-- Index pour la table `personnes`
--
ALTER TABLE `personnes`
  ADD PRIMARY KEY (`personne_id`);

--
-- Index pour la table `realisateur`
--
ALTER TABLE `realisateur`
  ADD PRIMARY KEY (`realisateur_id`),
  ADD KEY `personne_id` (`personne_id`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `acteurs`
--
ALTER TABLE `acteurs`
  MODIFY `acteur_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT pour la table `cast`
--
ALTER TABLE `cast`
  MODIFY `cast_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT pour la table `film`
--
ALTER TABLE `film`
  MODIFY `film_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT pour la table `personnes`
--
ALTER TABLE `personnes`
  MODIFY `personne_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT pour la table `realisateur`
--
ALTER TABLE `realisateur`
  MODIFY `realisateur_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `acteurs`
--
ALTER TABLE `acteurs`
  ADD CONSTRAINT `acteurs_ibfk_1` FOREIGN KEY (`personne_id`) REFERENCES `personnes` (`personne_id`);

--
-- Contraintes pour la table `cast`
--
ALTER TABLE `cast`
  ADD CONSTRAINT `cast_ibfk_1` FOREIGN KEY (`acteur_id`) REFERENCES `acteurs` (`acteur_id`),
  ADD CONSTRAINT `cast_ibfk_2` FOREIGN KEY (`realisateur_id`) REFERENCES `realisateur` (`realisateur_id`);

--
-- Contraintes pour la table `film`
--
ALTER TABLE `film`
  ADD CONSTRAINT `FK_FilmCast` FOREIGN KEY (`cast_id`) REFERENCES `cast` (`cast_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Contraintes pour la table `realisateur`
--
ALTER TABLE `realisateur`
  ADD CONSTRAINT `realisateur_ibfk_1` FOREIGN KEY (`personne_id`) REFERENCES `personnes` (`personne_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
