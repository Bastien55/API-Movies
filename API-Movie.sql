-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : db
-- Généré le : mar. 24 oct. 2023 à 18:11
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

-- --------------------------------------------------------

--
-- Structure de la table `cast`
--

CREATE TABLE `cast` (
  `cast_id` int(11) NOT NULL,
  `acteur_id` int(11) DEFAULT NULL,
  `realisateur_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
(1, 'Star Wars III', 'An interstellar war with cool knights', '1985-07-23', NULL),
(2, '3 jours max', 'trois jours max', '2023-10-25', NULL);

-- --------------------------------------------------------

--
-- Structure de la table `film_personne`
--

CREATE TABLE `film_personne` (
  `ID` int(11) NOT NULL,
  `personne_id` int(11) NOT NULL,
  `film_id` int(11) NOT NULL,
  `role` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `film_personne`
--

INSERT INTO `film_personne` (`ID`, `personne_id`, `film_id`, `role`) VALUES
(1, 1, 1, 'Acteur'),
(2, 2, 2, 'Acteur'),
(3, 2, 2, 'Réa');

-- --------------------------------------------------------

--
-- Structure de la table `personnes`
--

CREATE TABLE `personnes` (
  `personne_id` int(11) NOT NULL,
  `nom` varchar(10) DEFAULT NULL,
  `prenom` varchar(10) DEFAULT NULL,
  `date_naissance` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `personnes`
--

INSERT INTO `personnes` (`personne_id`, `nom`, `prenom`, `date_naissance`) VALUES
(1, 'MCGregor', 'Ewan', '1971-03-31'),
(2, 'Boudhali', 'Tarek', '1979-11-05');

-- --------------------------------------------------------

--
-- Structure de la table `realisateur`
--

CREATE TABLE `realisateur` (
  `realisateur_id` int(11) NOT NULL,
  `personne_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
-- Index pour la table `film_personne`
--
ALTER TABLE `film_personne`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `personne_id` (`personne_id`),
  ADD KEY `film_id` (`film_id`);

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
  MODIFY `acteur_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `cast`
--
ALTER TABLE `cast`
  MODIFY `cast_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `film`
--
ALTER TABLE `film`
  MODIFY `film_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT pour la table `film_personne`
--
ALTER TABLE `film_personne`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pour la table `personnes`
--
ALTER TABLE `personnes`
  MODIFY `personne_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT pour la table `realisateur`
--
ALTER TABLE `realisateur`
  MODIFY `realisateur_id` int(11) NOT NULL AUTO_INCREMENT;

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
-- Contraintes pour la table `film_personne`
--
ALTER TABLE `film_personne`
  ADD CONSTRAINT `film_personne_ibfk_1` FOREIGN KEY (`personne_id`) REFERENCES `personnes` (`personne_id`),
  ADD CONSTRAINT `film_personne_ibfk_2` FOREIGN KEY (`film_id`) REFERENCES `film` (`film_id`);

--
-- Contraintes pour la table `realisateur`
--
ALTER TABLE `realisateur`
  ADD CONSTRAINT `realisateur_ibfk_1` FOREIGN KEY (`personne_id`) REFERENCES `personnes` (`personne_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
